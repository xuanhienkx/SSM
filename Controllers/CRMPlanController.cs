using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;

namespace SSM.Controllers
{
    public class CRMPlanController : BaseController
    {
        private ICRMPLanProgramService programService;
        private ICRMPlanMonthService planMonthService;
        private ICRMPLanSaleService planSaleService;
        private ICRMPlanProgMonthServics planProgMonthServics;
        private UsersServices usersServices;

        public CRMPlanController()
        {
            programService = new CRMPLanProgramService();
            planMonthService = new CRMPlanMonthService();
            planSaleService = new CRMPLanSaleService();
            planProgMonthServics = new CRMPlanProgMonthServics();
            usersServices = new UsersServicesImpl();
        }

        public ActionResult Index()
        {
            long depId = CurrenUser.DeptId ?? 4;
            if (CurrenUser.IsAdminAndAcct())
                depId = 4;

            var currentYear = DateTime.Now.Year;
            var filter = new PlanFilter()
            {
                Id = depId,
                Year = currentYear
            };

            ViewBag.AllPrograms = programService.GetModelAll();
            ViewBag.AllSalse = usersServices.GetAll(x => x.DeptId == depId && x.IsActive == true);
            var listUserShow = planSaleService.GetAllByUser(CurrenUser, currentYear, depId);
            var listDeptShow = planSaleService.GetAllDeptList(currentYear, depId);
            var depList = usersServices.GetSalesDept();
            ViewBag.AlldeptSalseList = new SelectList(depList, "Id", "DeptName");
            ViewBag.PlanOfDep = listDeptShow;
            ViewBag.PlanFilter = filter;
            return View(listUserShow);
        }
        [HttpPost]
        public ActionResult Index(PlanFilter filter)
        {
            var listUserShow = planSaleService.GetAllByUser(CurrenUser, filter.Year, filter.Id);
            var listDeptShow = planSaleService.GetAllDeptList(filter.Year, filter.Id);
            ViewBag.PlanOfDep = listDeptShow;
            var depList = usersServices.GetSalesDept();
            ViewBag.AlldeptSalseList = new SelectList(depList, "Id", "DeptName");
            ViewBag.AllPrograms = programService.GetModelAll();
            var listSales = usersServices.GetAll(x => x.DeptId == filter.Id && x.IsActive == true);
            ViewBag.AllSalse = listSales;
            ViewBag.PlanFilter = filter;
            return PartialView("_List", listUserShow);
        }

        public ActionResult PlanOffice()
        {
            var filter = new PlanFilter()
            {
                Id = CurrenUser.ComId.Value,
                Year = DateTime.Now.Year
            };
            ViewBag.PlanFilter = filter;
            var depList = usersServices.getAllCompany();
            ViewBag.AllPrograms = programService.GetModelAll();
            ViewBag.AlldeptSalseList = new SelectList(depList, "Id", "CompanyName");
            var listDeptShow = planSaleService.GetAllDeptList(filter.Year, 0, filter.Id);
            if (!listDeptShow.Any() && DateTime.Now.Year <= filter.Year)
            {
                SetupdataForYear(filter.Year);
                listDeptShow = planSaleService.GetAllDeptList(filter.Year, 0, filter.Id);
            }
            return View(listDeptShow);
        }
        [HttpPost]
        public ActionResult PlanOffice(PlanFilter filter)
        {
            var depList = usersServices.getAllCompany();
            ViewBag.AlldeptSalseList = new SelectList(depList, "Id", "CompanyName");
            ViewBag.AllPrograms = programService.GetModelAll();
            var listDeptShow = planSaleService.GetAllDeptList(filter.Year, 0, filter.Id);
            if (!listDeptShow.Any() && DateTime.Now.Year <= filter.Year)
            {
                SetupdataForYear(filter.Year);
                listDeptShow = planSaleService.GetAllDeptList(filter.Year, 0, filter.Id);
            }
            ViewBag.PlanFilter = filter;
            return PartialView("_OfficeDepList", listDeptShow);
        }
        public ActionResult SetDefaulPlanData()
        {
            SetupdataForYear(DateTime.Now.Year);
            return View();
        }
        private void SetupdataForYear(int currentYear, long userId = 0)
        {
            var allSalse = usersServices.GetAll(
                   x =>
                       x.IsActive == true &&
                       (userId == 0 || x.Id == userId) &&
                       !x.Department.DeptFunction.Equals(UsersModel.Positions.Accountant.ToString()) &&
                       !x.Department.DeptFunction.Equals(UsersModel.DisplayPositions.Director.ToString()));
            var allPrograms = programService.GetModelAll();
            foreach (var user in allSalse)
            {
                var isExisted =
                        planSaleService.Any(
                            x =>
                                x.SalesId == user.Id && x.PlanYear == currentYear);
                if (isExisted)
                    continue;
                var pLanSalesModel = new CRMPLanSaleModel
                {
                    PlanYear = currentYear,
                    SalesId = user.Id
                };
                var planSale = planSaleService.InsertModel(pLanSalesModel);
                foreach (var program in allPrograms)
                {
                    var progMonthModel = new CRMPlanProgMonthModel
                    {
                        PlanYear = planSale.PlanYear,
                        PlanSalesId = planSale.Id,
                        ProgramId = program.Id
                    };
                    progMonthModel = planProgMonthServics.InsertModel(progMonthModel);
                    for (int i = 1; i <= 12; i++)
                    {
                        var splanModel = new CRMPlanMonthModel
                        {
                            PlanMonth = i,
                            PlanYear = progMonthModel.PlanYear,
                            PlanValue = 0,
                            ProgramMonthId = progMonthModel.Id
                        };
                        planMonthService.InsertModel(splanModel);
                    }
                }
            }
        }

        public ActionResult UpdatePlanValue(List<CRMPlanMonthModel> CRMPlanMonthModels)
        {
            var year = DateTime.Now.Year;
            foreach (var it in CRMPlanMonthModels)
            {
                year = it.PlanYear;
                var db = planMonthService.GetModelById(it.Id);
                db.PlanValue = it.PlanValue;
                db.PlanMonth = it.PlanMonth;
                db.PlanYear = it.PlanYear;
                db.ProgramMonthId = it.ProgramMonthId;
                planMonthService.UpdateModel(db);
            }
            var filter = new PlanFilter()
            {
                Id = CurrenUser.DeptId.Value,
                Year = year
            };
            return Index(filter);
        }

        public ActionResult ApprovalPlan(long id, bool isApproval = true)
        {
            var splan = planSaleService.GetByIModel(id);
            if (isApproval)
            {
                splan.ApprovalBy = CurrenUser;
                splan.ApprovedDate = DateTime.Now;
            }
            else
            {
                splan.ApprovalBy = null;
                splan.ApprovedDate = null;
                splan.SubmitedBy = null;
                splan.SubmitedDate = null;
            }
            planSaleService.UpdateModel(splan);
            var filter = new PlanFilter()
            {
                Id = CurrenUser.DeptId.Value,
                Year = splan.PlanYear
            };
            return Index(filter);
        }
        public ActionResult SubmitPlan(long id, bool isSubmit = true)
        {
            var splan = planSaleService.GetByIModel(id);
            if (isSubmit)
            {
                splan.SubmitedBy = CurrenUser;
                splan.SubmitedDate = DateTime.Now;
            }
            else
            {
                splan.ApprovalBy = null;
                splan.ApprovedDate = null;
                splan.SubmitedBy = null;
                splan.SubmitedDate = null;
            }
            planSaleService.UpdateModel(splan);
            var filter = new PlanFilter()
            {
                Id = CurrenUser.DeptId.Value,
                Year = splan.PlanYear
            };
            return Index(filter);
        }
    }
}