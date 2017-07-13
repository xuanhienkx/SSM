using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class WareHouseModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Warehouse Name")]
        public string Name { get; set; }
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
        [DisplayName("Code")]
        [Required]
        public string Code { get; set; }
        [DisplayName("Description")]
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? AreaId { get; set; } 
        public DateTime DateCreate { get; set; }
        public DateTime? DateModify { get; set; }
    }
}