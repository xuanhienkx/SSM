using System;

namespace SSM.Models
{
    public class SOAModel
    {
        public long AgentId { get; set; }
        public long ShipmentId { get; set; }
        public String DateFrom { get; set; }
        public String DateTo { get; set; }
        public double Balance { get; set; }
        public double Amount { get; set; }
        public String Currency { get; set; }
        public String TypeNote { get; set; }
        public bool IsPayment { get; set; }

        public SOAModel() { }
        public SOAModel(String _Currency, String _TypeNote, double _amount)
        {
            this.Currency = _Currency;
            this.Amount = _amount;
            this.TypeNote = _TypeNote;
            //this.IsPayment = _isPayment;
        }

    }
}