using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using SSM.Common;

using SSM.Models;
using SSM.Services;
using SSM.Utils;
using Microsoft.Reporting.WebForms;
using Microsoft.Office.Interop.Excel;
using System.IO;
namespace SSM.Controllers
{
    [HandleError]
    public class ReportController : BaseController
    {
        private ShipmentServices ShipmentServices1;
        private ReportServices ReportService1;
        private UsersServices UserService1;
        private ICustomerServices customerServices;
        private IAgentService agentService;
        private IServicesTypeServices servicesType;
        private List<ServicesType> servicesList;
        private IPerformanceReportService performanceService;
        List<SaleType> SaleTypes;
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            ShipmentServices1 = new ShipmentServicesImpl();
            ReportService1 = new ReportServicesImpl();
            UserService1 = new UsersServicesImpl();
            customerServices = new CustomerServices();
            agentService = new AgentService();
            servicesType = new ServicesTypeServices();
            performanceService = new PerformanceReportService();
            SaleTypes = SaleTypes ?? UserService1.getAllSaleTypes(false).ToList();
            GetDefaultList();
        }

        public void GetDefaultList()
        {
            ViewData["Services"] = Services;
            List<Agent> Agents = new List<Agent>();
            Agent InitA = new Agent();
            InitA.Id = 0;
            InitA.AbbName = "--All Agents--";
            Agents.Add(InitA);
            Agents.AddRange(agentService.GetAll(x => x.IsActive && (CurrenUser.IsAdmin() || x.IsHideUser == false)).OrderBy(x => x.AbbName));
            ViewData["Agents"] = new SelectList(Agents, "Id", "AbbName");
            List<Customer> Cnees = new List<Customer>();
            Customer InitC = new Customer();
            InitC.Id = 0;
            InitC.CompanyName = "--All Cnees--";
            Cnees.Add(InitC);
            Cnees.AddRange(customerServices.GetAll(x => (x.CustomerType == CustomerType.Cnee.ToString() || x.CustomerType == CustomerType.ShipperCnee.ToString()) && x.IsSee && (CurrenUser.IsAdmin() || x.IsHideUser == false)).OrderByDescending(x => x.CompanyName));
            ViewData["Cnees"] = new SelectList(Cnees, "Id", "CompanyName");
            List<Customer> Shippers = new List<Customer>();
            Customer Shipper1 = new Customer();
            Shipper1.Id = 0;
            Shipper1.CompanyName = "--All Shippers--";
            Shippers.Add(Shipper1);
            Shippers.AddRange(customerServices.GetAll(x => (x.CustomerType == CustomerType.Shipper.ToString() || x.CustomerType == CustomerType.ShipperCnee.ToString()) && x.IsSee && (CurrenUser.IsAdmin() || x.IsHideUser == false)).OrderByDescending(x => x.CompanyName));
            ViewData["Shippers"] = new SelectList(Shippers, "Id", "CompanyName");
            var sales = UserService1.GetAllSales(CurrenUser).OrderBy(x => x.FullName);
            var users = new List<User>() { new User() { Id = 0, FullName = "--All Users--" } };
            users.AddRange(sales);
            ViewData["Users"] = new SelectList(users, "Id", "FullName");
        }
        private SelectList Services
        {
            get
            {
                List<ServicesType> list = new List<ServicesType>();
                var servicesTypeItem = new ServicesType { Id = 0, SerivceName = "--All Services--" };
                list.Add(servicesTypeItem);
                list.AddRange(servicesList ?? servicesType.GetAll().OrderBy(x => x.SerivceName).ToList());
                return new SelectList(list, "Id", "SerivceName");
            }

        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SalePerformance(long ComId, long DeptId)
        {
            SalePerformamceModel SalePerformamceModel1 = null;
            SalePerformamceModel1 = (SalePerformamceModel)Session["SalePerformamceModel1"];
            if (SalePerformamceModel1 == null)
            {
                SalePerformamceModel1 = new SalePerformamceModel();
                DateTime Date1 = DateTime.Now.AddMonths(-1);
                SalePerformamceModel1.Month = Date1.Month;
                SalePerformamceModel1.Year = Date1.Year;
            }
            SalePerformanceProcess(ComId, DeptId, SalePerformamceModel1);

            return View(SalePerformamceModel1);
        }
        private List<SaleTypePerform> getAllSaleTypePerform(List<SaleType> SaleTypes)
        {
            List<SaleTypePerform> SaleTypePerforms = new List<SaleTypePerform>();
            foreach (SaleType SaleType1 in SaleTypes)
            {
                SaleTypePerform SaleTypePerform1 = new SaleTypePerform();
                SaleTypePerform1.SaleType = SaleType1.Name;
                SaleTypePerform1.Bonus = 0;
                SaleTypePerforms.Add(SaleTypePerform1);
            }
            return SaleTypePerforms;
        }
        private List<ViewPerformance> summaryViewPerformance(List<ViewPerformance> ViewPerformances)
        {
            List<ViewPerformance> results = new List<ViewPerformance>();

            foreach (ViewPerformance ViewPerformance1 in ViewPerformances)
            {
                if (!results.Contains(ViewPerformance1))
                {
                    ViewPerformance1.SaleTypePerforms = getAllSaleTypePerform(SaleTypes);
                    ViewPerformance1.setPerform(ViewPerformance1.SaleTypePerform1);
                    results.Add(ViewPerformance1);
                }
                else
                {
                    ViewPerformance ViewPerformanceExisted = results.Find(delegate (ViewPerformance ViewPerformance2)
                    {
                        return ViewPerformance2.Equals(ViewPerformance1);
                    });
                    ViewPerformanceExisted.setPerform(ViewPerformance1.SaleTypePerform1);
                    ViewPerformanceExisted.Bonus += ViewPerformance1.Bonus;
                    ViewPerformanceExisted.Perform += ViewPerformance1.Perform;
                    ViewPerformanceExisted.Percent += ViewPerformance1.Percent;
                    ViewPerformanceExisted.Shipments += ViewPerformance1.Shipments;

                }
            }
            return results.OrderBy(x => x.Name).ThenBy(x => x.SaleType).ToList();

        }
        private void SalePerformanceProcess(long ComId, long DeptId, SalePerformamceModel SalePerformamceModel1)
        {
            DateTime SearchDate = DateTime.Now;
            DateTime SearchDateTo = DateTime.Now;
            if (SalePerformamceModel1.Priod == 0)
            {
                SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay("" + SalePerformamceModel1.Month) + "/" + SalePerformamceModel1.Year);
                SearchDateTo = DateUtils.Convert2DateTime("" + DateUtils.ConvertDay("" + DateTime.DaysInMonth(SalePerformamceModel1.Year, SalePerformamceModel1.Month)) + "/" + DateUtils.ConvertDay("" + SalePerformamceModel1.Month) + "/" + SalePerformamceModel1.Year);
                SearchDateTo = SearchDateTo.AddHours(23).AddMinutes(59).AddSeconds(59);
            }
            else
            {
                SearchDate = DateUtils.Convert2DateTime(SalePerformamceModel1.DateFrom);
                //SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay("" + SearchDate.Month) + "/" + SearchDate.Year);
                SearchDateTo = SalePerformamceModel1.DateTo != null ? DateUtils.Convert2DateTime(SalePerformamceModel1.DateTo) : SearchDateTo.AddDays(1);
            }

            IEnumerable<ViewPerformance> ViewPerformances = null;
            IEnumerable<QuantityUnits> QuantityUnits1 = null;
            IEnumerable<ViewPerformance> ViewPerformancesDept = null;
            IEnumerable<QuantityUnits> QuantityUnits1Dept = null;
            IEnumerable<ViewPerformance> ViewPerformancesCom = null;


            IEnumerable<QuantityUnits> QuantityUnits1Com = null;
            #region search by monthly
            if (SalePerformamceModel1.Priod == 0)
            {
                if (UsersModel.isAdminOrDirctor(CurrenUser))
                {
                    ViewPerformancesCom = ReportService1.getViewPerformancesCom(SearchDate, SearchDateTo);
                    ViewData["ViewPerformancesCom"] = summaryViewPerformance(ViewPerformancesCom.ToList());
                    QuantityUnits1Com = ReportService1.getQuantityUnitsCom(SearchDate, SearchDateTo);
                    ViewData["QuantityUnits1Com"] = QuantityUnits1Com.ToList();
                    Company _Com = UserService1.getCompanyById(ComId);
                    if (_Com != null)
                    {
                        ViewData["ViewCompany"] = true;
                        Department _Dept = UserService1.getDepartmentById(DeptId);
                        if (_Dept != null)
                        {
                            ViewPerformances = ReportService1.getViewPerformancesSales(DeptId, SearchDate, SearchDateTo);
                            ViewData["ViewPerformances"] = summaryViewPerformance(ViewPerformances.ToList());
                            QuantityUnits1 = ReportService1.getQuantityUnitsSales(DeptId, SearchDate, SearchDateTo);
                            ViewData["QuantityUnits"] = QuantityUnits1.ToList();
                        }
                        ViewData["ViewDepartment"] = true;
                        ViewPerformancesDept = ReportService1.getViewPerformancesByDept(ComId, SearchDate, SearchDateTo);
                        ViewData["ViewPerformancesDept"] = summaryViewPerformance(ViewPerformancesDept.ToList());
                        QuantityUnits1Dept = ReportService1.getQuantityUnitsDept(ComId, SearchDate, SearchDateTo);
                        ViewData["QuantityUnits1Dept"] = QuantityUnits1Dept.ToList();

                    }
                }
                else if (UsersModel.isDeptManager(CurrenUser))
                {
                    ViewPerformances = ReportService1.getViewPerformancesSales(CurrenUser.Department.Id, SearchDate, SearchDateTo);
                    ViewData["ViewPerformances"] = summaryViewPerformance(ViewPerformances.ToList());
                    QuantityUnits1 = ReportService1.getQuantityUnitsSales(CurrenUser.Department.Id, SearchDate, SearchDateTo);
                    ViewData["QuantityUnits"] = QuantityUnits1.ToList();
                }
                else
                {
                    ViewPerformances = ReportService1.getViewPerformances(CurrenUser.Id, SearchDate, SearchDateTo);
                    ViewData["ViewPerformances"] = summaryViewPerformance(ViewPerformances.ToList());
                    QuantityUnits1 = ReportService1.getQuantityUnits(CurrenUser.Id, SearchDate, SearchDateTo);
                    ViewData["QuantityUnits"] = QuantityUnits1.ToList();
                }

            }
            #endregion
            #region search by period
            if (SalePerformamceModel1.Priod == 1)
            {
                if (SalePerformamceModel1.ServiceName == null)
                {
                    SalePerformamceModel1.ServiceName = "";
                }
                if (UsersModel.isAdminOrDirctor(CurrenUser))
                {
                    ViewPerformancesCom = ReportService1.getViewPerformancesCom(SalePerformamceModel1, SearchDate, SearchDateTo);
                    ViewData["ViewPerformancesCom"] = summaryViewPerformance(ViewPerformancesCom.ToList());
                    QuantityUnits1Com = ReportService1.getQuantityUnitsCom(SalePerformamceModel1, SearchDate, SearchDateTo);
                    ViewData["QuantityUnits1Com"] = QuantityUnits1Com.ToList();
                    Company _Com = UserService1.getCompanyById(ComId);
                    if (_Com != null)
                    {
                        IEnumerable<Shipment> PeriodShipments = ReportService1.getAllShipment(SalePerformamceModel1, SearchDate, SearchDateTo, ComId);
                        ViewData["PeriodShipments"] = PeriodShipments;
                        /*
                        ViewData["ViewCompany"] = true;
                        Department _Dept = UserService1.getDepartmentById(DeptId);
                        if (_Dept != null)
                        {
                            ViewPerformances = ReportService1.getViewPerformancesSales(DeptId, SalePerformamceModel1, SearchDate, SearchDateTo);
                            ViewData["ViewPerformances"] = ViewPerformances;
                            QuantityUnits1 = ReportService1.getQuantityUnitsSales(DeptId, SalePerformamceModel1, SearchDate, SearchDateTo);
                            ViewData["QuantityUnits"] = QuantityUnits1;

                        }
                        ViewData["ViewDepartment"] = true;
                        ViewPerformancesDept = ReportService1.getViewPerformancesByDept(ComId, SalePerformamceModel1, SearchDate, SearchDateTo);
                        ViewData["ViewPerformancesDept"] = ViewPerformancesDept;
                        QuantityUnits1Dept = ReportService1.getQuantityUnitsDept(ComId, SalePerformamceModel1, SearchDate, SearchDateTo);
                        ViewData["QuantityUnits1Dept"] = QuantityUnits1Dept;
                        */
                    }
                }
                else if (UsersModel.isDeptManager(CurrenUser))
                {
                    ViewPerformances = ReportService1.getViewPerformancesSales(CurrenUser.Department.Id, SalePerformamceModel1, SearchDate, SearchDateTo);
                    ViewData["ViewPerformances"] = summaryViewPerformance(ViewPerformances.ToList());
                    QuantityUnits1 = ReportService1.getQuantityUnitsSales(CurrenUser.Department.Id, SalePerformamceModel1, SearchDate, SearchDateTo);
                    ViewData["QuantityUnits"] = QuantityUnits1.ToList();
                }
                else
                {
                    ViewPerformances = ReportService1.getViewPerformances(CurrenUser.Id, SalePerformamceModel1, SearchDate, SearchDateTo);
                    ViewData["ViewPerformances"] = summaryViewPerformance(ViewPerformances.ToList());
                    QuantityUnits1 = ReportService1.getQuantityUnits(CurrenUser.Id, SalePerformamceModel1, SearchDate, SearchDateTo);
                    ViewData["QuantityUnits"] = QuantityUnits1.ToList();
                }
            }
            #endregion
            Session["SalePerformamceModel1"] = SalePerformamceModel1;
        }

        [HttpPost]
        public ActionResult SalePerformance(long ComId, long DeptId, SalePerformamceModel SalePerformamceModel1)
        {
            SalePerformanceProcess(ComId, DeptId, SalePerformamceModel1);
            return View(SalePerformamceModel1);
        }
        public ActionResult SalePerformanceDept()
        {
            SalePerformamceModel SalePerformamceModel1 = new SalePerformamceModel();
            DateTime Date1 = DateTime.Now;
            SalePerformamceModel1.Month = Date1.Month;
            SalePerformamceModel1.Year = Date1.Year;


            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay("" + Date1.Month) + "/" + Date1.Year);
            DateTime SearchDateTo = DateUtils.Convert2DateTime("" + DateTime.DaysInMonth(Date1.Year, Date1.Month) + "/" + DateUtils.ConvertDay("" + Date1.Month) + "/" + Date1.Year);
            IEnumerable<ViewPerformance> ViewPerformances = ReportService1.getViewPerformances(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["ViewPerformances"] = ViewPerformances;
            IEnumerable<QuantityUnits> QuantityUnits1 = ReportService1.getQuantityUnits(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["QuantityUnits"] = QuantityUnits1;

            IEnumerable<ViewPerformance> ViewPerformancesDept = ReportService1.getViewPerformancesByDept(CurrenUser.Department.Id, SearchDate, SearchDateTo);
            ViewData["ViewPerformancesDept"] = ViewPerformancesDept.ToList();
            IEnumerable<QuantityUnits> QuantityUnits1Dept = ReportService1.getQuantityUnitsDept(CurrenUser.Department.Id, SearchDate, SearchDateTo);
            ViewData["QuantityUnits1Dept"] = QuantityUnits1Dept;

            return View(SalePerformamceModel1);
        }
        [HttpPost]
        public ActionResult SalePerformanceDept(SalePerformamceModel SalePerformamceModel1)
        {
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + SalePerformamceModel1.Month + "/" + SalePerformamceModel1.Year);
            DateTime SearchDateTo = DateUtils.Convert2DateTime("" + DateTime.DaysInMonth(SalePerformamceModel1.Year, SalePerformamceModel1.Month) + "/" + SalePerformamceModel1.Month + "/" + SalePerformamceModel1.Year);
            IEnumerable<ViewPerformance> ViewPerformances = ReportService1.getViewPerformances(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["ViewPerformances"] = ViewPerformances;
            IEnumerable<QuantityUnits> QuantityUnits1 = ReportService1.getQuantityUnits(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["QuantityUnits"] = QuantityUnits1;

            IEnumerable<ViewPerformance> ViewPerformancesDept = ReportService1.getViewPerformancesByDept(CurrenUser.Company.Id, SearchDate, SearchDateTo);
            ViewData["ViewPerformancesDept"] = ViewPerformancesDept.ToList();
            IEnumerable<QuantityUnits> QuantityUnits1Dept = ReportService1.getQuantityUnitsDept(CurrenUser.Company.Id, SearchDate, SearchDateTo);
            ViewData["QuantityUnits1Dept"] = QuantityUnits1Dept;

            return View(SalePerformamceModel1);
        }
        public ActionResult SalePerformanceSale()
        {
            SalePerformamceModel SalePerformamceModel1 = new SalePerformamceModel();
            DateTime Date1 = DateTime.Now;
            SalePerformamceModel1.Month = Date1.Month;
            SalePerformamceModel1.Year = Date1.Year;


            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + Date1.Month + "/" + Date1.Year);
            DateTime SearchDateTo = DateUtils.Convert2DateTime("" + DateTime.DaysInMonth(Date1.Year, Date1.Month) + "/" + Date1.Month + "/" + Date1.Year);
            IEnumerable<ViewPerformance> ViewPerformances = ReportService1.getViewPerformances(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["ViewPerformances"] = ViewPerformances;
            IEnumerable<QuantityUnits> QuantityUnits1 = ReportService1.getQuantityUnits(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["QuantityUnits"] = QuantityUnits1;

            return View(SalePerformamceModel1);
        }

        [HttpPost]
        public ActionResult SalePerformanceSale(SalePerformamceModel SalePerformamceModel1)
        {
            DateTime SearchDate = DateUtils.Convert2DateTime("01/" + SalePerformamceModel1.Month + "/" + SalePerformamceModel1.Year);
            DateTime SearchDateTo = DateUtils.Convert2DateTime("" + DateTime.DaysInMonth(SalePerformamceModel1.Year, SalePerformamceModel1.Month) + "/" + SalePerformamceModel1.Month + "/" + SalePerformamceModel1.Year);
            IEnumerable<ViewPerformance> ViewPerformances = ReportService1.getViewPerformances(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["ViewPerformances"] = ViewPerformances;
            IEnumerable<QuantityUnits> QuantityUnits1 = ReportService1.getQuantityUnits(CurrenUser.Id, SearchDate, SearchDateTo);
            ViewData["QuantityUnits"] = QuantityUnits1;

            return View(SalePerformamceModel1);
        }

        public ActionResult DetailsReport()
        {
            LocalReport localReport = new LocalReport();

            localReport.ReportPath = Path.GetFullPath("~/Reports/Report1.rdlc");
            DateTime SearchDate = DateUtils.Convert2DateTime("01/10/2010");
            IEnumerable<PerformanceReport> ProcessReport = ReportService1.getSaleReport(CurrenUser.Id, 2010);
            IEnumerable<PerformanceReport> ResultReport = new List<PerformanceReport>();
            foreach (PerformanceReport PerformanceReport1 in ProcessReport)
            {

            }
            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", ReportService1.getSaleReport(CurrenUser.Id, 2010));

            List<ReportParameter> Params = new List<ReportParameter>();
            ReportParameter Item = new ReportParameter("PlanDisplay", "Plan of 2010");
            Params.Add(Item);
            localReport.SetParameters(Params.AsEnumerable());
            localReport.DataSources.Add(reportDataSource);
            string reportType = "PDF";
            string mimeType;
            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx

            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>PDF</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;
            //Render the report
            renderedBytes = localReport.Render(reportType, deviceInfo,
                out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
            Response.AddHeader("content-disposition", "attachment; filename=Report1." + fileNameExtension);
            return File(renderedBytes, mimeType);
        }

        private List<User> Users
        {
            get
            {
                List<User> list = null;
                var users = UserService1.GetQuery(x => x.IsActive
                                                       && (UsersModel.Functions.Operations.ToString().Equals(x.Department.DeptFunction)
                                                        || UsersModel.Functions.Sales.ToString().Equals(x.Department.DeptFunction)));
                if (!CurrenUser.IsAdminAndAcct())
                {
                    users = users.Where(x => x.DeptId == CurrenUser.DeptId);
                }
                list = users.OrderBy(x => x.FullName).ToList();
                return list;
            }
        }

        public ActionResult PersonalReport()
        {

            PersonReportModel model = new PersonReportModel();
            model.Year = DateTime.Now.Year;
            model.UserId = (int)CurrenUser.Id;
            Session["PersonReportModel"] = model;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReport(model.UserId, model.Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);

            if (Users != null)
            {
                ViewData["ListUser"] = new SelectList(Users, "Id", "FullName");
            }
            ViewData["ReportProcess"] = ReportProcess;
            return View(model);
        }
        [HttpPost]
        public ActionResult PersonalReport(PersonReportModel model)
        {
            Session["PersonReportModel"] = model;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReport(model.UserId, model.Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);
            if (Users != null)
            {
                ViewData["ListUser"] = new SelectList(Users, "Id", "FullName");
            }
            ViewData["ReportProcess"] = ReportProcess;
            return View(model);
        }
        public ActionResult PersonalReportExcel()
        {
            PersonReportModel model = (PersonReportModel)Session["PersonReportModel"];
            int CellX = 3;
            int CellY = 5;
            int MaxSaleType = 4;
            int Year = model.Year;
            int UserId = model.UserId;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReport(UserId, Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);

            string RealPath = Path.Combine(Server.MapPath("~/" + "/FileManager/Reports")
                                        , Path.GetFileName("Report.xls"));
            Application xl = new Application();
            Workbook wb = xl.Workbooks.Open(RealPath, 0, false, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            int pos = 0;
            pos = RealPath.LastIndexOf("\\");
            String tamplePath = RealPath.Substring(0, pos + 1) + "TemReport.xls";
            FileInfo fileInfo = new FileInfo(tamplePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            wb.SaveAs(tamplePath);
            Sheets xlsheets = wb.Sheets; //Get the sheets from workbook
            Worksheet excelWorksheet = (Worksheet)xlsheets[1];

            #region processing excel
            //View data for Report
            excelWorksheet.Cells[4][2] = "YEARLY REPORT " + Year;
            excelWorksheet.Cells[2][5] = "Year Plan " + Year;
            int totalSaleType = ReportProcess.ElementAt(0).PerformModels.Count;
            double[] TotalProfiltSaleTypes = new double[totalSaleType];
            double TotalPlan = 0, TotalProfilt = 0;
            for (int i = 0; i < 12; i++)
            {
                TotalPlan += ReportProcess.ElementAt(i).Plan;
                for (int ti = 0; ti < totalSaleType; ti++)
                {
                    TotalProfiltSaleTypes[ti] += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                    TotalProfilt += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                //TotalHandel += ReportProcess.ElementAt(i).ProfitHandel;
                //TotalSale += ReportProcess.ElementAt(i).ProfitSale;
                excelWorksheet.Cells[CellX + i][CellY] = ReportProcess.ElementAt(i).Plan;
                for (int ti = 0; ti < totalSaleType; ti++)
                {
                    excelWorksheet.Cells[CellX - 1][CellY + ti * 2 + 1] = "Profit Of " + ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).SaleType;
                    excelWorksheet.Cells[CellX - 1][CellY + ti * 2 + 2] = "Perform Of " + ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).SaleType + "(%)";
                    excelWorksheet.Cells[CellX + i][CellY + ti * 2 + 1] = ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                    excelWorksheet.Cells[CellX + i][CellY + ti * 2 + 2] = ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%";
                    excelWorksheet.Cells[15][CellY + ti * 2 + 1] = TotalProfiltSaleTypes[ti];
                    excelWorksheet.Cells[15][CellY + ti * 2 + 2] = divisible(TotalProfiltSaleTypes[ti] * 100, TotalPlan).ToString("0.00") + "%";
                }
                //excelWorksheet.Cells[CellX + i][CellY + 3] = ReportProcess.ElementAt(i).ProfitHandel;
                //excelWorksheet.Cells[CellX + i][CellY + 4] = ReportProcess.ElementAt(i).PerformPH.ToString("0.00") + "%";

                excelWorksheet.Cells[CellX + i][CellY + MaxSaleType * 2 + 1] = ReportProcess.ElementAt(i).SumProfit;
                excelWorksheet.Cells[CellX + i][CellY + MaxSaleType * 2 + 2] = ReportProcess.ElementAt(i).PerformSumProfit.ToString("0.00") + "%";
            }

            excelWorksheet.Cells[15][CellY] = TotalPlan;
            //excelWorksheet.Cells[15][CellY + 1] = TotalSale;
            // excelWorksheet.Cells[15][CellY + 2] = divisible(TotalSale * 100, TotalPlan).ToString("0.00") + "%";
            //excelWorksheet.Cells[15][CellY + 3] = TotalHandel;
            //excelWorksheet.Cells[15][CellY + 4] = divisible(TotalHandel * 100, TotalPlan).ToString("0.00") + "%";
            excelWorksheet.Cells[15][CellY + MaxSaleType * 2 + 1] = TotalProfilt;
            excelWorksheet.Cells[15][CellY + MaxSaleType * 2 + 2] = divisible(TotalProfilt * 100, TotalPlan).ToString("0.00") + "%";

            //delete redundant cell in excel hard line max 4
            for (int ec = 4; ec > totalSaleType; ec--)
            {
                ((Range)excelWorksheet.Rows.get_Item(5 + ec * 2)).Delete();
                ((Range)excelWorksheet.Rows.get_Item(5 + ec * 2 - 1)).Delete();
            }
            #endregion

            wb.Save();
            wb.Close();
            return File(tamplePath, "application/excel", "YearLyReport.xls");
        }
        private double rounFloor(double Value)
        {
            if (Value < 0) return 0.0;
            return Value;
        }
        private double divisible(double Number1, double Number2)
        {
            if (Number2 == 0)
            {
                return 0;
            }
            return Number1 / Number2;
        }

        private List<PerformModel> getAllPerformModel(List<SaleType> SaleTypes)
        {
            List<PerformModel> PerformModels = new List<PerformModel>();
            foreach (SaleType SaleType1 in SaleTypes)
            {
                PerformModel PerformModel1 = new PerformModel();
                PerformModel1.SaleType = SaleType1.Name;
                PerformModel1.Profit = 0;
                PerformModel1.Bonus = 0;
                PerformModels.Add(PerformModel1);
            }
            return PerformModels;
        }

        private List<PerformanceReport> reportProcessNew(IEnumerable<PerformanceReport> ReportsResult)
        {
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            List<SaleType> SaleTypes = UserService1.getAllSaleTypes(false).ToList();


            if (ReportsResult != null && ReportsResult.Count() > 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    PerformanceReport PerformanceReport1 = new PerformanceReport();
                    PerformanceReport1.Month = i;
                    PerformanceReport1.PerformModels = getAllPerformModel(SaleTypes);
                    foreach (PerformanceReport Report in ReportsResult)
                    {
                        if (Report.Month == i)
                        {
                            PerformanceReport1.Plan = Report.Plan;

                            PerformanceReport1.setPerform(Report.PerformModel1);

                            PerformanceReport1.ProfitHandel += Report.ProfitHandel;
                            PerformanceReport1.ProfitSale += Report.ProfitSale;

                            PerformanceReport1.PerformPS = divisible(PerformanceReport1.ProfitSale * 100, PerformanceReport1.Plan);
                            PerformanceReport1.PerformPH = divisible(PerformanceReport1.ProfitHandel * 100, PerformanceReport1.Plan);
                            // PerformanceReport1.SumBonus += Report.PerformModel1.Bonus;
                            //PerformanceReport1.PerformSumProfit = divisible(PerformanceReport1.SumProfit * 100, PerformanceReport1.Plan);
                        }
                    }
                    /*
                    PerformanceReport1.SumProfit = PerformanceReport1.ProfitSale + PerformanceReport1.ProfitHandel;
                    if (PerformanceReport1.Plan > 0)
                    {
                        PerformanceReport1.PerformPH = divisible(PerformanceReport1.ProfitHandel * 100, PerformanceReport1.Plan);
                        PerformanceReport1.PerformPS = divisible(PerformanceReport1.ProfitSale * 100, PerformanceReport1.Plan);

                        PerformanceReport1.PerformSumProfit = divisible(PerformanceReport1.SumProfit * 100, PerformanceReport1.Plan);
                    }*/
                    ReportProcess.Add(PerformanceReport1);
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    PerformanceReport PerformanceReport1 = new PerformanceReport();
                    PerformanceReport1.Month = i;
                    PerformanceReport1.PerformModels = getAllPerformModel(SaleTypes);
                    ReportProcess.Add(PerformanceReport1);
                }
            }
            return ReportProcess;
        }
        private List<PerformanceReport> reportProcess(IEnumerable<PerformanceReport> ReportsResult)
        {
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            if (ReportsResult != null && ReportsResult.Count() > 0)
            {
                for (int i = 1; i <= 12; i++)
                {
                    PerformanceReport PerformanceReport1 = new PerformanceReport();
                    PerformanceReport1.Month = i;
                    foreach (PerformanceReport Report in ReportsResult)
                    {
                        if (Report.Month == i)
                        {
                            PerformanceReport1.ProfitHandel += Report.ProfitHandel;
                            PerformanceReport1.ProfitSale += Report.ProfitSale;
                            PerformanceReport1.Plan = Report.Plan;
                            PerformanceReport1.PerformPS = divisible(PerformanceReport1.ProfitSale * 100, PerformanceReport1.Plan);
                            PerformanceReport1.PerformPH = divisible(PerformanceReport1.ProfitHandel * 100, PerformanceReport1.Plan);
                            PerformanceReport1.SumProfit = PerformanceReport1.ProfitHandel + PerformanceReport1.ProfitSale;
                            PerformanceReport1.PerformSumProfit = divisible(PerformanceReport1.SumProfit * 100, PerformanceReport1.Plan);
                        }
                    }
                    PerformanceReport1.SumProfit = PerformanceReport1.ProfitSale + PerformanceReport1.ProfitHandel;
                    if (PerformanceReport1.Plan > 0)
                    {
                        PerformanceReport1.PerformPH = divisible(PerformanceReport1.ProfitHandel * 100, PerformanceReport1.Plan);
                        PerformanceReport1.PerformPS = divisible(PerformanceReport1.ProfitSale * 100, PerformanceReport1.Plan);

                        PerformanceReport1.PerformSumProfit = divisible(PerformanceReport1.SumProfit * 100, PerformanceReport1.Plan);
                    }
                    ReportProcess.Add(PerformanceReport1);
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    PerformanceReport PerformanceReport1 = new PerformanceReport();
                    PerformanceReport1.Month = i;
                    ReportProcess.Add(PerformanceReport1);
                }
            }
            return ReportProcess;
        }
        public ActionResult DepartmentReportExcel()
        {
            DepartmentportModel model = (DepartmentportModel)Session["DepartmentportModel"];
            int CellX = 3;
            int CellY = 5;
            int MaxSaleType = 4;
            int Year = model.Year;
            int DeptId = model.DeptId;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReportDept(DeptId, Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);

            string RealPath = Path.Combine(Server.MapPath("~/" + "/FileManager/Reports")
                                        , Path.GetFileName("DeptReport.xls"));
            Application xl = new Application();
            Workbook wb = xl.Workbooks.Open(RealPath, 0, false, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            int pos = 0;
            pos = RealPath.LastIndexOf("\\");
            String tamplePath = RealPath.Substring(0, pos + 1) + "DeptTemReport.xls";
            FileInfo fileInfo = new FileInfo(tamplePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            wb.SaveAs(tamplePath);
            Sheets xlsheets = wb.Sheets; //Get the sheets from workbook
            Worksheet excelWorksheet = (Worksheet)xlsheets[1];
            #region processing excel
            //View data for Report
            excelWorksheet.Cells[4][2] = "YEARLY REPORT " + DateTime.Now.Year;
            excelWorksheet.Cells[2][5] = "Year Plan " + DateTime.Now.Year;

            int totalSaleType = ReportProcess.ElementAt(0).PerformModels.Count;
            double[] TotalProfiltSaleTypes = new double[totalSaleType];
            double TotalPlan = 0, TotalProfilt = 0;
            for (int i = 0; i < 12; i++)
            {
                TotalPlan += ReportProcess.ElementAt(i).Plan;
                for (int ti = 0; ti < totalSaleType; ti++)
                {
                    TotalProfiltSaleTypes[ti] += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                    TotalProfilt += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                //TotalHandel += ReportProcess.ElementAt(i).ProfitHandel;
                //TotalSale += ReportProcess.ElementAt(i).ProfitSale;
                excelWorksheet.Cells[CellX + i][CellY] = ReportProcess.ElementAt(i).Plan;
                for (int ti = 0; ti < totalSaleType; ti++)
                {
                    excelWorksheet.Cells[CellX - 1][CellY + ti * 2 + 1] = "Profit Of " + ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).SaleType;
                    excelWorksheet.Cells[CellX - 1][CellY + ti * 2 + 2] = "Perform Of " + ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).SaleType + "(%)";
                    excelWorksheet.Cells[CellX + i][CellY + ti * 2 + 1] = ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                    excelWorksheet.Cells[CellX + i][CellY + ti * 2 + 2] = ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%";
                    excelWorksheet.Cells[15][CellY + ti * 2 + 1] = TotalProfiltSaleTypes[ti];
                    excelWorksheet.Cells[15][CellY + ti * 2 + 2] = divisible(TotalProfiltSaleTypes[ti] * 100, TotalPlan).ToString("0.00") + "%";
                }
                //excelWorksheet.Cells[CellX + i][CellY + 3] = ReportProcess.ElementAt(i).ProfitHandel;
                //excelWorksheet.Cells[CellX + i][CellY + 4] = ReportProcess.ElementAt(i).PerformPH.ToString("0.00") + "%";

                excelWorksheet.Cells[CellX + i][CellY + MaxSaleType * 2 + 1] = ReportProcess.ElementAt(i).SumProfit;
                excelWorksheet.Cells[CellX + i][CellY + MaxSaleType * 2 + 2] = ReportProcess.ElementAt(i).PerformSumProfit.ToString("0.00") + "%";
            }

            excelWorksheet.Cells[15][CellY] = TotalPlan;
            //excelWorksheet.Cells[15][CellY + 1] = TotalSale;
            // excelWorksheet.Cells[15][CellY + 2] = divisible(TotalSale * 100, TotalPlan).ToString("0.00") + "%";
            //excelWorksheet.Cells[15][CellY + 3] = TotalHandel;
            //excelWorksheet.Cells[15][CellY + 4] = divisible(TotalHandel * 100, TotalPlan).ToString("0.00") + "%";
            excelWorksheet.Cells[15][CellY + MaxSaleType * 2 + 1] = TotalProfilt;
            excelWorksheet.Cells[15][CellY + MaxSaleType * 2 + 2] = divisible(TotalProfilt * 100, TotalPlan).ToString("0.00") + "%";

            //delete redundant cell in excel hard line max 4
            for (int ec = 4; ec > totalSaleType; ec--)
            {
                ((Range)excelWorksheet.Rows.get_Item(5 + ec * 2)).Delete();
                ((Range)excelWorksheet.Rows.get_Item(5 + ec * 2 - 1)).Delete();
            }
            #endregion

            #region Sheet2processing
            excelWorksheet = (Worksheet)xlsheets[2];
            EntitySet<User> ListUser = UserService1.getDepartmentById(DeptId).Users;
            int SaleCount = 0;
            int PerSaleCount = 0;
            int TotalRowBegin = 6;
            double[] TotalEachMonth = new double[14];
            foreach (User UserItem in ListUser)
            {
                long Id = UserItem.Id;
                double TotalPlanYear = UserService1.getReportYear(Id, Year);
                Range rangInsert = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount);
                rangInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                Range rangWasInsert = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount);
                SaleCount++;
                PerSaleCount++;

                Range rangPerInsert = (Range)excelWorksheet.Rows.get_Item(9 + PerSaleCount);
                rangPerInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                Range rangPerWasInsert = (Range)excelWorksheet.Rows.get_Item(9 + PerSaleCount);
                PerSaleCount++;
                rangWasInsert.Cells[3] = UserItem.FullName;
                rangPerWasInsert.Cells[5] = UserItem.FullName;
                rangWasInsert.Cells[4] = TotalPlanYear;
                rangWasInsert.Cells[5] = TotalPlanYear / 12;
                ReportsResult = ReportService1.getSaleReport(Id, Year);
                ReportProcess = reportProcess(ReportsResult);
                double TotalPerform = 0;
                for (int i = 0; i < 12; i++)
                {
                    double Perform = ReportProcess.ElementAt(i).ProfitSale + ReportProcess.ElementAt(i).ProfitHandel;
                    rangWasInsert.Cells[6 + i] = Perform;
                    rangPerWasInsert.Cells[6 + i] = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";
                    TotalPerform += Perform;
                    TotalEachMonth[i + 1] += Perform;
                }
                rangWasInsert.Cells[18] = "'" + TotalPerform;
                rangWasInsert.Cells[19] = "'" + rounFloor(TotalPlanYear - TotalPerform);
                rangPerWasInsert.Cells[18] = divisible(TotalPerform * 100, TotalPlanYear).ToString("0.00") + "%";
                rangPerWasInsert.Cells[19] = rounFloor(100 - (divisible(TotalPerform * 100, TotalPlanYear))).ToString("0.00") + "%";
                TotalEachMonth[0] += TotalPlanYear;
                TotalEachMonth[13] += TotalPerform;

            }
            Range rangInsertT = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount);
            rangInsertT.Cells[4] = TotalEachMonth[0];
            rangInsertT.Cells[5] = TotalEachMonth[0] / 12;
            rangInsertT.Cells[18] = "'" + TotalEachMonth[13];
            rangInsertT.Cells[19] = "'" + rounFloor(TotalEachMonth[0] - TotalEachMonth[13]);
            Range rangPerInsertT = (Range)excelWorksheet.Rows.get_Item(9 + PerSaleCount);
            rangPerInsertT.Cells[18] = divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            rangPerInsertT.Cells[19] = rounFloor(100 - (divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]))).ToString("0.00") + "%";
            for (int i = 0; i < 12; i++)
            {
                rangInsertT.Cells[6 + i] = TotalEachMonth[1 + i];
                rangPerInsertT.Cells[6 + i] = divisible(TotalEachMonth[1 + i] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            }
            #endregion
            rangInsertT = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin - 1);
            rangInsertT.Delete();
            rangInsertT = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount + 1);
            rangInsertT.Delete();
            wb.Save();
            wb.Close();
            return File(tamplePath, "application/excel", "YearLyReport.xls");
        }
        public ActionResult DepartmentReport()
        {
            DepartmentportModel model = new DepartmentportModel
            {
                Year = DateTime.Now.Year,
                DeptId = (int) CurrenUser.DeptId
            };
            Session["DepartmentportModel"] = model;  
            return ReportDept(model);
        }
        [HttpPost]
        public ActionResult DepartmentReport(DepartmentportModel model)
        { 
            Session["DepartmentportModel"] = model;
            return ReportDept(model);
        }


        private ActionResult ReportDept(DepartmentportModel model)
        {
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReportDept(model.DeptId, model.Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);
            ViewData["ListDepts"] = new SelectList(UserService1.GetAllDepartmentActive(CurrenUser), "Id", "DeptName");
            ViewData["ReportProcess"] = ReportProcess;
            List<ReportYearModel> ListReport1 = new List<ReportYearModel>();
            List<ReportYearModel> ListReport2 = new List<ReportYearModel>();
            EntitySet<User> ListUser = UserService1.getDepartmentById(model.DeptId).Users;

            double[] TotalEachMonth = new double[14];
            ReportYearModel ReportYearModel1 = null;
            ReportYearModel ReportYearModel2 = null;
            foreach (User UserItem in ListUser)
            {
                ReportYearModel1 = new ReportYearModel();
                ReportYearModel2 = new ReportYearModel();
                long Id = UserItem.Id;
                double TotalPlanYear = UserService1.getReportYear(Id, model.Year);
                ReportYearModel1.SaleName = UserItem.FullName;
                ReportYearModel2.SaleName = UserItem.FullName;
                ReportYearModel1.Plan = TotalPlanYear.ToString();
                ReportYearModel1.PlanPerMonth = (TotalPlanYear / 12).ToString("0");
                ReportsResult = ReportService1.getSaleReport(Id, model.Year);
                ReportProcess = reportProcess(ReportsResult);
                double TotalPerform = 0;
                double Perform = 0;
                for (int i = 0; i < 12; i++)
                {
                    Perform = ReportProcess.ElementAt(i).ProfitSale + ReportProcess.ElementAt(i).ProfitHandel;
                    TotalPerform += Perform;
                    TotalEachMonth[i + 1] += Perform;
                }
                Perform = ReportProcess.ElementAt(0).ProfitSale + ReportProcess.ElementAt(0).ProfitHandel;
                ReportYearModel1.Jan = Perform.ToString();
                ReportYearModel2.Jan = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(1).ProfitSale + ReportProcess.ElementAt(1).ProfitHandel;
                ReportYearModel1.Feb = Perform.ToString();
                ReportYearModel2.Feb = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(2).ProfitSale + ReportProcess.ElementAt(2).ProfitHandel;
                ReportYearModel1.Mar = Perform.ToString();
                ReportYearModel2.Mar = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(3).ProfitSale + ReportProcess.ElementAt(3).ProfitHandel;
                ReportYearModel1.Apr = Perform.ToString();
                ReportYearModel2.Apr = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(4).ProfitSale + ReportProcess.ElementAt(4).ProfitHandel;
                ReportYearModel1.May = Perform.ToString();
                ReportYearModel2.May = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(5).ProfitSale + ReportProcess.ElementAt(5).ProfitHandel;
                ReportYearModel1.Jun = Perform.ToString();
                ReportYearModel2.Jun = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(6).ProfitSale + ReportProcess.ElementAt(6).ProfitHandel;
                ReportYearModel1.Jul = Perform.ToString();
                ReportYearModel2.Jul = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(7).ProfitSale + ReportProcess.ElementAt(7).ProfitHandel;
                ReportYearModel1.Aug = Perform.ToString();
                ReportYearModel2.Aug = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(8).ProfitSale + ReportProcess.ElementAt(8).ProfitHandel;
                ReportYearModel1.Sep = Perform.ToString();
                ReportYearModel2.Sep = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(9).ProfitSale + ReportProcess.ElementAt(9).ProfitHandel;
                ReportYearModel1.Oct = Perform.ToString();
                ReportYearModel2.Oct = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(10).ProfitSale + ReportProcess.ElementAt(10).ProfitHandel;
                ReportYearModel1.Nov = Perform.ToString();
                ReportYearModel2.Nov = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(11).ProfitSale + ReportProcess.ElementAt(11).ProfitHandel;
                ReportYearModel1.Dec = Perform.ToString();
                ReportYearModel2.Dec = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                ReportYearModel1.Total = TotalPerform + "";
                ReportYearModel1.Remain = rounFloor(TotalPlanYear - TotalPerform).ToString("0.00") + "";
                ReportYearModel2.Total = divisible(TotalPerform * 100, TotalPlanYear).ToString("0.00") + "%";
                ReportYearModel2.Remain = rounFloor(100 - divisible(TotalPerform * 100, TotalPlanYear)).ToString("0.00") + "%";

                TotalEachMonth[0] += TotalPlanYear;
                TotalEachMonth[13] += TotalPerform;
                ListReport1.Add(ReportYearModel1);
                ListReport2.Add(ReportYearModel2);
            }
            ReportYearModel1 = new ReportYearModel();
            ReportYearModel2 = new ReportYearModel();
            ReportYearModel1.Plan = TotalEachMonth[0] + "";
            ReportYearModel1.PlanPerMonth = (TotalEachMonth[0] / 12).ToString("0");
            ReportYearModel1.Total = "" + TotalEachMonth[13];
            ReportYearModel1.Remain = "" + rounFloor(TotalEachMonth[0] - TotalEachMonth[13]);

            ReportYearModel2.Total = divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            ReportYearModel2.Remain = rounFloor(100 - divisible(TotalEachMonth[13] * 100, TotalEachMonth[0])).ToString("0.00") + "%";

            ReportYearModel1.Jan = TotalEachMonth[1].ToString();
            ReportYearModel2.Jan = divisible(TotalEachMonth[1] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Feb = TotalEachMonth[2].ToString();
            ReportYearModel2.Feb = divisible(TotalEachMonth[2] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Mar = TotalEachMonth[3].ToString();
            ReportYearModel2.Mar = divisible(TotalEachMonth[3] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Apr = TotalEachMonth[4].ToString();
            ReportYearModel2.Apr = divisible(TotalEachMonth[4] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.May = TotalEachMonth[5].ToString();
            ReportYearModel2.May = divisible(TotalEachMonth[5] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Jun = TotalEachMonth[6].ToString();
            ReportYearModel2.Jun = divisible(TotalEachMonth[6] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Jul = TotalEachMonth[7].ToString();
            ReportYearModel2.Jul = divisible(TotalEachMonth[7] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Aug = TotalEachMonth[8].ToString();
            ReportYearModel2.Aug = divisible(TotalEachMonth[8] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Sep = TotalEachMonth[9].ToString();
            ReportYearModel2.Sep = divisible(TotalEachMonth[9] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Oct = TotalEachMonth[10].ToString();
            ReportYearModel2.Oct = divisible(TotalEachMonth[10] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Nov = TotalEachMonth[11].ToString();
            ReportYearModel2.Nov = divisible(TotalEachMonth[11] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Dec = TotalEachMonth[12].ToString();
            ReportYearModel2.Dec = divisible(TotalEachMonth[12] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            ReportYearModel1.SaleName = "Total";
            ReportYearModel2.SaleName = "Total";
            ListReport1.Add(ReportYearModel1);
            ListReport2.Add(ReportYearModel2);
            ViewData["ListReport1"] = ListReport1;
            ViewData["ListReport2"] = ListReport2;
            return View(model);
        }
        //Offices Report
        public ActionResult OfficeReportExcel()
        {
            OfficeReportModel model = (OfficeReportModel)Session["OfficeReportModel"];
            int CellX = 3;
            int CellY = 5;
            int MaxSaleType = 4;
            int Year = model.Year;
            int OfficeId = model.OfficeId;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReportOffice(OfficeId, Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);

            string RealPath = Path.Combine(Server.MapPath("~/" + "/FileManager/Reports")
                                        , Path.GetFileName("OfficeReport.xls"));
            Application xl = new Application();
            Workbook wb = xl.Workbooks.Open(RealPath, 0, false, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            int pos = 0;
            pos = RealPath.LastIndexOf("\\");
            String tamplePath = RealPath.Substring(0, pos + 1) + "OfficeTemReport.xls";
            FileInfo fileInfo = new FileInfo(tamplePath);
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            wb.SaveAs(tamplePath);
            Sheets xlsheets = wb.Sheets; //Get the sheets from workbook
            Worksheet excelWorksheet = (Worksheet)xlsheets[1];
            #region processing excel
            //View data for Report
            excelWorksheet.Cells[4][2] = "YEARLY REPORT " + DateTime.Now.Year;
            excelWorksheet.Cells[2][5] = "Year Plan " + DateTime.Now.Year;
            int totalSaleType = ReportProcess.ElementAt(0).PerformModels.Count;
            double[] TotalProfiltSaleTypes = new double[totalSaleType];
            double TotalPlan = 0, TotalProfilt = 0;
            for (int i = 0; i < 12; i++)
            {
                TotalPlan += ReportProcess.ElementAt(i).Plan;
                for (int ti = 0; ti < totalSaleType; ti++)
                {
                    TotalProfiltSaleTypes[ti] += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                    TotalProfilt += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                }
            }

            for (int i = 0; i < 12; i++)
            {
                //TotalHandel += ReportProcess.ElementAt(i).ProfitHandel;
                //TotalSale += ReportProcess.ElementAt(i).ProfitSale;
                excelWorksheet.Cells[CellX + i][CellY] = ReportProcess.ElementAt(i).Plan;
                for (int ti = 0; ti < totalSaleType; ti++)
                {
                    excelWorksheet.Cells[CellX - 1][CellY + ti * 2 + 1] = "Profit Of " + ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).SaleType;
                    excelWorksheet.Cells[CellX - 1][CellY + ti * 2 + 2] = "Perform Of " + ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).SaleType + "(%)";
                    excelWorksheet.Cells[CellX + i][CellY + ti * 2 + 1] = ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                    excelWorksheet.Cells[CellX + i][CellY + ti * 2 + 2] = ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%";
                    excelWorksheet.Cells[15][CellY + ti * 2 + 1] = TotalProfiltSaleTypes[ti];
                    excelWorksheet.Cells[15][CellY + ti * 2 + 2] = divisible(TotalProfiltSaleTypes[ti] * 100, TotalPlan).ToString("0.00") + "%";
                }
                //excelWorksheet.Cells[CellX + i][CellY + 3] = ReportProcess.ElementAt(i).ProfitHandel;
                //excelWorksheet.Cells[CellX + i][CellY + 4] = ReportProcess.ElementAt(i).PerformPH.ToString("0.00") + "%";

                excelWorksheet.Cells[CellX + i][CellY + MaxSaleType * 2 + 1] = ReportProcess.ElementAt(i).SumProfit;
                excelWorksheet.Cells[CellX + i][CellY + MaxSaleType * 2 + 2] = ReportProcess.ElementAt(i).PerformSumProfit.ToString("0.00") + "%";
            }

            excelWorksheet.Cells[15][CellY] = TotalPlan;
            //excelWorksheet.Cells[15][CellY + 1] = TotalSale;
            // excelWorksheet.Cells[15][CellY + 2] = divisible(TotalSale * 100, TotalPlan).ToString("0.00") + "%";
            //excelWorksheet.Cells[15][CellY + 3] = TotalHandel;
            //excelWorksheet.Cells[15][CellY + 4] = divisible(TotalHandel * 100, TotalPlan).ToString("0.00") + "%";
            excelWorksheet.Cells[15][CellY + MaxSaleType * 2 + 1] = TotalProfilt;
            excelWorksheet.Cells[15][CellY + MaxSaleType * 2 + 2] = divisible(TotalProfilt * 100, TotalPlan).ToString("0.00") + "%";

            //delete redundant cell in excel hard line max 4
            for (int ec = 4; ec > totalSaleType; ec--)
            {
                ((Range)excelWorksheet.Rows.get_Item(5 + ec * 2)).Delete();
                ((Range)excelWorksheet.Rows.get_Item(5 + ec * 2 - 1)).Delete();
            }
            #endregion

            #region Sheet2processing
            excelWorksheet = (Worksheet)xlsheets[2];
            EntitySet<Department> ListDept = UserService1.getCompanyById(model.OfficeId).Departments;
            int SaleCount = 0;
            int PerSaleCount = 0;
            int TotalRowBegin = 6;
            double[] TotalEachMonth = new double[14];
            foreach (Department DepartmentItem in ListDept)
            {
                long Id = DepartmentItem.Id;
                double TotalPlanYear = UserService1.getReportYearDept(Id, Year);
                Range rangInsert = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount);
                rangInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                Range rangWasInsert = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount);
                SaleCount++;
                PerSaleCount++;

                Range rangPerInsert = (Range)excelWorksheet.Rows.get_Item(9 + PerSaleCount);
                rangPerInsert.Insert(Microsoft.Office.Interop.Excel.XlInsertShiftDirection.xlShiftDown);
                Range rangPerWasInsert = (Range)excelWorksheet.Rows.get_Item(9 + PerSaleCount);
                PerSaleCount++;
                rangWasInsert.Cells[3] = DepartmentItem.DeptName;
                rangPerWasInsert.Cells[5] = DepartmentItem.DeptName;
                rangWasInsert.Cells[4] = TotalPlanYear;
                rangWasInsert.Cells[5] = TotalPlanYear / 12;
                ReportsResult = ReportService1.getSaleReportDept(Id, Year);
                ReportProcess = reportProcessNew(ReportsResult);
                double TotalPerform = 0;
                for (int i = 0; i < 12; i++)
                {
                    double Perform = ReportProcess.ElementAt(i).ProfitSale + ReportProcess.ElementAt(i).ProfitHandel;
                    rangWasInsert.Cells[6 + i] = Perform;
                    rangPerWasInsert.Cells[6 + i] = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";
                    TotalPerform += Perform;
                    TotalEachMonth[i + 1] += Perform;
                }
                rangWasInsert.Cells[18] = "'" + TotalPerform;
                rangWasInsert.Cells[19] = "'" + rounFloor(TotalPlanYear - TotalPerform);
                rangPerWasInsert.Cells[18] = divisible(TotalPerform * 100, TotalPlanYear).ToString("0.00") + "%";
                rangPerWasInsert.Cells[19] = rounFloor(100 - (divisible(TotalPerform * 100, TotalPlanYear))).ToString("0.00") + "%";
                TotalEachMonth[0] += TotalPlanYear;
                TotalEachMonth[13] += TotalPerform;

            }
            Range rangInsertT = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount);
            rangInsertT.Cells[4] = TotalEachMonth[0];
            rangInsertT.Cells[5] = TotalEachMonth[0] / 12;
            rangInsertT.Cells[18] = "'" + TotalEachMonth[13];
            rangInsertT.Cells[19] = "'" + rounFloor(TotalEachMonth[0] - TotalEachMonth[13]);
            Range rangPerInsertT = (Range)excelWorksheet.Rows.get_Item(9 + PerSaleCount);
            rangPerInsertT.Cells[18] = divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            rangPerInsertT.Cells[19] = rounFloor(100 - (divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]))).ToString("0.00") + "%";
            for (int i = 0; i < 12; i++)
            {
                rangInsertT.Cells[6 + i] = TotalEachMonth[1 + i];
                rangPerInsertT.Cells[6 + i] = divisible(TotalEachMonth[1 + i] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            }
            #endregion
            rangInsertT = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin - 1);
            rangInsertT.Delete();
            rangInsertT = (Range)excelWorksheet.Rows.get_Item(TotalRowBegin + SaleCount + 1);
            rangInsertT.Delete();
            wb.Save();
            wb.Close();
            return File(tamplePath, "application/excel", "YearLyReport.xls");
        }
        public ActionResult OfficeReport()
        {
            int Year = DateTime.Now.Year;
            OfficeReportModel model = new OfficeReportModel();
            model.Year = DateTime.Now.Year;
            model.OfficeId = (int)CurrenUser.Company.Id;
            Session["OfficeReportModel"] = model;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReportOffice(model.OfficeId, Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);
            ViewData["ListOffices"] = new SelectList(UserService1.getAllCompany(), "Id", "CompanyName");
            ViewData["ReportProcess"] = ReportProcess;
            List<ReportYearModel> ListReport1 = new List<ReportYearModel>();
            List<ReportYearModel> ListReport2 = new List<ReportYearModel>();
            EntitySet<Department> ListDept = UserService1.getCompanyById(model.OfficeId).Departments;

            double[] TotalEachMonth = new double[14];
            ReportYearModel ReportYearModel1 = null;
            ReportYearModel ReportYearModel2 = null;
            foreach (Department DeptItem in ListDept)
            {
                if (!DeptItem.IsActive) continue;
                ReportYearModel1 = new ReportYearModel();
                ReportYearModel2 = new ReportYearModel();
                long Id = DeptItem.Id;
                double TotalPlanYear = UserService1.getReportYearDept(Id, Year);
                ReportYearModel1.SaleName = DeptItem.DeptName;
                ReportYearModel2.SaleName = DeptItem.DeptName;
                ReportYearModel1.Plan = TotalPlanYear.ToString();
                ReportYearModel1.PlanPerMonth = (TotalPlanYear / 12).ToString("0");
                ReportsResult = ReportService1.getSaleReportDept(Id, Year);
                ReportProcess = reportProcessNew(ReportsResult);
                double TotalPerform = 0;
                double Perform = 0;
                for (int i = 0; i < 12; i++)
                {
                    Perform = ReportProcess.ElementAt(i).ProfitSale + ReportProcess.ElementAt(i).ProfitHandel;
                    TotalPerform += Perform;
                    TotalEachMonth[i + 1] += Perform;
                }
                Perform = ReportProcess.ElementAt(0).ProfitSale + ReportProcess.ElementAt(0).ProfitHandel;
                ReportYearModel1.Jan = Perform.ToString();
                ReportYearModel2.Jan = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(1).ProfitSale + ReportProcess.ElementAt(1).ProfitHandel;
                ReportYearModel1.Feb = Perform.ToString();
                ReportYearModel2.Feb = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(2).ProfitSale + ReportProcess.ElementAt(2).ProfitHandel;
                ReportYearModel1.Mar = Perform.ToString();
                ReportYearModel2.Mar = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(3).ProfitSale + ReportProcess.ElementAt(3).ProfitHandel;
                ReportYearModel1.Apr = Perform.ToString();
                ReportYearModel2.Apr = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(4).ProfitSale + ReportProcess.ElementAt(4).ProfitHandel;
                ReportYearModel1.May = Perform.ToString();
                ReportYearModel2.May = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(5).ProfitSale + ReportProcess.ElementAt(5).ProfitHandel;
                ReportYearModel1.Jun = Perform.ToString();
                ReportYearModel2.Jun = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(6).ProfitSale + ReportProcess.ElementAt(6).ProfitHandel;
                ReportYearModel1.Jul = Perform.ToString();
                ReportYearModel2.Jul = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(7).ProfitSale + ReportProcess.ElementAt(7).ProfitHandel;
                ReportYearModel1.Aug = Perform.ToString();
                ReportYearModel2.Aug = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(8).ProfitSale + ReportProcess.ElementAt(8).ProfitHandel;
                ReportYearModel1.Sep = Perform.ToString();
                ReportYearModel2.Sep = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(9).ProfitSale + ReportProcess.ElementAt(9).ProfitHandel;
                ReportYearModel1.Oct = Perform.ToString();
                ReportYearModel2.Oct = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(10).ProfitSale + ReportProcess.ElementAt(10).ProfitHandel;
                ReportYearModel1.Nov = Perform.ToString();
                ReportYearModel2.Nov = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(11).ProfitSale + ReportProcess.ElementAt(11).ProfitHandel;
                ReportYearModel1.Dec = Perform.ToString();
                ReportYearModel2.Dec = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                ReportYearModel1.Total = TotalPerform + "";
                ReportYearModel1.Remain = rounFloor(TotalPlanYear - TotalPerform).ToString("0.00") + "";
                ReportYearModel2.Total = divisible(TotalPerform * 100, TotalPlanYear).ToString("0.00") + "%";
                ReportYearModel2.Remain = rounFloor(100 - divisible(TotalPerform * 100, TotalPlanYear)).ToString("0.00") + "%";

                TotalEachMonth[0] += TotalPlanYear;
                TotalEachMonth[13] += TotalPerform;
                ListReport1.Add(ReportYearModel1);
                ListReport2.Add(ReportYearModel2);
            }
            ReportYearModel1 = new ReportYearModel();
            ReportYearModel2 = new ReportYearModel();
            ReportYearModel1.Plan = TotalEachMonth[0] + "";
            ReportYearModel1.PlanPerMonth = (TotalEachMonth[0] / 12).ToString("0");
            ReportYearModel1.Total = "" + TotalEachMonth[13];
            ReportYearModel1.Remain = "" + rounFloor(TotalEachMonth[0] - TotalEachMonth[13]);

            ReportYearModel2.Total = divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            ReportYearModel2.Remain = rounFloor(100 - divisible(TotalEachMonth[13] * 100, TotalEachMonth[0])).ToString("0.00") + "%";

            ReportYearModel1.Jan = TotalEachMonth[1].ToString();
            ReportYearModel2.Jan = divisible(TotalEachMonth[1] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Feb = TotalEachMonth[2].ToString();
            ReportYearModel2.Feb = divisible(TotalEachMonth[2] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Mar = TotalEachMonth[3].ToString();
            ReportYearModel2.Mar = divisible(TotalEachMonth[3] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Apr = TotalEachMonth[4].ToString();
            ReportYearModel2.Apr = divisible(TotalEachMonth[4] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.May = TotalEachMonth[5].ToString();
            ReportYearModel2.May = divisible(TotalEachMonth[5] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Jun = TotalEachMonth[6].ToString();
            ReportYearModel2.Jun = divisible(TotalEachMonth[6] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Jul = TotalEachMonth[7].ToString();
            ReportYearModel2.Jul = divisible(TotalEachMonth[7] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Aug = TotalEachMonth[8].ToString();
            ReportYearModel2.Aug = divisible(TotalEachMonth[8] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Sep = TotalEachMonth[9].ToString();
            ReportYearModel2.Sep = divisible(TotalEachMonth[9] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Oct = TotalEachMonth[10].ToString();
            ReportYearModel2.Oct = divisible(TotalEachMonth[10] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Nov = TotalEachMonth[11].ToString();
            ReportYearModel2.Nov = divisible(TotalEachMonth[11] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Dec = TotalEachMonth[12].ToString();
            ReportYearModel2.Dec = divisible(TotalEachMonth[12] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            ReportYearModel1.SaleName = "Total";
            ReportYearModel2.SaleName = "Total";
            ListReport1.Add(ReportYearModel1);
            ListReport2.Add(ReportYearModel2);
            ViewData["ListReport1"] = ListReport1;
            ViewData["ListReport2"] = ListReport2;
            return View(model);
        }
        [HttpPost]
        public ActionResult OfficeReport(OfficeReportModel model)
        {
            int Year = model.Year;
            int OfficeId = model.OfficeId;
            Session["OfficeReportModel"] = model;
            IEnumerable<PerformanceReport> ReportsResult = ReportService1.getSaleReportOffice(OfficeId, Year);
            List<PerformanceReport> ReportProcess = new List<PerformanceReport>(12);
            ReportProcess = reportProcessNew(ReportsResult);
            ViewData["ListOffices"] = new SelectList(UserService1.getAllCompany(), "Id", "CompanyName");
            ViewData["ReportProcess"] = ReportProcess;

            List<ReportYearModel> ListReport1 = new List<ReportYearModel>();
            List<ReportYearModel> ListReport2 = new List<ReportYearModel>();
            EntitySet<Department> ListDept = UserService1.getCompanyById(model.OfficeId).Departments;

            double[] TotalEachMonth = new double[14];
            ReportYearModel ReportYearModel1 = null;
            ReportYearModel ReportYearModel2 = null;
            foreach (Department DeptItem in ListDept)
            {
                if (!DeptItem.IsActive) continue;
                ReportYearModel1 = new ReportYearModel();
                ReportYearModel2 = new ReportYearModel();
                long Id = DeptItem.Id;
                double TotalPlanYear = UserService1.getReportYearDept(Id, Year);
                ReportYearModel1.SaleName = DeptItem.DeptName;
                ReportYearModel2.SaleName = DeptItem.DeptName;
                ReportYearModel1.Plan = TotalPlanYear.ToString();
                ReportYearModel1.PlanPerMonth = (TotalPlanYear / 12).ToString("0");
                ReportsResult = ReportService1.getSaleReportDept(Id, Year);
                ReportProcess = reportProcessNew(ReportsResult);
                double TotalPerform = 0;
                double Perform = 0;
                for (int i = 0; i < 12; i++)
                {
                    Perform = ReportProcess.ElementAt(i).ProfitSale + ReportProcess.ElementAt(i).ProfitHandel;
                    TotalPerform += Perform;
                    TotalEachMonth[i + 1] += Perform;
                }
                Perform = ReportProcess.ElementAt(0).ProfitSale + ReportProcess.ElementAt(0).ProfitHandel;
                ReportYearModel1.Jan = Perform.ToString();
                ReportYearModel2.Jan = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(1).ProfitSale + ReportProcess.ElementAt(1).ProfitHandel;
                ReportYearModel1.Feb = Perform.ToString();
                ReportYearModel2.Feb = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(2).ProfitSale + ReportProcess.ElementAt(2).ProfitHandel;
                ReportYearModel1.Mar = Perform.ToString();
                ReportYearModel2.Mar = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(3).ProfitSale + ReportProcess.ElementAt(3).ProfitHandel;
                ReportYearModel1.Apr = Perform.ToString();
                ReportYearModel2.Apr = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(4).ProfitSale + ReportProcess.ElementAt(4).ProfitHandel;
                ReportYearModel1.May = Perform.ToString();
                ReportYearModel2.May = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(5).ProfitSale + ReportProcess.ElementAt(5).ProfitHandel;
                ReportYearModel1.Jun = Perform.ToString();
                ReportYearModel2.Jun = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(6).ProfitSale + ReportProcess.ElementAt(6).ProfitHandel;
                ReportYearModel1.Jul = Perform.ToString();
                ReportYearModel2.Jul = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(7).ProfitSale + ReportProcess.ElementAt(7).ProfitHandel;
                ReportYearModel1.Aug = Perform.ToString();
                ReportYearModel2.Aug = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(8).ProfitSale + ReportProcess.ElementAt(8).ProfitHandel;
                ReportYearModel1.Sep = Perform.ToString();
                ReportYearModel2.Sep = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(9).ProfitSale + ReportProcess.ElementAt(9).ProfitHandel;
                ReportYearModel1.Oct = Perform.ToString();
                ReportYearModel2.Oct = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(10).ProfitSale + ReportProcess.ElementAt(10).ProfitHandel;
                ReportYearModel1.Nov = Perform.ToString();
                ReportYearModel2.Nov = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                Perform = ReportProcess.ElementAt(11).ProfitSale + ReportProcess.ElementAt(11).ProfitHandel;
                ReportYearModel1.Dec = Perform.ToString();
                ReportYearModel2.Dec = divisible(Perform * 100, TotalPlanYear).ToString("0.00") + "%";

                ReportYearModel1.Total = TotalPerform + "";
                ReportYearModel1.Remain = rounFloor(TotalPlanYear - TotalPerform).ToString("0.00") + "";
                ReportYearModel2.Total = divisible(TotalPerform * 100, TotalPlanYear).ToString("0.00") + "%";
                ReportYearModel2.Remain = rounFloor(100 - divisible(TotalPerform * 100, TotalPlanYear)).ToString("0.00") + "%";

                TotalEachMonth[0] += TotalPlanYear;
                TotalEachMonth[13] += TotalPerform;
                ListReport1.Add(ReportYearModel1);
                ListReport2.Add(ReportYearModel2);
            }
            ReportYearModel1 = new ReportYearModel();
            ReportYearModel2 = new ReportYearModel();
            ReportYearModel1.Plan = TotalEachMonth[0] + "";
            ReportYearModel1.PlanPerMonth = (TotalEachMonth[0] / 12).ToString("0");
            ReportYearModel1.Total = "" + TotalEachMonth[13];
            ReportYearModel1.Remain = "" + rounFloor(TotalEachMonth[0] - TotalEachMonth[13]);

            ReportYearModel2.Total = divisible(TotalEachMonth[13] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            ReportYearModel2.Remain = rounFloor(100 - divisible(TotalEachMonth[13] * 100, TotalEachMonth[0])).ToString("0.00") + "%";

            ReportYearModel1.Jan = TotalEachMonth[1].ToString();
            ReportYearModel2.Jan = divisible(TotalEachMonth[1] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Feb = TotalEachMonth[2].ToString();
            ReportYearModel2.Feb = divisible(TotalEachMonth[2] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Mar = TotalEachMonth[3].ToString();
            ReportYearModel2.Mar = divisible(TotalEachMonth[3] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Apr = TotalEachMonth[4].ToString();
            ReportYearModel2.Apr = divisible(TotalEachMonth[4] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.May = TotalEachMonth[5].ToString();
            ReportYearModel2.May = divisible(TotalEachMonth[5] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Jun = TotalEachMonth[6].ToString();
            ReportYearModel2.Jun = divisible(TotalEachMonth[6] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Jul = TotalEachMonth[7].ToString();
            ReportYearModel2.Jul = divisible(TotalEachMonth[7] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Aug = TotalEachMonth[8].ToString();
            ReportYearModel2.Aug = divisible(TotalEachMonth[8] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Sep = TotalEachMonth[9].ToString();
            ReportYearModel2.Sep = divisible(TotalEachMonth[9] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Oct = TotalEachMonth[10].ToString();
            ReportYearModel2.Oct = divisible(TotalEachMonth[10] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Nov = TotalEachMonth[11].ToString();
            ReportYearModel2.Nov = divisible(TotalEachMonth[11] * 100, TotalEachMonth[0]).ToString("0.00") + "%";

            ReportYearModel1.Dec = TotalEachMonth[12].ToString();
            ReportYearModel2.Dec = divisible(TotalEachMonth[12] * 100, TotalEachMonth[0]).ToString("0.00") + "%";
            ReportYearModel1.SaleName = "Total";
            ReportYearModel2.SaleName = "Total";
            ListReport1.Add(ReportYearModel1);
            ListReport2.Add(ReportYearModel2);
            ViewData["ListReport1"] = ListReport1;
            ViewData["ListReport2"] = ListReport2;
            return View(model);
        }

        public ActionResult RepostSummary()
        {
            SalePerformamceModel performamceModel = null;
            performamceModel = (SalePerformamceModel)Session["SalePerformamceModel1"];
            if (performamceModel == null)
            {
                performamceModel = new SalePerformamceModel();
                DateTime date = DateTime.Now;
                performamceModel.Month = date.Month;
                performamceModel.Year = date.Year;
            }
            //  SalePerformanceProcess(ComId, DeptId, salePerformamceModel);
            SalePerformceGetAll(performamceModel);
            return View(performamceModel);
        }
        [HttpPost]
        public ActionResult RepostSummary(SalePerformamceModel model, long? comId = null)
        {
            SalePerformceGetAll(model, comId);
            return View(model);
        }

        public void SalePerformceGetAll(SalePerformamceModel model, long? comId = null)
        {
            DateTime searchDate = DateTime.Now;
            DateTime searchDateTo = DateTime.Now;
            model.Year = model.Year == 0 ? searchDate.Year : model.Year;
            model.Month = model.Month == 0 ? searchDate.Month : model.Month;
            if (model.Priod == 0)
            {
                searchDate = DateUtils.Convert2DateTime("01/" + DateUtils.ConvertDay("" + model.Month) + "/" + model.Year);
                searchDateTo = DateUtils.Convert2DateTime("" + DateUtils.ConvertDay("" + DateTime.DaysInMonth(model.Year, model.Month)) + "/" + DateUtils.ConvertDay("" + model.Month) + "/" + model.Year);
            }
            else
            {
                try
                {
                    searchDate = DateUtils.Convert2DateTime(model.DateFrom);
                    searchDateTo = model.DateTo != null ? DateUtils.Convert2DateTime(model.DateTo) : searchDateTo.AddDays(1);
                }
                catch (Exception e)
                {
                    Logger.LogError(e);
                }
            }

            var viewPerformancesCom = performanceService.GetPerformancesCom(searchDate, searchDateTo, comId);
            ViewData["ViewPerformancesCom"] = summaryViewPerformance(viewPerformancesCom.ToList());
            /* var quantityUnits1Com = ReportService1.getQuantityUnitsCom(viewPerformancesCom, searchDate, searchDateTo);
             ViewData["QuantityUnits1Com"] = quantityUnits1Com.ToList();*/
        }
    }
}
