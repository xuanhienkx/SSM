using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations; 

namespace SSM.Models
{
    public class SupplierModels
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Supplier Name")]
        public string FullName { get; set; }
        [DisplayName("Type")]
        public string Type { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("PhoneNumber")]
        public string PhoneNumber { get; set; }
        [DisplayName("Fax")]
        public string Fax { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("Company Name")]//Abb Name
        [Required]
        public string CompanyName { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CountryId { get; set; } 
        public DateTime DateCreate { get; set; }
        public DateTime? DateModify { get; set; }
    }
}