using System.Collections.Generic;
using SSM.Models;

namespace SSM.ViewModels
{
    public class SummaryInventoryViewModel
    {
        public IssueVoucherModel IssueVoucher { get; set; }
        public List<MonthYear> Summary { get; set; }
    }
}