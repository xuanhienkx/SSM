using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using SSM.Common;
using SSM.Services;
using SSM.ViewModels.Reports;

namespace SSM.Controllers
{
    public class ProfitReportController : BaseController
    {
        private IRevenueServices revenueServices;
        public static String FILTER_SEARCH_MODEL = "FILTER_SEARCH_MODEL";
        public ProfitReportController()
        {
            revenueServices = new RevenueServices();
        }
        public ActionResult Index()
        {
            FilterProfit filter = new FilterProfit()
            {
                BeginDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now,
                Top = 10,
                TypeFilterProfit = TypeFilterProfit.Sales
            };
            ViewBag.FilterProfit = filter;
            ViewBag.ProfitType = TypeFilterProfit.Sales;
            var list = new List<ProfitReportModel>();
            return View(list);
        }
        [HttpPost]
        public ActionResult Index(FilterProfit filter)
        {
            filter.EndDate = filter.EndDate.AddHours(23).AddMinutes(59).AddSeconds(59);
            var list = revenueServices.ProfitReportModels(filter);
            ViewBag.FilterProfit = filter;
            ViewBag.ProfitType = filter.TypeFilterProfit;
            Session[FILTER_SEARCH_MODEL] = filter;
            return PartialView("_DataFiter", list);
        }
        [HttpGet]
        public ActionResult ShippmetsDetailType(string refObjId)
        {
            FilterProfit filter = (FilterProfit)Session[FILTER_SEARCH_MODEL];
            var shipmentList = revenueServices.GetAllShipments(refObjId, filter);
            ViewBag.ProfitType = filter.TypeFilterProfit;
            var value = new
            {
                Views = this.RenderPartialView("_ShipmentList", shipmentList),
                CloseOther = true,
                Title = string.Format(@"Shipment detail of {0}", filter.TypeFilterProfit.ToString()),
                ColumnClass = "col-md-12 col-md-offset-0"
            };
            return JsonResult(value, true); 
        }
    }
}