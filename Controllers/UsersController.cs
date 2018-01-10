using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DotNetOpenAuth.Messaging;
using SSM.Common;

using SSM.Models;
using SSM.Services;
using SSM.Utils;
using SSM.ViewModels.Shared;

namespace SSM.Controllers
{
    [HandleError]
    public partial class UsersController : BaseController
    {
        private UsersServices UsersServices1 { get; set; }
        private User User1;
        private SelectList Positions
        {
            get
            {
                var positions = from UsersModel.DisplayPositions p in Enum.GetValues(typeof(UsersModel.DisplayPositions))
                                select new { Id = p, Name = p.ToString() };
                return new SelectList(positions, "Id", "Name");
            }

        }
        private SelectList PositionsAll
        {
            get
            {
                var positions = from UsersModel.Positions p in Enum.GetValues(typeof(UsersModel.Positions))
                                select new { Id = p, Name = p.ToString() };
                return new SelectList(positions, "Id", "Name");
            }

        }
        private SelectList Functions
        {
            get
            {
                var functions = from UsersModel.Functions p in Enum.GetValues(typeof(UsersModel.Functions))
                                select new { Id = p, Name = p.ToString() };
                return new SelectList(functions, "Id", "Name");
            }

        }
        private SelectList SaleTypes
        {
            get
            {
                var functions = from ShipmentModel.SaleTypes st in Enum.GetValues(typeof(ShipmentModel.SaleTypes))
                                select new { Id = st, Name = st.ToString() };
                return new SelectList(functions, "Id", "Name");
            }

        }
        private SelectList Levels
        {
            get
            {
                var Levels = from UsersModel.Levels l in Enum.GetValues(typeof(UsersModel.Levels))
                             select new { Id = (int)(UsersModel.Levels)l + 1, Name = l.GetStringLabel() };
                return new SelectList(Levels, "Id", "Name");
            }

        }
        private Grid<UsersModel> userGrid;
        public static String USER_SEARCH_MODEL = "USER_SEARCH_MODEL";
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            User1 = (User)Session[AccountController.USER_SESSION_ID];
            UsersServices1 = new UsersServicesImpl();
            ViewData["Companies"] = new SelectList(UsersServices1.getAllCompany(), "Id", "CompanyName");
            ViewData["AllCompanies"] = UsersServices1.getAllCompany();
            ViewData["Departments"] = new SelectList(UsersServices1.GetAllDepartmentActive(CurrenUser, true), "Id", "DeptName");
            ViewData["Positions"] = Positions;
            ViewData["PositionsAll"] = PositionsAll;
            ViewData["Functions"] = Functions;
            ViewData["Levels"] = Levels;
            ViewData["SaleTypesEnum"] = SaleTypes;

        }

        //
        // GET: /Users/List
        public ActionResult Index()
        {
            userGrid = (Grid<UsersModel>)Session[USER_SEARCH_MODEL];
            if (userGrid == null)
            {
                userGrid = new Grid<UsersModel>
                            (
                                new Pager
                                {
                                    CurrentPage = 1,
                                    PageSize = 10,
                                    Sord = "asc",
                                    Sidx = "FullName"
                                }
                            );
                userGrid.SearchCriteria = new UsersModel();
            }
            UpdateGridUserData();

            return View(userGrid);
        }
        [HttpPost]
        public ActionResult Index(Grid<UsersModel> grid)
        {
            userGrid = grid;
            Session[USER_SEARCH_MODEL] = grid;
            try
            {
                userGrid.ProcessAction();
                UpdateGridUserData();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
            return PartialView("_ListData", userGrid);
        }
        private void UpdateGridUserData()
        {
            var totalRow = 0;
            var page = userGrid.Pager;
            var sort = new SSM.Services.SortField(string.IsNullOrEmpty(page.Sidx) ? "FullName" : page.Sidx, page.Sord == "asc");
            var model = userGrid.SearchCriteria ?? new UsersModel();

            var user = UsersServices1.GetQuery(x =>
                (string.IsNullOrEmpty(model.UserName) || x.UserName.Contains(model.UserName))
               && (string.IsNullOrEmpty(model.FullName) || x.FullName.Contains(model.FullName))
               && (string.IsNullOrEmpty(model.RoleName) || x.RoleName == model.RoleName));
            user = user.OrderBy(sort);
            totalRow = user.Count();
            userGrid.Pager.Init(totalRow);
            if (totalRow == 0)
            {
                userGrid.Data = new List<UsersModel>();
                return;
            }

            var list = UsersServices1.GetListPager(user, page.CurrentPage, page.PageSize);
            var listUserModel = list.Select(u => UsersServicesImpl.ConvertModel(u)).ToList();
            userGrid.Data = listUserModel;
        }

        // GET: /Users/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }


        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            UsersModel UsersModel1 = new UsersModel();
            UsersModel1.RoleName = UsersModel.Positions.Sales.ToString();
            return View(UsersModel1);
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public ActionResult Create(UsersModel UsersModel1, long[] CompanyControls)
        {
            if (ModelState.IsValid)
            {
                if (UsersServices1.getUserById(UsersModel1.UserName) != null)
                {
                    ModelState.AddModelError("UserName", "UserName have existed");
                    return View(UsersModel1);
                }
                try
                {
                    UsersModel1.IsActive = true;
                    long UserId1 = UsersServices1.createUser(UsersModel1);
                    if (CompanyControls.Count() <= 0)
                    {
                        CompanyControls[0] = UsersModel1.ComId;
                    }
                    UsersServices1.createCompanyControl(UserId1, CompanyControls);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    e.GetBaseException();
                    return View(UsersModel1);
                }
            }
            return View(UsersModel1);
        }
        //
        // GET: /Users/Edit/5
        public ActionResult Detail(int id)
        {
            UsersModel Model1 = UsersServices1.getUserModelById((long)id);
            if (Model1 == null)
            {
                return View("Index");

            }
            return View(Model1);
        }

        public ActionResult Edit(int id)
        {
            UsersModel Model1 = UsersServices1.getUserModelById((long)id);
            if (Model1 == null)
            {
                return View("Create");

            }
            return View(Model1);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(UsersModel UsersModel1, long[] CompanyControls)
        {
            try
            {
                // TODO: Add update logic here 
                UsersServices1.updateUser(UsersModel1);
                UsersServices1.createCompanyControl(UsersModel1.Id, CompanyControls);
                return RedirectToAction("Index");
            }
            catch (SqlException ex)
            {
                throw;
            }
            catch (Exception e)
            {
                throw e;
                return View(UsersModel1);
            }
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int Id)
        {
            User user = null;
            bool result = UsersServices1.deleteUserById(Id);
            List<UsersModel> ListUser1 = UsersServices1.getAllUser();
            ViewData["ListUser1"] = ListUser1;
            if (!result)
            {
                user = UsersServices1.getUserById(Id);
                ViewData["deleteError"] = true;
            }
            return RedirectToAction("Index");
        }

        //
        // POST: /Users/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Department(long id)
        {
            IEnumerable<Department> DepartmentList1 = UsersServices1.getAllDepartment();
            ViewData["DepartmentList1"] = DepartmentList1;
            Department Department1 = UsersServices1.getDepartmentById(id);
            if (Department1 == null)
            {
                var model = new DepartmentModel() { IsActive = true };
                return View(model);
            }
            ViewData["ModifyDept"] = "ModifyDept";
            DepartmentModel DepartmentModel1 = UsersServices1.getDepartmentModelById(id);
            return View(DepartmentModel1);
        }
        [HttpPost]
        public ActionResult Department(DepartmentModel DepartmentModel1)
        {
            if (ModelState.IsValid)
            {
                Department Department1 = UsersServices1.getDepartmentById(DepartmentModel1.Id);
                if (Department1 != null)
                {
                    UsersServices1.updateDepartment(DepartmentModel1);
                }
                else
                {
                    UsersServices1.insertDepartment(DepartmentModel1);
                }
            }
            else
            {
                ViewData["ModifyDept"] = "ModifyDept";
            }
            IEnumerable<Department> DepartmentList1 = UsersServices1.getAllDepartment();
            ViewData["DepartmentList1"] = DepartmentList1;
            return View(DepartmentModel1);
        }
        public ActionResult DepartmentDelete(long Id)
        {
            UsersServices1.deleteDeptById(Id);
            return RedirectToAction("Department", new { id = -1 });
        }

        public ActionResult Company(long id)
        {
            IEnumerable<Company> CompanyList1 = UsersServices1.getAllCompany();
            ViewData["CompanyList1"] = CompanyList1;
            Company Company1 = UsersServices1.getCompanyById(id);
            if (Company1 == null)
            {
                return View();
            }
            ViewData["ModifyCom"] = "ModifyCom";
            CompanyModel CompanyModel1 = UsersServices1.getCompanyModelById(id);
            return View(CompanyModel1);
        }
        [HttpPost]
        public ActionResult Company(CompanyModel CompanyModel1)
        {
            if (ModelState.IsValid)
            {
                Company Company1 = UsersServices1.getCompanyById(CompanyModel1.Id);
                if (Company1 != null)
                {
                    UsersServices1.updateCompany(CompanyModel1);
                }
                else
                {
                    CompanyModel1.Id = 0;
                    UsersServices1.insertCompany(CompanyModel1);
                }

            }
            else
            {
                ViewData["ModifyCom"] = "ModifyCom";
            }
            IEnumerable<Company> CompanyList1 = UsersServices1.getAllCompany();
            ViewData["CompanyList1"] = CompanyList1;
            return View(CompanyModel1);
        }
        public ActionResult CompanyDelete(long Id)
        {
            UsersServices1.deleteComById(Id);
            return RedirectToAction("Company", new { id = -1 });
        }
        public ActionResult DisplaySetting(long Id)
        {
            ViewData["SaleTypes"] = UsersServices1.getAllSaleTypes(false);
            SettingModel SettingModel1 = new SettingModel();
            var seting = UsersServices1.PageSettingNumber();
            SettingModel1.PageNumber = seting == null ? "0" : seting.DataValue;
            seting = UsersServices1.getTaxCommissiong();
            SettingModel1.TaxCommistion = seting == null ? "0" : seting.DataValue;

            if (Id == 0)
            {
                return View(SettingModel1);
            }
            SaleType SaleType1 = UsersServices1.getSaleTypeById(Id);
            if (SaleType1 != null)
            {
                SettingModel1.Id = SaleType1.Id;
                SettingModel1.SaleType = SaleType1.Name;
                SettingModel1.Bonus = Convert.ToDouble(SaleType1.Value.Value);
                ViewData["Action"] = "Update";
            }
            SettingModel1.Id = -1;
            ViewData["Action"] = "Create";
            return View(SettingModel1);
        }

        [HttpPost]
        public ActionResult TaxSettting(SettingModel SettingModel1)
        {
            Setting Setting1 = new Setting();
            UsersServices1.UpdateTaxCommission(long.Parse(SettingModel1.TaxCommistion));
            UsersServices1.UpdatePageSeting(int.Parse(SettingModel1.PageNumber));
            return RedirectToAction("DisplaySetting", new { id = 0 });
        }
        [HttpPost]
        public ActionResult SaleTypesSettting(SettingModel SettingModel1)
        {
            try
            {
                SaleType SaleType1 = UsersServices1.getSaleTypeById(SettingModel1.Id);
                if (SaleType1 != null)
                {
                    SaleType1.Name = SettingModel1.SaleType;
                    SaleType1.Value = Convert.ToDecimal(SettingModel1.Bonus);
                    SaleType1.Active = true;
                    UsersServices1.UpdateSaleType(SaleType1);
                }
                else
                {
                    SaleType1 = new SaleType();
                    SaleType1.Name = SettingModel1.SaleType;
                    SaleType1.Value = Convert.ToDecimal(SettingModel1.Bonus);
                    SaleType1.Active = true;
                    UsersServices1.InsertSaleType(SaleType1);
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return RedirectToAction("DisplaySetting", new { id = 0 });
        }
        public ActionResult DeActivateSaleType(long Id)
        {
            SaleType SaleType1 = UsersServices1.getSaleTypeById(Id);
            if (SaleType1 != null)
            {
                SaleType1.Active = false;
                UsersServices1.UpdateSaleType(SaleType1);
            }
            return RedirectToAction("DisplaySetting", new { id = 0 });
        }
        public ActionResult DeleteSaleType(long Id)
        {
            UsersServices1.DeleteSaleType(Id);
            return RedirectToAction("DisplaySetting", new { id = 0 });
        }
        public ActionResult ActivateSaleType(long Id)
        {
            SaleType SaleType1 = UsersServices1.getSaleTypeById(Id);
            if (SaleType1 != null)
            {
                SaleType1.Active = true;
                UsersServices1.UpdateSaleType(SaleType1);
            }
            return RedirectToAction("DisplaySetting", new { id = 0 });
        }

        public ActionResult CheckDeleteSalesType(long id)
        {
            ViewBag.checkDelete = UsersServices1.CheckSaleTypeFree(id);
            return PartialView("_CheckDelete", id);
        }
        private bool isManager()
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Manager.ToString()))
            {
                return true;
            }
            return false;
        }
        private bool isDirector()
        {
            if (User1.RoleName.Equals(UsersModel.Positions.Director.ToString()))
            {
                return true;
            }
            return false;
        }
        private bool isAccountant()
        {
            if (UsersModel.Functions.Accountant.ToString().Equals(User1.Department != null ? User1.Department.DeptFunction : ""))
            {
                return true;
            }
            return false;
        }
        public ActionResult Plan4SalesRedirect()
        {
            if (isDirector() || isAccountant())
            {
                return RedirectToAction("DirectorPlan4Sales", new { id = 0 });
            }
            if (isManager())
            {
                return RedirectToAction("ManagerPlan4Sales", new { id = 0 });
            }
            return RedirectToAction("ManagerPlan4Sales", new { id = 0 });
        }

        public ActionResult ManagerPlan4Sales(long Id)
        {
            SalePlanModel SalePlanModel1 = null;
            try
            {
                SalePlanModel1 = (SalePlanModel)Session[SalePlanModel.USER_SALE_PLAN_SESSION];
                if (SalePlanModel1 == null)
                {
                    SalePlanModel1 = new SalePlanModel();
                    DateTime Date1 = DateTime.Now;
                    SalePlanModel1.Month = Date1.Month;
                    SalePlanModel1.Year = Date1.Year;
                    Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
                }

                DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
                ViewData["PlanFor"] = DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year;
                User UserSale = UsersServices1.getUserById(Id);
                if (UserSale != null)
                {
                    ViewData["Action"] = "Update";
                    SalePlan SalePlan1 = UsersServices1.getSalePlanByUser(Id, SearchDate);
                    SalePlanModel1.SaleName = UserSale.FullName;
                    if (SalePlan1 != null)
                    {
                        SalePlanModel1.PlanValue = Convert.ToDouble(SalePlan1.PlanValue);
                    }
                    else { SalePlanModel1.PlanValue = 0; }
                }

                SalePlan DeptSalePlan = UsersServices1.getSalePlanByDept(User1.DeptId.Value, SearchDate);
                if (DeptSalePlan == null)
                {
                    ViewData["DeptSalePlan"] = "0.0";
                }
                else
                {
                    ViewData["DeptSalePlan"] = DeptSalePlan.PlanValue.Value.ToString("0.0");
                }

                ViewData["SalePlans"] = UsersServices1.getPlanDept(User1.DeptId.Value, SearchDate);
            }
            catch (Exception e) { }
            return View(SalePlanModel1);
        }
        [HttpPost]
        public ActionResult ManagerPlan4Sales(long Id, SalePlanModel SalePlanModel1)
        {
            Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;

            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
            SalePlan SalePlan1 = UsersServices1.getSalePlanByUser(Id, SearchDate);
            User UserSale = UsersServices1.getUserById(Id);
            UsersServices1.UpdateSalePlan(Id, SearchDate, SalePlanModel1.PlanValue);
            ViewData["SalePlans"] = UsersServices1.getPlanDept(User1.DeptId.Value, SearchDate);
            return RedirectToAction("ManagerPlan4Sales", new { id = 0 });
        }

        public ActionResult DirectorPlan4Sales(long Id)
        {
            SalePlanModel SalePlanModel1 = (SalePlanModel)Session[SalePlanModel.USER_SALE_PLAN_SESSION];
            if (SalePlanModel1 == null)
            {
                SalePlanModel1 = new SalePlanModel();
                DateTime Date1 = DateTime.Now;
                SalePlanModel1.Month = Date1.Month;
                SalePlanModel1.Year = Date1.Year;
                Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
            }
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
            ViewData["PlanFor"] = DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year;
            Company Company1 = UsersServices1.getCompanyById(Id);
            if (Company1 != null)
            {
                ViewData["Action"] = "Update";
                SalePlan SalePlan1 = UsersServices1.getSalePlanByCom(Id, SearchDate);
                SalePlanModel1.SaleName = Company1.CompanyName;
                if (SalePlan1 != null)
                {
                    SalePlanModel1.PlanValue = Convert.ToDouble(SalePlan1.PlanValue);
                }
                else { SalePlanModel1.PlanValue = 0; }
            }
            SalePlanModel1.PlanOfficeType = SalePlanModel.PlanType.OFFICE.ToString();
            ViewData["PlanTypeList"] = SalePlanModel.PlanTypeList;
            ViewData["SalePlans"] = UsersServices1.getPlanComs(SearchDate);
            return View(SalePlanModel1);
        }
        [HttpPost]
        public ActionResult DirectorPlan4Sales(long Id, SalePlanModel SalePlanModel1)
        {
            if (SalePlanModel.PlanType.DEPARTMENT.ToString().Equals(SalePlanModel1.PlanOfficeType))
            {
                return RedirectToAction("Plan4Depts", new { id = 0 });
            }
            if (SalePlanModel.PlanType.QUOTA_IN_MONTH.ToString().Equals(SalePlanModel1.PlanOfficeType))
            {
                return RedirectToAction("QuataInMonth", new { id = 0 });
            }
            Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
            UsersServices1.UpdateSalePlanCom(Id, SearchDate, SalePlanModel1.PlanValue);
            ViewData["SalePlans"] = UsersServices1.getPlanComs(SearchDate);
            return RedirectToAction("DirectorPlan4Sales", new { id = 0 });
        }
        public ActionResult Plan4Depts(long Id)
        {
            SalePlanModel SalePlanModel1 = (SalePlanModel)Session[SalePlanModel.USER_SALE_PLAN_SESSION];
            if (SalePlanModel1 == null)
            {
                SalePlanModel1 = new SalePlanModel();
                DateTime Date1 = DateTime.Now;
                SalePlanModel1.Month = Date1.Month;
                SalePlanModel1.Year = Date1.Year;
                Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
            }
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
            ViewData["PlanFor"] = DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year;
            Department DepartmentSale = UsersServices1.getDepartmentById(Id);
            if (DepartmentSale != null)
            {
                ViewData["Action"] = "Update";
                SalePlan SalePlan1 = UsersServices1.getSalePlanByDept(Id, SearchDate);
                SalePlanModel1.SaleName = DepartmentSale.DeptName;
                if (SalePlan1 != null)
                {
                    SalePlanModel1.PlanValue = Convert.ToDouble(SalePlan1.PlanValue);
                }
                else { SalePlanModel1.PlanValue = 0; }
            }
            SalePlanModel1.PlanOfficeType = SalePlanModel.PlanType.DEPARTMENT.ToString();
            ViewData["PlanTypeList"] = SalePlanModel.PlanTypeList;
            ViewData["SalePlans"] = UsersServices1.getPlanDepts(User1.ComId.Value, SearchDate);
            return View(SalePlanModel1);
        }
        [HttpPost]
        public ActionResult Plan4Depts(long Id, SalePlanModel SalePlanModel1)
        {
            if (SalePlanModel.PlanType.OFFICE.ToString().Equals(SalePlanModel1.PlanOfficeType))
            {
                return RedirectToAction("DirectorPlan4Sales", new { id = 0 });
            }
            if (SalePlanModel.PlanType.QUOTA_IN_MONTH.ToString().Equals(SalePlanModel1.PlanOfficeType))
            {
                return RedirectToAction("QuataInMonth", new { id = 0 });
            }
            Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
            UsersServices1.UpdateSalePlanDept(Id, SearchDate, SalePlanModel1.PlanValue);
            ViewData["SalePlans"] = UsersServices1.getPlanDepts(User1.ComId.Value, SearchDate);
            return RedirectToAction("Plan4Depts", new { id = 0 });
        }
        public ActionResult QuataInMonth()
        {
            SalePlanModel SalePlanModel1 = (SalePlanModel)Session[SalePlanModel.USER_SALE_PLAN_SESSION];
            if (SalePlanModel1 == null)
            {
                SalePlanModel1 = new SalePlanModel();
                DateTime Date1 = DateTime.Now;
                SalePlanModel1.Month = Date1.Month;
                SalePlanModel1.Year = Date1.Year;
                Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
            }
            SalePlanModel1.PlanOfficeType = SalePlanModel.PlanType.QUOTA_IN_MONTH.ToString();
            ViewData["PlanTypeList"] = SalePlanModel.PlanTypeList;
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay(SalePlanModel1.Month.ToString()) + "/" + SalePlanModel1.Year);
            ViewData["SalePlans4Com"] = UsersServices1.getPlanComs(SearchDate);
            ViewData["SalePlans4Dept"] = UsersServices1.getPlanDepts(SearchDate);
            if (User1.ComId != null)
            {
                ViewData["SalePlans4Salses"] = UsersServices1.getQuataInMonth(User1.ComId.Value, SearchDate);
            }

            return View(SalePlanModel1);
        }

        [HttpPost]
        public ActionResult QuataInMonth(SalePlanModel SalePlanModel1)
        {
            if (SalePlanModel.PlanType.OFFICE.ToString().Equals(SalePlanModel1.PlanOfficeType))
            {
                return RedirectToAction("DirectorPlan4Sales", new { id = 0 });
            }
            else if (SalePlanModel.PlanType.DEPARTMENT.ToString().Equals(SalePlanModel1.PlanOfficeType))
            {
                return RedirectToAction("Plan4Depts", new { id = 0 });
            }

            Session[SalePlanModel.USER_SALE_PLAN_SESSION] = SalePlanModel1;
            return RedirectToAction("QuataInMonth", new { id = 0 });
        }
        public ActionResult SetUserActive(int id, bool isActive)
        {
            UsersServices1.SetActive(id, isActive);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
        public ActionResult SetDepartActive(int id, bool isActive)
        {
            UsersServices1.SetActiveDepartment(id, isActive);
            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}
