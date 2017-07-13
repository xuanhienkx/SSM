using System;

namespace SSM.Models.CRM
{
    public class CRMEmailHistoryModel
    {
        public long Id { get; set; }
        public string ToAddress { get; set; }
        public string CcAddress { get; set; }
        public string Subject { get; set; }
        public DateTime DateSend { get; set; }
        public long PriceId { get; set; }
        public CRMPriceQuotation PriceQuotation { get; set; }
    }
}