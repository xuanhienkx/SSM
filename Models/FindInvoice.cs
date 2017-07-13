using System;

namespace SSM.Models
{
    public class FindInvoice
    {
        public String InvoiceNo { get; set; }
        public long ShipmentId { get; set; }
        public int ShipmentPriod { get; set; }
        public String DateFrom { get; set; }
        public String DateTo { get; set; }
        public bool UnIssueInvoice { get; set; }
    }
}