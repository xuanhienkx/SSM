using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public enum CustomerType
    {
        [StringLabel("Shipper")]
        Shipper,
        [StringLabel("Cnee")]
        Cnee,
        [StringLabel("Shipper & Cnee")]
        ShipperCnee,
        [StringLabel("Trading")]
        Trading,
        [StringLabel("Other")]
        CoType,

    }
    public class CustomerModel
    {

        public long Id { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public String FullName { get; set; }
        [DisplayName("Type")]
        public String Type { get; set; }
        [DisplayName("Address")]
        public String Address { get; set; }
        [DisplayName("PhoneNumber")]
        public String PhoneNumber { get; set; }
        [DisplayName("Fax")]
        public String Fax { get; set; }
        [DisplayName("Email")]
        public String Email { get; set; }
        [DisplayName("CompanyName")]
        [Required]
        public String CompanyName { get; set; }
        [DisplayName("Description")]
        public String Description { get; set; }
        public long UserId { get; set; }
        [DisplayName("See")]
        public bool IsSee { get; set; }
        [DisplayName("Move")]
        public bool IsMove { get; set; }
        public User MovedByUser { get; set; }
        public long MovedUserId { get; set; }
        public long CrmCusId { get; set; }
        public bool IsHideUser { get; set; }

    }

    public class CustomerGroup
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string Description { get; set; }
        public int MainGroup { get; set; }

    }
}