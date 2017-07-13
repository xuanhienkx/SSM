using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models.CRM// SSM.Models.CRM
{
    public class CRMPriceQuotationModel
    {
        public long Id { get; set; }
        [Required(ErrorMessage = "{0} không được để trống!")]
        public string Subject { get; set; }

        public string Description { get; set; }
        [Required(ErrorMessage = "{0} không được để trống!")]
        public string CrmCusName { get; set; }
        public long CrmCusId { get; set; }

        public int CountSendMail { get; set; }

        public int StatusId { get; set; }

        public User ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public User CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? LastDateSend { get; set; }

        public bool IsDelete { get; set; }

        public List<CRMEmailHistory> CRMEmailHistories { get; set; }

        public CRMCustomer CRMCustomer { get; set; }
        public bool IsCusCreate { get; set; }
    }

    public enum CRMPriceStaus : byte
    {
        [StringLabel("Tất cả")]
        All = 0,
        [StringLabel("Đang theo dõi")]
        Following = 1,
        [StringLabel("Thành công")]
        Finished = 2,
        [StringLabel("Huỷ")]
        Cancel = 3,
        //[StringLabel("Phản hồi")]
        //Confirm = 4,
        [StringLabel("Orther")]
        Orther = 9,
    }

    public class PriceSearchModel
    {
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string CustomerName { get; set; }
        public string Subject { get; set; }
        public string DateType { get; set; }
        public long CusId { get; set; }
        public long SalesId { get; set; }
        public long DepId { get; set; }
        public long? RefId { get; set; }
    }
}