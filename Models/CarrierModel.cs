using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public enum CarrierType
    {
        ShippingLine,
        AirLine,
        Fowarder,
        Other
    }
    public class CarrierModel
    {
       
        public long Id { get; set; }
        [Required]
        [DisplayName("Name")]
        public String CarrierAirLineName { get; set; }
        public String Type { get; set; }
        [DisplayName("Address")]
        public String Address { get; set; }
        [DisplayName("PhoneNumber")]
        public String PhoneNumber { get; set; }
        [DisplayName("Fax")]
        public String Fax { get; set; }
        [DisplayName("Email")]
        public String AbbName { get; set; }
        [DisplayName("Description")]
        public String Description { get; set; } 
        public bool IsActive { get; set; }
        public bool IsHideUser { get; set; }
        public bool IsSee { get; set; }
    }
}