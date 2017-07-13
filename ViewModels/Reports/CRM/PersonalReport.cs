using System.Collections.Generic;
using System.Text.RegularExpressions;
using SSM.Models;

namespace SSM.ViewModels.Reports.CRM
{
    public class PersonalReport
    {
        public string PlanName { get; set; }
        public int PlanValue { get; set; }
        public int PlanValueNextMonth { get; set; }
        public int FistExc { get; set; }
        public int LastExc { get; set; }
        public int TotalExc { get; set; }
        public int Odds { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal FrirsPercen
        {
            get
            {
                return PlanValue > 0 ? (FistExc / PlanValue) * 100 : 0;
            }
        }

        public int LastPercen
        {
            get
            {
                return PlanValue > 0 ? (LastExc / PlanValue) * 100 : 0;
            }
        }
        public int TotalPercen
        {
            get
            {
                return PlanValue > 0 ? (TotalExc / PlanValue) * 100 : 0;
            }
        }
        public string Classification
        {
            get
            {
                if (TotalPercen >= 78)
                    return "A";
                if (TotalPercen >= 48)
                    return "B";
                return "C";
            }
        }

    }

    public class CustomerSammaryReport
    {
        public CRMCustomer Customer { get; set; }
        public int Month { get; set; }
        public int ShipmentCount { get; set; } 
    }

    public class SalesTypeSummaryReport
    {
        public SaleType SaleType { get; set; }
        public List<CustomerSammaryReport> SammaryReports { get; set; }
    }

    public class PlanYearSummaryReport
    {
        public string PlanName{ get; set; }
        public int Month{ get; set; }
        public int Year{ get; set; }
        public int Execu { get; set; }
        public int Odds { get; set; }
    }
}