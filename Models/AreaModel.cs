using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class AreaModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Province/City")]
        public String AreaAddress { get; set; }
        [DisplayName("Country")]
        public long CountryId { get; set; }
        [DisplayName("Description")]
        public String Description { get; set; }
        public bool IsTrading { get; set; }
        public bool IsSee { get; set; }
        public bool IsHideUser { get; set; }
        public Country Country { get; set; }
    }
}