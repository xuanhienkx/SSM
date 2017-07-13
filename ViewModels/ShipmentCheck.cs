namespace SSM.ViewModels
{
    public enum ColorStatus
    {
        SalesRevised = 1,
        AcctRequest = 2,
        NoBill = 3
    }
    public class ShipmentCheck
    {
        public bool IsTradingCheck { get; set; }
        public bool IsFreightCheck { get; set; }
        public bool IsControl { get; set; }
        public long SearchQuickView { get; set; }
        public ColorStatus ColorStatus { get; set; }

    }
}