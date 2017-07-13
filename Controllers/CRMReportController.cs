using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Xml.Linq;
using SSM.Models;
using SSM.Models.CRM;
using SSM.Services;
using SSM.Services.CRM;
using SSM.ViewModels.Reports.CRM;

namespace SSM.Controllers
{
    public class CRMReportController : BaseController
    {
        #region Defined

        private ICRMPLanProgramService programService;
        private ICRMPlanMonthService planMonthService;
        private ICRMPLanSaleService planSaleService;
        private ICRMPlanProgMonthServics planProgMonthServics;
        private UsersServices usersServices;
        private ICRMStatusService crmStatusService;
        private ICRMContactService crmContactService;
        private ICRMCustomerService crmCustomerService;
        private ICustomerServices customerServices;
        private ICRMEvetypeService evetypeServiceevetypeService;
        private ICRMPriceQuotationService priceQuotationService;
        private ICRMEventService eventService;
        private IAgentService agentService;

        public CRMReportController()
        {
            programService = new CRMPLanProgramService();
            planMonthService = new CRMPlanMonthService();
            planSaleService = new CRMPLanSaleService();
            planProgMonthServics = new CRMPlanProgMonthServics();
            usersServices = new UsersServicesImpl();
            crmCustomerService = new CRMCustomerService();
            customerServices = new CustomerServices();
            crmContactService = new CRMContactService();
            priceQuotationService = new CRMPriceQuotationService();
            eventService = new CRMEventService();
            agentService = new AgentService();
            crmStatusService = new CRMStatusService();
            evetypeServiceevetypeService = new CRMEvetypeService();
        }

        #endregion

        #region Plan follow report

        public ActionResult Index()
        {
            var filter = new PlanFilter
            {
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                ReportType = ReportType.Monthly,
                Id = CurrenUser.Id,
                NextMonth = DateTime.Now.AddMonths(1).Month,
                NextYear = DateTime.Now.AddMonths(1).Year,
                SummaryByType = SummaryByType.ByUser
            };
            filter.FromDate = new DateTime(filter.Year, filter.Month,1);
            filter.ToDate = new DateTime(filter.NextYear, filter.NextMonth, 1);
            var reports = ProcessPersonalMonthlyReports(filter);
            var listCusHashPrice = ListCustomersHashPrice(filter);
            var listCusHashEvent = ListCustomerEvent(filter);
            ViewBag.ListPrice = listCusHashPrice;
            ViewBag.ListEvents = listCusHashEvent;
            return View(reports);
        }
        [HttpPost]
        public ActionResult Index(int id, PlanFilter filter)
        {
            filter.ReportType = ReportType.Monthly;
            filter.FromDate = new DateTime(filter.Year, filter.Month, 1);
            var nextMonth = filter.FromDate.AddMonths(1);
            filter.NextMonth = nextMonth.Month;
            filter.NextYear = nextMonth.Year;
            filter.ToDate = nextMonth;

            filter.SummaryByType = SummaryByType.ByUser;
            var reports = ProcessPersonalMonthlyReports(filter);
            var listCusHashPrice = ListCustomersHashPrice(filter);
            var listCusHashEvent = ListCustomerEvent(filter);
            ViewBag.ListPrice = listCusHashPrice;
            ViewBag.ListEvents = listCusHashEvent;
            return PartialView("_PorsonalDataReport", reports);
        }

        public ActionResult PersonalFollowp(SummaryByType type)
        {
            var filter = new PlanFilter
            {
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                ReportType = ReportType.Year,
                NextMonth = DateTime.Now.AddMonths(1).Month,
                NextYear = DateTime.Now.AddMonths(1).Year,
                SummaryByType = type
            };
            filter.FromDate = new DateTime(filter.Year, filter.Month, 1);
            var nextMonth = filter.FromDate.AddMonths(1);
            filter.NextMonth = nextMonth.Month;
            filter.NextYear = nextMonth.Year;
            filter.ToDate = nextMonth;
            switch (filter.SummaryByType)
            {
                case SummaryByType.ByUser:
                    filter.Id = CurrenUser.Id;
                    break;
                case SummaryByType.ByDeparment:
                    filter.Id = CurrenUser.DeptId.Value;
                    break;
                case SummaryByType.ByOffice:
                    filter.Id = CurrenUser.ComId.Value;
                    break;
            }
            var reports = ProcessPersonalFollowupReports(filter);
            var planYearReports = ProcessPersonalMonthlyReports(filter);
            ViewBag.CustomerFollowup = reports;
            ViewBag.SummaryByType = type;
            ViewBag.PlanYear = planYearReports;
            return View();
        }
        [HttpPost]
        public ActionResult PersonalFollowp(PlanFilter filter)
        {
            filter.FromDate = new DateTime(filter.Year, filter.Month, 1);
            var nextMonth = filter.FromDate.AddMonths(1);
            filter.NextMonth = nextMonth.Month;
            filter.NextYear = nextMonth.Year;
            filter.ToDate = nextMonth;
            var reports = ProcessPersonalFollowupReports(filter);
            var planYearReports = ProcessPersonalMonthlyReports(filter);
            ViewBag.CustomerFollowup = reports;
            ViewBag.PlanYear = planYearReports;
            ViewBag.SummaryByType = filter.SummaryByType;
            return PartialView("_PorsonalFollowupReport");
        }

        private List<PersonalReport> ProcessPersonalMonthlyReports(PlanFilter filter)
        {

            ViewBag.PlanFilter = filter; 
            var plans = planMonthService.GetQuery(x =>
                                         (filter.Month == 0 || x.PlanMonth == filter.Month)
                                         && x.PlanYear == filter.Year);
            switch (filter.SummaryByType)
            {
                case SummaryByType.ByUser:
                    plans = plans.Where(x => x.CRMPlanProgMonth.CRMPLanSale.SalesId == filter.Id);
                    var usersList = usersServices.GetAllSales(CurrenUser, false).ToList();
                    if (CurrenUser.IsAdmin())
                    {
                        usersList.Add(CurrenUser);
                    }
                    ViewBag.AlldeptSalseList = new SelectList(usersList.OrderBy(x => x.FullName), "Id", "FullName");
                    break;
                case SummaryByType.ByDeparment:
                    plans = plans.Where(x => x.CRMPlanProgMonth.CRMPLanSale.Sales.DeptId == filter.Id);
                    ViewBag.AlldeptSalseList = new SelectList(usersServices.GetAllDepartmentActive(CurrenUser), "Id", "DeptName");
                    break;
                case SummaryByType.ByOffice:
                    plans = plans.Where(x => x.CRMPlanProgMonth.CRMPLanSale.Sales.ComId == filter.Id);
                    ViewBag.AlldeptSalseList = new SelectList(usersServices.GetCompanies(CurrenUser), "Id", "CompanyName");
                    break;
            }
            var reports = new List<PersonalReport>();
            var summary = crmCustomerService.SummarysList(CurrenUser, filter);
            var totalShipmentExc = summary.Sum(x => x.TotalShippments);//So lo thanh cong
            var totalSuccessExc = summary.Sum(x => x.SuccessFully);// khach hang moi thanh cong
            var totalVistedExc = summary.Sum(x => x.TotalVisitedSuccess);//Vieng tham
            var totaldocdExc = summary.Sum(x => x.TotalDocument);//Bai viet
            var totalEmailExc = summary.Sum(x => x.TotalFirstSendEmail);//Guid bao gia
            var hatMonthShipmentExc = 0;//So lo thanh cong
            var hatMonthSuccessExc = 0;// khach hang moi thanh cong
            var hatMonthVistedExc = 0;//Vieng tham
            var hatMonthdocdExc = 0;//Bai viet
            var hatMonthEmailExc = 0;//Guid bao gia

            // report for personal monthly
            if (filter.ReportType == ReportType.Monthly && filter.Month > 0)
            {
                var nextTime = new DateTime(filter.Year, filter.Month, 1).AddMonths(1);
                filter.NextMonth = nextTime.Month;
                filter.NextYear = nextTime.Year;
                var cusHatMonth = summary.Where(x => x.CreatedDate <= new DateTime(filter.Year, filter.Month, 15, 23, 59, 59)).ToList();
                hatMonthShipmentExc = cusHatMonth.Sum(x => x.TotalShippments);//So lo thanh cong
                hatMonthSuccessExc = cusHatMonth.Sum(x => x.SuccessFully);// khach hang moi thanh cong
                hatMonthVistedExc = cusHatMonth.Sum(x => x.TotalVisitedSuccess);//Vieng tham
                hatMonthdocdExc = cusHatMonth.Sum(x => x.TotalDocument);//Bai viet
                hatMonthEmailExc = cusHatMonth.Sum(x => x.TotalFirstSendEmail);//Guid bao gia
            }
            var planMonths = plans.Where(x => (filter.Month == 0 || x.PlanMonth == filter.Month) && x.PlanYear == filter.Year).ToList();
            foreach (var p in planMonths)
            {
                var report = new PersonalReport
                {
                    PlanName = p.CRMPlanProgMonth.CRMPlanProgram.Name,
                    PlanValue = p.PlanValue,
                    Month = p.PlanMonth,
                    Year = p.PlanYear
                };
                if (filter.ReportType == ReportType.Monthly && filter.Month > 0)
                {
                    var next = planMonthService.FindEntity(x => x.ProgramMonthId == p.Id && x.PlanMonth == filter.NextMonth && filter.NextYear == x.PlanYear);
                    var nextPlan = next != null ? next.PlanValue : 0;
                    report.PlanValueNextMonth = nextPlan;
                }


                switch (p.CRMPlanProgMonth.ProgramId)
                {
                    case 1:
                        report.TotalExc = totalSuccessExc;
                        report.FistExc = hatMonthSuccessExc;
                        report.LastExc = totalSuccessExc - hatMonthSuccessExc;
                        break;
                    case 2:
                        report.TotalExc = totalEmailExc;
                        report.FistExc = hatMonthEmailExc;
                        report.LastExc = totalEmailExc - hatMonthEmailExc;
                        break;
                    case 3:
                        report.TotalExc = totalVistedExc;
                        report.FistExc = hatMonthVistedExc;
                        report.LastExc = totalVistedExc - hatMonthVistedExc;
                        break;
                    case 4:
                        report.TotalExc = totalShipmentExc;
                        report.FistExc = hatMonthShipmentExc;
                        report.LastExc = totalShipmentExc - hatMonthShipmentExc;
                        break;
                    case 5:
                        report.TotalExc = totaldocdExc;
                        report.FistExc = hatMonthdocdExc;
                        report.LastExc = totaldocdExc - hatMonthdocdExc;
                        break;
                }
                report.Odds = report.TotalExc - report.PlanValue;
                reports.Add(report);
            }

            ViewBag.PlanMount = plans.ToList();
            return reports;
        }

        private List<ReportCustomerViewModel> ListCustomersHashPrice(PlanFilter filter)
        {
            var list =
                priceQuotationService.GetAll(
                            x =>
                               (filter.Month == 0 || x.LastDateSend.Value.Month == filter.Month) && x.LastDateSend.Value.Year == filter.Year &&
                                x.CreatedById == filter.Id && x.CountSendMail == 1).Select(x => new ReportCustomerViewModel
                                {
                                    DateTime = x.LastDateSend.Value,
                                    CompanyName = x.CRMCustomer.CompanyShortName,
                                    ContactName = x.CRMCustomer.CRMContacts.Aggregate("", (current, u) => current + (u.FullName + "/" + u.Phone + "<br/>"))
                                });
            return list.ToList();
        }
        private List<ReportCustomerViewModel> ListCustomerEvent(PlanFilter filter)
        {
            var list =
                eventService.GetAll(
                            x =>
                               (filter.Month == 0 || x.DateBegin.Month == filter.Month) && x.DateBegin.Year == filter.Year &&
                                x.CreatedById == filter.Id && x.IsEventAction == false).Select(x => new ReportCustomerViewModel
                                {
                                    DateTime = x.DateBegin,
                                    CompanyName = x.CRMCustomer.CompanyShortName,
                                    ContactName = x.CRMCustomer.CRMContacts.Aggregate("\n", (current, u) => current + (u.FullName + "/" + u.Phone + "<br/>"))
                                });
            return list.ToList();
        }

        private List<SalesTypeSummaryReport> ProcessPersonalFollowupReports(PlanFilter filter)
        {
            var list = crmCustomerService.SalesTypeSumaryReport(filter);
            return list;
        }

        private List<PlanYearSummaryReport> ProcessProsonalPlanFollowup(PlanFilter filter)
        {
            var summary = crmCustomerService.SummarysList(CurrenUser, filter);
            var totalShipmentExc = summary.Sum(x => x.TotalShippments);//So lo thanh cong
            var totalSuccessExc = summary.Sum(x => x.SuccessFully);// khach hang moi thanh cong
            var totalVistedExc = summary.Sum(x => x.TotalVisitedSuccess);//Vieng tham
            var totaldocdExc = summary.Sum(x => x.TotalDocument);//Bai viet
            var totalEmailExc = summary.Sum(x => x.TotalFirstSendEmail);//Guid bao gia
            var plans = planMonthService.GetQuery(x =>
                                        (filter.Month == 0 || x.PlanMonth == filter.Month)
                                        && x.PlanYear == filter.Year);
            var listUserShow = planSaleService.GetAllByUser(CurrenUser, filter.Year, filter.Id);
            switch (filter.SummaryByType)
            {
                case SummaryByType.ByUser:
                    plans = plans.Where(x => x.CRMPlanProgMonth.CRMPLanSale.SalesId == filter.Id);
                    break;
                case SummaryByType.ByDeparment:
                    plans = plans.Where(x => x.CRMPlanProgMonth.CRMPLanSale.Sales.DeptId == filter.Id);
                    break;
                case SummaryByType.ByOffice:
                    plans = plans.Where(x => x.CRMPlanProgMonth.CRMPLanSale.Sales.ComId == filter.Id);
                    break;
            }
            var planMonths = plans.Where(x => x.PlanMonth == filter.Month).ToList();
            foreach (var p in planMonths)
            {
                for (int i = 1; i <= 12; i++)
                {

                }
            }
            return new List<PlanYearSummaryReport>();
        }


        #endregion

        #region TopProfit Report
        [HttpGet]
        public ActionResult CRMTopProfit()
        {
            var filter = new CRMFilterProfit
            {
                BeginDate = DateTime.Today.AddMonths(-1),
                EndDate = DateTime.Now,
                Top = 10
            };
            ViewBag.Filter = filter;
            ViewBag.Users = usersServices.GetAllSales(CurrenUser, false);
            ViewBag.Depts = usersServices.GetAllDepartmentActive(CurrenUser);
            ViewBag.Offices = usersServices.getAllCompany();
            var list = ResultFilter(filter);
            return View(list);
        }
        [HttpPost]
        public ActionResult CRMTopProfit(CRMFilterProfit filter)
        {
            ViewBag.Filter = filter;
            var list = ResultFilter(filter);
            return PartialView("_CrmTopProfitList", list);
        }

        private List<CRMFilterProfitResult> ResultFilter(CRMFilterProfit filter)
        {

            var qr = crmCustomerService.GetCRMListProfit(filter, CurrenUser);
            return qr;
        }

        #endregion

        #region Follow profit report

        [HttpGet]
        public ActionResult CRMFollowProfit(bool isProfit = true)
        {
            var filter = new CRMFilterFollowProfit
            {
                BeginDate = DateTime.Today.AddMonths(-1),
                EndDate = DateTime.Now,
                IsProfit = isProfit,
                Top = 10
            };
            ViewBag.Filter = filter;
            ViewBag.Users = usersServices.GetAllSales(CurrenUser, false);
            ViewBag.Depts = usersServices.GetAllDepartmentActive(CurrenUser);
            ViewBag.Offices = usersServices.getAllCompany();
            ViewBag.Countrys = crmCustomerService.GetAllCountry();
            ViewBag.Agents = agentService.GetQuery(x => x.IsActive && x.IsHideUser == false && x.IsSee == true).OrderBy(x => x.AbbName);
            //ViewBag.Status = CRMStatusCode.;
            var list = ResultFollowFilter(filter);
            return View(list);
        }
        [HttpPost]
        public ActionResult CRMFollowProfit(CRMFilterFollowProfit filter)
        {
            ViewBag.Filter = filter;
            if (filter.BeginDate == null) filter.BeginDate = new DateTime(2017, 1, 1);
            if (filter.EndDate == null) filter.EndDate = DateTime.Now;
            else filter.EndDate = filter.EndDate.Value.AddHours(23).AddMinutes(59);
            var list = ResultFollowFilter(filter);
            return PartialView("_CrmTopProfitList", list);
        }



        private List<CrmFilterTopProfitByStore> ResultFollowFilter(CRMFilterFollowProfit filter)
        {

            var crmstting = usersServices.CRMDayCanelSettingNumber();
            var days = int.Parse(crmstting.DataValue);
            filter.DaysOfLost = days;
            var qr = crmCustomerService.GetCRMListFollowProfit(filter, CurrenUser);
            return qr;

        }

        #endregion
    }
}