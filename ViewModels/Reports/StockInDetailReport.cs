namespace SSM.ViewModels.Reports
{
    public class StockInDetailReport
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Unit { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseAddress { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}