using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using System.Xml.Linq;
using AutoMapper;
using SSM.Common;
using SSM.Models;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    public class DataController : BaseController
    {
        #region Definetion
        private Grid<Customer> _customerGrid;
        private Grid<CarrierAirLine> carriedGrid;
        private Grid<AreaModel> areaGrid;
        private Grid<Agent> agentGrid;
        private Grid<Country> countryGrid;
        public static String CUSTOMER_SEARCH_MODEL = "CUSTOMER_SEARCH_MODEL";
        public static String CARRIER_SEARCH_MODEL = "CARRIER_SEARCH_MODEL";
        public static String AREA_SEARCH_MODEL = "AREA_SEARCH_MODEL";
        public static String AGENT_SEARCH_MODEL = "AGENT_SEARCH_MODEL";
        public static String COUNTRY_SEARCH_MODEL = "COUNTRY_SEARCH_MODEL";
        private ICustomerServices customerServices;
        private ICarrierService carrierService;
        private IAreaService areaService;
        private IAgentService agentService;
        private ICountryService countryService;
        private IUnitService unitService;
        private IEnumerable<Country> countries;
        private IProvinceService province;
        private ShipmentServices shipmentServices;
        private ICRMService crmService;
        public string sessageStaus;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            customerServices = new CustomerServices();
            carrierService = new CarrierService();
            areaService = new AreaService();
            agentService = new AgentService();
            countryService = new CountryService();
            unitService = new UnitService();
            province = new ProvinceService();
            shipmentServices = new ShipmentServicesImpl();
            crmService = new CRMService();
            ViewData["CountryList"] = new SelectList(Countries, "Id", "CountryName");
            sessageStaus = string.Empty;

        }

        public DataController()
        {
            ViewBag.MessageStaus = sessageStaus;
        }
        protected IEnumerable<Country> Countries
        {
            get
            {
                countries = countries != null && countries.Any() ? countries : countryService.GetAllCountry();
                return countries;
            }
        }

        #endregion

        #region Customer
        public ActionResult Index()
        {
            _customerGrid = (Grid<Customer>)Session[CUSTOMER_SEARCH_MODEL];
            if (_customerGrid == null)
            {
                _customerGrid = new Grid<Customer>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "FullName"
                                }


                            );
                _customerGrid.SearchCriteria = new Customer();
            }
            UpdateGridCustomerData();

            ViewData["CustomerTypes"] = CustomerTypes;

            return View(_customerGrid);
        }
        [HttpPost]
        public ActionResult Index(Grid<Customer> grid, bool isNotShipment = false)
        {
            _customerGrid = grid;
            Session[CUSTOMER_SEARCH_MODEL] = grid;
            _customerGrid.ProcessAction();
            UpdateGridCustomerData(isNotShipment);
            return PartialView("_CustomerList", _customerGrid);
        }

        private SelectList CustomerTypes
        {
            get
            {
                var customerTypes = from CustomerType CT in Enum.GetValues(typeof(CustomerType))
                                    where CT != CustomerType.CoType
                                    select new { Id = CT, Name = CT.GetStringLabel() };
                return new SelectList(customerTypes.OrderBy(x => x.Name), "Id", "Name");
            }

        }
        private void UpdateGridCustomerData(bool isNotShipment = false)
        {
            ViewData["CustomerTypes"] = CustomerTypes;
            var totalRow = 0;
            var page = _customerGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "FullName" : page.Sidx, page.Sord == "asc");
            IEnumerable<Customer> customers = customerServices.GetAll(_customerGrid.SearchCriteria, sort, out totalRow, page.CurrentPage, page.PageSize, CurrenUser);
            _customerGrid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _customerGrid.Data = new List<Customer>();
                return;
            }

            _customerGrid.Data = customers;
        }
        [HttpGet]
        public ActionResult EditCustomer(long id)
        {
            ViewData["CustomerTypes"] = CustomerTypes;

            if (id == 0)
                return PartialView("_CustomerFormEdit", new CustomerModel());
            var model = customerServices.GetModelById(id);
            return PartialView("_CustomerFormEdit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCustomer(CustomerModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    customerServices.UpdateCustomer(model);
                }
                else
                {
                    model.UserId = CurrenUser.Id;
                    model.Id = 0;
                    customerServices.InsertCustomer(model);
                }
                return Json(1);
            }
            ViewData["CustomerTypes"] = CustomerTypes;
            return PartialView("_CustomerFormEdit", model);
        }

        public ActionResult CustomerDelete(long id)
        {
            var shipments = shipmentServices.Count(x => x.CneeId == id || x.ShipperId == id);
            if (shipments > 0)
            {
                var value = new
                {
                    Views = "This customer have shipment, you can not delete it",
                    Title = "Error!",
                    IsRemve = false,
                    Success = false,
                    TdId = "del_" + id
                };
                return JsonResult(value, true);
            }

            if (customerServices.DeleteCustomer(id))
            {
                var crmcus = crmService.FindEntity(x => x.SsmCusId == id);
                if (crmcus != null)
                {
                    crmService.SetMoveCustomerToData(crmcus.Id, false, id);
                }
            }
            var value2 = new
            {
                Views = "Bạn đã xoá thành công",
                Title = "Success!",
                IsRemve = true,
                TdId = "del_" + id
            };
            return JsonResult(value2, true);
        }

        public ActionResult SetSeeCustomer(long id, bool isChecked)
        {
            var cus = customerServices.GetModelById(id);
            cus.IsSee = isChecked;
            customerServices.UpdateCustomer(cus);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetIsHideUser(long id, bool isChecked)
        {
            var cus = customerServices.GetById(id);
            cus.IsHideUser = isChecked;
            customerServices.Commited();
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetMoveToCrmCustomer(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = customerServices.GetById(id);
                cus.IsMove = isChecked;
                cus.MovedBy = CurrenUser.Id;
                countryService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Carrier
        [HttpGet]
        public ActionResult Carrier()
        {
            carriedGrid = (Grid<CarrierAirLine>)Session[CARRIER_SEARCH_MODEL];
            if (carriedGrid == null)
            {
                carriedGrid = new Grid<CarrierAirLine>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "CarrierAirLineName"
                                }


                            );
                carriedGrid.SearchCriteria = new CarrierAirLine();
            }
            UpdateGridCarrierData();

            return View(carriedGrid);
        }
        [HttpPost]
        public ActionResult Carrier(Grid<CarrierAirLine> grid)
        {
            carriedGrid = grid;
            Session[CARRIER_SEARCH_MODEL] = grid;
            carriedGrid.ProcessAction();
            UpdateGridCarrierData();
            return PartialView("_CarrierList", carriedGrid);
        }

        private void UpdateGridCarrierData()
        {
            var totalRow = 0;
            ViewData["CarrierTypes"] = CarrierTypes;
            var page = carriedGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "CarrierAirLineName" : page.Sidx, page.Sord == "asc");
            //IEnumerable<ca> customers = customerServices.GetAll(_customerGrid.SearchCriteria, sort, out totalRow, page.CurrentPage, page.PageSize);
            var qr = carrierService.GetAll(carriedGrid.SearchCriteria);
            if (!CurrenUser.IsAdmin())
                qr = qr.Where(x => x.IsHideUser == false);
            qr = qr.OrderBy(sort);
            totalRow = qr.Count();
            carriedGrid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                carriedGrid.Data = new List<CarrierAirLine>();
                return;
            }

            carriedGrid.Data = carrierService.GetListPager(qr, page.CurrentPage, page.PageSize);
        }

        [HttpGet]
        public ActionResult EditCarrier(long id)
        {
            ViewData["CarrierTypes"] = CarrierTypes;
            if (id == 0)
                return PartialView("_CarrieFormEdit", new CarrierModel());
            var model = carrierService.GetModelById(id);
            return PartialView("_CarrieFormEdit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCarrier(CarrierModel model)
        {
            ViewData["CarrierTypes"] = CarrierTypes;
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    carrierService.UpdateCarrier(model);
                }
                else
                {
                    carrierService.InsertCarrier(model);
                }
                return Json(1);
            }
            return PartialView("_CarrieFormEdit", model);
        }

        public ActionResult CarrierDelete(long id)
        {
            carrierService.DeleteCarrier(id);
            return RedirectToAction("Carrier");
        }
        public ActionResult SetCarrierActive(int id, bool isActive)
        {
            carrierService.SetActive(id, isActive);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetCarrierIsSee(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = carrierService.GetById(id);
                cus.IsSee = isChecked;
                carrierService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetCarrierIsHideUser(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = carrierService.GetById(id);
                cus.IsHideUser = isChecked;
                carrierService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Agent
        [HttpGet]
        public ActionResult Agent()
        {
            agentGrid = (Grid<Agent>)Session[AGENT_SEARCH_MODEL];
            if (carriedGrid == null)
            {
                agentGrid = new Grid<Agent>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "AbbName"
                                }


                            );
                agentGrid.SearchCriteria = new Agent();
            }
            UpdateGridAgentData();

            return View(agentGrid);
        }
        [HttpPost]
        public ActionResult Agent(Grid<Agent> grid)
        {
            agentGrid = grid;
            Session[AGENT_SEARCH_MODEL] = grid;
            agentGrid.ProcessAction();
            UpdateGridAgentData();
            return PartialView("_AgentList", agentGrid);
        }

        private void UpdateGridAgentData()
        {
            ViewData["CountryListAgent"] = new SelectList(Countries, "CountryName", "CountryName");
            var totalRow = 0;
            var page = agentGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "AgentName" : page.Sidx, page.Sord == "asc");
            var qr = agentService.GetAll(agentGrid.SearchCriteria);
            if (!CurrenUser.IsAdmin())
                qr = qr.Where(x => x.IsHideUser == false);
            qr = qr.OrderBy(sort);
            totalRow = qr.Count();
            agentGrid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                agentGrid.Data = new List<Agent>();
                return;
            }
            agentGrid.Data = agentService.GetListPager(qr, page.CurrentPage, page.PageSize);
        }

        [HttpGet]
        public ActionResult EditAgent(long id)
        {
            ViewBag.MessageStaus = sessageStaus;
            ViewData["CountryListAgent"] = new SelectList(Countries, "CountryName", "CountryName");
            if (id == 0)
                return PartialView("_AgentFormEdit", new AgentModel() { User = CurrenUser });
            var model = agentService.GetModelById(id);
            return PartialView("_AgentFormEdit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAgent(AgentModel model)
        {
            ViewData["CountryListAgent"] = new SelectList(Countries, "CountryName", "CountryName");
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    agentService.UpdateAgent(model, out sessageStaus);
                }
                else
                {
                    model.User = CurrenUser;
                    agentService.InsertAgent(model, out sessageStaus);
                }
                return Json(1);
            }

            return PartialView("_AgentFormEdit", model);
        }

        public ActionResult DeleteAgent(long id)
        {

            agentService.DeleteAgent(id, out sessageStaus);
            return RedirectToAction("Agent");
        }
        public ActionResult SetAgentActive(int id, bool isActive)
        {
            agentService.SetActive(id, isActive);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SeteAgentIsHideUser(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = agentService.GetById(id);
                cus.IsHideUser = isChecked;
                agentService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SeteAgentIsSee(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = agentService.GetById(id);
                cus.IsSee = isChecked;
                agentService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Area
        [HttpGet]
        public ActionResult Area()
        {
            areaGrid = (Grid<AreaModel>)Session[AREA_SEARCH_MODEL];
            if (areaGrid == null)
            {
                areaGrid = new Grid<AreaModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "AreaAddress"
                                }


                            )
                { SearchCriteria = new AreaModel() };
            }
            UpdateGridAreaData();

            return View(areaGrid);
        }
        [HttpPost]
        public ActionResult Area(Grid<AreaModel> grid)
        {
            areaGrid = grid;
            Session[AREA_SEARCH_MODEL] = grid;
            areaGrid.ProcessAction();
            UpdateGridAreaData();
            return PartialView("_AreaList", areaGrid);
        }

        private void UpdateGridAreaData()
        {
            var totalRow = 0;
            var page = areaGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "AreaAddress" : page.Sidx, page.Sord == "asc");

            var area = new Area();
            if (areaGrid.SearchCriteria != null)
            {
                area = new Area
                {
                    AreaAddress = areaGrid.SearchCriteria.AreaAddress,
                    CountryId = areaGrid.SearchCriteria.CountryId,
                    trading_yn = areaGrid.SearchCriteria.IsTrading,
                };
            }
            var qr = areaService.GetAll(area);
            if (!CurrenUser.IsAdmin())
                qr = qr.Where(x => x.IsHideUser == false);
            qr = qr.OrderBy(sort);
            totalRow = qr.Count();
            areaGrid.Pager.Init(totalRow);
            List<AreaModel> list = new List<AreaModel>();
            if (totalRow == 0)
            {
                areaGrid.Data = list;
                return;
            }
            var data = areaService.GetListPager(qr, page.CurrentPage, page.PageSize);
            list.AddRange(data.Select(db => Mapper.Map<AreaModel>(db)));
            areaGrid.Data = list;
        }

        [HttpGet]
        public ActionResult EditArea(long id)
        {
            ViewData["CountryList"] = new SelectList(Countries, "Id", "CountryName");
            if (id == 0)
                return PartialView("_AreaFormEdit", new AreaModel());
            var model = areaService.GetModelById(id);
            return PartialView("_AreaFormEdit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditArea(AreaModel model)
        {
            ViewData["CountryList"] = new SelectList(Countries, "Id", "CountryName");
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    areaService.UpdateArea(model);
                }
                else
                {
                    areaService.InsertArea(model);
                }
                return Json(1);
            }
            return PartialView("_AreaFormEdit", model);
        }

        public ActionResult DeleteArea(long id)
        {
            areaService.DeleteArea(id);
            return RedirectToAction("Area");
        }
        [HttpPost]
        public ActionResult SetAreaTrading(long id, bool istrading)
        {
            if (CurrenUser.IsAdmin())
            {
                areaService.UpdateTradingArea(id, istrading);
            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetAreaIsHideUser(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = areaService.GetById(id);
                cus.IsHideUser = isChecked;
                areaService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetAreaIsSee(long id, bool isChecked)
        {
            if (CurrenUser.IsAdmin())
            {
                var cus = areaService.GetById(id);
                cus.IsSee = isChecked;
                areaService.Commited();

            }
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Country
        [HttpGet]
        public ActionResult Country()
        {
            countryGrid = (Grid<Country>)Session[COUNTRY_SEARCH_MODEL];
            if (areaGrid == null)
            {
                countryGrid = new Grid<Country>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "CountryName"
                                }


                            );
                countryGrid.SearchCriteria = new Country();
            }
            UpdateGridCountryData();

            return View(countryGrid);
        }
        [HttpPost]
        public ActionResult Country(Grid<Country> grid)
        {
            countryGrid = grid;
            Session[COUNTRY_SEARCH_MODEL] = grid;
            countryGrid.ProcessAction();
            UpdateGridCountryData();
            return PartialView("_CountryList", countryGrid);
        }

        private void UpdateGridCountryData()
        {
            var totalRow = 0;
            var page = countryGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "CountryName" : page.Sidx, page.Sord == "asc");
            var qr = countryService.GetQuery(x => string.IsNullOrEmpty(countryGrid.SearchCriteria.CountryName) || x.CountryName.Contains(countryGrid.SearchCriteria.CountryName));
            qr = qr.OrderBy(sort);
            totalRow = qr.Count();
            countryGrid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                countryGrid.Data = new List<Country>();
                return;
            }
            countryGrid.Data = countryService.GetListPager(qr, page.CurrentPage, page.PageSize);
        }

        [HttpGet]
        public ActionResult EditCountry(long id)
        {
            var model = countryService.GetModelById(id) ?? new CountryModel();
            var value = new
            {
                Views = this.RenderPartialView("_CountryFormEdit", model),
                Title = "Control country",
            };
            return JsonResult(value, true);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCountry(CountryModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    countryService.UpdateCountry(model);
                }
                else
                {
                    if (!Countries.Any(m => m.CountryName.ToLower().Equals(model.CountryName.ToLower())))
                        countryService.InsertCountry(model);
                }
                return Json(new
                {
                    Message = @"Thành công",
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            return PartialView("_CountryFormEdit", model);
        }

        public ActionResult DeleteCountry(long id)
        {
            countryService.DeleteCountry(id);
            return RedirectToAction("Country");
        }

        public ActionResult LoadXmlData()
        {
            string appDirectory = Server.MapPath(@"~/countriesStates.xml");
            var data = Helpers.LoadCitiesXmlData(appDirectory);
            var allcountriesDb = countryService.GetAllCountry().Select(x => x.CountryName).Distinct().ToList();
            var mCountries = data.CountryModels.Where(x => !allcountriesDb.Contains(x.CountryName)).ToList();

            var cities = data.ProvinceModels;
            foreach (var country in mCountries)
            {
                var id = countryService.GetIdByName(country.CountryName);
                CountryModel countryToDb = country;
                if (id ==0)
                { 
                    countryToDb.Id = 0; 
                    countryService.InsertCountry(countryToDb);
                }
            }

            foreach (var city in cities)
            {
                if (city == null)
                    continue;
                var country = city.Country;
                if (country == null)
                    continue;
                var dbCountry = countryService.GetModelByName(country.CountryName);
                if (dbCountry == null)
                    continue;
                city.CountryId = dbCountry.Id;
                if (dbCountry.Id == 126)
                    city.Name = Helpers.NonUnicode(city.Name);
                var dbcity = province.FindEntity(c => c.Name == city.Name && c.CountryId == dbCountry.Id);
                if (dbcity == null)
                {
                    city.Id = 0;
                    province.InsertProvince(city);
                }
                else
                {
                    province.UpdateProvince(city);
                }
            }

            return Json("Ok", JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoadJsonData()
        {
            return Json("OK");
        }

        public ActionResult ProvinceByCountry(long countryId, string name)
        {
            @ViewBag.CountryName = name;
            @ViewBag.CountryId = countryId;
            var provinces = province.GetQuery(x => x.CountryId == countryId).OrderBy(x => x.Name).ToList();
            var value = new

            {
                Views = this.RenderPartialView("_stateList", provinces),
                Title = name,
            };
            return JsonResult(value, true);
        }
        [HttpPost]
        public ActionResult ProvinceByCountry(string ProvinceName, long countryId)
        {
            var provinces =
                province.GetQuery(
                        x =>
                            x.CountryId == countryId &&
                            (string.IsNullOrEmpty(ProvinceName) || x.Name.Contains(ProvinceName)))
                    .OrderBy(x => x.Name)
                    .ToList();
            return PartialView("_provinceList", provinces);
        }

        public ActionResult ProvinceEdit(long id, long countryId = 0)
        {
            var model = province.GetModelById(id) ?? new ProvinceModel() { CountryId = countryId };
            var value = new
            {
                Views = this.RenderPartialView("_province", model),
                Title = "Create/Edit Province",
                ColumnClass = "col-md-6 col-md-offset-3"
            };
            return JsonResult(value, true);
        }
        [HttpPost]
        public ActionResult ProvinceEdit(ProvinceModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    province.UpdateProvince(model);
                }
                else
                {
                    if (!province.Any(m => m.Name.ToLower().Equals(model.Name.ToLower())))
                        province.InsertProvince(model);
                }
                var provinces = province.GetQuery(x => x.CountryId == model.CountryId).OrderBy(x => x.Name).ToList();
                var value = this.RenderPartialView("_provinceList", provinces);
                return JsonResult(value);
            }
            return PartialView("_province", model);
        }

        public ActionResult DeleteProvince(long id, long countryId)
        {
            province.DeleteProvince(id);
            var provinces = province.GetQuery(x => x.CountryId == countryId).OrderBy(x => x.Name).ToList();
            var value = this.RenderPartialView("_provinceList", provinces);
            return PartialView("_provinceList", provinces);
        }
        #endregion

        #region Unit

        public ActionResult Unit()
        {
            var list = unitService.GetQuery().OrderBy(x => x.Unit1).ToList();
            return View(list);
        }
        [HttpGet]
        public ActionResult EditUnit(long id)
        {
            ViewData["ServiceTypes"] = ServiceTypes;
            if (id == 0)
                return PartialView("_UnitFormEdit", new UnitModel());
            var model = unitService.GetModelById(id);
            return PartialView("_UnitFormEdit", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUnit(UnitModel model)
        {
            ViewData["ServiceTypes"] = ServiceTypes;
            if (ModelState.IsValid)
            {
                if (model.Id > 0)
                {
                    unitService.UpdateUnit(model);
                }
                else
                {
                    unitService.InsertUnit(model);
                }
                return Json(1);
            }
            return PartialView("_UnitFormEdit", model);
        }

        public ActionResult DeleteUnit(long id)
        {
            unitService.DeleteUnit(id);
            return RedirectToAction("Unit");
        }

        #endregion
    }
}