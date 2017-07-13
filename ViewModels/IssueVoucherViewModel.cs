using System;
using SSM.Models;

namespace SSM.ViewModels
{
    public class IssueVoucherViewModel
    {
        public DateTime VoucherDate { get; set; }
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
        public Warehouse Warehouse { get; set; }
        public int Quantity { get; set; }
        public string UOM { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string VoucherNo { get; set; }
    }

    public class StockCardFilter
    {
        public long ProductId { get; set; }
        public long SupplierId { get; set; }
        public int WarehouseId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}