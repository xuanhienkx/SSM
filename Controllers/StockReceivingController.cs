using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SSM.Common;
using SSm.Common;

using SSM.Models;
using SSM.Reports;
using SSM.Services;
using SSM.ViewModels;
using SSM.ViewModels.Reports;
using SSM.ViewModels.Shared;
using Helpers = SSM.Common.Helpers;
using RequestContext = System.Web.Routing.RequestContext;

namespace SSM.Controllers
{
    public class StockReceivingController : BaseController
    {
        private Grid<OrderModel> grid;
        private const string STOCKRECIVING_GIRD_MODEL = "STOCKRECIVING_GIRD_MODEL";
        private const string STOCKRECIVING_SEARCH_MODEL = "STOCKRECIVING_SEARCH_MODEL";
        private ICustomerServices customerServices;
        private IProductServices productServices;
        private IWarehouseSevices warehouseSevices;
        private ISupplierServices supplierServices;
        private IStockReceivingService stockReceivingService;
        private SelectList currencyList, warehouseList;
        private IEnumerable<Warehouse> warehouses;
        private TradingStockSearch filterModel;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            productServices = new ProductServices();
            warehouseSevices = new WareHouseServices();
            stockReceivingService = new StockReceivingService();
            supplierServices = new SupplierSerivecs();
            customerServices = new CustomerServices();
            if (!Helpers.AllowTrading)
            {
                throw new HttpException("You are not authorized to access this page");
            }

        }
        private IEnumerable<Curency> curencies;
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
            curencies = curencies ?? stockReceivingService.GetAllCurencies();
            if (currencyList == null) currencyList = new SelectList(curencies, "Id", "Code");
            var userTrading = stockReceivingService.GetAllUserTrading();
            var users = userTrading.Select(x => new SelectListItem
            {
                Text = x.FullName,
                Value = x.Id.ToString()
            }).ToList();

            users.Insert(0, new SelectListItem()
            {
                Text = "--All--",
                Value = "0"
            });

            ViewData["UserTrading"] = users;
            ViewData["Currency"] = currencyList;
            ViewBag.vStatus = VoucherStatus.Pending;
            ViewBag.Islook = "";
            ViewData["Warehouses"] = stocks;
            ViewBag.SearchingMode = filterModel ?? new TradingStockSearch();
        }


        [HttpGet]
        public ActionResult Index()
        {
            grid = (Grid<OrderModel>)Session[STOCKRECIVING_GIRD_MODEL];
            filterModel = (TradingStockSearch)Session[STOCKRECIVING_SEARCH_MODEL];
            filterModel = filterModel ?? new TradingStockSearch();
            if (grid == null)
            {
                grid = new Grid<OrderModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 20,
                                    Sord = "desc",
                                    Sidx = "VoucherDate"
                                }
                            );
                grid.SearchCriteria = new OrderModel();
            }

            long SearchQuickView = Session["SearchQuickView"] == null ? 0 : (long)Session["SearchQuickView"];
            Session["SearchQuickView"] = SearchQuickView;
            UpdateGridData();
            return View(grid);
        }
        [HttpPost]
        public ActionResult Index(TradingStockSearch filter, Grid<OrderModel> gridModel)
        {
            filterModel = filter;
            grid = gridModel;
            Session[STOCKRECIVING_GIRD_MODEL] = grid;
            Session[STOCKRECIVING_SEARCH_MODEL] = filterModel;
            grid.ProcessAction();
            UpdateGridData();
            return PartialView("_ListData", grid);
        }

        private void UpdateGridData()
        {
            var orderField = new SSM.Services.SortField(grid.Pager.Sidx, grid.Pager.Sord == "asc");
            filterModel.SortField = orderField;
            GetDefaultData();
            var totalRow = 0;
            grid.Data = stockReceivingService.GetAllModel(filterModel, grid.Pager.CurrentPage, grid.Pager.PageSize, out totalRow, CurrenUser);
            grid.Pager.Init(totalRow);
        }

        public ActionResult Create()
        {
            GetDefaultData();
            ViewBag.Islook = "new";
            var order = new OrderModel();
            order.Supplier = new Supplier();
            order.Curency = curencies.FirstOrDefault(x => x.Code == "USD");
            order.VoucherCode = 1;
            order.Status = VoucherStatus.Pending;
            order.VoucherNo = stockReceivingService.GetVoucherNo();
            order.ExchangeRate = 22500;
            return View(order);
        }
        [HttpPost]
        public ActionResult Create(OrderModel model)
        {
            GetDefaultData();
            ViewBag.Islook = "new";
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.CreateBy = CurrenUser.Id;
                model.VoucherId = stockReceivingService.GetVoucherId();
                model.DateCreate = DateTime.Now;
                long voucherId = model.VoucherId;
                stockReceivingService.Insert(model, out voucherId);
                return RedirectToAction("Edit", new { id = voucherId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }

        public ActionResult Edit(long id)
        {
            GetDefaultData();
            var order = stockReceivingService.GetByModel(id);
            ViewBag.Islook = "look";
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(OrderModel model)
        {
            GetDefaultData();
            ViewBag.Islook = "";
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.ModifyBy = CurrenUser.Id;
                model.DateModify = DateTime.Now;
                stockReceivingService.Update(model);
                return RedirectToAction("Edit", new { id = model.VoucherId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

        }

        public ActionResult BlankEditorRow(int tabindex)
        {
            GetDefaultData();
            ViewData["tabindex"] = tabindex;
            return PartialView("_StockDetailView", new OrderDetailModel());
        }
        public JsonResult CountrySuggest(string term)
        {
            var result = stockReceivingService.GetAllCountry()
                .Where(x => x.CountryName.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Id).Take(20)
                .ToList()
                .Select(x => new { id = x.Id, Display = x.CountryName });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductSuggest(string term)
        {
            var result = productServices.GetAll(term, 20);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductNameSuggest(string term)
        {
            var result = productServices.GetAll(x => x.Name.ToLower().Contains(term.ToLower()), 20)
               .Select(x => new { id = x.Code.Trim(), Display = x.Name.Trim() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProductCodeSuggest(string term)
        {
            var result = productServices.GetAll(x => x.Code.ToLower().Contains(term.ToLower()), 20)
                .Select(x => new { id = x.Name.Trim(), Display = x.Code.Trim() });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CustomerSuggest(string term)
        {
            var result = customerServices.GetQuery(x => x.FullName.Contains(term))
                .OrderBy(x => x.FullName).Take(20)
                .ToList()
                .Select(x => new { id = x.Id, Display = x.FullName });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// If auto suggestion not valid will be show dialog for user Select;
        /// </summary>
        /// <param name="id">CurrenId</param>
        /// <param name="name">Name found</param>
        /// <param name="modelName">Model on DB for</param>
        /// <returns></returns>

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult CheckSuggest(int? id, string name, string modelName, int tabIndex)
        {

            var supps = new List<StockRecevingDiagloModel>();
            if (!id.HasValue)
                id = 0;
            switch (modelName)
            {
                case "Supplier":
                    if (id != 0)
                    {
                        var supplierById = supplierServices.GetSupplier(id.Value);
                        if (supplierById != null && supplierById.FullName == name)
                            return Json("ok");
                    }
                    supps =
                       supplierServices.GetAll()
                           .Where(x => (string.IsNullOrEmpty(name) || x.FullName.Contains(name))).OrderBy(x => x.FullName)
                           .Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.FullName, Other = "N/A" })
                           .ToList();
                    if (!supps.Any())
                    {
                        supps = supplierServices.GetAll().Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.FullName, Other = "N/A" }).ToList();
                    }
                    break;
                case "Customer":
                    var customer = new Customer();
                    if (id != 0)
                    {
                        customer = customerServices.GetById(id.Value);
                        if (customer != null && customer.FullName == name)
                            return Json("ok");
                    }
                    customer.FullName = name;
                    supps =
                        customerServices.GetQuery()
                            .Where(x => (string.IsNullOrEmpty(name) || x.FullName.Contains(name))).OrderBy(x => x.FullName)
                            .Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.FullName, Other = "N/A" })
                            .ToList();
                    if (!supps.Any())
                    {
                        supps = customerServices.GetQuery().Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.FullName, Other = "N/A" }).ToList();
                    }
                    break;
                case "Country":
                    if (id != 0)
                    {
                        var countryById = stockReceivingService.GetAllCountry().FirstOrDefault(x => x.Id == id);
                        if (countryById != null && countryById.CountryName == name)
                            return Json("ok");
                    }
                    supps =
                        stockReceivingService.GetAllCountry()
                            .Where(x => (string.IsNullOrEmpty(name) || x.CountryName.Contains(name))).OrderBy(x => x.CountryName)
                            .Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.CountryName, Other = "N/A" })
                            .ToList();
                    if (!supps.Any())
                    {
                        supps = stockReceivingService.GetAllCountry().Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.CountryName, Other = "N/A" }).ToList();
                    }
                    break;
                case "Product":
                    if (id != 0 && productServices.Exists(name))
                        return Json("ok");

                    supps = productServices.GetAll(name, 20);
                    if (!supps.Any())
                    {
                        supps = productServices.GetAll().Select(x => new StockRecevingDiagloModel { Id = x.Id, Display = x.Name, Other = "N/A" }).ToList();
                    }
                    break;
            }
            ViewData["TabIndexAdd"] = tabIndex;
            ViewData["nameSearch"] = name;
            ViewData["modelName"] = modelName;
            var html = this.RenderPartialView("_EnterSearchDialogView", supps);
            return Json(html, JsonRequestBehavior.AllowGet);
        }

        public void Print(long id)
        {
            var allEverest = new List<StockInDetailReport>();
            var order = stockReceivingService.GetByModel(id);
            allEverest.AddRange(order.OrderDetails.Select(x => new StockInDetailReport
            {
                ProductCode = x.Product.Code ?? "",
                ProductName = x.Product.Name ?? "",
                WarehouseName = x.Warehouse.Name ?? "",
                Unit = x.UOM ?? "",
                WarehouseAddress = x.Warehouse.Address ?? "",
                Price = x.PriceReceive,
                Quantity = x.Quantity,
                Amount = x.Total
            }));

            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath(@"~/bin/Reports"), "RpStockIn.rpt"));
                rd.SetDataSource(allEverest);
                rd.SetParameterValue("supplierName", order.Supplier.FullName ?? "");
                rd.SetParameterValue("supplierAddress", order.Supplier.Address ?? "");
                rd.SetParameterValue("voucherDate", order.VoucherDate ?? DateTime.Now);
                rd.SetParameterValue("strDate",
                    string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", order.VoucherDate ?? DateTime.Now));
                rd.SetParameterValue("totalString", order.TTT.DecimalToString(CodeCurrency.USD));
                rd.SetParameterValue("vnAmount", order.VnTTT.ToString("N0"));
                rd.SetParameterValue("strVnAmount", order.VnTTT.DecimalToString(CodeCurrency.VND));
                rd.SetParameterValue("voucherNo", order.VoucherNo ?? " ");
                rd.SetParameterValue("note", string.IsNullOrEmpty(order.NotePrints) ? " " : order.NotePrints);
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response,
                    false, string.Format("phieunhap_{0}", order.VoucherNo));
                rd.Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
        }
        public FileResult GetFile(string fileName)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "App_Data/";
            return File(path + fileName, System.Net.Mime.MediaTypeNames.Application.Pdf, fileName);
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var mt = stockReceivingService.GetById(id);
                if (mt != null && mt.status == (byte)VoucherStatus.Pending)
                {

                    Logger.Log(string.Format("{0} delete stock order with id {1} bill {2}", CurrenUser.FullName, mt.VoucherID, mt.VoucherNo));
                    stockReceivingService.DeleteOrder(id);

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return RedirectToAction("Index");
        }

        public ActionResult StockCardAction(long id, string status)
        {
            VoucherStatus vStatus = (VoucherStatus)Enum.Parse(typeof(VoucherStatus), status);
            stockReceivingService.VoucherAction(vStatus, CurrenUser.Id, id);
            ViewBag.vStatus = vStatus;
            // var button = ModelExtensions.StockButtonAction(currentUser, vStatus, id, );
            var model = stockReceivingService.GetByModel(id);
            var statusview = this.RenderPartialView("_StatusView", model);
            return Json(new
            {
                status = vStatus.ToString(),
                //  button = button,
                view = statusview
            }, JsonRequestBehavior.AllowGet);
        }

        public void TestView()
        {
            ReportDocument rd = new ReportDocument();
            string strRptPath = System.Web.HttpContext.Current.Server.MapPath("~/") + "Reports//" + "generic.rpt";
            rd.Load(strRptPath);
            rd.SetDataSource(GetStudents());
            rd.SetParameterValue("fromDate", DateTime.Now.AddDays(-3).ToShortDateString());
            rd.SetParameterValue("toDate", DateTime.Now.ToShortDateString());
            rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");


        }
        private List<Student> GetStudents()
        {
            return new List<Student>() { 
            new Student(){StudentID=1,StudentName="Hasibul"},
            new Student(){StudentID=2,StudentName="Tst"}
            };
        }
        public ActionResult UpdateNote(long id, string note)
        {
            try
            {
                var model = stockReceivingService.GetByModel(id);
                model.NotePrints = note;
                stockReceivingService.Update(model);
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}