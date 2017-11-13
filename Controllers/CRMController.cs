using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Infodinamica.Framework.Exportable.Engines;
using Infodinamica.Framework.Exportable.Engines.Excel;
using Microsoft.Office.Interop.Excel;
using Resources;
using SSM.Common;
using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels;
using SSM.ViewModels.Shared;
using SortField = SSM.Services.SortField;

namespace SSM.Controllers
{
    public class CRMController : BaseController
    {
        #region Defined properties     

        private ISettingService settingService;
        private ICRMService crmService;
        private ICRMPriceQuotationService priceQuotationService;
        private ICRMEventService eventService;
        private IServerFileService fileService;
        private ICRMDocumentService documentService;
        private ICRMGroupService crmGroupService;
        private ICRMSourceService crmSourceService;
        private ICRMStatusService crmStatusService;
        private ICRMJobCategoryService jobCategoryService;
        private UsersServices usersServices;
        private ICRMContactService crmContactService;
        private ICRMCustomerService crmCustomerService;
        private IProvinceService provinceService;
        private ICustomerServices customerServices;
        private ICRMEvetypeService evetypeService;
        private ICRMPLanProgramService planProgramService;
        private ICRMPriceStatusService priceStatusService;
        private IHistoryService historyService;
        private const String CRMCUSTOMER_SEARCH_MODEL = "CRMCUSTOMER_SEARCH_MODEL";
        private Grid<CRMFilterProfitResult> _customerGrid;
        private CRMSearchModel filterModel;
        public const string CRM_SEARCH_MODEL = "CRM_SEARCH_MODEL";
        public const string CRM_SEARCH_RESULT = "CRM_SEARCH_RESULT";
        private IEnumerable<SaleType> saleTypes;
        private ICRMEmailHistoryService emailHistoryService;
        private ICRMScheduleServiec scheduleServiec;
        private ICRMUserFollowCustomerService followCustomerService;
        private ICRMUserFollowEventService followEventService;
        private ShipmentServices shipmentServices;
        private int days = 365;
        #endregion
        #region Contractor
        public CRMController()
        {
            settingService = new SettingServices();
            crmService = new CRMService();
            priceQuotationService = new CRMPriceQuotationService();
            eventService = new CRMEventService();
            documentService = new CRMDocumentService();
            fileService = new ServerFileService();
            crmCustomerService = new CRMCustomerService();
            crmGroupService = new CRMGroupService();
            crmSourceService = new CRMSourceService();
            crmStatusService = new CRMStatusService();
            jobCategoryService = new CRMJobCategoryService();
            crmContactService = new CRMContactService();
            usersServices = new UsersServicesImpl();
            provinceService = new ProvinceService();
            customerServices = new CustomerServices();
            evetypeService = new CRMEvetypeService();
            planProgramService = new CRMPLanProgramService();
            historyService = new HistoryService();
            emailHistoryService = new CRMEmailHistoryService();
            scheduleServiec = new CRMScheduleServiec();
            priceStatusService = new CRMPriceStatusService();
            followCustomerService = new CRMUserFollowCustomerService();
            followEventService = new CRMUserFollowEventService();
            shipmentServices = new ShipmentServicesImpl();
            var crmstting = usersServices.CRMDayCanelSettingNumber();
            days = int.Parse(crmstting.DataValue);

        }

        private IList<CRMStatus> statuses;
        private IList<CRMSource> sources;
        private IList<CRMJobCategory> jobCategories;
        private IList<CRMGroup> groups;
        private List<User> users;
        private List<Province> provinces;
        private IEnumerable<Country> countries;
        private IEnumerable<Department> departments;

        public void IndexDefaltData()
        {
            statuses = statuses ?? crmStatusService.GetAll();
            sources = sources ?? crmSourceService.GetAll();
            saleTypes = saleTypes ?? usersServices.getAllSaleTypes(true);
            users = users ?? usersServices.GetAllSales(CurrenUser, false).OrderBy(x => x.FullName).ToList();
            departments = departments ?? usersServices.GetAllDepartmentActive(CurrenUser);
            groups = groups ?? crmGroupService.GetAll();
            ViewData["Statuses"] = statuses;
            ViewData["Sources"] = sources;
            ViewData["SaleTypes"] = saleTypes;
            ViewData["UserSalesList"] = users;
            ViewData["Departments"] = departments;
            ViewData["Groups"] = groups;
        }
        public void GetBaseData()
        {
            statuses = statuses ?? crmStatusService.GetAll();
            sources = sources ?? crmSourceService.GetAll();
            jobCategories = jobCategories ?? jobCategoryService.GetAll();
            groups = groups ?? crmGroupService.GetAll();
            saleTypes = saleTypes ?? usersServices.getAllSaleTypes(true);
            provinces = provinces ?? provinceService.GetAllByCountryId(126);
            ViewData["Statuses"] = statuses;
            ViewData["Sources"] = sources;
            ViewData["JobCategories"] = jobCategories;
            ViewData["Groups"] = groups;
            countries = countries ?? crmGroupService.GetAllCountry();
            ViewData["Countries"] = countries;
            ViewData["States"] = provinces;
            ViewData["SaleTypes"] = saleTypes;
            users = users ?? usersServices.GetAllSales(CurrenUser, false).OrderBy(x => x.FullName).ToList();
            ViewData["UserSalesList"] = users;
        }
        #endregion
        #region Index Function
        public ActionResult Index()
        {
            filterModel = (CRMSearchModel)Session[CRM_SEARCH_MODEL];
            _customerGrid = (Grid<CRMFilterProfitResult>)Session[CRMCUSTOMER_SEARCH_MODEL];
            if (_customerGrid == null)
            {
                _customerGrid = new Grid<CRMFilterProfitResult>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 50,
                                    Sord = "asc",
                                    Sidx = "CompanyShortName"
                                }
                            );
            }
            //var listUpdate =
            //    crmCustomerService.GetAll(
            //        x =>
            //            x.SsmCusId.HasValue && x.CRMStatus.Code != 4 &&
            //            (x.DateCancel == null || x.DateCancel.Value >= DateTime.Now.AddHours(-1)));
            //foreach (var crm in listUpdate)
            //{
            //    SetSatusCrm(crm);
            //}
            UpdateGrid();
            IndexDefaltData();
            return View(_customerGrid);
        }

        [HttpPost]
        public ActionResult Index(CRMSearchModel fModel, Grid<CRMFilterProfitResult> grid, string ClearSearch)
        {
            _customerGrid = grid;
            _customerGrid.ProcessAction();
            filterModel = fModel;
            if (ClearSearch == "yes")
            {
                filterModel = null;
            }

            UpdateGrid();
            Session[CRMCUSTOMER_SEARCH_MODEL] = _customerGrid;
            return PartialView("_List", _customerGrid);
        }

        private void UpdateGrid()
        {
            var totalRow = 0;
            int totalHashSipment = 0;
            var page = _customerGrid.Pager;
            filterModel = filterModel ?? new CRMSearchModel
            {
                PeriodDate = PeriodDate.CreateDate,
                FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                ToDate = DateTime.Now,
            };
            ViewBag.SearchingMode = filterModel;
            Session[CRM_SEARCH_MODEL] = filterModel;

            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "CompanyShortName" : page.Sidx, page.Sord == "asc");
            int totalSeccessCount = 0;
            int totalClientCount = 0;
            int totalFolowCount = 0;
            var list = new List<CRMFilterProfitResult>();
            #region getData

            IEnumerable<CRMFilterProfitResult> customers = crmCustomerService.GetAllCRMResults(filterModel, sort, out totalRow, page.CurrentPage, page.PageSize, CurrenUser, out totalSeccessCount, out totalClientCount, out totalFolowCount);
            _customerGrid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                _customerGrid.Data = new List<CRMFilterProfitResult>();
                ViewBag.TotalDisplay = string.Empty;
                Session[CRM_SEARCH_RESULT] = null;
                return;
            }
            var dblist = customers.ToList();

            foreach (var itemC in dblist)
            {
                if (itemC.AnyShipmentOld == true)
                    itemC.Code = CRMStatusCode.Client;
                else if (itemC.AnyShipmentOld == false && itemC.TotalShipmentSuccess > 0)
                    itemC.Code = CRMStatusCode.Success;
                else
                    itemC.Code = CRMStatusCode.Potential;
                list.Add(itemC);
            }


            #endregion

            _customerGrid.Data = list;
            //cached result for export excel                        
            int followOrther, cusFollow, cusRegular, cuscompleted, cusFinish, totalCusTyle, totalCus;
            followOrther = cusFollow = cusRegular = cuscompleted = cusFinish = totalCusTyle = totalCus = 0;

            if (CurrenUser.IsDirecter())
            {
                totalCus = totalRow;
                followOrther = list.Count(x => (filterModel.SalesId == 0 || x.Customer.CreatedById != filterModel.SalesId)
                && (filterModel.DeptId == 0 || x.CreaterdBy.DeptId != filterModel.DeptId));
                cusFollow = list.Count(x => x.Code == CRMStatusCode.Potential
                && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId)
                && (filterModel.DeptId == 0 || x.CreaterdBy.DeptId == filterModel.DeptId));
                // cusRegular = list.Count(x => x.Customer.CRMStatus.Code == (byte)CRMStatusCode.Lost
                //&& (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId)
                // && (filterModel.DeptId == 0 || x.CreaterdBy.DeptId == filterModel.DeptId));
                cuscompleted = list.Count(x => x.Code == CRMStatusCode.Success
                && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId)
                && (filterModel.DeptId == 0 || x.CreaterdBy.DeptId == filterModel.DeptId));
                cusFinish = list.Count(x => x.Code == CRMStatusCode.Client
                && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId)
                && (filterModel.DeptId == 0 || x.CreaterdBy.DeptId == filterModel.DeptId));
                // labelName = "(Of Office)";
                if (filterModel.SalesId != 0)
                {
                    totalCus = totalRow - followOrther;
                }
                else if (filterModel.DeptId != 0)
                {
                    totalCus = totalRow - followOrther;// labelName = "( Of Department)";
                }
            }
            else if (CurrenUser.IsDepManager())
            {
                //  labelName = "(Of Department)";
                // if (filterModel.SalesId != 0) labelName = string.Empty;
                followOrther = list.Count(x => x.Customer.User.DeptId != CurrenUser.DeptId && (filterModel.SalesId == 0 || x.Customer.CreatedById != filterModel.SalesId));
                cusFollow = list.Count(x => x.Code == CRMStatusCode.Potential && x.CreaterdBy.DeptId == CurrenUser.DeptId && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId));
                // cusRegular = list.Count(x =>  x.Code == CRMStatusCode.Regularly && x.CreaterdBy.DeptId == CurrenUser.DeptId && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId));
                cuscompleted = list.Count(x => x.Code == CRMStatusCode.Success && x.CreaterdBy.DeptId == CurrenUser.DeptId && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId));
                cusFinish = list.Count(x => x.Code == CRMStatusCode.Client && x.CreaterdBy.DeptId == CurrenUser.DeptId && (filterModel.SalesId == 0 || x.Customer.CreatedById == filterModel.SalesId));
                totalCus = totalRow - followOrther;
            }
            else
            {
                //  labelName = string.Empty;
                followOrther = list.Count(x => x.Customer.CreatedById != CurrenUser.Id);
                cusFollow = list.Count(x => x.Code == CRMStatusCode.Potential && x.Customer.CreatedById == CurrenUser.Id);
                //  cusRegular = list.Count(x => x.Customer.CRMStatus.Code == (byte)CRMStatusCode.Regularly && x.Customer.CreatedById == CurrenUser.Id);
                cuscompleted = list.Count(x => x.Code == CRMStatusCode.Success && x.Customer.CreatedById == CurrenUser.Id);
                cusFinish = list.Count(x => x.Code == CRMStatusCode.Client && x.Customer.CreatedById == CurrenUser.Id);
                totalCus = totalRow - followOrther;
            }
            var totalPage = page.PageSize > totalRow ? totalRow : page.PageSize;
            totalCusTyle = cusFollow + cuscompleted;
            var totalgolobalCusTyle = totalFolowCount + totalSeccessCount;
            ViewBag.TotalDisplay = string.Format(Resources.Resource.CRM_CUSTOMER_LIST_TOTAL, totalPage, "", cusFollow,
                cuscompleted, cusFinish);
            ViewBag.TotalCots = string.Format(Resources.Resource.CRM_CUSTOMER_COTS, cuscompleted, totalCusTyle, totalCusTyle == 0 ? "N/A" : "" + cuscompleted * 100 / totalCusTyle, "");
            ViewBag.TotalSummaryDisplay = string.Format(Resources.Resource.CRM_CUSTOMER_LIST_TOTAL, totalCus, "", totalFolowCount,
                totalSeccessCount, totalClientCount);
            ViewBag.TotalSummaryCots = string.Format(Resources.Resource.CRM_CUSTOMER_COTS, totalSeccessCount, totalgolobalCusTyle, totalgolobalCusTyle == 0 ? "N/A" : "" + totalSeccessCount * 100 / totalgolobalCusTyle, "");
        }
        #endregion
        #region Base Data
        public ActionResult CRMBaseList()
        {
            var baseGrid = new Grid<CRMBaseModel>(new Pager
            {
                CurrentPage = 1,
                PageSize = 20,
                Sord = "asc",
                Sidx = "Name"
            });
            baseGrid.SearchCriteria = new CRMBaseModel() { ModelTypeName = BaseModelTypeName.JOBCATEGORY };
            baseGrid = UpdateBaseGird(baseGrid);
            return View(baseGrid);
        }

        Grid<CRMBaseModel> UpdateBaseGird(Grid<CRMBaseModel> grid)
        {
            var sort = new SortField(grid.Pager.Sidx, grid.Pager.Sord == "asc");
            switch (grid.SearchCriteria.ModelTypeName)
            {
                case BaseModelTypeName.SOURCE:
                    grid.Data = crmSourceService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List Nguồn";
                    ViewBag.ModelType = ModelType.CRMSource;
                    break;
                case BaseModelTypeName.GROUP:
                    grid.Data = crmGroupService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List Group";
                    ViewBag.ModelType = ModelType.CRMGroup;
                    break;
                case BaseModelTypeName.STATUS:
                    grid.Data = crmStatusService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List Status";
                    ViewBag.ModelType = ModelType.CRMStatus;
                    break;
                case BaseModelTypeName.EVENTTYPE:
                    grid.Data = evetypeService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List Event type";
                    ViewBag.ModelType = ModelType.CRMEventType;
                    break;
                case BaseModelTypeName.PLANPROGRAM:
                    grid.Data = planProgramService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List Program of plan";
                    ViewBag.ModelType = ModelType.CRMPlanProgram;
                    break;
                case BaseModelTypeName.PRICESTATUS:
                    grid.Data = priceStatusService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List price Status of plan";
                    ViewBag.ModelType = ModelType.CRMPriceStatus;
                    break;
                case BaseModelTypeName.JOBCATEGORY:
                default:
                    grid.Data = jobCategoryService.GetList(sort, grid.SearchCriteria.Name);
                    ViewBag.Title = "List Ngành nghề kinh doanh";
                    ViewBag.ModelType = ModelType.CRMJobCategory;
                    break;
            }
            return grid;
        }
        [HttpPost]
        public ActionResult CRMBaseList(Grid<CRMBaseModel> grid)
        {
            grid.ProcessAction();
            grid = UpdateBaseGird(grid);
            return PartialView("_ListBase", grid);
        }

        [HttpGet]
        public ActionResult AddBaseData(ModelType modelType, bool isList = false)
        {
            if (modelType == ModelType.UserModel)
                return PartialView("_UserListSugget");
            var model = new CRMBaseModel { ModelType = modelType };
            if (modelType == ModelType.CRMGroup)
                ViewData["Groups"] = crmGroupService.GetAll(x => !x.ParentId.HasValue);
            ViewBag.IsList = isList;
            if (isList == true)
            {
                var value = new
                {
                    Views = this.RenderPartialView("_CRMBaseView", model),
                    Title = string.Format(@"Tạo thông tin cho {0}", modelType.ToString()),
                };
                return JsonResult(value, true);
            }
            return PartialView("_CRMBaseView", model);
        }
        [HttpPost]
        public ActionResult AddBaseData(CRMBaseModel model, bool isList = false)
        {
            ViewBag.IsList = isList;
            if (model.ModelType == ModelType.CRMGroup)
                ViewData["Groups"] = crmGroupService.GetAll(x => !x.ParentId.HasValue);
            if (!ModelState.IsValid)
            {
                return PartialView("_CRMBaseView", model);
            }
            try
            {
                CRMBaseModel data = new CRMBaseModel();
                if (model.Id > 0)
                    data = model;
                switch (model.ModelType)
                {
                    case ModelType.CRMSource:
                        if (model.Id > 0)
                            crmSourceService.UpdateCRMGroup(model);
                        else
                            data = crmSourceService.InsertCRMGroup(model);
                        break;
                    case ModelType.CRMGroup:
                        if (model.Id > 0)
                            crmGroupService.UpdateCRMGroup(model);
                        else
                            data = crmGroupService.InsertCRMGroup(model);
                        break;
                    case ModelType.CRMJobCategory:
                        if (model.Id > 0)
                            jobCategoryService.UpdateCRMGroup(model);
                        else
                            data = jobCategoryService.InsertCRMGroup(model);
                        break;
                    case ModelType.CRMStatus:
                        if (model.Id > 0)
                            crmStatusService.UpdateCRMGroup(model);
                        else
                            data = crmStatusService.InsertCRMGroup(model);
                        break;
                    case ModelType.CRMEventType:
                        if (model.Id > 0)
                            evetypeService.UpdateModel(model);
                        else
                            data = evetypeService.InsertModel(model);
                        break;
                    case ModelType.CRMPlanProgram:
                        if (model.Id > 0)
                            planProgramService.UpdateModel(model);
                        else
                            data = planProgramService.InsertModel(model);
                        break;
                    case ModelType.CRMPriceStatus:
                        if (model.Id > 0)
                            priceStatusService.UpdateModel(model);
                        else
                            data = priceStatusService.InsertModel(model);
                        break;
                }
                return Json(new
                {
                    Success = true,
                    type = model.ModelType,
                    Message = "Tạo mới thành công",
                    Model = data
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error! " + ex.Message);
                Logger.Log(ex.ToString());
                return PartialView("_CRMBaseView", model);
            }

        }
        [HttpGet]
        public ActionResult EditBaseData(int id, ModelType modelType, bool isList = true)
        {
            CRMBaseModel data = new CRMBaseModel();
            switch (modelType)
            {
                case ModelType.CRMSource:
                    data = Mapper.Map<CRMBaseModel>(crmSourceService.FindEntity(x => x.Id == id));
                    break;
                case ModelType.CRMGroup:
                    ViewData["Groups"] = crmGroupService.GetAll(x => !x.ParentId.HasValue);
                    data = Mapper.Map<CRMBaseModel>(crmGroupService.FindEntity(x => x.Id == id));
                    break;
                case ModelType.CRMJobCategory:
                    data = Mapper.Map<CRMBaseModel>(jobCategoryService.FindEntity(x => x.Id == id));
                    break;
                case ModelType.CRMStatus:
                    data = Mapper.Map<CRMBaseModel>(crmStatusService.FindEntity(x => x.Id == id));
                    break;
                case ModelType.CRMEventType:
                    data = Mapper.Map<CRMBaseModel>(evetypeService.FindEntity(x => x.Id == id));
                    break;
                case ModelType.CRMPlanProgram:
                    data = Mapper.Map<CRMBaseModel>(planProgramService.FindEntity(x => x.Id == id));
                    break;
                case ModelType.CRMPriceStatus:
                    data = Mapper.Map<CRMBaseModel>(priceStatusService.FindEntity(x => x.Id == id));
                    break;
            }
            ViewBag.IsList = isList;
            var value = new
            {
                Views = this.RenderPartialView("_CRMBaseView", data),
                Title = string.Format(@"Sữa thông tin cho {0}", modelType.ToString()),
            };
            return JsonResult(value, true);

        }

        public ActionResult DelBaseData(int id, ModelType modelType)
        {
            CRMBaseModel data = new CRMBaseModel();
            switch (modelType)
            {
                case ModelType.CRMSource:
                    data = Mapper.Map<CRMBaseModel>(crmSourceService.FindEntity(x => x.Id == id));
                    crmSourceService.DeleteCRMGroup(data);
                    break;
                case ModelType.CRMGroup:
                    data = Mapper.Map<CRMBaseModel>(crmGroupService.FindEntity(x => x.Id == id));
                    crmGroupService.DeleteCRMGroup(data);
                    break;
                case ModelType.CRMJobCategory:
                    data = Mapper.Map<CRMBaseModel>(jobCategoryService.FindEntity(x => x.Id == id));
                    jobCategoryService.DeleteCRMGroup(data);
                    break;
                case ModelType.CRMStatus:
                    data = Mapper.Map<CRMBaseModel>(crmStatusService.FindEntity(x => x.Id == id));
                    crmStatusService.DeleteCRMGroup(data);
                    break;
                case ModelType.CRMEventType:
                    data = Mapper.Map<CRMBaseModel>(evetypeService.FindEntity(x => x.Id == id));
                    evetypeService.DeleteModel(data);
                    break;
                case ModelType.CRMPlanProgram:
                    data = Mapper.Map<CRMBaseModel>(planProgramService.FindEntity(x => x.Id == id));
                    planProgramService.DeleteModel(data);
                    break;
                case ModelType.CRMPriceStatus:
                    data = Mapper.Map<CRMBaseModel>(priceStatusService.FindEntity(x => x.Id == id));
                    priceStatusService.DeleteModel(data);
                    break;
            }
            var value = new
            {
                Views = "Bạn đã xoá thành công",
                Title = "Success!",
                IsRemve = true,
                ColumnClass = "col-md-6 col-md-offset-2",
                TdId = "del_" + id
            };
            return JsonResult(value, true);
        }

        public ActionResult CreateBaseData()
        {
            var model = new CRMBaseModel();
            model.Name = "Nội bộ";
            model.Description = "Thông tin từ nội bộ";
            crmSourceService.InsertCRMGroup(model);
            model.Name = "Khách hàng công ty";
            crmGroupService.InsertCRMGroup(model);
            model.Name = "May mặc";
            jobCategoryService.InsertCRMGroup(model);
            model.Name = "Theo dõi";
            crmStatusService.InsertCRMGroup(model);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Create CMR
        [HttpGet]
        public ActionResult Create()
        {
            GetBaseData();
            var model = new CRMCustomerModel();
            var status = crmStatusService.FindEntity(x => x.Code == (byte)CRMStatusCode.Potential);
            model.CRMStatus = status;
            model.DataType = CRMDataType.CRMNew;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CRMCustomerModel model)
        {
            if (!ModelState.IsValid || !CheckValidation(model))
            {
                GetBaseData();
                return View(model);
            }
            model.DataType = CRMDataType.CRMNew;
            model.CreatedBy = CurrenUser;
            model.CreatedDate = DateTime.Now;
            var id = crmService.InsertToDb(model);
            if (model.CRMContacts != null && model.CRMContacts.Any())
            {
                foreach (var ctModel in model.CRMContacts)
                {
                    ctModel.CmrCustomerId = id;
                    crmContactService.InsertToDb(ctModel);
                }
            }
            return RedirectToAction("Edit", new { id = id });
        }
        public ActionResult MoveCustomerToCRM(long id)
        {
            try
            {
                var cus = customerServices.GetById(id);
                if (cus == null)
                    return Json(new
                    {
                        Message = @"Không tìm thấy khách hàng này!",
                        Success = false,
                        dialog = true
                    }, JsonRequestBehavior.AllowGet);
                if (string.IsNullOrEmpty(cus.CompanyName) || string.IsNullOrEmpty(cus.FullName))
                {
                    return Json(new
                    {
                        Message = @"Tên khách hàng hiện tại không được hoàn thành, vui làng cập nhật tên khách hàng trước khi chuyển qua hệ thống CRM",
                        Success = false,
                        dialog = true
                    }, JsonRequestBehavior.AllowGet);
                }

                var existed = crmCustomerService.FindEntity(x => x.CompanyShortName == cus.CompanyName);
                if (existed != null)
                {
                    return Json(new
                    {
                        Message = string.Format(@"Tên khách hàng {0} đã tồn tại trong CRM với Id{1}", existed.CompanyShortName, existed.Id),
                        Success = false,
                        dialog = true
                    }, JsonRequestBehavior.AllowGet);
                }
                var crmCus = new CRMCustomerModel
                {
                    DataType = CRMDataType.SsmCustomer,
                    CompanyName = string.IsNullOrEmpty(cus.FullName) ? "[nhap ten cong ty]" : cus.FullName,
                    CompanyShortName = string.IsNullOrEmpty(cus.CompanyName) ? "[nhap ten viet tat]" : cus.CompanyName,
                    CreatedDate = DateTime.Now,
                    CustomerType = (CustomerType)Enum.Parse(typeof(CustomerType), cus.CustomerType),
                    CrmAddress = string.IsNullOrEmpty(cus.Address) ? "[nhap dia chi]" : cus.Address,
                    Description = cus.Description,
                    SsmCusId = cus.Id,
                    CreatedBy = CurrenUser
                };

                crmCus.CRMStatus = crmStatusService.FindEntity(x => x.Code == (byte)CRMStatusCode.Potential);
                saleTypes = saleTypes ?? usersServices.getAllSaleTypes(true);
                crmCus.SaleType = saleTypes.FirstOrDefault(x => x.Active == true);
                crmCus.CRMJobCategory = jobCategoryService.GetQuery().FirstOrDefault();
                crmCus.CRMGroup = crmGroupService.GetQuery().FirstOrDefault();
                crmCus.CRMSource = crmSourceService.GetQuery().FirstOrDefault();
                crmCus.Province = provinceService.GetById(2075);
                long newCusid = crmService.InsertToDb(crmCus);
                if (id > 0)
                {
                    var contact = new CRMContactModel()
                    {
                        FullName = "[nhap ten lien lac]",
                        Phone = string.IsNullOrEmpty(cus.PhoneNumber) ? "[nhap so dien thoai]" : cus.PhoneNumber,
                        Email = string.IsNullOrEmpty(cus.Email) ? "[nhap email]" : cus.Email,
                        Phone2 = cus.Fax,
                        CmrCustomerId = newCusid

                    };
                    crmContactService.InsertToDb(contact);
                    cus.IsMove = true;
                    cus.MovedBy = CurrenUser.Id;
                    cus.CrmCusId = newCusid;
                    customerServices.Commited();
                }
                return Json(new
                {
                    Message = @"Customer đã chuyển qua CRM thành công",
                    Success = true,
                    dialog = true,
                    MoveBy = CurrenUser.FullName,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(new
                {
                    Message = ex.Message,
                    Success = false,
                    dialog = true,
                    MoveBy = CurrenUser.FullName,
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
        #region Edit CRM
        [HttpGet]
        public ActionResult Edit(long id)
        {
            // var db = crmCustomerService.GetById(id);
            filterModel = (CRMSearchModel)Session[CRM_SEARCH_MODEL];
            filterModel = filterModel ?? new CRMSearchModel();
            ViewBag.searchTime = "";
            var model = crmCustomerService.GetModelById(id);
            CRMStatus status = SetSatusCrm(model, filterModel);
            model.CRMStatus = status;
            model.Summary = crmCustomerService.Summary(id, filterModel.FromDate.Value, filterModel.ToDate.Value);
            if (filterModel.ToDate != null || filterModel.FromDate != null)
            {
                ViewBag.searchTime =
                    $"Status [{model.CRMStatus.Name}] for period time from {filterModel.FromDate ?? DateTime.MinValue:dd/MM/yyyy} to {(filterModel.ToDate ?? DateTime.Now):dd/MM/yyyy}";
            }
            GetBaseData();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CRMCustomerModel model)
        {
            filterModel = (CRMSearchModel)Session[CRM_SEARCH_MODEL];
            var cus = crmCustomerService.GetById(model.Id);
            model.CreatedBy = usersServices.getUserById(cus.CreatedById.Value);
            if (filterModel == null)
            {
                filterModel = new CRMSearchModel()
                {
                    FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    ToDate = DateTime.Now
                };
            }
            else
            {
                filterModel.FromDate = filterModel.FromDate ?? new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                filterModel.ToDate = filterModel.ToDate ?? DateTime.Now;

            }
            if (!ModelState.IsValid || !CheckValidation(model))
            {
                var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
                model.Summary = crmCustomerService.Summary(model.Id, filterModel.FromDate.Value, filterModel.ToDate.Value);
                GetBaseData();
                return View(model);
            }
            try
            {

                //  model.Summary = crmCustomerService.Summary(model.Id, filterModel.FromDate.Value, filterModel.ToDate.Value);
                var oldSales = cus.User;
                model.ModifyBy = CurrenUser;
                model.ModifyDate = DateTime.Now;
                crmService.UpdateToDb(model);
                if (crmContactService.DeleteAllCtOfCus(model.Id))
                {
                    foreach (var contact in model.CRMContacts)
                    {
                        contact.CmrCustomerId = model.Id;
                        crmContactService.InsertToDb(contact);
                    }
                }
                if (model.MoveToId != null && model.MoveToId > 0)
                {
                    HistoryWirter(model, oldSales);
                    var newUser = usersServices.FindEntity(x => x.Id == model.MoveToId);
                    if (!string.IsNullOrEmpty(newUser.Email))
                    {
                        SendEmailChangeUserFollow(cus, oldSales, newUser);
                        Logger.LogError($"Send email Move {cus.CompanyName}/{cus.Id}");
                    }
                }
                return RedirectToAction("Edit", new { id = model.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                model.Summary = crmCustomerService.Summary(model.Id, filterModel.FromDate.Value, filterModel.ToDate.Value);
                GetBaseData();
                return View(model);
            }
        }

        public void SendEmailChangeUserFollow(CRMCustomer cus, User fromUser, User toUser)
        {
            string absUrl = Url.Action("Edit", "CRM", new { id = cus.Id }, Request.Url.Scheme);
            var link = $"<a href=\"{absUrl}\" >click vào đây</a>";
            EmailModel model = new EmailModel { Subject = $"{toUser.FullName } bạn có chuyển theo dõi khách hàng từ {fromUser.FullName}" };
            var content = $"Dear {toUser.FullName } <br/> Bạn được yêu cầu theo dõi khách hàng {cus.CompanyName}-{cus.Id} từ {fromUser.FullName}. Bạn có thể {link} để xem chi tiết";
            model.User = fromUser;
            if (string.IsNullOrEmpty(fromUser.Email))
            {
                model.User = usersServices.FindEntity(x => x.UserName == "admin" && x.RoleName == "Administrator");
            }
            model.Message = content;
            model.EmailTo = toUser.Email;
            model.IsUserSend = true;
            var email = new EmailCommon { EmailModel = model };
            string errorMessages;
            email.SendEmail(out errorMessages, true);

        }
        #endregion
        #region Suggest Function
        public JsonResult CRMCustomerSuggest(string term)
        {
            var qr = crmCustomerService.GetQuery(x => x.CompanyShortName.ToLower().Contains(term.ToLower()));
            if (!CurrenUser.IsDirecter())
            {
                if (CurrenUser.IsDepManager())
                    qr = qr.Where(x => x.User.DeptId == CurrenUser.DeptId);
                else
                    qr = qr.Where(x => x.CreatedById == CurrenUser.Id);
            }
            var result = qr
                .OrderBy(x => x.CompanyShortName).Take(20)
                .Select(x => new { id = x.Id, Display = x.CompanyShortName })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CategorySuggest(string term)
        {
            var result = jobCategoryService.GetQuery(x => x.Name.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.Name).Take(20)
                .Select(x => new { id = x.Id, Display = x.Name })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ProvinceSuggest(string term, long countryId)
        {
            var result = provinceService.GetQuery(x => x.Name.ToLower().Contains(term.ToLower()) && countryId == x.CountryId)
                .OrderBy(x => x.Name).Take(20)
                .Select(x => new { id = x.Id, Display = x.Name })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CoutrySuggest(string term)
        {
            var result = provinceService.GetCountrys(x => x.CountryName.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.CountryName).Take(20)
                .Select(x => new { id = x.Id, Display = x.CountryName })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserSuggest(string term)
        {
            var result = usersServices.GetQuery(x => x.FullName.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.FullName).Take(20)
                .Select(x => new { id = x.Id, Display = x.FullName })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserSuggestFollow(string term, string notyetId)
        {
            var listNotYet = notyetId.Split('-').Where(x => !string.IsNullOrEmpty(x)).Select(x => long.Parse(x)).ToList();
            var result = usersServices.GetQuery(x => !listNotYet.Contains(x.Id) && x.FullName.Contains(term.Trim()) && (
             UsersModel.Positions.Operations.ToString().Equals(x.Department.DeptFunction) || UsersModel.Positions.Sales.ToString().Equals(x.Department.DeptFunction)))
                .OrderBy(x => x.FullName).Take(20)
                .Select(x => new { id = x.Id, Display = x.FullName })
                .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DepartmentSuggest(string term)
        {
            var result = usersServices.GetAllDepartmentActive(CurrenUser, true)
                .OrderBy(x => x.DeptName).Take(20)
                .ToList()
                .Select(x => new { id = x.Id, Display = x.DeptName });
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SuggestUser(string term)
        {
            var result = usersServices.GetAllSales(CurrenUser, false).Where(x => x.FullName.ToLower().Contains(term.ToLower()))
                .OrderBy(x => x.FullName).Take(20)
                .ToList()
                .Select(x => new { id = x.Id, Display = x.FullName, DeptName = x.Department.DeptName }).Distinct();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CustomerSuggest(string term)
        {
            var result =
                crmCustomerService.GetQuery(
                    x =>
                        x.CreatedById == CurrenUser.Id && x.CompanyShortName.ToLower().Contains(term.ToLower()) &&
                        x.IsUserDelete == false);
            result = result.OrderBy(x => x.CompanyShortName);
            var list = result.Take(20).Select(x => new { id = x.Id, Display = x.CompanyShortName });
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region CRM function helpers
        public ActionResult BlankContactRow()
        {
            return PartialView("_CrmContractView", new CRMContactModel());
        }
        private bool CheckValidation(CRMCustomerModel model)
        {
            bool valid = true;

            if (model.CrmCountryId == 0 && !string.IsNullOrEmpty(model.CountryName))
            {
                ModelState.AddModelError("CountryName", "Quốc gia không đúng vui lòng chọn lại");
                valid = false;
            }
            if (model.Province.Id == 0 && !string.IsNullOrEmpty(model.StateName))
            {
                ModelState.AddModelError("StateName", "Tỉnh thành không phù hợp vui lòng chọn lại");
                valid = false;
            }
            var existed = crmCustomerService.FindEntity(x => x.CompanyShortName == model.CompanyShortName && x.Id != model.Id);
            if (existed != null)
            {
                ModelState.AddModelError("CompanyShortName", "Tên viết tắt đã tồn tại trong hệ thống CRM");
                valid = false;
            }
            /*var existphone = crmCustomerService.FindEntity(x => x.CompanyShortName == model.CompanyShortName && x.Id != model.Id);
            if (existed != null)
            {
                ModelState.AddModelError("CompanyShortName", "Tên viết tắt đã tồn tại trong hệ thống CRM");
                valid = false;
            }*/
            var duplicates = model.CRMContacts.GroupBy(s => s.Email)
                .SelectMany(grp => grp.Skip(1));
            if (duplicates.Any())
            {
                ModelState.AddModelError("", $"Email không được trùng...");
                valid = false;
            }
            foreach (var contact in model.CRMContacts)
            {

                var ctphone = crmContactService.Any(x => x.CmrCustomerId != model.Id && x.Phone == contact.Phone);
                if (ctphone)
                {
                    ModelState.AddModelError("", $"Số điện thoại {contact.Phone} đã tồn tại ở khách hàng khác");
                    valid = false;
                }
                var ctemail = crmContactService.Any(x => x.CmrCustomerId != model.Id && x.Email == contact.Email);
                if (ctemail)
                {
                    ModelState.AddModelError("", $"Địa chỉ email {contact.Email} đã tồn tại ở khách hàng khác");
                    valid = false;
                }

            }
            return valid;
        }
        #endregion
        #region CRM Move customer

        public ActionResult ResetCrmCus(long id)
        {
            var crmCus = crmCustomerService.GetById(id);
            crmCus.CrmStatusId = 1;
            crmCus.DateCancel = DateTime.Now;
            crmCustomerService.Commited();
            return RedirectToAction("Edit", new { id = id });
        }
        public ActionResult MoveToCus(long id)
        {
            var crmCus = crmCustomerService.GetModelById(id);
            if (crmCus == null)
            {
                return Json(new
                {
                    Message = "Không tim thấy CRM Customer với ID=" + id.ToString("D6"),
                    Success = false,
                    dialog = true,
                    MoveBy = CurrenUser.FullName,
                }, JsonRequestBehavior.AllowGet);
            }
            //if (crmCus.DataType == CRMDataType.SsmCustomer)
            //{
            //    return Json(new
            //    {
            //        Message = "Khách hàng này đã tồn tại ở Data customer",
            //        Success = false,
            //        dialog = true,
            //        MoveBy = CurrenUser.FullName,
            //    }, JsonRequestBehavior.AllowGet);
            //}
            try
            {
                var cus = new CustomerModel();
                if (crmCus.SsmCusId.HasValue)
                {
                    cus = customerServices.GetModelById(crmCus.SsmCusId.Value) ?? new CustomerModel();
                }
                cus.FullName = crmCus.CompanyName;
                cus.CompanyName = crmCus.CompanyShortName;
                cus.Type = CustomerType.ShipperCnee.ToString();
                cus.Address = crmCus.CrmAddress;
                cus.PhoneNumber = crmCus.CrmPhone;
                if (crmCus.Province != null)
                    cus.Address += ", " + crmCus.StateName + ", " + crmCus.CountryName;
                cus.Description = crmCus.Description;
                var contact = crmCus.CRMContacts.FirstOrDefault();
                if (contact != null)
                {
                    var cts = string.Format("Thông tin liên hệ Name:{0}-Phone:{1}-Email:{2}", contact.FullName,
                        contact.Phone, contact.Email);
                    cus.Description = cus.Description + "\n" + cts;
                }
                cus.Fax = crmCus.CrmPhone;
                cus.UserId = crmCus.CreatedBy.Id;
                cus.IsMove = true;
                cus.CrmCusId = crmCus.Id;
                cus.MovedUserId = CurrenUser.Id;
                if (crmCus.SsmCusId.HasValue)
                {
                    customerServices.UpdateCustomer(cus);
                }
                else
                {
                    var nCus = customerServices.InsertCustomer(cus);
                    if (nCus != null)
                    {
                        crmService.SetMoveCustomerToData(crmCus.Id, true, nCus.Id);
                    }

                }
                return Json(new
                {
                    Message = "Đã chuyển sang Customer list thành công",
                    Success = true,
                    dialog = true,
                    MoveBy = CurrenUser.FullName,
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                return Json(new
                {
                    Message = ex.Message,
                    Success = false,
                    dialog = true,
                    MoveBy = CurrenUser.FullName,
                }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        public ActionResult Delete(long id)
        {
            var cus = crmCustomerService.GetModelById(id);
            if (cus.Summary != null && cus.Summary.TotalShippments > 0)
            {
                var value = new
                {
                    Views = "this customer have shipment, you can not delete it",
                    Title = "Error!",
                    IsRemve = false,
                    Success = false,
                    TdId = "del_" + id
                };
                return JsonResult(value, true);
            }
            if (!CurrenUser.IsDirecter())
            {
                cus.IsUserDelete = true;
                cus.ModifyBy = CurrenUser;
                cus.ModifyDate = DateTime.Now;
                crmService.UpdateToDb(cus);
            }
            else
            {

                try
                {
                    var contacts = crmContactService.GetAll(x => x.CmrCustomerId == id);
                    if (contacts.Any())
                        crmContactService.DeleteAll(contacts);
                    var prices = priceQuotationService.GetAll(x => x.CrmCusId == id);
                    if (prices.Any())
                    {
                        foreach (var price in prices)
                        {
                            var emailhist = emailHistoryService.GetAll(x => x.PriceId == price.Id);
                            if (emailhist.Any())
                            {
                                emailHistoryService.DeleteAll(emailhist);
                            }
                            priceQuotationService.Delete(price);
                        }
                    }

                    var events = eventService.GetAll(x => x.CrmCusId == id);
                    if (events.Any())
                    {
                        foreach (var crmVisit in events)
                        {
                            var filelist = fileService.GetServerFile(crmVisit.Id, new CRMEventModel().GetType().ToString());
                            if (filelist.Any())
                            {
                                foreach (var serverFile in filelist)
                                {
                                    if (serverFile == null) continue;
                                    if (System.IO.File.Exists(Server.MapPath(serverFile.Path)))
                                        System.IO.File.Delete(Server.MapPath(serverFile.Path));
                                }
                            }
                            var followsEv = followEventService.GetAll(x => x.VisitId == id);
                            if (followsEv.Any())
                            {
                                followEventService.DeleteAll(followsEv);
                            }
                        }

                        eventService.DeleteAll(events);
                    }
                    var docs = documentService.GetAll(x => x.CrmCusId == id);
                    if (docs.Any())
                    {
                        foreach (var doc in docs)
                        {
                            var filelist = fileService.GetServerFile(doc.Id, new CrmCusDocumentModel().GetType().ToString());
                            if (filelist.Any())
                            {
                                foreach (var serverFile in filelist)
                                {
                                    if (serverFile != null)
                                        if (System.IO.File.Exists(Server.MapPath(serverFile.Path)))
                                            System.IO.File.Delete(Server.MapPath(serverFile.Path));
                                    fileService.Delete(serverFile);
                                }
                            }
                        }
                        documentService.DeleteAll(docs);
                    }

                    if (cus.SsmCusId.HasValue && cus.SsmCusId.Value > 0)
                    {
                        var ssmcus = customerServices.GetById(cus.SsmCusId.Value);
                        if (ssmcus != null)
                        {
                            ssmcus.CrmCusId = (long?)null;
                            ssmcus.IsMove = false;
                            ssmcus.MovedBy = (long?)null;
                            customerServices.Commited();
                        }

                    }
                    var follows = followCustomerService.GetAll(x => x.CrmId == id);
                    if (follows.Any())
                    {
                        followCustomerService.DeleteAll(follows);
                    }
                    crmCustomerService.DeleteToDb(cus);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                    var value = new
                    {
                        Views = e.Message,
                        Title = "Error!",
                        IsRemve = false,
                        Success = false,
                        TdId = "del_" + id
                    };
                    return JsonResult(value, true);
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
        public ActionResult UndoCustomer(long id)
        {
            var cus = crmCustomerService.GetById(id);
            if (CurrenUser.IsDirecter())
            {
                cus.IsUserDelete = false;
                cus.ModifyById = CurrenUser.Id;
                cus.ModifyDate = DateTime.Now;
                crmCustomerService.Commited();
            }
            var value = new
            {
                Views = "Bạn đã khôi phục hành công",
                Title = "Success!",
                IsRemve = false,
                IsRefreshList = true,
                TdId = "del_" + id
            };
            return JsonResult(value, true);
        }
        public void HistoryWirter(CRMCustomerModel customer, User user)
        {


            var history = new HistoryModel()
            {
                HistoryMessage = String.Format(@"{0} was move created by from {1}-{2} to userId {3}", CurrenUser.FullName, user.Id, user.FullName, customer.MoveToId),
                UserId = CurrenUser.Id,
                ActionName = "Move user crmCustomer",
                CreateTime = DateTime.Now,
                ObjectId = customer.Id,
                ObjectType = new CRMCustomerModel().GetType().ToString(),
                IsLasted = false,
                Id = 0,
                IsRevisedRequest = false
            };
            historyService.Save(history);
        }
        public ActionResult GetSummary(long id)
        {
            filterModel = (CRMSearchModel)Session[CRMController.CRM_SEARCH_MODEL];
            var data = crmCustomerService.Summary(id, filterModel.FromDate.Value, filterModel.ToDate.Value);
            return PartialView("_summary", data);
        }

        public ActionResult GetSummaryList(long id)
        {
            filterModel = (CRMSearchModel)Session[CRMController.CRM_SEARCH_MODEL];
            var data = crmCustomerService.Summary(id, filterModel.FromDate.Value, filterModel.ToDate.Value);
            return PartialView("_ListSummaryCount", data);
        }
        public ActionResult ShipmentDetail(long id)
        {
            filterModel = (CRMSearchModel)Session[CRMController.CRM_SEARCH_MODEL];
            ViewBag.ShimentPeriod = null;
            var cus = crmCustomerService.GetById(id);
            ViewBag.ProfitType = cus.CompanyName;
            var data = crmCustomerService.GetListShipments(cus.SsmCusId.Value);
            if (data.Any() && filterModel != null)
            {
                if (filterModel.FromDate != null && filterModel.ToDate != null)
                {
                    var shipementForPeriod = data.Where(
                          x => x.DateShp >= filterModel.FromDate && x.DateShp <= filterModel.ToDate).ToList();
                    ViewBag.ShimentPeriod = shipementForPeriod;
                    data = data.Where(x => x.DateShp < filterModel.FromDate).ToList();

                }

            }
            var value = new
            {
                Views = this.RenderPartialView("_ShipmentList", data),
                CloseOther = true,
                Title = string.Format(@"Shipment detail"),
            };
            return JsonResult(value, true);
        }
        public ActionResult SalseTradingDetail(long id)
        {
            var cus = crmCustomerService.GetById(id);
            ViewBag.ProfitType = cus.CompanyName;
            var data = crmCustomerService.GetListSalesTrading(cus.SsmCusId.Value);
            var value = new
            {
                Views = this.RenderPartialView("_ListSalesTrading", data),
                CloseOther = true,
                Title = string.Format(@"Customer Trading detail"),
            };
            return JsonResult(value, true);
        }
        public int Days
        {
            get
            {
                int day = 365;
                var crmstting = usersServices.CRMDayCanelSettingNumber();
                day = int.Parse(crmstting.DataValue);
                return day;
            }
        }
        public CRMStatus SetSatusCrm(CRMCustomerModel crm, CRMSearchModel flModel)
        {
            CRMStatusCode code = CRMStatusCode.Potential;
            if (!crm.SsmCusId.HasValue) return crmStatusService.FindEntity(x => x.Code == (byte)code);
            flModel.FromDate = flModel.FromDate ?? crm.CreatedDate;
            flModel.ToDate = flModel.ToDate ?? DateTime.Now;

            var lastShipment = shipmentServices.GetQuery(s => s.CneeId.Value == crm.SsmCusId.Value || s.ShipperId.Value == crm.SsmCusId.Value);
            // var isCancel = lastShipment.Any(x => x.DateShp.Value.AddDays(days) <= flModel.FromDate);
            var isClient = lastShipment.Any(x => x.DateShp <= flModel.FromDate);
            var shipmentCurrent = lastShipment.Count(x => x.DateShp >= flModel.FromDate && x.DateShp <= flModel.ToDate);

            if (isClient)
                code = CRMStatusCode.Client;
            else
                code = shipmentCurrent >= 1 ? CRMStatusCode.Success : CRMStatusCode.Potential;

            return crmStatusService.FindEntity(x => x.Code == (byte)code);
        }
        #region User follow manager
        public ActionResult GetUserFollowDialog(long id, string name)
        {
            var customer = crmCustomerService.GetById(id);
            ViewBag.Customer = customer;
            var mode = followCustomerService.GetListModelsByCus(id);
            if (!mode.Any())
            {
                mode = new List<CRMFollowCusUserModel>();
            }
            var view = this.RenderPartialView("_UserFollowList", mode);
            var value = new
            {
                Views = view,
                CloseOther = true,
                Title = string.Format(@"Control user Follow for customer {0}", name),
            };
            return JsonResult(value, true);
        }
        [HttpPost]
        public ActionResult AddFollow(long cusId, long userId)
        {
            var exited = followCustomerService.Any(x => x.CrmId == cusId && x.UserId == userId);
            if (exited)
            {
                return Json(new
                {
                    Message = "Sale nay đã tồn tại trong theo dõi",
                    Success = false,
                    dialog = true
                }, JsonRequestBehavior.AllowGet);
            }
            var mode = new CRMFollowCusUserModel()
            {
                CrmId = cusId,
                UserId = userId,
                AddById = CurrenUser.Id
            };
            var id = followCustomerService.InsertToDb(mode);
            var db = followCustomerService.GetModelById(id);
            var view = this.RenderPartialView("_UserFollowItem", db);
            var tr = string.Format("<tr id='user_{0}'>{1}</tr>", db.Id, view);
            return Json(tr, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleleUserFollow(long id)
        {
            var db = followCustomerService.GetModelById(id);
            if (!db.IsLook)
            {
                followCustomerService.DeleteToDb(id);
                if (db.UserId == CurrenUser.Id && CurrenUser.IsStaff())
                {
                    var value2 = new
                    {
                        Views = "Bạn đã xoá thành công",
                        Title = "Success!",
                        Redirect = true,
                        Url = Url.Action("Index", "CRM")
                    };
                    return JsonResult(value2, true);
                }
                var value = new
                {
                    Views = "Bạn đã xoá thành công",
                    Title = "Success!",
                    IsRemve = true,
                    TdId = "del_" + id
                };
                return JsonResult(value, true);
            }
            else
            {
                var result = new CommandResult(false)
                {
                    ErrorResults = new[] { "Sale is look by owner, you can not leave!" }
                };
                return JsonResult(result, null, true);
            }
        }
        public ActionResult SetLookUser(long id, bool isLook)
        {
            var db = followCustomerService.GetModelById(id);
            if (db.IsLook && db.LockById != CurrenUser.DeptId && CurrenUser.IsDepManager() && !CurrenUser.IsAdmin())
            {
                var value = new
                {
                    Views = "This locked by director. Please contact him for unlock",
                    Title = "Error!",
                    ColumnClass = "col-md-6 col-md-offset-3"

                };
                return JsonResult(value, true);
            }
            followCustomerService.Look(id, isLook, CurrenUser);
            db = followCustomerService.GetModelById(id);
            var view = this.RenderPartialView("_UserFollowItem", db);
            return Json(view, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetFollowUser(long id)
        {
            var cus = crmCustomerService.GetModelById(id);
            var str = string.Empty;
            if (cus.FollowCusUsers != null && cus.FollowCusUsers.Any())
            {
                str = cus.FollowCusUsers.Aggregate("", (current, u) => current + (u.User.FullName + ";"));
            }
            return PartialView("_FollowUserlistView", str);
        }
        #endregion
        public ActionResult CrmSetting()
        {
            SettingModel setting = new SettingModel();
            var db = settingService.GetByDataCode(SettingModel.CRM_DAYCANCEL_SETTING);
            setting.PageNumber = db == null ? "0" : db.DataValue;
            setting.Id = db == null ? 0 : db.Id;
            return PartialView("_crmSettingView", setting);
        }
        [HttpPost]
        public ActionResult CrmSetting(SettingModel setting)
        {
            var db = settingService.GetByDataCode(SettingModel.CRM_DAYCANCEL_SETTING);
            db.DataValue = setting.PageNumber;
            settingService.Commited();
            return PartialView("_crmSettingView", setting);
        }

        public ActionResult ExportExcel()
        {
            filterModel = (CRMSearchModel)Session[CRM_SEARCH_MODEL];
            filterModel = filterModel ?? new CRMSearchModel
            {
                PeriodDate = PeriodDate.CreateDate,
                FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                ToDate = DateTime.Now,
            };
            var totalRow = 0;
            var sort = new SSM.Services.SortField("CompanyShortName", true);
            var listData = crmCustomerService.GetAllSearchNotPaging(filterModel, sort, CurrenUser, out totalRow);
            if (totalRow == 0) return View();
            var exportData = listData.ToList().Select(x => new
            {
                Id = $"'{x.Customer.Id:D6}",
                AbbName = x.Customer.CompanyShortName,
                CustomerName = x.Customer.CompanyName,
                ContactName = x.Customer?.CRMContacts?.FirstOrDefault()?.FullName,
                Email = x.Customer.CRMContacts?.FirstOrDefault()?.Email,
                Phone = $"'{x.Customer.CRMContacts?.FirstOrDefault()?.Phone}",
                Description = x.Customer.Description,
                Status = x.Status.Name,
                CreatedDate = x.Customer.CreatedDate.ToString("dd/MM/yyyy"),
                UpDate = x.Customer.ModifyDate?.ToString("dd/MM/yyyy"),
                Quotation = $"{x.TotalQuotation:#,###}",
                Visit = $"{x.TotalVisit:#,###}",
                Shipment = $"{ x.TotalShipment:#,###}",
                SaleType = x.SaleType,
                SalesName = x.CreaterdBy.FullName,
                Source = x.Source,
                Follow = x.FollowName,

            }).ToList();
            IExcelExportEngine engine = new ExcelExportEngine();
            engine.SetFormat(ExcelVersion.XLSX);
            engine.AddData(exportData);
            var fileName = $"CrmExport_{DateTime.Now:yyyy-MM-dd-HH-mm-ss}.xlsx";
            //var filePath = Server.MapPath("//")+ fileName;
            MemoryStream memory = engine.Export();

            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);

        }
    }
}