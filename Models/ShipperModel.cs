using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class ShipperModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Shipper Name")]
        public String ShipperName { get; set; }
        [DisplayName("Address")]
        public String Address { get; set; }
        [DisplayName("PhoneNumber")]
        public String PhoneNumber { get; set; }
        [DisplayName("Email")]
        public String Email { get; set; }
        [DisplayName("Description")]
        public String Description { get; set; }
    }
}