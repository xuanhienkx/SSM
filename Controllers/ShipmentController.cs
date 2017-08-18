using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SSM.Common;

using SSM.Models;
using System.Web.Routing;
using SSM.Services;
using SSM.ViewModels;
using SSM.ViewModels.Shared;
using SSM.Utils;
namespace SSM.Controllers
{
    public class ShipmentController : BaseController
    {
        #region Properties
        private ShipmentServices ShipmentServices1;
        private UsersServices UsersServices1;
        private User User1;
        private Grid<Shipment> _grid;
        private GridNew<Customer, CustomerModel> _customerGrid;
        private ShipmentCheck shipmentCheck;
        private ICustomerServices customerServices;
        private IAgentService agentService;
        private ICarrierService carrierService;
        private IAreaService areaService;
        private IUnitService unitService;
        private IServicesTypeServices servicesType;
        private IHistoryService historyService;
        private ICountryService countryService;

        public static String SHIPMENT_SEARCH_MODEL = "SHIPMENT_SEARCH_MODEL";
        public static String SHIPMENT_SEARCH_CHECK = "SHIPMENT_SEARCH_CHECK";
        public static String CUSTOMER_SEARCH_MODEL = "CUSTOMER_SEARCH_MODEL";
        public static String REVENUE_ID = "REVENUE_ID";
        #endregion
        #region Define
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            User1 = (User)Session[AccountController.USER_SESSION_ID];
            ShipmentServices1 = new ShipmentServicesImpl();
            UsersServices1 = new UsersServicesImpl();
            customerServices = new CustomerServices();
            agentService = new AgentService();
            carrierService = new CarrierService();
            areaService = new AreaService();
            unitService = new UnitService();
            servicesType = new ServicesTypeServices();
            historyService = new HistoryService();
            countryService = new CountryService();
            ViewData["CompanyLogo"] = UsersServices1.getComSetting(CompanyInfo.COMPANY_LOGO);
            ViewData["CompanyHeader"] = UsersServices1.getComSetting(CompanyInfo.COMPANY_HEADER);
            ViewData["CompanyFooter"] = UsersServices1.getComSetting(CompanyInfo.COMPANY_FOOTER);
            ViewData["AccountInfo"] = UsersServices1.getComSetting(CompanyInfo.ACCOUNT_INFO);
        }

        private IEnumerable<Customer> cnee;
        private IEnumerable<Customer> shippers;
        private IEnumerable<SaleType> saleTypes;
        private IEnumerable<Agent> agents;
        private IEnumerable<Area> areas;
        private IEnumerable<CarrierAirLine> carrierAirLines;
        private IEnumerable<Unit> units;


        protected IEnumerable<Customer> Cnee
        {
            get
            {
                return cnee = cnee ??
                    customerServices.GetAll(x => (x.CustomerType == CustomerType.Cnee.ToString() || x.CustomerType == CustomerType.ShipperCnee.ToString()) && x.IsSee == true && (CurrenUser.IsAdmin() || x.IsHideUser == false))
                  .OrderBy(x => x.CompanyName);
            }
        }
        protected IEnumerable<Customer> Shippers
        {
            get
            {
                return shippers = shippers ??
                    customerServices.GetAll(x => (x.CustomerType == CustomerType.Shipper.ToString() || x.CustomerType == CustomerType.ShipperCnee.ToString()) && x.IsSee == true && (CurrenUser.IsAdmin() || x.IsHideUser == false))
                  .OrderBy(x => x.CompanyName);
            }
        }

        private void GetDefaultData()
        {


            saleTypes = saleTypes ?? UsersServices1.getAllSaleTypes(true);
            agents = agents != null && agents.Any() ? agents : agentService.GetAll(a => a.IsActive && (CurrenUser.IsAdmin() || a.IsHideUser == false)).OrderBy(x => x.AbbName);
            areas = areas != null && areas.Any() ? areas : areaService.GetAll(x => x.IsSee == true && (CurrenUser.IsAdmin() || x.IsHideUser == false)).OrderBy(x => x.AreaAddress);
            carrierAirLines = carrierService.GetQuery(x => x.IsActive && x.IsSee == true && (CurrenUser.IsAdmin() || x.IsHideUser == false)).OrderBy(x => x.AbbName).ToList();
            units = units != null && units.Any() ? units : unitService.GetAll().OrderBy(x => x.Unit1);
            var countries = countryService.GetAll().ToList();
            ViewData["Statuses"] = Statuses;
            ViewData["ServicesAll"] = ServicesAll;
            ViewData["SaleTypes"] = SaleTypes;
            ViewData["SaleTypeST"] = SaleTypes;
            ViewData["RevenueST"] = saleTypes.Select(x => new SaleType()
            {
                Value = (decimal?)Convert.ToDouble(x.Value),
                Name = x.Name,
                Active = x.Active,
                Id = x.Id
            });
            ViewData["AgentsList"] = new SelectList(agents, "Id", "AbbName");
            ViewData["CneesList"] = new SelectList(Cnee, "Id", "CompanyName");
            ViewData["Agents"] = agents;//index
            ViewData["Cnees"] = Cnee;
            ViewData["Shippers"] = new SelectList(Shippers, "Id", "CompanyName");
            ViewData["ShippersFull"] = Shippers;
            ViewData["Areas"] = new SelectList(areas, "Id", "AreaAddress");
            ViewData["Carriers"] = new SelectList(carrierAirLines, "Id", "AbbName");
            ViewData["Units"] = new SelectList(units, "Unit1", "Unit1");
            ViewData["CountryList"] = new SelectList(countries, "Id", "CountryName");
            ViewData["CountryNameList"] = new SelectList(countries, "CountryName", "CountryName");
            List<String> List1 = new List<String>();
            ViewData["AreaListDep"] = new SelectList(List1);
            /*ToDo get all sales will modify later*/


            List<UsersModel> ListUser = new List<UsersModel>();
            if (User1.IsAdminAndAcct() || User1.IsOperations())
            {
                ListUser.AddRange(UsersServices1.getOpnSaleUser());
            }
            else if (User1.IsDepManager())
            {
                ListUser.AddRange(UsersServices1.getUsersBy(User1.DeptId.Value));
            }
            if (ListUser.Count > 0)
            {
                ListUser = ListUser.Distinct().ToList();
                ViewData["Sales"] = new SelectList(ListUser.Where(x => x.IsActive).OrderBy(x => x.FullName), "Id", "FullName");
            }
            ViewData["InvTypes"] = InvTypes;
            ViewData["CurrencyTypes"] = CurrencyTypes;





        }
        public SelectList Statuses
        {
            get
            {
                Array values = Enum.GetValues(typeof(ShipmentModel.RevenueStatusCollec));
                List<ListItem> items = new List<ListItem>(values.Length);
                items.Add(new ListItem
                {
                    Text = "--Select a status--",
                    Value = ""
                });
                foreach (var i in values)
                {
                    items.Add(new ListItem
                    {
                        Text = ShipmentModel.ViewStatus(i.ToString()),
                        Value = i.ToString()
                    });
                }

                return new SelectList(items, "Value", "Text");
            }
        }

        private SelectList InvTypes
        {
            get
            {
                var invs = from RevenueModel.InvTypes Inv in Enum.GetValues(typeof(RevenueModel.InvTypes))
                           select new { Id = Inv, Name = Inv.GetStringLabel() };
                return new SelectList(invs, "Id", "Name");
            }
        }

        private SelectList CurrencyTypes
        {
            get
            {
                var invs = from RevenueModel.CurrencyTypes Inv in Enum.GetValues(typeof(RevenueModel.CurrencyTypes))
                           select new { Id = Inv, Name = Inv.GetStringLabel() };
                return new SelectList(invs, "Id", "Name");
            }
        }
        private IEnumerable<ServicesType> servicesList;
        private SelectList Services
        {
            get
            {
                List<ServicesType> list = new List<ServicesType>();
                var servicesTypeItem = new ServicesType { Id = 0, SerivceName = "--All Services--" };
                list.Add(servicesTypeItem);
                list.AddRange(servicesList ?? servicesType.GetAll(x => x.IsActive).OrderBy(x => x.SerivceName).ToList());
                return new SelectList(list, "Id", "SerivceName");
            }

        }
        private SelectList ServicesAll
        {
            get
            {
                List<ServicesType> list = new List<ServicesType>();
                var servicesTypeItem = new ServicesType { Id = 0, SerivceName = "--All Services--" };
                list.Add(servicesTypeItem);
                list.AddRange(servicesType.GetAll().OrderBy(x => x.SerivceName).ToList());
                return new SelectList(list, "Id", "SerivceName");
            }

        }
        #endregion
        #region Shippemt
        #region Index Shippment

        public ActionResult Index()
        {

            string pageSize = "100";

            if (Cookie != null)
            {
                if (Cookie["ShipmentList"] != null)
                    pageSize = Cookie["ShipmentList"];
            }
            else
            {
                pageSize = UsersServices1.PageSettingNumber().DataValue;
            }

            _grid = (Grid<Shipment>)Session[SHIPMENT_SEARCH_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<Shipment>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = int.Parse(pageSize),
                                    Sord = "desc",
                                    Sidx = "DateShp"
                                }
                            );
                _grid.SearchCriteria = new Shipment();
            }

            long SearchQuickView = Session["SearchQuickView"] == null ? 0 : (long)Session["SearchQuickView"];
            Session["SearchQuickView"] = SearchQuickView;
            shipmentCheck = (ShipmentCheck)Session[SHIPMENT_SEARCH_CHECK] ?? new ShipmentCheck();
            UpdateGridData(SearchQuickView);
            ViewData["ShipmentCheck"] = shipmentCheck;
            return View(_grid);
        }

        [HttpPost]
        public ActionResult Index(Grid<Shipment> grid, long SearchQuickView, ShipmentCheck searCheckModel)
        {
            _grid = grid;
            Session[SHIPMENT_SEARCH_MODEL] = grid;

            if (SearchQuickView == 0)
            {
                SearchQuickView = Session["SearchQuickView"] == null ? 0 : (long)Session["SearchQuickView"];
            }
            shipmentCheck = searCheckModel;
            Session[SHIPMENT_SEARCH_CHECK] = shipmentCheck;
            Session["SearchQuickView"] = SearchQuickView;
            if (_grid.GridAction == GridAction.ChangePageSize)
            {
                SetCookiePager(_grid.Pager.PageSize, "ShipmentList");
            }
            _grid.ProcessAction();

            UpdateGridData(SearchQuickView);
            ViewData["ShipmentCheck"] = shipmentCheck;
            return PartialView("_ListData", _grid);
        }

        public ActionResult ListControl()
        {
            string pageSize;

            if (Cookie != null && Cookie["ShipmentList"] != null)
            {
                pageSize = Cookie["ShipmentList"];
            }
            else
            {
                pageSize = UsersServices1.PageSettingNumber().DataValue;
            }

            _grid = (Grid<Shipment>)Session[SHIPMENT_SEARCH_MODEL];
            if (_grid == null)
            {
                _grid = new Grid<Shipment>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = int.Parse(pageSize),
                                    Sord = "desc",
                                    Sidx = "DateShp"
                                }
                            );
                _grid.SearchCriteria = new Shipment();
            }

            long SearchQuickView = Session["SearchQuickView"] == null ? 0 : (long)Session["SearchQuickView"];
            Session["SearchQuickView"] = SearchQuickView;
            shipmentCheck = new ShipmentCheck() { IsControl = true };
            UpdateGridData(SearchQuickView);
            ViewData["ShipmentCheck"] = shipmentCheck;
            return View(_grid);
        }
        [HttpPost]
        public ActionResult ListControl(Grid<Shipment> grid, long SearchQuickView, ShipmentCheck searCheckModel)
        {
            _grid = grid;
            Session[SHIPMENT_SEARCH_MODEL] = grid;
            if (SearchQuickView == 0)
            {
                SearchQuickView = Session["SearchQuickView"] == null ? 0 : (long)Session["SearchQuickView"];
            }
            shipmentCheck = searCheckModel;
            shipmentCheck.IsControl = true;
            Session["SearchQuickView"] = SearchQuickView;
            _grid.ProcessAction();
            if (_grid.GridAction == GridAction.ChangePageSize)
            {
                SetCookiePager(_grid.Pager.PageSize, "ShipmentList");
            }
            UpdateGridData(SearchQuickView);
            ViewData["ShipmentCheck"] = shipmentCheck;
            return PartialView("_ListDataControl", _grid);
        }

        public List<long> UListAvilibel()
        {
            List<long> UserIds1 = new List<long>();
            UserIds1.Add(User1.Id);
            if (User1.IsAdmin() || User1.IsAccountant())
                UserIds1 = new List<long>();
            else if (User1.IsDirecter() || User1.IsAccountant())
            {
                UserIds1.AddRange(User1.Company.Users.Where(x => x.IsActive).Select(_user => _user.Id).ToList());
                if (!User1.IsComDirecter()) return UserIds1.Distinct().ToList();
                var lu = UsersServices1.GetAllUsersComs(User1);
                UserIds1 = UsersServices1.GetAll(x => lu.Contains(x.ComId.Value)).Select(_user => _user.Id).ToList();
                if (UserIds1.Count == 0)
                {
                    UserIds1.Add(User1.Id);
                }
            }
            else
                if (UsersModel.isDeptManager(User1))
            {
                UserIds1.AddRange(from _user in User1.Department.Users
                                  where _user.IsActive == true
                                      && _user.ComId == User1.ComId
                                  select _user.Id);
            }
            return UserIds1.Distinct().ToList();
        }

        private void UpdateGridData(long QuickView)
        {
            GetDefaultData();
            int Skip = (_grid.Pager.CurrentPage - 1) * _grid.Pager.PageSize;
            int Take = _grid.Pager.PageSize;
            List<long> UserIds1 = UListAvilibel();
            var shipments = ShipmentServices1.GetQueryShipment(ShipmentServices1.ConvertShipment(_grid.SearchCriteria), User1, UserIds1, QuickView);

            var orderField = new SSM.Services.SortField(string.IsNullOrEmpty(_grid.Pager.Sidx) ? "DateShp" : _grid.Pager.Sidx, _grid.Pager.Sord == "asc");


            if (shipmentCheck.IsFreightCheck && !shipmentCheck.IsTradingCheck)
            {
                shipments = shipments.Where(x => x.IsTrading == null || x.IsTrading.Value == false);
            }
            else if (shipmentCheck.IsTradingCheck && !shipmentCheck.IsFreightCheck)
            {
                shipments = shipments.Where(x => x.IsTrading != null && x.IsTrading.Value == true);
            }

            if (shipmentCheck.IsFreightCheck == shipmentCheck.IsTradingCheck == false)
                shipmentCheck.IsFreightCheck = shipmentCheck.IsTradingCheck = true;
            if (shipmentCheck.IsControl)
            {
                shipments = shipments.Where(x => x.IsControl == true);
            }

            switch (shipmentCheck.ColorStatus)
            {
                case ColorStatus.SalesRevised:
                    shipments = shipments.Where(x => x.Revenue != null && x.Revenue.IsRevised);
                    break;
                case ColorStatus.AcctRequest:
                    shipments = shipments.Where(x => x.Revenue != null && x.Revenue.IsRequest);
                    break;
                case ColorStatus.NoBill:
                    shipments = shipments.Where(x =>
                      (x.MasterNum == null || x.MasterNum == "" || x.MasterNum.Equals("CHUA BILL")) || x.HouseNum == null || x.HouseNum == "" || x.HouseNum.Equals("CHUA BILL"));
                    break;
                default:
                    break;
            }
            shipments = shipments.OrderBy(orderField).ThenBy(x => x.ShipmentRef).ThenBy(x => x.ControlStep);//.ThenBy(x => x.ControlStep);
            int totalRows = shipments.Count();
            _grid.Pager.Init(totalRows);

            if (totalRows == 0)
            {
                _grid.Data = new List<Shipment>();
                return;
            }
            ViewData["ShipmentCheck"] = shipmentCheck;
            // var shipmentsViewList =GetListPager(Shipments,sh)
            var shipmentsViewList = shipments.Skip(Skip).Take(Take).ToList();
            _grid.Data = shipmentsViewList;
            _grid.SearchCriteria.CompanyId = 0;

        }
        #endregion
        #region Ajax load data function
        public JsonResult GetJsonByCountry(long id, long CountryId)
        {
            IEnumerable<Area> AreaList = ShipmentServices1.getAllAreaByCountry(CountryId);
            List<AreaModel> AreaModels = new List<AreaModel>();
            foreach (Area Area1 in AreaList)
            {
                AreaModel AreaModel1 = new AreaModel();
                AreaModel1.Id = Area1.Id;
                AreaModel1.AreaAddress = Area1.AreaAddress;
                AreaModel1.CountryId = CountryId;
                AreaModels.Add(AreaModel1);
            }
            return this.Json(AreaModels, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetUnitJsonByService(long id, String ServiceName)
        {
            List<Unit> UnitsDS = new List<Unit>();
            if (ShipmentModel.Services.AirInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.AirOutbound.ToString().Equals(ServiceName))
            {
                UnitsDS = unitService.GetAll(x => x.ServiceType.Equals("Air")).OrderBy(x => x.Unit1).ToList();
            }
            else if (ShipmentModel.Services.SeaInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.SeaOutbound.ToString().Equals(ServiceName))
            {
                UnitsDS = unitService.GetAll(x => x.ServiceType.Equals("Sea")).OrderBy(x => x.Unit1).ToList();
            }
            else
            {
                UnitsDS = unitService.GetAll().OrderBy(x => x.Unit1).ToList();
            }
            return this.Json(UnitsDS, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetAgentsJsonByService(String DateFrom, String DateTo)
        {
            List<Agent> UnitsDS = new List<Agent>();
            List<AgentModel> JsonUnitsDS = new List<AgentModel>();
            SOAModel SOAModel1 = new SOAModel();
            SOAModel1.DateFrom = DateFrom;
            SOAModel1.DateTo = DateTo;
            UnitsDS = ShipmentServices1.GetAgentBySOA(SOAModel1).OrderBy(x => x.AbbName).ToList();

            foreach (Agent carrier in UnitsDS)
            {
                AgentModel model = new AgentModel();
                model.Id = carrier.Id;
                model.Address = carrier.Address;
                model.AbbName = carrier.AbbName;
                model.Description = carrier.Description;
                JsonUnitsDS.Add(model);
            }
            return this.Json(JsonUnitsDS, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCarrierJsonByService(long id, String ServiceName)
        {
            List<CarrierAirLine> UnitsDS = new List<CarrierAirLine>();
            List<CarrierModel> JsonUnitsDS = new List<CarrierModel>();
            if (ShipmentModel.Services.AirInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.AirOutbound.ToString().Equals(ServiceName))
            {
                UnitsDS = carrierService.GetAllByType(CarrierType.AirLine.ToString()).ToList();
            }
            else if (ShipmentModel.Services.SeaInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.SeaOutbound.ToString().Equals(ServiceName))
            {
                UnitsDS = carrierService.GetAllByType(CarrierType.ShippingLine.ToString()).ToList();

            }
            else
            {
                UnitsDS = carrierService.GetAll().ToList();
            }
            foreach (CarrierAirLine carrier in UnitsDS)
            {
                CarrierModel model = new CarrierModel();
                model.Id = carrier.Id;
                model.Address = carrier.CarrierAirLineName;
                model.AbbName = carrier.AbbName;
                model.Description = carrier.Description;
                JsonUnitsDS.Add(model);
            }
            return this.Json(JsonUnitsDS, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Create Shipment
        public ActionResult Create()
        {
            ShipmentModel ShipmentModel1 = new ShipmentModel();
            IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(125);
            IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(125);
            IEnumerable<User> userList = UsersServices1.GetAll(x => !x.RoleName.Equals(UsersModel.Positions.Director) && !x.RoleName.Equals(UsersModel.Positions.Accountant));
            GetDefaultData();
            ShipmentModel1.CountryDeparture = 125;
            ShipmentModel1.CountryDestination = 125;
            ShipmentModel1.ServiceId = 3;
            ShipmentModel1.QtyUnit = "KGS";
            ViewData["Services"] = Services;
            ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
            ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
            ViewData["UserName"] = User1.FullName;

            ViewData["Units"] = new SelectList(unitService.GetAll(x => x.ServiceType == "Air").OrderBy(x => x.Unit1).ToList(), "Unit1", "Unit1");
            ViewData["Carriers"] = carrierService.GetAllByType(CarrierType.ShippingLine.ToString()).ToList();
            ShipmentModel1.Dateshp = DateTime.Now.ToString("dd/MM/yyyy");

            return View(ShipmentModel1);
        }

        [HttpPost]
        public ActionResult Create(ShipmentModel ShipmentModel1, String submitType)
        {
            ShipmentModel1.VoucherId = null;
            ShipmentModel1.IsTrading = false;
            GetDefaultData();
            if (ModelState.IsValid)
            {
                try
                {
                    ShipmentModel1.DepartmentId = User1.Department.Id;
                    ShipmentModel1.SaleId = User1.Id;
                    ShipmentModel1.CompanyId = User1.ComId.Value;
                    ShipmentModel1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                    ShipmentModel1.ServiceName = servicesType.FindEntity(x => x.Id == ShipmentModel1.ServiceId).SerivceName;
                    ShipmentServices1.InsertShipment(ShipmentModel1);
                    return RedirectToAction("Index", "Shipment", new { id = 0 });
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                    ViewData["Services"] = Services;
                    return View(ShipmentModel1);
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Logger.LogError(messages);
                IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDeparture);
                IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDestination);
                IEnumerable<CarrierAirLine> carriers = carrierService.GetAllByType(CarrierType.ShippingLine.ToString()).ToList();
                ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
                ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
                ViewData["UserName"] = User1.FullName;
                ViewData["Units"] = new SelectList(unitService.GetAll(x => x.ServiceType == "Sea").OrderBy(x => x.Unit1).ToList(), "Unit1", "Unit1");
                ViewData["Carriers"] = carriers;
                ViewData["Services"] = Services;
            }
            return View(ShipmentModel1);
        }
        #endregion

        #region Edit shipment
        private void InitService(String ServiceName)
        {
            List<CarrierAirLine> Carriers = new List<CarrierAirLine>();
            List<CarrierModel> JsonUnitsDS = new List<CarrierModel>();
            if (ShipmentModel.Services.AirInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.AirOutbound.ToString().Equals(ServiceName))
            {
                Carriers = carrierService.GetAllByType(CarrierType.AirLine.ToString()).ToList();
            }
            else if (ShipmentModel.Services.SeaInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.SeaOutbound.ToString().Equals(ServiceName))
            {
                Carriers = carrierService.GetAllByType(CarrierType.ShippingLine.ToString()).ToList();

            }
            else
            {
                Carriers = carrierService.GetAll().ToList();
            }
            List<Unit> UnitsDS = new List<Unit>();
            if (ShipmentModel.Services.AirInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.AirOutbound.ToString().Equals(ServiceName))
            {
                UnitsDS = unitService.GetAll(x => x.ServiceType.Equals("Air")).ToList();
            }
            else if (ShipmentModel.Services.SeaInbound.ToString().Equals(ServiceName) || ShipmentModel.Services.SeaOutbound.ToString().Equals(ServiceName))
            {
                UnitsDS = unitService.GetAll(x => x.ServiceType.Equals("Sea")).ToList();
            }
            else
            {
                UnitsDS = unitService.GetAll().ToList();
            }
            ViewData["Units"] = new SelectList(UnitsDS.OrderBy(x => x.Unit1).ToList(), "Unit1", "Unit1");
            ViewData["Carriers"] = Carriers;
        }
        public ActionResult Edit(int id)
        {
            ShipmentModel ShipmentModel1 = ShipmentServices1.GetShipmentModelById(id);
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            //ShipmentModel1.LockShipment = LookShipment(Shipment1);

            if (Shipment1 == null || (Shipment1.IsLock != null && Shipment1.IsLock.Value) || !User1.IsOwnner(Shipment1.SaleId.Value))
            {
                return RedirectToAction("DetailShipment", new { Id = Shipment1.Id });
            }
            GetDefaultData();
            ViewData["Services"] = Services;
            //ShipmentModel1.Id = id;
            ViewData["UserName"] = User1.FullName;
            ShipmentModel1.CountryDeparture = (Shipment1.Area != null && Shipment1.Area.CountryId != null) ? Shipment1.Area.CountryId.Value : 0;
            ShipmentModel1.CountryDestination = (Shipment1.Area1 != null && Shipment1.Area1.CountryId != null) ? Shipment1.Area1.CountryId.Value : 0;
            IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDeparture);
            IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDestination);

            ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
            ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
            InitService(ShipmentModel1.TypeServices.SerivceName);

            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            if (ShipmentModel1.IsMainControl)
            {
                LoadViewUserInConsol(ShipmentModel1);
                ViewBag.ListMember = ShipmentServices1.GetListMemberOfConsol(ShipmentModel1.Id);
            }
            return View(ShipmentModel1);
        }

        private void LoadViewUserInConsol(ShipmentModel ShipmentModel1)
        {
            var users =
              UsersServices1.GetQuery(
                  x =>
                      !x.Department.DeptFunction.Equals(UsersModel.Positions.Administrator.ToString()) &&
                      !x.Department.DeptFunction.Equals(UsersModel.Positions.Director.ToString()) &&
                      !x.Department.DeptFunction.Equals(UsersModel.Positions.Accountant.ToString())
                      && x.IsActive == true)
                      .OrderBy(x => x.FullName).ToList();
            var listAll = users;
            var listSelect = new List<SelectListItem>();
            if (ShipmentModel1.UserListInControl != null && ShipmentModel1.UserListInControl.Count >= 0)
            {
                listSelect.AddRange(from userId in ShipmentModel1.UserListInControl
                                    select UsersServices1.getUserById(userId)
                                        into u
                                    where u != null
                                    select new SelectListItem()
                                    {
                                        Value = u.Id.ToString(),
                                        Text = u.FullName
                                    });


            }
            ViewBag.UserList = listAll;
            ViewBag.UserListSelect = listSelect;
        }

        private bool LookShipment(Shipment Shipment1)
        {
            if (Shipment1.CreateDate != null && (Shipment1.CreateDate.Value.AddDays(60).CompareTo(DateTime.Now) <= 0) && (Shipment1.IsLock == null || !Shipment1.IsLock.Value))
            {
                Shipment1.IsLock = true;
                Shipment1.LockDate = DateTime.Now;
                ShipmentServices1.UpdateAny();
                return true;
            }
            else
                return false;
        }

        public ActionResult DetailShipment(int id)
        {
            ShipmentModel ShipmentModel1 = ShipmentServices1.GetShipmentModelById(id);
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            GetDefaultData();
            //ShipmentModel1.LockShipment = LookShipment(Shipment1);
            ShipmentModel1.Id = id;
            ViewData["UserName"] = UsersServices1.getUserById(Shipment1.SaleId.Value).FullName;
            ShipmentModel1.CountryDeparture = Shipment1.Area != null ? Shipment1.Area.CountryId.Value : 0;
            ShipmentModel1.CountryDestination = Shipment1.Area1 != null ? Shipment1.Area1.CountryId.Value : 0;
            IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDeparture);
            IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDestination);
            ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
            ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
            InitService(ShipmentModel1.TypeServices.SerivceName);
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Services"] = Services;
            viewDocument(Shipment1);
            return View(ShipmentModel1);
        }
        private void viewDocument(Shipment Shipment1)
        {
            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {
                ViewData["ArriveNotice"] = "ArriveNotice";
            }
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                ViewData["BillLanding"] = "BillLanding";
            }
        }

        private void UpdateBonusRevenue(long id, string type)
        {
            var revenew = ShipmentServices1.getRevenueModelById(id);
            revenew.SaleType = type;
            revenew.BonRequest = getBonRequest(type);
            revenew.BonApprove = (int)revenew.BonRequest;
            ShipmentServices1.UpdateRevenue(revenew);

        }

        [HttpPost]
        public ActionResult Edit(ShipmentModel ShipmentModel1, int id)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            ViewData["Error"] = string.Empty;
            ViewData["Services"] = Services;
            if (ModelState.IsValid)
            {
                try
                {
                    ShipmentModel1.DepartmentId = Shipment1.DeptId;
                    ShipmentModel1.SaleId = Shipment1.SaleId.Value;
                    ShipmentModel1.CompanyId = Shipment1.CompanyId.Value;
                    if (ShipmentModel1.LockShipment)
                    {
                        ShipmentModel1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Locked.ToString();
                    }
                    ShipmentModel1.RevenueStatus = Shipment1.RevenueStatus;
                    if (Shipment1.RevenueStatus == "Submited" || Shipment1.RevenueStatus == "Approved")
                    {
                        ShipmentModel1.Dateshp = Shipment1.DateShp != null
                            ? Shipment1.DateShp.Value.ToString("dd/MM/yyyy")
                            : "";
                    }
                    var serviceType = servicesType.FindEntity(x => x.Id == ShipmentModel1.ServiceId);
                    if (serviceType != null)
                        Shipment1.ServiceName = serviceType.SerivceName;
                    ShipmentServices1.UpdateShipment(ShipmentModel1);
                    if (ShipmentModel1.IsTrading)
                    {
                        UpdateBonusRevenue(Shipment1.Id, Shipment1.SaleType);
                    }
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception);
                    string messages = string.Join("; ", ModelState.Values
                                      .SelectMany(x => x.Errors)
                                      .Select(x => x.ErrorMessage));
                    Logger.LogError(messages);
                    GetDefaultData();
                    if (ShipmentModel1.IsMainControl)
                    {
                        LoadViewUserInConsol(ShipmentModel1);
                    }
                    var svType = servicesType.GetModelById(ShipmentModel1.ServiceId);
                    InitService(svType.SerivceName);
                    return View(ShipmentModel1);
                }
            }
            else
            {
                string messages = string.Join("; ", ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => x.ErrorMessage));
                Logger.LogError(messages);
                ViewData["Error"] = messages;
                ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
                viewDocument(Shipment1);
                IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDeparture);
                IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(ShipmentModel1.CountryDestination);
                ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
                ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");

                ViewData["Services"] = Services;
                GetDefaultData();
                if (ShipmentModel1.IsMainControl)
                {
                    LoadViewUserInConsol(ShipmentModel1);
                }
                var svType = servicesType.GetModelById(ShipmentModel1.ServiceId);
                InitService(svType.SerivceName);
                return View(ShipmentModel1);
            }



            return RedirectToAction("Edit", new { id = id });
        }
        public ActionResult UpdatePOD(long id)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            Shipment1.isDelivered = true;
            Shipment1.DeliveredDate = DateTime.Now;
            ShipmentServices1.UpdateAny();
            return RedirectToAction("Index", "Shipment", new { id = 0 });
        }
        public ActionResult CancelPOD(long id)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            Shipment1.isDelivered = false;
            ShipmentServices1.UpdateAny();
            return RedirectToAction("Index", "Shipment", new { id = 0 });
        }
        public ActionResult Delete(int id)
        {
            ShipmentServices1.DeleteShipment(id);
            return RedirectToAction("Index", "Shipment", new { id = 0 });
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index", "Shipment", new { id = 0 });
            }
            catch
            {
                return View();
            }
        }
        #endregion
        private SelectList SaleTypes
        {
            get
            {
                try
                {
                    List<String> SaleTypes = new List<string>();
                    IEnumerable<SaleType> SaleTypeIe = UsersServices1.getAllSaleTypes(true);
                    foreach (SaleType sale1 in SaleTypeIe)
                    {
                        SaleTypes.Add(sale1.Name);
                    }
                    return new SelectList(SaleTypes);
                }
                catch (Exception e)
                {

                }
                return new SelectList(new List<String>());
            }
        }
        private void displayActions(Shipment Shipment1)
        {
            bool isAllowSubmit = !(Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString())
                || Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Locked.ToString()));
            bool isApproved = Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Approved.ToString());
            bool isAllowApproveOrLock = !(Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Pending.ToString())
                || Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Locked.ToString()));
            var isSale = (User1.Id == Shipment1.SaleId.Value) && isAllowSubmit;
            ViewData["SalesAction"] = isSale;
            ViewData["DirectorAction"] = User1.AllowApproRevenue && isAllowApproveOrLock;
            ViewData["AccountAction"] = UsersModel.Functions.Accountant.ToString().Equals(User1.Department != null ? User1.Department.DeptFunction : "") && (isApproved || isAllowApproveOrLock);
        }
        #endregion
        #region Revenue
        public ActionResult RevenueCommon(long id)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            displayActions(Shipment1);
            User SubmitUser = UsersServices1.getUserById(Shipment1.SaleId.Value);
            ViewData["SubmitName"] = "";
            ViewData["SubmitDate"] = "";
            ViewData["DateShp"] = Shipment1.DateShp.Value.ToString("dd/MM/yyyy");
            var settingTax = UsersServices1.getTaxCommissiong();
            var taxs = settingTax == null ? "10" : settingTax.DataValue;
            ViewData["TaxRate"] = int.Parse(taxs.Trim());
            if ("SeaInbound".Contains(Shipment1.ServicesType.SerivceName))
            {
                ViewData["DateShpLabel"] = "ETA";
            }
            else if ("SeaOutbound".Contains(Shipment1.ServicesType.SerivceName))
            {
                ViewData["DateShpLabel"] = "ETD";
            }
            else
            {
                ViewData["DateShpLabel"] = "DATE";
            }
            if (SubmitUser != null && Shipment1.SubmitDate != null)
            {
                ViewData["SubmitName"] = "Submit by : " + SubmitUser.FullName;
                ViewData["SubmitDate"] = "Submit date : " + (Shipment1.SubmitDate != null ? Shipment1.SubmitDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["ShipperRevenue"] = Shipment1.Customer1 != null ? Shipment1.Customer1.FullName : string.Empty;
            ViewData["CneeRevenue"] = Shipment1.Customer != null ? Shipment1.Customer.FullName : string.Empty;
            ViewData["POLPOD"] = Shipment1.Area != null ? (Shipment1.Area.AreaAddress + " / " + Shipment1.Area1.AreaAddress) : string.Empty;
            ViewData["HBill"] = Shipment1.HouseNum;
            ViewData["MBill"] = Shipment1.MasterNum;
            //ViewData["SFreights"] = Shipment1.SFreight;
            ViewData["Quantity"] = Shipment1.QtyNumber + "X" + Shipment1.QtyUnit;
            ViewData["AgentRev"] = Shipment1.Agent != null ? Shipment1.Agent.AbbName : string.Empty;
            ViewData["AgentRevId"] = Shipment1.Agent != null ? Shipment1.Agent.Id : 0;
            ViewData["CarrierRev"] = Shipment1.CarrierAirLine != null ? Shipment1.CarrierAirLine.AbbName : string.Empty;
            ViewData["SaleType"] = Shipment1.SaleType;
            ViewData["SaleTypes"] = SaleTypes;
            GetDefaultData();
            RevenueModel RevenueModel1 = ShipmentServices1.getRevenueModelById(id);

            if (RevenueModel1 != null)
            {
                SubmitUser = UsersServices1.getUserById(RevenueModel1.ApproveId != null ? RevenueModel1.ApproveId.Value : 0);
                ViewData["ApproveName"] = "";
                ViewData["ApprovedDate"] = "";
                if (SubmitUser != null && Shipment1.ApprovedDate != null)
                {
                    ViewData["ApproveName"] = "Approve by : " + SubmitUser.FullName;
                    ViewData["ApprovedDate"] = "Approved date : " + (Shipment1.ApprovedDate != null ? Shipment1.ApprovedDate.Value.ToString("dd/MM/yyyy HH:mm:ss") : "");
                }
                SubmitUser = UsersServices1.getUserById(RevenueModel1.AccountId != null ? RevenueModel1.AccountId.Value : 0);
                ViewData["AccountName"] = "";
                if (SubmitUser != null)
                {
                    ViewData["AccountName"] = "Invoice Issued by : " + SubmitUser.FullName;
                }
                RevenueModel1.Id = id;
                ViewData["TaxCommission"] = RevenueModel1.Tax;

            }
            if (RevenueModel1 == null)
            {
                RevenueModel1 = new RevenueModel();
                RevenueModel1.BonRequest = getBonRequest(Shipment1.SaleType);
                RevenueModel1.BonApprove = (int)RevenueModel1.BonRequest;
                RevenueModel1.Id = id;
                RevenueModel1.PaidToCarrier = Shipment1.CarrierAirId.Value;
                RevenueModel1.InvAgentId1 = 141;
                RevenueModel1.InvAgentId2 = 141;
                RevenueModel1.SRate = 0;
                RevenueModel1.Shipment = Shipment1;
                RevenueModel1.IsControl = Shipment1.IsMainShipment;
                RevenueModel1.SaleType = Shipment1.SaleType;
            }

            RevenueModel1.SaleType = RevenueModel1.BonApprove > 0
                ? getSaleType(RevenueModel1.BonApprove)
                : getSaleType(RevenueModel1.BonRequest);
            ViewData["SaleType"] = RevenueModel1.SaleType;
            viewDocument(Shipment1);
            var history = historyService.GeModelLasted(id, new RevenueModel().GetType().ToString());
            ViewBag.HisgoryMessage = history == null ? string.Empty : history.HistoryMessage;
            if (Request.IsAjaxRequest())
            {
                return PartialView("_RevenueDetailAcctView", RevenueModel1);
            }
            return View(RevenueModel1);
        }
        public ActionResult Revenue(long id)
        {
            return RevenueCommon(id);

        }
        public ActionResult PrintRevenue(long id)
        {
            return RevenueCommon(id);

        }
        private double getBonRequest(String Type)
        {
            IEnumerable<SaleType> list = UsersServices1.getAllSaleTypes(true);
            foreach (SaleType sale in list)
            {
                if (sale.Name.Equals(Type))
                {
                    return Convert.ToDouble(sale.Value.Value);
                }
            }
            return 0;
        }
        private String getSaleType(double value)
        {
            IEnumerable<SaleType> SaleTypes = UsersServices1.getAllSaleTypes(true);
            foreach (SaleType SaleType1 in SaleTypes)
            {
                if (Convert.ToDouble(SaleType1.Value).Equals(value))
                {
                    return SaleType1.Name;
                }
            }
            return ShipmentModel.SaleTypes.Handle.ToString();
        }

        private void UpdateRevenueControl(Revenue Revenue1)
        {
            if (Revenue1.Shipment == null || Revenue1.Shipment.ShipmentRef == null) return;
            if (Revenue1.Shipment.IsMainShipment == true)
            {
                Revenue1.IsControl = true;
            }
            ShipmentServices1.UpdateRevenueOfControl(Revenue1.Shipment.ShipmentRef.Value);
        }
        [HttpPost]
        public ActionResult Revenue(RevenueModel RevenueModel1, long id, String RevenueAction)
        {
            try
            {
                Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
                Revenue Revenue1 = ShipmentServices1.getRevenueById(RevenueModel1.Id);
                RevenueModel1.SaleType = getSaleType(RevenueModel1.BonApprove);
                RevenueModel1.Shipment = Shipment1;
                viewDocument(Shipment1);
                displayActions(Shipment1);
                var settingTax = UsersServices1.getTaxCommissiong();
                var taxs = settingTax == null ? "10" : settingTax.DataValue;
                var history = historyService.GeModelLasted(id, new RevenueModel().GetType().ToString());
                ViewBag.HisgoryMessage = history == null ? string.Empty : history.HistoryMessage;
                ViewData["TaxRate"] = int.Parse(taxs.Trim());
                var bonApproval = RevenueModel1.BonApprove;// parseFloat(jQuery("#BonRequest").val().replace(/\,/g, ''));
                var earning = RevenueModel1.Earning;//parseFloat(jQuery("#Earning").val().replace(/\,/g, ''));
                var amountBonus2 = bonApproval * earning / 100;
                RevenueModel1.AmountBonus2 = Convert.ToDecimal(amountBonus2);
                if (Revenue1 != null)
                {
                    Revenue1.EXRemark = RevenueModel1.EXRemark;
                    Revenue1.INRemark = RevenueModel1.INRemark;
                    Revenue1.SaleType = RevenueModel1.SaleType;
                }
                var action = new string[]
                                            {
                                                "Submit",
                                                "AccountantCheck"
                                            };
                if (!action.Contains(RevenueAction))
                    HistoryWirter(RevenueAction, id);
                else
                {
                    if (Revenue1 != null && Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Submited.ToString()))
                    {

                        var dbModel = RevenueModel.ConvertModel(Revenue1);
                        var obj1 = RevenueCalculateViewModel.ConvertModel(dbModel);
                        var obj2 = RevenueCalculateViewModel.ConvertModel(RevenueModel1);
                        obj1.AmountBonus1 = Math.Round(obj1.AmountBonus1, 2);
                        obj1.AmountBonus2 = Math.Round(obj1.AmountBonus2, 2);
                        obj2.Tax = obj1.Tax;

                        List<string> listMessage;
                        var check = obj1.DeepEquals(obj2, out listMessage);
                        if (!check && listMessage.Any())
                        {
                            string message = string.Format("{0}/ {1} / {2}: ", DateTime.Now.ToString("HH:mm"),
                                User1.FullName,
                               "Submit".Equals(RevenueAction)
                               ? "Revised" : "Request");
                            message += ";" + string.Join(";", listMessage);
                            HistoryWirter(RevenueAction, id, message, true);
                            RevenueModel1.IsRevised = message.Contains("Revised");
                            RevenueModel1.IsRequest = message.Contains("Request");
                        }
                        else
                        {
                            RevenueModel1.IsRevised = false;
                            RevenueModel1.IsRequest = RevenueAction.Equals("AccountantCheck");
                            HistoryWirter(RevenueAction, id);
                        }
                    }
                }
                if ("Close".Equals(RevenueAction))
                {
                    Session[REVENUE_ID] = Shipment1.Id;
                    return RedirectToAction("Index");
                }
                if ("Cancel".Equals(RevenueAction))
                {
                    if (Shipment1 != null)
                    {
                        if (Revenue1 != null)
                        {
                            Revenue1.IsRevised = false;
                            Revenue1.IsRequest = false;
                            Revenue1.ApproveId = (long?)null;
                            Revenue1.SaleType = RevenueModel1.SaleType;
                        }
                        Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                        Shipment1.ApprovedDate = null;
                        Shipment1.SubmitDate = null;
                        ShipmentServices1.UpdateAny();
                        if (Shipment1.IsControl && !Shipment1.IsMainShipment)
                        {
                            ShipmentServices1.UpdateMainShipmentStatus(Shipment1.ShipmentRef.Value);
                        }
                    }
                    Session[REVENUE_ID] = Shipment1.Id;
                    return RedirectToAction("Index");
                }
                if ("GetBack".Equals(RevenueAction))
                {
                    if (Shipment1 != null)
                    {
                        if (Revenue1 != null)
                        {
                            Revenue1.IsRevised = false;
                            Revenue1.IsRequest = false;
                            Revenue1.SaleType = RevenueModel1.SaleType;
                        }
                        Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                        Shipment1.SubmitDate = null;
                        ShipmentServices1.UpdateShipment();
                    }
                    return RedirectToAction("Revenue", new { id = id });
                }
                if ("Approve".Equals(RevenueAction))
                {
                    if (Shipment1 != null)
                    {
                        RevenueModel1.IsRevised = false;
                        RevenueModel1.IsRequest = false;
                        RevenueModel1.ApproveId = User1.Id;
                        ShipmentServices1.UpdateRevenue(RevenueModel1);
                        Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Approved.ToString();
                        Shipment1.ApprovedDate = DateTime.Now;
                        if (Shipment1.IsControl)
                        {
                            ShipmentServices1.UpdateMainShipmentStatus(Shipment1.ShipmentRef.Value);
                        }
                        ShipmentServices1.UpdateAny();
                        //UpdateRevenueControl(Revenue1);
                    }
                    Session[REVENUE_ID] = Shipment1.Id;
                    return RedirectToAction("Index");
                }
                if ("Update".Equals(RevenueAction))
                {
                    ShipmentServices1.UpdateAny();
                    Session[REVENUE_ID] = Shipment1.Id;

                    if (Shipment1.RevenueStatus.Equals(ShipmentModel.RevenueStatusCollec.Pending.ToString()))
                    {
                        ShipmentServices1.UpdateRevenue(RevenueModel1);
                        Revenue1 = ShipmentServices1.getRevenueById(RevenueModel1.Id);
                        //(Revenue1);
                    }
                    return RedirectToAction("Revenue", new { id = id });
                }

                if (ModelState.IsValid)
                {
                    RevenueModel1.Tax = Convert.ToInt32(UsersServices1.getTaxCommissiong().DataValue);
                    Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Submited.ToString();
                    Shipment1.SubmitDate = DateTime.Now;
                    ShipmentServices1.UpdateRevenue(RevenueModel1);
                    ShipmentServices1.deleteSOAInvoice(RevenueModel1.Id);
                    ShipmentServices1.deleteSOAStatistic(RevenueModel1.Id);
                    RevenueModel1.InvAgentId1 = RevenueModel1.InvAgentId1 ?? 141;
                    Revenue1 = ShipmentServices1.getRevenueById(RevenueModel1.Id);
                    #region Accounting
                    if (RevenueModel1.InvAgentId1 != null && RevenueModel1.InvAgentId1.Value.Equals(RevenueModel1.InvAgentId2.Value))
                    {
                        SOAInvoice SOAInvoice1 = new SOAInvoice();
                        SOAInvoice1.AgentId = RevenueModel1.InvAgentId1.Value;
                        Agent Agent1 = agentService.GetById(SOAInvoice1.AgentId);
                        SOAInvoice1.AgentName = Agent1.AgentName;
                        SOAInvoice1.Amount1 = RevenueModel1.InvAmount1;
                        SOAInvoice1.TypeNote1 = RevenueModel1.InvType1;
                        SOAInvoice1.Currency1 = RevenueModel1.InvCurrency1;
                        SOAInvoice1.Amount2 = RevenueModel1.InvAmount2;
                        SOAInvoice1.TypeNote2 = RevenueModel1.InvType2;
                        SOAInvoice1.Currency2 = RevenueModel1.InvCurrency2;
                        SOAInvoice1.RevenueId = RevenueModel1.Id;
                        SOAInvoice1.ShipmentId = RevenueModel1.Id;
                        SOAInvoice1.DateUpdate = DateTime.Now;
                        SOAInvoice1.DateShp = Shipment1.DateShp;
                        ShipmentServices1.insertSOAInvoice(SOAInvoice1);
                    }
                    else
                    {

                        SOAInvoice SOAInvoice1 = new SOAInvoice();
                        SOAInvoice1.AgentId = RevenueModel1.InvAgentId1.Value;
                        Agent Agent1 = agentService.GetById(SOAInvoice1.AgentId);
                        SOAInvoice1.AgentName = Agent1.AgentName;
                        SOAInvoice1.Amount1 = RevenueModel1.InvAmount1;
                        SOAInvoice1.TypeNote1 = RevenueModel1.InvType1;
                        SOAInvoice1.Currency1 = RevenueModel1.InvCurrency1;
                        SOAInvoice1.Amount2 = 0;
                        SOAInvoice1.TypeNote2 = "";
                        SOAInvoice1.Currency2 = "";
                        SOAInvoice1.RevenueId = RevenueModel1.Id;
                        SOAInvoice1.ShipmentId = RevenueModel1.Id;
                        SOAInvoice1.DateUpdate = DateTime.Now;
                        SOAInvoice1.DateShp = Shipment1.DateShp;
                        ShipmentServices1.insertSOAInvoice(SOAInvoice1);

                        SOAInvoice SOAInvoice2 = new SOAInvoice();
                        SOAInvoice2.AgentId = RevenueModel1.InvAgentId2.Value;
                        Agent Agent2 = agentService.GetById(SOAInvoice2.AgentId);
                        SOAInvoice2.AgentName = Agent2.AgentName;
                        SOAInvoice2.Amount1 = 0;
                        SOAInvoice2.TypeNote1 = "";
                        SOAInvoice2.Currency1 = "";
                        SOAInvoice2.Amount2 = RevenueModel1.InvAmount2;
                        SOAInvoice2.TypeNote2 = RevenueModel1.InvType2;
                        SOAInvoice2.Currency2 = RevenueModel1.InvCurrency2;
                        SOAInvoice2.RevenueId = RevenueModel1.Id;
                        SOAInvoice2.ShipmentId = RevenueModel1.Id;
                        SOAInvoice2.DateUpdate = DateTime.Now;
                        SOAInvoice2.DateShp = Shipment1.DateShp;
                        ShipmentServices1.insertSOAInvoice(SOAInvoice2);
                    }
                    //SOA statistic
                    SOAStatistic SOAStatistic1 = new SOAStatistic();
                    SOAStatistic1.AgentId = RevenueModel1.InvAgentId1.Value;
                    SOAStatistic1.ShipmentId = RevenueModel1.Id;
                    SOAStatistic1.DateUpdate = DateTime.Now;
                    SOAStatistic1.DateShp = Shipment1.DateShp;
                    SOAStatistic1.TypeNote = RevenueModel1.InvType1;
                    SOAStatistic1.Currency = RevenueModel1.InvCurrency1;
                    SOAStatistic1.Amount = RevenueModel1.InvAmount1;
                    ShipmentServices1.insertSOAStatistic(SOAStatistic1);

                    SOAStatistic SOAStatistic2 = new SOAStatistic();
                    SOAStatistic2.AgentId = RevenueModel1.InvAgentId2.Value;
                    SOAStatistic2.ShipmentId = RevenueModel1.Id;
                    SOAStatistic2.DateUpdate = DateTime.Now;
                    SOAStatistic2.DateShp = Shipment1.DateShp;
                    SOAStatistic2.TypeNote = RevenueModel1.InvType2;
                    SOAStatistic2.Currency = RevenueModel1.InvCurrency2;
                    SOAStatistic2.Amount = RevenueModel1.InvAmount2;
                    ShipmentServices1.insertSOAStatistic(SOAStatistic2);
                    #endregion
                    //UpdateRevenueControl(Revenue1);
                }
                else
                {
                    GetDefaultData();
                    return View(RevenueModel1);
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
            }
            return RedirectToAction("Revenue", new { id = id });
        }
        public ActionResult CancelRevenue(long id)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            if (Shipment1 != null)
            {
                Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                ShipmentServices1.UpdateShipment();
            }
            return RedirectToAction("Revenue", new { id = id });
        }
        public ActionResult ApproveRevenue(long id)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
            if (Shipment1 != null)
            {
                Shipment1.RevenueStatus = ShipmentModel.RevenueStatusCollec.Approved.ToString();
                RevenueModel RevenueModel1 = ShipmentServices1.getRevenueModelById(id);
                RevenueModel1.ApproveId = User1.Id;
                ShipmentServices1.UpdateRevenue(RevenueModel1);
                //ShipmentServices1.UpdateShipment();
            }
            return RedirectToAction("Revenue", new { id = id });
        }
        public ActionResult RevenueDetail(long id)
        {
            return RevenueCommon(id);
        }

        [HttpPost]
        public ActionResult RevenueAccount(RevenueModel RevenueModel1, long id)
        {
            if (ModelState.IsValid)
            {
                Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
                Revenue Revenue1 = ShipmentServices1.getRevenueById(id);
                ShipmentServices1.deleteInvoiceByRev(id);
                Revenue1.AccountId = User1.Id;
                Revenue1.AccInv1 = RevenueModel1.AccInv1;
                Revenue1.AccInv2 = RevenueModel1.AccInv2;
                Revenue1.AccInv3 = RevenueModel1.AccInv3;
                Revenue1.AccInv4 = RevenueModel1.AccInv4;
                Revenue1.AccInvDate1 = null;
                Revenue1.AccInvDate2 = null;
                Revenue1.AccInvDate3 = null;
                Revenue1.AccInvDate4 = null;
                Revenue1.EXRemark = RevenueModel1.EXRemarkHidden;
                Revenue1.INRemark = RevenueModel1.INRemarkHidden;
                if (Shipment1 == null)
                {
                    return View("Revenue", RevenueModel1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate1) && !StringUtils.isNullOrEmpty(Revenue1.AccInv1))
                {
                    Revenue1.AccInvDate1 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate1);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv1.Trim();
                    Invoice1.Date = Revenue1.AccInvDate1;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate3) && !StringUtils.isNullOrEmpty(Revenue1.AccInv3))
                {
                    Revenue1.AccInvDate3 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate3);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv3.Trim();
                    Invoice1.Date = Revenue1.AccInvDate3;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate2) && !StringUtils.isNullOrEmpty(Revenue1.AccInv2))
                {
                    Revenue1.AccInvDate2 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate2);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv2.Trim();
                    Invoice1.Date = Revenue1.AccInvDate2;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate4) && !StringUtils.isNullOrEmpty(Revenue1.AccInv4))
                {
                    Revenue1.AccInvDate4 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate4);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv4.Trim();
                    Invoice1.Date = Revenue1.AccInvDate4;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                ShipmentServices1.UpdateAny();
            }
            else
            {
                GetDefaultData();
                return View("Revenue", RevenueModel1);
            }
            return RedirectToAction("Revenue", new { id = id });
        }

        [HttpPost]
        public ActionResult RevenueDetail(RevenueModel RevenueModel1, long id)
        {
            if (ModelState.IsValid)
            {
                Shipment Shipment1 = ShipmentServices1.GetShipmentById(id);
                viewDocument(Shipment1);
                Revenue Revenue1 = ShipmentServices1.getRevenueById(id);
                ShipmentServices1.deleteInvoiceByRev(id);
                Revenue1.AccountId = User1.Id;
                Revenue1.AccInv1 = RevenueModel1.AccInv1;
                Revenue1.AccInv2 = RevenueModel1.AccInv2;
                Revenue1.AccInv3 = RevenueModel1.AccInv3;
                Revenue1.AccInv4 = RevenueModel1.AccInv4;
                Revenue1.EXRemark = RevenueModel1.EXRemarkHidden;
                Revenue1.INRemark = RevenueModel1.INRemarkHidden;
                GetDefaultData();
                if (Shipment1 == null)
                {
                    return View("RevenueDetail", RevenueModel1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate1) && !StringUtils.isNullOrEmpty(Revenue1.AccInv1))
                {
                    Revenue1.AccInvDate1 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate1);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv1.Trim();
                    Invoice1.Date = Revenue1.AccInvDate1;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate3) && !StringUtils.isNullOrEmpty(Revenue1.AccInv3))
                {
                    Revenue1.AccInvDate3 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate3);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv3.Trim();
                    Invoice1.Date = Revenue1.AccInvDate3;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate2) && !StringUtils.isNullOrEmpty(Revenue1.AccInv2))
                {
                    Revenue1.AccInvDate2 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate2);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv2.Trim();
                    Invoice1.Date = Revenue1.AccInvDate2;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                if (!StringUtils.isNullOrEmpty(RevenueModel1.AccInvDate4) && !StringUtils.isNullOrEmpty(Revenue1.AccInv4))
                {
                    Revenue1.AccInvDate4 = DateUtils.Convert2DateTime(RevenueModel1.AccInvDate4);
                    Shipment1.IsInvoiced = true;
                    InvoideIssued Invoice1 = new InvoideIssued();
                    Invoice1.RevenueId = id;
                    Invoice1.ShipmentId = id;
                    Invoice1.InvoiceNo = Revenue1.AccInv4.Trim();
                    Invoice1.Date = Revenue1.AccInvDate4;
                    ShipmentServices1.insertInvoice(Invoice1);
                }
                ShipmentServices1.UpdateAny();
            }
            else
            {
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_RevenueDetailAcctView", RevenueModel1);
                }
                return View("RevenueDetail", RevenueModel1);
            }
            if (Request.IsAjaxRequest())
            {
                return Json(new { finish = true, message = string.Format("Update invoice successfully!!") },
                    JsonRequestBehavior.AllowGet);
            }
            return RedirectToAction("RevenueDetail", new { id = id });
        }
        #endregion
        #region Bill
        public ActionResult PrintArriveNotice(long ShipmentId, string typedoc = "SGN")
        {
            ViewData["Typedoc"] = typedoc;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ArriveNoticeModel ArriveNoticeModel1 = new ArriveNoticeModel();
            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {
                ArriveNoticeModel1 = new ArriveNoticeModel();
                ArriveNotice ArriveNotice1 = Shipment1.ArriveNotices.ElementAt(0);
                ArriveNoticeModel.ConvertArriveNotice(ArriveNotice1, ArriveNoticeModel1);
            }
            if (Shipment1.Revenue != null)
            {
                ViewData["Revenue"] = Shipment1.Revenue;
            }
            if (typedoc.Equals("HAN"))
            {
                return View("PrintArriveNoticeHAN", ArriveNoticeModel1);
            }
            return View(ArriveNoticeModel1);
        }
        public ActionResult DeliveryOrder(long ShipmentId, string typedoc = "SGN")
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Typedoc"] = typedoc;
            viewDocument(Shipment1);
            ArriveNoticeModel ArriveNoticeModel1 = new ArriveNoticeModel();
            ArriveNoticeModel1.CompanyName = Shipment1.Customer.FullName + "\r\n" + Shipment1.Customer.Address;
            ArriveNoticeModel1.DOAddress = ArriveNoticeModel1.CompanyName;
            ArriveNoticeModel1.BillNumber = Shipment1.HouseNum;
            ArriveNoticeModel1.ShiperName = Shipment1.CarrierAirLine.AbbName;
            if (ArriveNoticeModel1.DOAddress == null || ArriveNoticeModel1.DOAddress.Trim().Equals(""))
            {
                ArriveNoticeModel1.DOAddress = ShipmentServices1.getLastArriveNotice().DOAddress;
            }
            ArriveNoticeModel1.DOCompanyAddress = ShipmentServices1.getLastArriveNotice().DOCompanyAddress;
            ArriveNoticeModel1.ShipmentId = ShipmentId;
            ArriveNoticeModel1.DOENTitle = "(DELIVERY ORDER)";
            ArriveNoticeModel1.DOVNTitle = "LỆNH GIAO HÀNG";
            ArriveNoticeModel1.ToVN = "THƯƠNG VỤ - KHO VẬN CẢNG";
            ArriveNoticeModel1.ToEN = "PORT AUTHORITY";

            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {

                ArriveNotice ArriveNotice1 = Shipment1.ArriveNotices.ElementAt(0);
                ArriveNoticeModel.ConvertArriveNotice(ArriveNotice1, ArriveNoticeModel1);
                if (ArriveNoticeModel1.DOENTitle == null || ArriveNoticeModel1.DOENTitle.Trim() == "")
                    ArriveNoticeModel1.DOENTitle = "(DELIVERY ORDER)";
                if (ArriveNoticeModel1.DOVNTitle == null || ArriveNoticeModel1.DOVNTitle.Trim() == "")
                    ArriveNoticeModel1.DOVNTitle = "LỆNH GIAO HÀNG";
                if (ArriveNoticeModel1.ToVN == null || ArriveNoticeModel1.ToVN.Trim() == "")
                    ArriveNoticeModel1.ToVN = "THƯƠNG VỤ - KHO VẬN CẢNG";
                if (ArriveNoticeModel1.ToEN == null || ArriveNoticeModel1.ToEN.Trim() == "")
                    ArriveNoticeModel1.ToEN = "PORT AUTHORITY";
                if (ArriveNoticeModel1.DOAddress == null || ArriveNoticeModel1.DOAddress.Trim().Equals(""))
                {
                    ArriveNoticeModel1.DOAddress = ArriveNoticeModel1.CompanyName;
                }
                if (ArriveNoticeModel1.DOCompanyAddress == null || ArriveNoticeModel1.DOCompanyAddress.Trim().Equals(""))
                {
                    ArriveNoticeModel1.DOCompanyAddress = ArriveNoticeModel1.CompanyAddress;
                }

            }
            if (typedoc.Equals("HAN"))
            {
                return View("DeliveryOrderHAN", ArriveNoticeModel1);
            }
            return View(ArriveNoticeModel1);
        }
        [HttpPost]
        public ActionResult DeliveryOrder(ArriveNoticeModel ArriveNoticeModel1, long ShipmentId, string typedoc = "SGN")
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Typedoc"] = typedoc;
            viewDocument(Shipment1);
            ArriveNoticeModel1.OrderDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {
                ArriveNotice ArriveNotice1 = Shipment1.ArriveNotices.ElementAt(0);
                ArriveNotice1 = ShipmentServices1.getArriveNotice(ArriveNotice1.Id);
                ArriveNotice1.DeliveryDate = ArriveNoticeModel1.DeliveryDate;
                ArriveNotice1.DOAddress = ArriveNoticeModel1.DOAddress;
                ArriveNotice1.DOCompanyAddress = ArriveNoticeModel1.DOCompanyAddress;
                ArriveNotice1.GoodsDescription = ArriveNoticeModel1.GoodsDescription;
                ArriveNotice1.GrossWeight = ArriveNoticeModel1.GrossWeight;
                ArriveNotice1.ShippingMark = ArriveNoticeModel1.ShippingMark;
                ArriveNotice1.NoCTNS = ArriveNoticeModel1.NoCTNS;
                ArriveNotice1.CBM = ArriveNoticeModel1.CBM;
                ArriveNotice1.DOENTitle = ArriveNoticeModel1.DOENTitle;
                ArriveNotice1.DOVNTitle = ArriveNoticeModel1.DOVNTitle;
                ArriveNotice1.ToEN = ArriveNoticeModel1.ToEN;
                ArriveNotice1.ToVN = ArriveNoticeModel1.ToVN;
                ArriveNotice1.DOLogo = ArriveNoticeModel1.DOLogo;
                ArriveNotice1.DOHeader = ArriveNoticeModel1.DOHeader;
                ArriveNotice1.DOFooter = ArriveNoticeModel1.DOFooter;
                ArriveNotice1.AddressOfSign = ArriveNoticeModel1.AddressOfSign;
                ArriveNotice1.CompanyAddress = ArriveNoticeModel1.CompanyAddress;
                ShipmentServices1.updateArriveNotice();
                ArriveNotice1 = ShipmentServices1.getArriveNotice(ArriveNotice1.Id);


            }
            else
            {
                ArriveNotice ArriveNotice1 = new ArriveNotice();
                ArriveNotice1.ShipmentId = Shipment1.Id;
                ArriveNotice1.DeliveryDate = ArriveNoticeModel1.DeliveryDate;
                ArriveNotice1.DOAddress = ArriveNoticeModel1.DOAddress;
                ArriveNotice1.DOCompanyAddress = ArriveNoticeModel1.DOCompanyAddress;
                ArriveNotice1.GoodsDescription = ArriveNoticeModel1.GoodsDescription;
                ArriveNotice1.GrossWeight = ArriveNoticeModel1.GrossWeight;
                ArriveNotice1.ShippingMark = ArriveNoticeModel1.ShippingMark;
                ArriveNotice1.NoCTNS = ArriveNoticeModel1.NoCTNS;
                ArriveNotice1.CBM = ArriveNoticeModel1.CBM;
                ArriveNotice1.DOENTitle = ArriveNoticeModel1.DOENTitle;
                ArriveNotice1.DOVNTitle = ArriveNoticeModel1.DOVNTitle;
                ArriveNotice1.ToEN = ArriveNoticeModel1.ToEN;
                ArriveNotice1.ToVN = ArriveNoticeModel1.ToVN;
                ArriveNotice1.DOLogo = ArriveNoticeModel1.DOLogo;
                ArriveNotice1.DOHeader = ArriveNoticeModel1.DOHeader;
                ArriveNotice1.DOFooter = ArriveNoticeModel1.DOFooter;
                ArriveNotice1.AddressOfSign = ArriveNoticeModel1.AddressOfSign;
                ArriveNotice1.CompanyAddress = ArriveNoticeModel1.CompanyAddress;
                //ArriveNoticeModel.ConvertArriveNotice(ArriveNoticeModel1, ArriveNotice1);
                ShipmentServices1.insertArriveNotice(ArriveNotice1);

            }
            if (typedoc.Equals("HAN"))
            {
                return View("DeliveryOrderHAN", ArriveNoticeModel1);
            }
            return View(ArriveNoticeModel1);
        }

        public ActionResult PrintDeliveryOrder(long ShipmentId, string typedoc = "SGN")
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ArriveNoticeModel ArriveNoticeModel1 = ArriveNoticeModel1 = new ArriveNoticeModel();
            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {

                ArriveNotice ArriveNotice1 = Shipment1.ArriveNotices.ElementAt(0);
                ArriveNoticeModel.ConvertArriveNotice(ArriveNotice1, ArriveNoticeModel1);
            }
            if (typedoc.Equals("HAN"))
            {
                return View("PrintDeliveryOrderHAN", ArriveNoticeModel1);
            }
            return View(ArriveNoticeModel1);
        }
        public ActionResult ArriveNotice(long ShipmentId, long BillDetailId, string typedoc = "SGN")
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Typedoc"] = typedoc;
            viewDocument(Shipment1);
            ArriveNoticeModel ArriveNoticeModel1 = new ArriveNoticeModel();
            ArriveNoticeModel1.CompanyName = Shipment1.Customer.FullName + "\r\n" + Shipment1.Customer.Address;
            ArriveNoticeModel1.DOAddress = ArriveNoticeModel1.CompanyName;
            ArriveNoticeModel1.BillNumber = Shipment1.HouseNum;
            ArriveNoticeModel1.ShiperName = Shipment1.CarrierAirLine.AbbName;
            ArriveNoticeModel1.CompanyAddress = ShipmentServices1.getLastArriveNotice().CompanyAddress;
            ArriveNoticeModel1.DOCompanyAddress = ShipmentServices1.getLastArriveNotice().DOCompanyAddress;
            ArriveNoticeModel1.ShipmentId = ShipmentId;
            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {
                ArriveNotice ArriveNotice1 = Shipment1.ArriveNotices.ElementAt(0);
                ArriveNoticeModel.ConvertArriveNotice(ArriveNotice1, ArriveNoticeModel1);
                if (ArriveNoticeModel1.CompanyName == null || ArriveNoticeModel1.CompanyName.Trim().Equals(""))
                {
                    ArriveNoticeModel1.CompanyName = ArriveNoticeModel1.DOAddress;
                }
                if (ArriveNoticeModel1.CompanyAddress == null || ArriveNoticeModel1.CompanyAddress.Trim().Equals(""))
                {
                    ArriveNoticeModel1.CompanyAddress = ArriveNoticeModel1.DOCompanyAddress;
                }
            }
            if (Shipment1.Revenue != null)
            {
                ViewData["Revenue"] = Shipment1.Revenue;
            }
            return View(ArriveNoticeModel1);
        }
        [HttpPost]
        public ActionResult ArriveNotice(ArriveNoticeModel ArriveNoticeModel1, long ShipmentId, long BillDetailId, string typedoc = "SGN")
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Typedoc"] = typedoc;
            viewDocument(Shipment1);
            ArriveNoticeModel1.OrderDate = DateTime.Now.ToString("dd/MM/yyyy");
            if (Shipment1.ArriveNotices != null && Shipment1.ArriveNotices.Count > 0)
            {
                ArriveNotice ArriveNotice1 = Shipment1.ArriveNotices.ElementAt(0);
                ArriveNotice1 = ShipmentServices1.getArriveNotice(ArriveNotice1.Id);
                ArriveNoticeModel.ConvertArriveNotice(ArriveNoticeModel1, ArriveNotice1);

                ShipmentServices1.updateArriveNotice();
                ArriveNotice1 = ShipmentServices1.getArriveNotice(ArriveNotice1.Id);
            }
            else
            {
                ArriveNotice ArriveNotice1 = new ArriveNotice();
                ArriveNoticeModel.ConvertArriveNotice(ArriveNoticeModel1, ArriveNotice1);
                ShipmentServices1.insertArriveNotice(ArriveNotice1);

            }
            return View(ArriveNoticeModel1);
        }
        public ActionResult BookingNote(long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BookingNoteModel BookingNoteModel1 = new BookingNoteModel();
            BookingNoteModel1.ShipmentId = ShipmentId;
            // ArriveNoticeModel1.CompanyName = Shipment1.Customer.FullName + "\r\n" + Shipment1.Customer.Address;
            BookingNoteModel1.ShipperName = Shipment1.Customer1.FullName + "\r\n" + Shipment1.Customer1.Address;
            BookingNoteModel1.Consignee = Shipment1.Customer.FullName;
            if (Shipment1.BookingNotes != null && Shipment1.BookingNotes.Count > 0)
            {
                BookingNote BookingNote1 = Shipment1.BookingNotes.ElementAt(0);
                BookingNoteModel.ConvertBookingNote(BookingNote1, BookingNoteModel1);

            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BookingNoteModel1);
        }
        [HttpPost]
        public ActionResult BookingNote(BookingNoteModel BookingNoteModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            if (Shipment1.BookingNotes != null && Shipment1.BookingNotes.Count > 0)
            {
                BookingNote BookingNote1 = Shipment1.BookingNotes.ElementAt(0);
                BookingNote1 = ShipmentServices1.getBookingNote(BookingNote1.Id);
                BookingNoteModel.ConvertBookingNote(BookingNoteModel1, BookingNote1);

                ShipmentServices1.updateArriveNotice();
                BookingNote1 = ShipmentServices1.getBookingNote(BookingNote1.Id);
            }
            else
            {
                BookingNote BookingNote1 = new BookingNote();
                BookingNoteModel.ConvertBookingNote(BookingNoteModel1, BookingNote1);
                ShipmentServices1.insertBookingNote(BookingNote1);
            }
            return RedirectToAction("BookingNote", new { ShipmentId = ShipmentId, BillDetailId = 0 });
        }
        public ActionResult PrintBookingNote(long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BookingNoteModel BookingNoteModel1 = new BookingNoteModel();
            BookingNoteModel1.ShipmentId = ShipmentId;
            if (Shipment1.BookingNotes != null && Shipment1.BookingNotes.Count > 0)
            {
                BookingNote BookingNote1 = Shipment1.BookingNotes.ElementAt(0);
                BookingNoteModel.ConvertBookingNote(BookingNote1, BookingNoteModel1);
            }
            return View(BookingNoteModel1);
        }
        public ActionResult BillLanding(long ShipmentId)
        {
            BillLandingModel BillLandingModel1 = new BillLandingModel();
            BillLandingModel1.ShipmentId = ShipmentId;
            BillLandingModel1.BillLandingNo = ShipmentId.ToString();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BillLandingModel1.PhipperName = Shipment1.Customer1.FullName + "\r\n" + Shipment1.Customer1.Address;
            BillLandingModel1.ConsignedOrder = Shipment1.Customer.FullName + "\r\n" + Shipment1.Customer.Address;
            BillLandingModel1.ForRelease = Shipment1.Agent.AgentName + "\r\n" + Shipment1.Agent.Address;
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                BillLanding BillLanding1 = Shipment1.BillLandings.ElementAt(0);
                BillLandingModel.ConvertBillLanding(BillLanding1, BillLandingModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BillLandingModel1);
        }
        [HttpPost]
        public ActionResult BillLanding(BillLandingModel BillLandingModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                BillLanding BillLanding1 = Shipment1.BillLandings.ElementAt(0);
                BillLanding1 = ShipmentServices1.getBillLanding(BillLanding1.Id);
                BillLandingModel.ConvertBillLanding(BillLandingModel1, BillLanding1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                BillLanding BillLanding1 = new BillLanding();
                BillLandingModel.ConvertBillLanding(BillLandingModel1, BillLanding1);
                ShipmentServices1.insertBillLanding(BillLanding1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BillLandingModel1);
        }
        public ActionResult PrintBillLanding(long ShipmentId)
        {
            BillLandingModel BillLandingModel1 = new BillLandingModel();
            BillLandingModel1.ShipmentId = ShipmentId;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                BillLanding BillLanding1 = Shipment1.BillLandings.ElementAt(0);
                BillLandingModel.ConvertBillLanding(BillLanding1, BillLandingModel1);
            }
            return View(BillLandingModel1);
        }
        public ActionResult DebitNote(long id, long ShipmentId)
        {
            DebitNoteModel DebitNoteModel1 = new DebitNoteModel();
            DebitNoteModel1.ShipmentId = ShipmentId;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            DebitNoteModel1.CompanyTo = Shipment1.Agent.AgentName + "\r\n" + Shipment1.Agent.Address;
            DebitNoteModel1.Origin = Shipment1.Area.AreaAddress + "," + Shipment1.Area.Country.CountryName;
            DebitNoteModel1.Destination = Shipment1.Area1.AreaAddress + "," + Shipment1.Area1.Country.CountryName;
            DebitNoteModel1.HAWB_HBL = Shipment1.HouseNum + "/" + Shipment1.MasterNum;
            DebitNoteModel1.Weight = Shipment1.QtyNumber + "X" + Shipment1.QtyUnit;
            DebitNoteModel1.CompanyFrom = UsersServices1.getComSetting(CompanyInfo.ACCOUNT_INFO);
            if (Shipment1.DebitNotes != null && Shipment1.DebitNotes.Count > 0)
            {
                DebitNote DebitNote1 = Shipment1.DebitNotes.ElementAt(0);
                DebitNoteModel.ConvertDebitNote(DebitNote1, DebitNoteModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(DebitNoteModel1);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DebitNote(long id, long ShipmentId, DebitNoteModel DebitNoteModel1)
        {
            DebitNoteModel1.ShipmentId = ShipmentId;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.DebitNotes != null && Shipment1.DebitNotes.Count > 0)
            {
                DebitNote DebitNote1 = Shipment1.DebitNotes.ElementAt(0);
                DebitNote1 = ShipmentServices1.getDebitNote(DebitNote1.Id);
                DebitNoteModel.ConvertDebitNote(DebitNoteModel1, DebitNote1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                DebitNote DebitNote1 = new DebitNote();
                DebitNoteModel.ConvertDebitNote(DebitNoteModel1, DebitNote1);
                ShipmentServices1.insertDebitNote(DebitNote1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(DebitNoteModel1);
        }

        public ActionResult PrintDebitNote(long id, long ShipmentId)
        {
            DebitNoteModel DebitNoteModel1 = new DebitNoteModel();
            DebitNoteModel1.ShipmentId = ShipmentId;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.DebitNotes != null && Shipment1.DebitNotes.Count > 0)
            {
                DebitNote DebitNote1 = Shipment1.DebitNotes.ElementAt(0);
                DebitNoteModel.ConvertDebitNote(DebitNote1, DebitNoteModel1);
            }
            return View(DebitNoteModel1);
        }

        public ActionResult BLDetail(long ShipmentId)
        {
            BillLandingModel BillLandingModel1 = new BillLandingModel();
            BillLandingModel1.ShipmentId = ShipmentId;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                BillLanding BillLanding1 = Shipment1.BillLandings.ElementAt(0);
                BillLandingModel.ConvertBillLanding(BillLanding1, BillLandingModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BillLandingModel1);
        }
        [HttpPost]
        public ActionResult BLDetail(BillLandingModel BillLandingModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                BillLanding BillLanding1 = Shipment1.BillLandings.ElementAt(0);
                BillLanding1 = ShipmentServices1.getBillLanding(BillLanding1.Id);
                BillLanding1.BillTo = BillLandingModel1.BillTo;
                BillLanding1.BKG = BillLandingModel1.BKG;

                BillLanding1.BillFrom = BillLandingModel1.BillFrom;
                BillLanding1.BLComAddress = BillLandingModel1.BLComAddress;
                ShipmentServices1.UpdateAny();
                BillLandingModel.ConvertBillLanding(BillLanding1, BillLandingModel1);
            }
            else
            {
                BillLanding BillLanding1 = new BillLanding();
                BillLandingModel.ConvertBillLanding(BillLandingModel1, BillLanding1);
                ShipmentServices1.insertBillLanding(BillLanding1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BillLandingModel1);
        }
        public ActionResult PrintBLDetail(long ShipmentId)
        {
            BillLandingModel BillLandingModel1 = new BillLandingModel();
            BillLandingModel1.ShipmentId = ShipmentId;
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BillLandings != null && Shipment1.BillLandings.Count > 0)
            {
                BillLanding BillLanding1 = Shipment1.BillLandings.ElementAt(0);
                BillLandingModel.ConvertBillLanding(BillLanding1, BillLandingModel1);
            }
            return View(BillLandingModel1);
        }
        public ActionResult UnIssuedInvoice()
        {

            _gridUInvoice = (Grid<Shipment>)Session[FINDINVOICE_MODEL];
            findInvoiceModel = (FindInvoice)Session[FINDINVOICE_SEARCH_MODEL];
            findInvoiceModel = findInvoiceModel ?? new FindInvoice()
            {
                DateTo = DateTime.Now.ToString("dd/MM/yyyy"),
                DateFrom = DateTime.Now.AddMonths(-2).ToString("dd/MM/yyyy"),
                ShipmentPriod = 1
            };

            findInvoiceModel.UnIssueInvoice = true;
            if (_gridUInvoice == null)
            {
                _gridUInvoice = new Grid<Shipment>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 20,
                                    Sord = "desc",
                                    Sidx = "DateShp"
                                }


                            );
            }
            UpdateGirdUnInvoce(findInvoiceModel);
            return View(_gridUInvoice);
        }

        public const string FINDINVOICE_MODEL = "FINDINVOICE_MODEL";
        public const string FINDUNINVOICE_MODEL = "FINDUNINVOICE_MODEL";
        public const string FINDINVOICE_SEARCH_MODEL = "FINDINVOICE_SEARCH_MODEL";
        private Grid<InvoideIssued> _gridInvoice;
        private Grid<Shipment> _gridUInvoice;
        FindInvoice findInvoiceModel = null;
        public ActionResult FindInvoice()
        {

            _gridInvoice = (Grid<InvoideIssued>)Session[FINDINVOICE_MODEL];
            findInvoiceModel = (FindInvoice)Session[FINDINVOICE_SEARCH_MODEL];
            findInvoiceModel = findInvoiceModel ?? new FindInvoice()
            {
                DateTo = DateTime.Now.ToString("dd/MM/yyyy"),
                DateFrom = DateTime.Now.AddMonths(-2).ToString("dd/MM/yyyy"),
                ShipmentPriod = 1
            };

            ViewData["FindInvoice"] = findInvoiceModel;
            if (_gridInvoice == null)
            {
                _gridInvoice = new Grid<InvoideIssued>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 20,
                                    Sord = "desc",
                                    Sidx = "Date"
                                }


                            );
            }
            UpdateGridInvoiceIssued(findInvoiceModel);
            return View(_gridInvoice);
        }
        private void UpdateGridInvoiceIssued(FindInvoice findInvoice)
        {
            var totalRow = 0;
            Pager page = _gridInvoice.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "Date" : page.Sidx, page.Sord == "Desc");
            var list = ShipmentServices1.searchInvoice(findInvoice);
            list = list.OrderBy(sort);
            totalRow = list.Count();
            _gridInvoice.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _gridInvoice.Data = new List<InvoideIssued>();
                return;
            }
            int skip = (page.CurrentPage - 1) * page.PageSize;
            if (skip > totalRow)
                skip = 0;
            int take = page.PageSize;
            var listView = list.Skip(skip).Take(take).ToList();
            _gridInvoice.Data = listView;
        }

        private void UpdateGirdUnInvoce(FindInvoice findInvoice)
        {
            var totalRow = 0;
            Pager page = _gridUInvoice.Pager;
            findInvoice.ShipmentPriod = 1;
            //var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "DateShp" : page.Sidx, page.Sord == "Desc");
            var list = ShipmentServices1.getAllUnIssuedInvoice(findInvoice);
            // list = list.OrderBy(sort);
            totalRow = list.Count();
            _gridUInvoice.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _gridUInvoice.Data = new List<Shipment>();
                return;
            }
            int skip = (page.CurrentPage - 1) * page.PageSize;
            if (skip > totalRow)
                skip = 0;
            int take = page.PageSize;
            var listView = list.Skip(skip).Take(take).ToList();
            _gridUInvoice.Data = listView;
        }
        [HttpPost]
        public ActionResult FindInvoice(Grid<InvoideIssued> grid, Grid<Shipment> girdUninvoice, FindInvoice FindInvoice1)
        {
            ViewData["FindInvoice"] = FindInvoice1;

            if (FindInvoice1.UnIssueInvoice)
            {
                Session[FINDUNINVOICE_MODEL] = girdUninvoice;
                _gridUInvoice = girdUninvoice;
                Session[FINDINVOICE_SEARCH_MODEL] = FindInvoice1;
                _gridUInvoice.ProcessAction();
                UpdateGirdUnInvoce(FindInvoice1);
                return PartialView("_UnIssuedInvoice", _gridUInvoice);
            }
            else
            {
                _gridInvoice = grid;
                Session[FINDINVOICE_MODEL] = grid;
                Session[FINDINVOICE_SEARCH_MODEL] = FindInvoice1;
                _gridInvoice.ProcessAction();
                UpdateGridInvoiceIssued(FindInvoice1);
                return PartialView("_InvoiceList", _gridInvoice);
            }

        }

        public ActionResult ViewSOA()
        {
            agents = agents ?? agentService.GetAll(a => a.IsActive).OrderBy(x => x.AbbName);
            ViewData["AgentsList"] = new SelectList(agents, "Id", "AbbName");
            return View();
        }
        [HttpPost]
        public ActionResult ViewSOA(SOAModel Model1)
        {
            agents = agents ?? agentService.GetAll(a => a.IsActive).OrderBy(x => x.AbbName);
            ViewData["AgentsList"] = new SelectList(agents, "Id", "AbbName");
            if (Model1.AgentId <= 0)
            {
                return View(Model1);
            }
            ViewData["AgentsList"] = new SelectList(ShipmentServices1.GetAgentBySOA(Model1), "Id", "AbbName");

            double USDAmount = 0, GBPAmount = 0, EURAmount = 0, AUDAmount = 0;
            IEnumerable<SOAModel> AgentSOA = ShipmentServices1.getAgentSOA(Model1);
            foreach (SOAModel SOAModel1 in AgentSOA)
            {
                if (RevenueModel.CurrencyTypes.USD.ToString().Equals(SOAModel1.Currency)
                    && (RevenueModel.InvTypes.VNCredit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentCredit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    USDAmount = USDAmount + SOAModel1.Amount;
                }
                else
                    if (RevenueModel.CurrencyTypes.USD.ToString().Equals(SOAModel1.Currency)
                    && (RevenueModel.InvTypes.VNDebit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentDebit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    USDAmount = USDAmount - SOAModel1.Amount;
                }
                else
                        //--------------------------------------
                        if (RevenueModel.CurrencyTypes.EUR.ToString().Equals(SOAModel1.Currency)
                        && (RevenueModel.InvTypes.VNCredit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentCredit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    EURAmount = EURAmount + SOAModel1.Amount;
                }
                else
                            if (RevenueModel.CurrencyTypes.EUR.ToString().Equals(SOAModel1.Currency)
                            && (RevenueModel.InvTypes.VNDebit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentDebit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    EURAmount = EURAmount - SOAModel1.Amount;
                }
                else
                                //--------------------------------------
                                if (RevenueModel.CurrencyTypes.GBP.ToString().Equals(SOAModel1.Currency)
                                && (RevenueModel.InvTypes.VNCredit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentCredit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    GBPAmount = GBPAmount + SOAModel1.Amount;
                }
                else
                                    if (RevenueModel.CurrencyTypes.GBP.ToString().Equals(SOAModel1.Currency)
                                    && (RevenueModel.InvTypes.VNDebit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentDebit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    GBPAmount = GBPAmount - SOAModel1.Amount;
                }
                else
                                        //--------------------------------------------
                                        if (RevenueModel.CurrencyTypes.AUD.ToString().Equals(SOAModel1.Currency)
                                        && (RevenueModel.InvTypes.VNCredit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentCredit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    AUDAmount = AUDAmount + SOAModel1.Amount;
                }
                else
                                            if (RevenueModel.CurrencyTypes.AUD.ToString().Equals(SOAModel1.Currency)
                                            && (RevenueModel.InvTypes.VNDebit.ToString().Equals(SOAModel1.TypeNote) || RevenueModel.InvTypes.AgentDebit.ToString().Equals(SOAModel1.TypeNote)))
                {
                    AUDAmount = AUDAmount - SOAModel1.Amount;
                }

            }
            ViewData["USDAmount"] = USDAmount;
            ViewData["EURAmount"] = EURAmount;
            ViewData["GBPAmount"] = GBPAmount;
            ViewData["AUDAmount"] = AUDAmount;

            if (StringUtils.isNullOrEmpty(Model1.DateFrom))
            {
                Model1.DateFrom = "";
            }

            if (StringUtils.isNullOrEmpty(Model1.DateTo))
            {
                Model1.DateTo = "";
            }

            IEnumerable<SOAInvoice> ListSOA = ShipmentServices1.getSOAInvoice(Model1);
            int status = 2;
            var soaInvoices = ListSOA.ToList();
            if (soaInvoices.Any())
            {
                if (soaInvoices.All(x => x.IsPayment))
                {
                    status = 1;
                }
                else if (soaInvoices.All(x => !x.IsPayment))
                {
                    status = 0;
                }
            }
            ViewData["StatusSOA"] = status;
            ViewData["ListSOA"] = soaInvoices;
            return View(Model1);
        }

        public ActionResult BookingConfirm(long Id, long ShipmentId)
        {
            BookingConfirmModel BookingConfirmModel1 = new BookingConfirmModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BookingConfirmModel1.ShipmentId = ShipmentId;
            if (Shipment1.BookingConfirms != null && Shipment1.BookingConfirms.Count > 0)
            {
                BookingConfirm Booking1 = Shipment1.BookingConfirms.ElementAt(0);
                BookingConfirmModel.ConvertBookingConfirm(Booking1, BookingConfirmModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;

            viewDocument(Shipment1);
            return View(BookingConfirmModel1);
        }
        public ActionResult PrintBookingConfirm(long Id, long ShipmentId)
        {
            BookingConfirmModel BookingConfirmModel1 = new BookingConfirmModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BookingConfirmModel1.ShipmentId = ShipmentId;
            if (Shipment1.BookingConfirms != null && Shipment1.BookingConfirms.Count > 0)
            {
                BookingConfirm Booking1 = Shipment1.BookingConfirms.ElementAt(0);
                BookingConfirmModel.ConvertBookingConfirm(Booking1, BookingConfirmModel1);
            }
            return View(BookingConfirmModel1);
        }
        [HttpPost]
        public ActionResult BookingConfirm(BookingConfirmModel BookingConfirmModel1, long Id, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BookingConfirms != null && Shipment1.BookingConfirms.Count > 0)
            {
                BookingConfirm BookingConfirm1 = Shipment1.BookingConfirms.ElementAt(0);
                BookingConfirm1 = ShipmentServices1.getBookingConfirm(BookingConfirm1.Id);
                BookingConfirmModel.ConvertBookingConfirm(BookingConfirmModel1, BookingConfirm1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                BookingConfirm BookingConfirm1 = new Models.BookingConfirm();
                BookingConfirmModel.ConvertBookingConfirm(BookingConfirmModel1, BookingConfirm1);
                ShipmentServices1.insertBookingConfirm(BookingConfirm1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BookingConfirmModel1);
        }
        //-----------------------------
        public ActionResult RequestPayment(long Id, long ShipmentId)
        {
            RequestPaymentModel RequestPaymentModel1 = new RequestPaymentModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            RequestPaymentModel1.ShipmentId = ShipmentId;
            RequestPaymentModel1.DepartmentName = User1.Department.DeptName;
            RequestPaymentModel1.BillNumber = Shipment1.HouseNum;
            RequestPaymentModel1.ShipmentCode = Shipment1.MasterNum;
            RequestPaymentModel1.CustomerName = Shipment1.Customer.FullName;
            RequestPaymentModel1.CarrierName = Shipment1.CarrierAirLine.CarrierAirLineName;

            if (Shipment1.RequestPayments != null && Shipment1.RequestPayments.Count > 0)
            {
                RequestPayment Request1 = Shipment1.RequestPayments.ElementAt(0);
                RequestPaymentModel.ConvertRequestPayment(Request1, RequestPaymentModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(RequestPaymentModel1);
        }
        #endregion
        #region PaymentPrint
        public ActionResult PrintRequestPayment(long Id, long ShipmentId)
        {
            RequestPaymentModel RequestPaymentModel1 = new RequestPaymentModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            RequestPaymentModel1.ShipmentId = ShipmentId;
            RequestPaymentModel1.DepartmentName = User1.Department.DeptName;
            RequestPaymentModel1.BillNumber = Shipment1.HouseNum;
            RequestPaymentModel1.CustomerName = Shipment1.Customer.FullName;
            RequestPaymentModel1.CarrierName = Shipment1.CarrierAirLine.CarrierAirLineName;

            if (Shipment1.RequestPayments != null && Shipment1.RequestPayments.Count > 0)
            {
                RequestPayment Request1 = Shipment1.RequestPayments.ElementAt(0);
                RequestPaymentModel.ConvertRequestPayment(Request1, RequestPaymentModel1);
            }
            return View(RequestPaymentModel1);
        }
        [HttpPost]
        public ActionResult RequestPayment(RequestPaymentModel RequestPaymentModel1, long Id, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.RequestPayments != null && Shipment1.RequestPayments.Count > 0)
            {
                RequestPayment RequestPayment1 = Shipment1.RequestPayments.ElementAt(0);
                RequestPayment1 = ShipmentServices1.getRequestPayment(RequestPayment1.Id);
                RequestPaymentModel.ConvertRequestPayment(RequestPaymentModel1, RequestPayment1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                RequestPayment RequestPayment1 = new Models.RequestPayment();
                RequestPaymentModel.ConvertRequestPayment(RequestPaymentModel1, RequestPayment1);
                ShipmentServices1.insertRequestPayment(RequestPayment1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(RequestPaymentModel1);
        }
        //-----------------------------
        public ActionResult AuthorLetter(long Id, long ShipmentId)
        {
            AuthorLetter LastAuthorLetter = ShipmentServices1.getLastAuthorLetter();
            AuthorLetterModel AuthorLetterModel1 = new AuthorLetterModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            AuthorLetterModel1.ShipmentId = ShipmentId;
            AuthorLetterModel1.BenB = Shipment1.Customer.FullName + "<br />" + Shipment1.Customer.Address;
            AuthorLetterModel1.MAWBNo = Shipment1.MasterNum;
            AuthorLetterModel1.HAWNNo = Shipment1.HouseNum;
            AuthorLetterModel1.Flight = Shipment1.CarrierAirLine.CarrierAirLineName;
            AuthorLetterModel1.CompanyAddress = LastAuthorLetter.CompanyAddress;
            AuthorLetterModel1.DearTo = LastAuthorLetter.DearTo;
            AuthorLetterModel1.BenA = LastAuthorLetter.BenA;
            if (Shipment1.AuthorLetters != null && Shipment1.AuthorLetters.Count > 0)
            {
                AuthorLetter Author1 = Shipment1.AuthorLetters.ElementAt(0);
                AuthorLetterModel.convertAuthorLetter(Author1, AuthorLetterModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(AuthorLetterModel1);
        }
        public ActionResult PrintAuthorLetter(long Id, long ShipmentId)
        {
            AuthorLetterModel AuthorLetterModel1 = new AuthorLetterModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            AuthorLetterModel1.ShipmentId = ShipmentId;
            if (Shipment1.AuthorLetters != null && Shipment1.AuthorLetters.Count > 0)
            {
                AuthorLetter Author1 = Shipment1.AuthorLetters.ElementAt(0);
                AuthorLetterModel.convertAuthorLetter(Author1, AuthorLetterModel1);
            }
            return View(AuthorLetterModel1);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AuthorLetter(AuthorLetterModel AuthorLetterModel1, long Id, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.AuthorLetters != null && Shipment1.AuthorLetters.Count > 0)
            {
                AuthorLetter AuthorLetter1 = Shipment1.AuthorLetters.ElementAt(0);
                AuthorLetter1 = ShipmentServices1.getAuthorLetter(AuthorLetter1.Id);
                AuthorLetterModel.convertAuthorLetter(AuthorLetterModel1, AuthorLetter1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                AuthorLetter AuthorLetter1 = new Models.AuthorLetter();
                AuthorLetterModel.convertAuthorLetter(AuthorLetterModel1, AuthorLetter1);
                ShipmentServices1.insertAuthorLetter(AuthorLetter1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(AuthorLetterModel1);
        }
        public ActionResult PaymentVoucher(long ShipmentId)
        {
            PaymentVocherModel PaymentVocherModel1 = new PaymentVocherModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            PaymentVocherModel1.ShipmentId = ShipmentId;

            if (Shipment1.PaymentVouchers != null && Shipment1.PaymentVouchers.Count > 0)
            {
                PaymentVoucher PaymentVoucher1 = Shipment1.PaymentVouchers.ElementAt(0);
                PaymentVocherModel.convertPaymentVoucher(PaymentVoucher1, PaymentVocherModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Shipment"] = Shipment1;
            viewDocument(Shipment1);
            return View(PaymentVocherModel1);
        }

        [HttpPost]
        public ActionResult PaymentVoucher(PaymentVocherModel PaymentVocherModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.PaymentVouchers != null && Shipment1.PaymentVouchers.Count > 0)
            {
                PaymentVoucher PaymentVoucher1 = Shipment1.PaymentVouchers.ElementAt(0);
                PaymentVoucher1 = ShipmentServices1.getPaymentVoucher(PaymentVoucher1.Id);
                PaymentVocherModel.convertPaymentVoucher(PaymentVocherModel1, PaymentVoucher1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                PaymentVoucher PaymentVoucher1 = new Models.PaymentVoucher();
                PaymentVocherModel.convertPaymentVoucher(PaymentVocherModel1, PaymentVoucher1);
                ShipmentServices1.insertPaymentVoucher(PaymentVoucher1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Shipment"] = Shipment1;
            viewDocument(Shipment1);
            return View(PaymentVocherModel1);
        }

        public ActionResult PrintPaymentVoucher(long ShipmentId)
        {
            PaymentVocherModel PaymentVocherModel1 = new PaymentVocherModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            PaymentVocherModel1.ShipmentId = ShipmentId;
            PaymentVocherModel1.CompanyName = "EMBASSY FREIGHT VIETNAM";
            if (Shipment1.PaymentVouchers != null && Shipment1.PaymentVouchers.Count > 0)
            {
                PaymentVoucher PaymentVoucher1 = Shipment1.PaymentVouchers.ElementAt(0);
                PaymentVocherModel.convertPaymentVoucher(PaymentVoucher1, PaymentVocherModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            ViewData["Shipment"] = Shipment1;
            viewDocument(Shipment1);
            return View(PaymentVocherModel1);
        }
        public ActionResult BookingRequest(long ShipmentId)
        {
            BookingRequestModel BookingRequestModel1 = new BookingRequestModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BookingRequestModel1.ShipmentId = ShipmentId;
            if (Shipment1.BookingRequests != null && Shipment1.BookingRequests.Count > 0)
            {
                BookingRequest Request1 = Shipment1.BookingRequests.ElementAt(0);
                BookingRequestModel.ConvertBookingRequest(Request1, BookingRequestModel1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BookingRequestModel1);
        }
        public ActionResult PrintBookingRequest(long ShipmentId)
        {
            BookingRequestModel BookingRequestModel1 = new BookingRequestModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            BookingRequestModel1.ShipmentId = ShipmentId;
            if (Shipment1.BookingRequests != null && Shipment1.BookingRequests.Count > 0)
            {
                BookingRequest Request1 = Shipment1.BookingRequests.ElementAt(0);
                BookingRequestModel.ConvertBookingRequest(Request1, BookingRequestModel1);
            }
            return View(BookingRequestModel1);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult BookingRequest(BookingRequestModel BookingModel, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.BookingRequests != null && Shipment1.BookingRequests.Count > 0)
            {
                BookingRequest BookingRequest1 = Shipment1.BookingRequests.ElementAt(0);
                BookingRequest1 = ShipmentServices1.getBookingRequest(BookingRequest1.Id);
                BookingRequestModel.ConvertBookingRequest(BookingModel, BookingRequest1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                BookingRequest BookingRequest1 = new Models.BookingRequest();
                BookingRequestModel.ConvertBookingRequest(BookingModel, BookingRequest1);
                ShipmentServices1.insertBookingRequest(BookingRequest1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(BookingModel);
        }

        //Manifest action
        public ActionResult Manifest(long ShipmentId)
        {
            ManifestModel ManifestModel1 = new ManifestModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ManifestModel1.ShipmentId = ShipmentId;
            if (Shipment1.Manifests != null && Shipment1.Manifests.Count > 0)
            {
                Manifest Manifest1 = Shipment1.Manifests.ElementAt(0);
                ManifestModel.convertManifest(Manifest1, ManifestModel1);
            }
            else
            {
                ManifestModel1.DEPARTURE = Shipment1.Area.AreaAddress + ", " + Shipment1.Area.Country.CountryName;
                ManifestModel1.ESTINATION = Shipment1.Area1.AreaAddress + ", " + Shipment1.Area1.Country.CountryName;
                ManifestModel1.SHIPPER = Shipment1.Customer1.FullName + "\r\n" + Shipment1.Customer1.Address;
                ManifestModel1.SHIPPER = Shipment1.Customer.FullName + "\r\n" + Shipment1.Customer.Address;
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(ManifestModel1);
        }
        public ActionResult PrintManifest(long ShipmentId)
        {
            ManifestModel ManifestModel1 = new ManifestModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            ManifestModel1.ShipmentId = ShipmentId;
            if (Shipment1.Manifests != null && Shipment1.Manifests.Count > 0)
            {
                Manifest Manifest1 = Shipment1.Manifests.ElementAt(0);
                ManifestModel.convertManifest(Manifest1, ManifestModel1);
            }
            else
            {
                ManifestModel1.DEPARTURE = Shipment1.Area.AreaAddress + ", " + Shipment1.Area.Country.CountryName;
                ManifestModel1.ESTINATION = Shipment1.Area1.AreaAddress + ", " + Shipment1.Area1.Country.CountryName;
                ManifestModel1.SHIPPER = Shipment1.Customer1.FullName + "\r\n" + Shipment1.Customer1.Address;
                ManifestModel1.SHIPPER = Shipment1.Customer.FullName + "\r\n" + Shipment1.Customer.Address;
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(ManifestModel1);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Manifest(ManifestModel ManifestModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.Manifests != null && Shipment1.Manifests.Count > 0)
            {
                Manifest Manifest1 = Shipment1.Manifests.ElementAt(0);
                Manifest1 = ShipmentServices1.getManifest(Manifest1.Id);
                ManifestModel.convertManifest(ManifestModel1, Manifest1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                Manifest Manifest1 = new Models.Manifest();
                ManifestModel.convertManifest(ManifestModel1, Manifest1);
                ShipmentServices1.insertManifest(Manifest1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(ManifestModel1);
        }
        //Transitment Advised action
        public ActionResult TransitmentAdvised(long ShipmentId)
        {
            TransitmentAdvisedModel TransitmentAdvisedModel1 = new TransitmentAdvisedModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            TransitmentAdvisedModel1.ShipmentId = ShipmentId;
            if (Shipment1.TransitmentAdviseds != null && Shipment1.TransitmentAdviseds.Count > 0)
            {
                TransitmentAdvised TransitmentAdvised1 = Shipment1.TransitmentAdviseds.ElementAt(0);
                TransitmentAdvisedModel.convertTransitment(TransitmentAdvised1, TransitmentAdvisedModel1);
            }
            else
            {
                TransitmentAdvisedModel1.AdvisedBL = Shipment1.HouseNum;
                TransitmentAdvisedModel1.Measurement = Shipment1.QtyUnit;
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(TransitmentAdvisedModel1);
        }
        public ActionResult PrintTransitmentAdvised(long ShipmentId)
        {
            TransitmentAdvisedModel TransitmentAdvisedModel1 = new TransitmentAdvisedModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            TransitmentAdvisedModel1.ShipmentId = ShipmentId;
            if (Shipment1.TransitmentAdviseds != null && Shipment1.TransitmentAdviseds.Count > 0)
            {
                TransitmentAdvised TransitmentAdvised1 = Shipment1.TransitmentAdviseds.ElementAt(0);
                TransitmentAdvisedModel.convertTransitment(TransitmentAdvised1, TransitmentAdvisedModel1);
            }
            else
            {
                TransitmentAdvisedModel1.AdvisedBL = Shipment1.HouseNum;
                TransitmentAdvisedModel1.Measurement = Shipment1.QtyUnit;
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(TransitmentAdvisedModel1);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult TransitmentAdvised(TransitmentAdvisedModel TransitmentAdvisedModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.TransitmentAdviseds != null && Shipment1.TransitmentAdviseds.Count > 0)
            {
                TransitmentAdvised TransitmentAdvised1 = Shipment1.TransitmentAdviseds.ElementAt(0);
                TransitmentAdvised1 = ShipmentServices1.getTransitmentAdvised(TransitmentAdvised1.Id);
                TransitmentAdvisedModel.convertTransitment(TransitmentAdvisedModel1, TransitmentAdvised1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                TransitmentAdvised TransitmentAdvised1 = new Models.TransitmentAdvised();
                TransitmentAdvisedModel.convertTransitment(TransitmentAdvisedModel1, TransitmentAdvised1);
                ShipmentServices1.insertTransitmentAdvised(TransitmentAdvised1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(TransitmentAdvisedModel1);
        }
        public ActionResult HAirWayBill(long ShipmentId)
        {
            HouseAirWayBillModel HouseAirWayBillModel1 = new HouseAirWayBillModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            HouseAirWayBillModel1.ShipmentId = ShipmentId;
            if (Shipment1.HAirWayBills != null && Shipment1.HAirWayBills.Count > 0)
            {
                HAirWayBill HAirWayBill1 = Shipment1.HAirWayBills.ElementAt(0);
                HouseAirWayBillModel.convertHAirWayBill(HAirWayBill1, HouseAirWayBillModel1);
            }
            else
            {
                HouseAirWayBillModel1.IssueBy = "<html><head>"
        + "<title></title></head>"
    + "<body><p>Not Negotiable</p><p>"
            + "<span style=\"font-size: 20px;\"><strong>Air Waybill</strong></span><br />Issued By</p>"
        + "<p><img alt=\"\" src=\"../../Images/transitment.png\" style=\"width: 200px; height: 40px;\" /></p>"
        + "<p><span style=\"font-family: arial,helvetica,sans-serif;\"><span style=\"font-size: 12px;\">Copies 1, 2 and 3 of this Air Waybill are originals and have the same validity</span></span></p>"
    + "</body></html>";
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(HouseAirWayBillModel1);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult HAirWayBill(HouseAirWayBillModel HouseAirWayBillModel1, long ShipmentId)
        {
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            if (Shipment1.HAirWayBills != null && Shipment1.HAirWayBills.Count > 0)
            {
                HAirWayBill HAirWayBill1 = Shipment1.HAirWayBills.ElementAt(0);
                HAirWayBill1 = ShipmentServices1.getHAirWayBill(HAirWayBill1.Id);
                HouseAirWayBillModel.convertHAirWayBill(HouseAirWayBillModel1, HAirWayBill1);
                ShipmentServices1.UpdateAny();
            }
            else
            {
                HAirWayBill HAirWayBill1 = new Models.HAirWayBill();
                HouseAirWayBillModel.convertHAirWayBill(HouseAirWayBillModel1, HAirWayBill1);
                ShipmentServices1.insertHAirWayBill(HAirWayBill1);
            }
            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(HouseAirWayBillModel1);
        }
        public ActionResult PrintHAirWayBill(long ShipmentId)
        {
            HouseAirWayBillModel HouseAirWayBillModel1 = new HouseAirWayBillModel();
            Shipment Shipment1 = ShipmentServices1.GetShipmentById(ShipmentId);
            HouseAirWayBillModel1.ShipmentId = ShipmentId;
            if (Shipment1.HAirWayBills != null && Shipment1.HAirWayBills.Count > 0)
            {
                HAirWayBill HAirWayBill1 = Shipment1.HAirWayBills.ElementAt(0);
                HouseAirWayBillModel.convertHAirWayBill(HAirWayBill1, HouseAirWayBillModel1);
            }

            ViewData["ServiceName"] = Shipment1.ServicesType.SerivceName;
            viewDocument(Shipment1);
            return View(HouseAirWayBillModel1);
        }
        #endregion

        public ActionResult ChangePaymentSoa(string ids, bool isPament)
        {
            List<long> listofIDs;
            var listId = ids.Split(';');
            listofIDs = listId.Where(x => !string.IsNullOrEmpty(x)).Select(it => Convert.ToInt64(it)).ToList();
            ShipmentServices1.ChangeSoaPayment(listofIDs, isPament);
            return Json("ok");
        }

        #region ShipmentControll

        public ActionResult CreateControl()
        {

            ShipmentModel model = new ShipmentModel();
            GetDefaultData();
            ViewData["Services"] = Services;
            IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(125).OrderBy(x => x.AreaAddress);
            IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(125).OrderBy(x => x.AreaAddress);
            var users =
                UsersServices1.GetQuery(
                    x =>
                        !x.Department.DeptFunction.Equals(UsersModel.Positions.Administrator.ToString()) &&
                        !x.Department.DeptFunction.Equals(UsersModel.Positions.Director.ToString()) &&
                        !x.Department.DeptFunction.Equals(UsersModel.Positions.Accountant.ToString()) &&
                        x.IsActive == true)
                        .OrderBy(x => x.FullName).ToList();
            model.CountryDeparture = 126;
            model.CountryDestination = 126;
            model.QtyNumber = 1;
            model.DepartureId = 5;
            model.DestinationId = 5;
            model.ServiceId = 3;
            model.QtyUnit = "KGS";
            ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
            ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
            ViewData["UserName"] = User1.FullName;
            ViewBag.UserList = users;
            ViewBag.UserListSelect = new List<User>();
            model.ShipperId = GetConsolCustomerId();
            model.CneeId = GetConsolCustomerId();
            model.IsMainControl = true;

            ViewData["Units"] = new SelectList(unitService.GetAll(x => x.ServiceType == "Air").ToList(), "Unit1", "Unit1");
            ViewData["Carriers"] = carrierService.GetAllByType(CarrierType.ShippingLine.ToString()).ToList();
            model.Dateshp = DateTime.Now.ToString("dd/MM/yyyy");
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateControl(ShipmentModel model)
        {
            model.VoucherId = null;
            model.IsTrading = false;
            GetDefaultData();
            ViewData["Services"] = Services;
            if (ModelState.IsValid)
            {
                try
                {
                    model.DepartmentId = User1.Department.Id;
                    model.SaleId = User1.Id;
                    model.CompanyId = User1.ComId.Value;
                    model.RevenueStatus = ShipmentModel.RevenueStatusCollec.Pending.ToString();
                    model.ServiceName = servicesType.FindEntity(x => x.Id == model.ServiceId).SerivceName;
                    model.HouseNum = string.Format(@"has {0} hbl", model.UserListInControl.Count);
                    model.IsControl = true;
                    model.ControlStep = ShipmentServices1.GetStepControlNumber(0);
                    ShipmentServices1.InsertShipment(model);
                    return RedirectToAction("Index", "Shipment", new { id = 0 });
                }
                catch (Exception exception)
                {
                    Logger.LogError(exception);
                    return View(model);
                }
            }
            else
            {
                string messages = string.Join("\n", ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                Logger.LogError(messages);
                IEnumerable<Area> AreaListDep = ShipmentServices1.getAllAreaByCountry(125).OrderBy(x => x.AreaAddress);
                IEnumerable<Area> AreaListDes = ShipmentServices1.getAllAreaByCountry(125).OrderBy(x => x.AreaAddress);
                var users =
                    UsersServices1.GetQuery(
                        x =>
                            !x.Department.DeptFunction.Equals(UsersModel.Positions.Administrator.ToString()) &&
                            !x.Department.DeptFunction.Equals(UsersModel.Positions.Director.ToString()) &&
                            !x.Department.DeptFunction.Equals(UsersModel.Positions.Accountant.ToString()) &&
                            x.IsActive == true)
                            .OrderBy(x => x.FullName).ToList();
                model.CountryDeparture = 125;
                model.CountryDestination = 125;
                ViewData["AreaListDep"] = new SelectList(AreaListDep, "Id", "AreaAddress");
                ViewData["AreaListDes"] = new SelectList(AreaListDes, "Id", "AreaAddress");
                ViewData["UserName"] = User1.FullName;
                ViewBag.UserList = users;
                ViewBag.UserListSelect = users.Where(x => model.UserListInControl.Contains(x.Id)).ToList();
                model.ShipperId = GetConsolCustomerId();
                model.CneeId = GetConsolCustomerId();

                ViewData["Units"] = new SelectList(unitService.GetAll(x => x.ServiceType == "Sea").OrderBy(x => x.Unit1).ToList(), "Unit1", "Unit1");
                ViewData["Carriers"] = carrierService.GetAllByType(CarrierType.ShippingLine.ToString()).ToList();
                model.Dateshp = DateTime.Now.ToString("dd/MM/yyyy");
                return View(model);
            }
        }

        private long GetConsolCustomerId()
        {
            var custormer = customerServices.FindEntity(x => x.FullName.Equals("CONSOL"));
            if (custormer != null) return custormer.Id;
            custormer = new Customer()
            {
                FullName = "CONSOL",
                Address = "CONSOL",
                CompanyName = "CONSOL",
                CustomerType = "CoType",
                UserId = 1
            };
            customerServices.Insert(custormer);
            custormer = customerServices.FindEntity(x => x.FullName.Equals("CONSOL"));
            return custormer.Id;
        }

        public ActionResult AddMemberToConsol(long id, long idRef)
        {
            var member = ShipmentServices1.GetShipmentById(idRef);
            ResultCommand result;
            if (member == null)
            {
                result = new ResultCommand()
                {
                    IsFinished = false,
                    Message = "Shipment member not found with ref " + idRef
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result = ShipmentServices1.AddMemberToConsol(id, member);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult RemoveMemberFromConsol(long id, long idRef)
        {
            var member = ShipmentServices1.GetShipmentById(idRef);
            ResultCommand result;
            if (member == null)
            {
                result = new ResultCommand()
                {
                    IsFinished = false,
                    Message = "Shipment member not found with ref " + idRef
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            result = ShipmentServices1.RemoveMemberOfConsol(id, member);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public void HistoryWirter(string actionName, long id, string message = "", bool isRevisedRequest = false)
        {

            var history = new HistoryModel()
            {
                HistoryMessage = !string.IsNullOrEmpty(message) ? message : String.Format("{0} đã {1} Revenue id {2}", User1.FullName, actionName, id),
                UserId = User1.Id,
                ActionName = actionName,
                CreateTime = DateTime.Now,
                ObjectId = id,
                ObjectType = new RevenueModel().GetType().ToString(),
                IsLasted = true,
                Id = 0,
                IsRevisedRequest = isRevisedRequest
            };
            historyService.SetUnLasted(history.ObjectId.Value, history.ObjectType);
            historyService.Save(history);
        }
    }
}
