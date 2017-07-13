using System;

namespace SSM.ViewModels.Reports
{
    public class ProfitReportModel
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public decimal Profit { get; set; }
        public int TotalShipment { get; set; }
    }

    public class FilterProfit
    {
        // khongo tinh console
        // tinh console 
        // Quanty tinh console hoac khong
        // All, 10,20,30, profit =0 khong show
//If console not lole
//If lole not console
        // profit khong bao gio tinh console, 
        public TypeFilterProfit TypeFilterProfit { get; set; }
        public int Top { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    public enum TypeFilterProfit
    {
        Agent,
        Department,
        Cnee,
        Shipper,
        Sales,
        SalesType,
        Servics 
    }
}