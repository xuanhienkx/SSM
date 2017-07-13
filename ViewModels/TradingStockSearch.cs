using System;
using Microsoft.Office.Interop.Excel;
using SSM.Models;

namespace SSM.ViewModels
{
    public class TradingStockSearch
    {
        public DateTime VoucherDate { get; set; }
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public Warehouse Warehouse { get; set; }
        public User CreatedBy { get; set; }
        public long CreatedId { get; set; }
        public string HBL { get; set; }
        public string MBL { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public SSM.Services.SortField SortField { get; set; }
    }
}