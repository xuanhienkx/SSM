using System.ComponentModel.DataAnnotations;

namespace SSM.Models.CRM
{
    public class CRMContactModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        [Display(Name = "Họ tên")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]  
        public string Phone { get; set; }

        public string Phone2 { get; set; }
        [Required(ErrorMessage = "{0} không được để trống")]
        public string Email { get; set; }

        public string Email2 { get; set; }
        public long CmrCustomerId { get; set; }
        public CRMCustomer CRMCustomer { get; set; }
        public int Order { get; set; }
    }
}