using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using SSM.Common;
using SSm.Common;

using SSM.Models;
using SSM.Services;
using SSM.ViewModels;
using SSM.ViewModels.Reports;
using SSM.ViewModels.Shared;
using Helpers = SSM.Common.Helpers;
using RequestContext = System.Web.Routing.RequestContext;

namespace SSM.Controllers
{

    public class SalesController : BaseController
    {
        private Grid<SalesModel> grid;
        private const string SALES_GIRD_MODEL = "SALES_GIRD_MODEL";
        private const string SALES_SEARCH_MODEL = "SALES_SEARCH_MODEL";
        private SelectList currencyList;
        private ShipmentServices shipmentServices;
        private IProductServices productServices;
        private IWarehouseSevices warehouseSevices;
        private IStockReceivingService stockReceivingService;
        private ISalesServices salesServices;
        private IEnumerable<Warehouse> warehouses;
        private TradingStockSearch filterModel;
        private UsersServices usersServices;
        private IServicesTypeServices servicesType;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            shipmentServices = new ShipmentServicesImpl();
            productServices = new ProductServices();
            warehouseSevices = new WareHouseServices();
            stockReceivingService = new StockReceivingService();
            salesServices = new SalesServices();
            usersServices = new UsersServicesImpl();
            servicesType = new ServicesTypeServices();
            if (!Helpers.AllowTrading)
            {
                throw new HttpException("You are not authorized to access this page");
            }
        }

        private IEnumerable<Curency> curencies;
        private void GetDefaultData()
        {
            curencies = curencies ?? stockReceivingService.GetAllCurencies();
            if (currencyList == null) currencyList = new SelectList(curencies, "Id", "Code");
            ViewData["Currency"] = currencyList;

            ViewBag.vStatus = VoucherStatus.Pending;
            ViewBag.Islook = "";
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
            var userTrading = salesServices.GetAllUserTrading();
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
            ViewData["Warehouses"] = stocks;
            ViewBag.SearchingMode = filterModel ?? new TradingStockSearch();
        }


        public ActionResult Index()
        {
            grid = (Grid<SalesModel>)Session[SALES_GIRD_MODEL];
            filterModel = (TradingStockSearch)Session[SALES_SEARCH_MODEL];
            filterModel = filterModel ?? new TradingStockSearch();
            if (grid == null)
            {
                grid = new Grid<SalesModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 20,
                                    Sord = "desc",
                                    Sidx = "VoucherDate"
                                }
                            );
                grid.SearchCriteria = new SalesModel();
            }

            UpdateGridData();
            return View(grid);
        }
        [HttpPost]
        public ActionResult Index(TradingStockSearch model, Grid<SalesModel> gridview)
        {
            grid = gridview;
            filterModel = model;
            Session[SALES_GIRD_MODEL] = grid;
            Session[SALES_SEARCH_MODEL] = filterModel;
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
            var mts = salesServices.GetAllModel(filterModel, grid.Pager.CurrentPage, grid.Pager.PageSize, out totalRow, CurrenUser);
            grid.Pager.Init(totalRow);

            grid.Data = mts;
        }
        public ActionResult Create()
        {
            GetDefaultData();
            ViewBag.Islook = "new";
            var order = new SalesModel();
            order.Customer = new Customer();
            order.Curency = curencies.FirstOrDefault(x => x.Code == "USD");
            order.VoucherCode = stockReceivingService.GetVoucherCodeId("HDA");
            order.VoucherNo = salesServices.GetVoucherNo();
            order.Status = VoucherStatus.Pending;
            order.ExchangeRate = 22500;
            return View(order);
        }
        [HttpPost]
        public ActionResult Create(SalesModel model)
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
                salesServices.InsertSale(model, out voucherId);
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
            var order = salesServices.GetModelById(id);
            if (order == null)
            {
                ViewBag.message = string.Format("Not fount delivery bill with id {0}", id);
                RedirectToAction("Index");
            }
            ViewBag.Islook = "look";
            return View(order);
        }
        [HttpPost]
        public ActionResult Edit(SalesModel model)
        {
            GetDefaultData();
            ViewBag.Islook = "";
            if (!ModelState.IsValid)
                return View(model);
            try
            {
                model.ModifyBy = CurrenUser.Id;
                model.DateModify = DateTime.Now;
                salesServices.Update(model);
                return RedirectToAction("Edit", new { id = model.VoucherId });
            }
            catch (Exception ex)
            {
                model.UserSubmited = model.UserSubmited ?? new User();
                model.UserChecked = model.UserChecked ?? new User();
                model.UserApproved = model.UserApproved ?? new User();
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }

        }
        public ActionResult BlankEditorRow(int tabindex)
        {
            GetDefaultData();
            ViewData["tabindex"] = tabindex;
            return PartialView("_SalesDetailView", new SalesDetailModel());
        }
        public void Print(long id)
        {
            var allEverest = new List<StockInDetailReport>();
            var order = salesServices.GetModelById(id);
            allEverest.AddRange(order.DetailModels.Select(x => new StockInDetailReport
            {
                ProductCode = x.Product.Code,
                ProductName = x.Product.Name,
                WarehouseName = x.Warehouse.Name,
                Unit = x.UOM,
                WarehouseAddress = x.Warehouse.Address ?? string.Empty,
                Price = x.VnPrice,
                Quantity = x.Quantity,
                Amount = x.VnPrice * x.Quantity
            }));

            try
            {
                ReportDocument rd = new ReportDocument();
                rd.Load(Path.Combine(Server.MapPath(@"~/bin/Reports"), "RpStockOut.rpt"));
                rd.SetDataSource(allEverest);
                rd.SetParameterValue("supplierName", order.Customer.FullName ?? "");
                rd.SetParameterValue("supplierAddress", order.Customer.Address ?? "");
                rd.SetParameterValue("voucherDate", order.VoucherDate ?? DateTime.Now);
                rd.SetParameterValue("note", string.IsNullOrEmpty(order.NotePrints) ? " " : order.NotePrints);
                rd.SetParameterValue("strDate",
                    string.Format("Ngày {0:dd} tháng {0:MM} năm {0:yyyy}", order.VoucherDate ?? DateTime.Now));
                rd.SetParameterValue("totalString", order.VnAmount.DecimalToString(CodeCurrency.VND));
                rd.SetParameterValue("voucherNo", order.VoucherNo ?? " ");
                rd.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response,
                    false, string.Format("phieuxuat_{0}", order.VoucherNo));
                rd.Dispose();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw ex;
            }
        }
        public ActionResult Delete(long id)
        {
            try
            {
                var mt = salesServices.GetById(id);
                if (mt != null && mt.Status == (byte)VoucherStatus.Pending)
                {
                    if (mt.Shipments != null)
                    {
                        shipmentServices.DeleteShipment(mt.Shipments.Id);
                    }
                    Logger.Log(string.Format("{0} delete sale order with id {1} bill {2}", CurrenUser.FullName, mt.VoucherID, mt.VoucherNo));
                    salesServices.DeleteOrder(mt);

                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetQtyInventory(int vid, int proId, int wId)
        {
            return Json(salesServices.GetQtyInventory(vid, proId, wId).ToString("N"), JsonRequestBehavior.AllowGet);
        }
        public ActionResult StockCardAction(long id, string status)
        {
            VoucherStatus vStatus = (VoucherStatus)Enum.Parse(typeof(VoucherStatus), status);
            ViewBag.vStatus = vStatus;
            //if (vStatus == VoucherStatus.Submited)
            //{
            //    var check = salesServices.CheckValidQty(id);
            //    if (!check)
            //    {
            //        return Json(0, JsonRequestBehavior.AllowGet);
            //    }
            //}
            salesServices.StockCardAction(vStatus, CurrenUser.Id, id);

            // var button = ModelExtensions.StockButtonAction(CurrenUser, vStatus, id);
            var model = salesServices.GetModelById(id);
            var statusview = this.RenderPartialView("_StatusView", model);
            return Json(new
            {
                status = vStatus.ToString(),
                // button = button,
                view = statusview
            }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult CreateShippmet(long id)
        {
            try
            {
                var link = "N/A";
                var existShipment = shipmentServices.GetShipmentByOrder(id);
                var order = salesServices.GetModelById(id);
                if (existShipment != null)
                {
                    var statusviews = this.RenderPartialView("_StatusView", order);

                    link = string.Format("<a href=\"/Shipment/Edit/{0}\" target=\"_bank\">{0}</a>", existShipment.Id);
                    return Json(new
                    {
                        Error = false,
                        RefId = link,
                        view = statusviews
                    }, JsonRequestBehavior.AllowGet);
                }

                var ship = new ShipmentModel()
                {
                    QtyNumber = 1,
                    QtyUnit = "ship(s)",
                    ServiceName = "Trading",
                    CarrierAirId = 360,
                    Dateshp = order.VoucherDate.ToString(),
                    CneeId = order.Customer.Id,
                    DepartmentId = order.UserCreated.DeptId ?? 0,
                    SaleId = order.UserCreated.Id,
                    CompanyId = order.UserCreated.ComId ?? 0,
                    IsTrading = true,
                    AgentId = 141,
                    VoucherId = id,
                    SaleType = ShipmentModel.SaleTypes.Handle.ToString(),//"Sales",
                    RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString(),
                    CountryDeparture = 126,
                    DepartureId = 2,
                    CountryDestination = 126,
                    DestinationId = 2,
                    ShipperId = 1754,
                    ServiceId = servicesType.GetId("Trading")

                };
                if (shipmentServices.InsertShipment(ship))
                {

                    try
                    {
                        var newShip = shipmentServices.GetShipmentByOrder(id);
                        var amount = (double)order.SumTotal;
                        var funds = (double)order.Amount0;
                        var revenus = new RevenueModel()
                        {
                            INAutoValue1 = amount,
                            INVI = amount,
                            Income = amount,
                            EXManualValue1 = funds,
                            EXVI = funds,
                            Expense = funds,
                            EarningVI = amount - funds,
                            Earning = amount - funds,
                            Id = newShip.Id,
                            InvCurrency1 = order.Curency.Code,
                            InvCurrency2 = order.Curency.Code,
                            InvType1 = "AgentDebit",
                            InvType2 = "AgentCredit",
                            InvAgentId1 = 141,
                            InvAgentId2 = 141,
                            PaidToCarrier = 126,
                            BonRequest = GetBonRequest(newShip.SaleType),
                            SaleType = newShip.SaleType,
                            AutoName1 = "Total exork amount",
                            EXManualName1 = "Cost of sales"
                        };
                        revenus.AmountBonus2 = Convert.ToDecimal(revenus.BonRequest * revenus.Earning / 100);
                        if (!shipmentServices.UpdateRevenue(revenus))
                        {
                            shipmentServices.DeleteShipment(newShip.Id);
                            throw new Exception("Not Create Revenue");
                        }
                        link = string.Format("<a href=\"/Shipment/Edit/{0}\" target=\"_bank\">{0}</a>", newShip.Id);
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(ex);
                        throw ex;
                    }
                }
                else
                {
                    return Json(new
                    {
                        Message = "Có lỗi trong lúc tạo shipment lòng liên hệ người quản trị",
                        Error = true
                    });
                }
                var model = salesServices.GetModelById(id);
                var statusview = this.RenderPartialView("_StatusView", model);

                return Json(new
                {
                    Error = false,
                    RefId = link,
                    view = statusview
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                var newShip = shipmentServices.GetShipmentByOrder(id);
                shipmentServices.DeleteShipment(newShip.Id);
                Logger.LogError(ex);
                return Json(new
                {
                    Message = ex.Message,
                    Error = true
                });
            }

        }
        private double GetBonRequest(String Type)
        {
            IEnumerable<SaleType> list = usersServices.getAllSaleTypes(true);
            foreach (SaleType sale in list)
            {
                if (sale.Name.Equals(Type))
                {
                    return Convert.ToDouble(sale.Value.Value);
                }
            }
            return 0;
        }
        public ActionResult Revenue(long id)
        {
            var model = salesServices.GetModelById(id);
            return PartialView("_Revenue", model);
        }

        public ActionResult UpdateNote(long id, string note)
        {
            try
            {
                var model = salesServices.GetModelById(id);
                model.NotePrints = note;
                salesServices.Update(model);
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