using System;
using System.Collections.Generic;
using SSM.Models;

namespace SSM.Models
{
    public class IssueVoucherModel
    {
        public DateTime VoucherDate { get; set; }
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
        public Customer Customer { get; set; }
        public Warehouse Warehouse { get; set; }
        public decimal Quantity { get; set; }
        public decimal QuantityPendingOut { get; set; }
        public decimal QuantityOut { get; set; }
        public string UOM { get; set; }
        public decimal Price { get; set; }
        public decimal PriceOut { get; set; }
        public decimal Amount { get; set; }
        public decimal Amount0 { get; set; }
        public decimal AmountOut { get; set; }
        public decimal AmountInventory { get; set; }
        public string VoucherNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool StockIn { get; set; }
        public bool StockOut { get; set; }
        public bool HashOut { get; set; }
        public int Year { get; set; }
        public int TopRowDetail { get; set; }
    }

    public class SummaryInventory
    {
        public int Year { get; set; }
        public decimal Quntity { get; set; }  
        public decimal SumAmount { get; set; }
        public List<MonthYear> MonthYears { get; set; }
        
    }

    public class MonthYear
    {
        public int Month { get; set; }
        public decimal Qty { get; set; }
        public decimal QtyOut { get; set; }
        public decimal AmmountIn{ get; set; }
        public decimal AmmountOut{ get; set; }
        public decimal Ammount0 { get; set; }
    }
}