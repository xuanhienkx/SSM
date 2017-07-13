using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;
using Microsoft.Office.Interop.Excel;
using SSM.Common;
using SSM.Models;
using SSM.Services;
using SSM.ViewModels;
using SSM.ViewModels.Shared;
using Helpers = SSM.Common.Helpers;

namespace SSM.Controllers
{
    public class IssueVoucherController : Controller
    {
        private IStockCardService stockCardService;
        private IWarehouseSevices warehouseSevices;
        private ISalesServices salesServices;
        private IEnumerable<Product> products;
        private IEnumerable<Supplier> suppliers;
        private IEnumerable<Warehouse> warehouses;
        private Grid<IssueVoucherModel> grid;
        private SummaryInventoryViewModel viewReprotModel;
        private const string STOCKCARD_SEARCH_MODEL = "STOCKCARD_SEARCH_MODEL";
        private const string STOCKCARDINOUT_SEARCH_MODEL = "STOCKCARDINOUT_SEARCH_MODEL";
        private const string MONTHYEAR_SEARCH_MODEL = "MONTHYEAR_SEARCH_MODEL";
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            if (!Helpers.AllowTrading)
            {
                throw new HttpException("You are not authorized to access this page");
            }
            stockCardService = new StockCardService();
            warehouseSevices = new WareHouseServices();
            salesServices = new SalesServices();


        }


        private void GetDefaultData()
        {
            warehouses = warehouses == null || !warehouses.Any() ? warehouseSevices.GetAll() : warehouses;
            var stocks = warehouses.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();
            stocks.Insert(0, new SelectListItem()
            {
                Text = "--All--",
                Value = "0"
            });
            ViewData["Warehouses"] = stocks;
        }

        private List<int> GetListYear()
        {
            List<int> years = Enumerable.Range(DateTime.Now.Year, 10).ToList();
            return years;
        }
        public ActionResult Index()
        {
            GetDefaultData();
            grid = (Grid<IssueVoucherModel>)Session[STOCKCARD_SEARCH_MODEL];
            if (grid == null)
            {
                grid = new Grid<IssueVoucherModel>(
                    new Pager
                    {
                        CurrentPage = 1,
                        PageSize = 10,
                    })
                {
                    SearchCriteria = new IssueVoucherModel() { HashOut = false, StockIn = true, StockOut = false }
                };
            }
            var filter = grid.SearchCriteria ?? new IssueVoucherModel();
            var totalRow = 0;
            var list = stockCardService.GetListInventory(filter, grid.Pager.CurrentPage, grid.Pager.PageSize, out  totalRow);
            grid.Data = list.ToList();
            grid.Pager.Init(totalRow);
            return View(grid);
        }
        [HttpPost]
        public ActionResult Index(Grid<IssueVoucherModel> modeGrid)
        {
            GetDefaultData();
            grid = modeGrid;
            Session[STOCKCARD_SEARCH_MODEL] = grid;
            grid.ProcessAction();
            var filter = grid.SearchCriteria ?? new IssueVoucherModel() { HashOut = false, StockIn = true, StockOut = false };
            var totalRow = 0;
            var list = stockCardService.GetListInventory(filter, grid.Pager.CurrentPage, grid.Pager.PageSize, out totalRow);
            grid.Data = list.ToList();
            grid.Pager.Init(totalRow);
            return PartialView("_ListInventory",grid);
        }

        public ActionResult ReportStockInOut()
        {
            GetDefaultData();
            grid = (Grid<IssueVoucherModel>)Session[STOCKCARDINOUT_SEARCH_MODEL];
            if (grid == null)
            {
                grid = new Grid<IssueVoucherModel>(
                    new Pager
                    {
                        CurrentPage = 1,
                        PageSize = 10,
                    })
                {
                    SearchCriteria = new IssueVoucherModel() { HashOut = true, StockIn = true, StockOut = true,TopRowDetail = 20}
                };
            }
            var filter = grid.SearchCriteria ?? new IssueVoucherModel();
            var totalRow = 0;
            var list = stockCardService.GetList(filter, grid.Pager.CurrentPage, grid.Pager.PageSize, out totalRow);

            grid.Data = list.ToList();
            grid.Pager.Init(totalRow);
            return View(grid);
        }
        [HttpPost]
        public ActionResult ReportStockInOut(Grid<IssueVoucherModel> modeGrid)
        {
            GetDefaultData();
            grid = modeGrid;
            Session[STOCKCARDINOUT_SEARCH_MODEL] = grid;
            grid.ProcessAction();
            var filter = grid.SearchCriteria ?? new IssueVoucherModel() { HashOut = true, StockIn = true, StockOut = true };
            var totalRow = 0;
            var list = stockCardService.GetList(filter, grid.Pager.CurrentPage, grid.Pager.PageSize, out totalRow);
            grid.Data = list.ToList();
            grid.Pager.Init(totalRow);
            return PartialView("_ListWarehouseSaving", grid);
        }

        public ActionResult MonthYearReport()
        {
            GetDefaultData();

            viewReprotModel = (SummaryInventoryViewModel)Session[MONTHYEAR_SEARCH_MODEL];
            if (viewReprotModel == null)
            {
                viewReprotModel = new SummaryInventoryViewModel();
                viewReprotModel.IssueVoucher = viewReprotModel.IssueVoucher ?? new IssueVoucherModel() { HashOut = true, StockIn = true, StockOut = true, Year = DateTime.Now.Year };
            }
            var list = stockCardService.GetReportList(viewReprotModel.IssueVoucher);
            viewReprotModel.Summary = list;
            return View(viewReprotModel);
        }
        [HttpPost]
        public ActionResult MonthYearReport(SummaryInventoryViewModel filterMode)
        {
            GetDefaultData();

            viewReprotModel = filterMode;

            if (viewReprotModel == null)
            {
                viewReprotModel = new SummaryInventoryViewModel();
                viewReprotModel.IssueVoucher = viewReprotModel.IssueVoucher ?? new IssueVoucherModel() { HashOut = true, StockIn = true, StockOut = true, Year = DateTime.Now.Year };
            }
            var list = stockCardService.GetReportList(viewReprotModel.IssueVoucher);
            Session[MONTHYEAR_SEARCH_MODEL] = viewReprotModel;
            viewReprotModel.Summary = list;
            return View(viewReprotModel);
        }

        public ActionResult CalculateCost()
        {
            var model = new CalculateCostViewModel
            {
                Year = DateTime.Now.Year,
                FromMonth = DateTime.Now.Month - 6,
                ToMonth = DateTime.Now.Month,
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateCost(CalculateCostViewModel model)
        {

            string message = string.Empty;
            message = stockCardService.CaculateCost(model);
            ViewBag.Message = message;
            return View(model);
            // return Json(message, JsonRequestBehavior.AllowGet);

        }

        public ActionResult MoveStockToNextYear()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MoveStockToNextYear(int year)
        {
            string message = string.Empty;
            message = stockCardService.CaculateCostToNextYear(year);
            ViewBag.Message = message;
            return View(year);
        }
        public ActionResult ShowProductSalseHold(long productId)
        {
            var list = salesServices.GetAll(x => x.DT81s.Any(p => p.ProductID == productId) && x.Status == (byte)VoucherStatus.Pending);
            var models = list.Select(x => new ShowQtyPending
            {
                VoucherNo = x.VoucherNo,
                Quantity = x.DT81s.Where(p => p.ProductID == productId).Sum(s => s.Quantity ?? 0),
                SaffName = x.User.FullName
            }).ToList();
            return PartialView("_UserHoldOrderPending", models);
        }
        public ActionResult CalculateCostOnSaleOrder(long id)
        {
            var message = string.Empty;
            try
            {
                var order = salesServices.GetModelById(id);
                var dates = order.VoucherDate.Value;
                var model = new CalculateCostViewModel()
                {
                    FromMonth = dates.Month,
                    ToMonth =DateTime.Now.Month,
                    Year = dates.Year

                };
                message = stockCardService.CaculateCost(model); 
                var orderN = salesServices.GetById(id);
                var cost = orderN.T_Amount0.Value;
                if (cost == 0)
                {
                    cost = orderN.DT81s.ToList().Sum(x => x.Amount0.Value);
                }
                return Json(new
                {
                    Error = false,
                    Message = message + string.Format("<br/> <span class='successfuly'> <label>Cost Of Sales:</label>{0}</span>", cost),
                    Cost = cost,

                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                message = string.Format("<span class='error'> {0}</span>", ex.Message);
                return Json(new
                {
                    Error = true,
                    Message = message

                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}