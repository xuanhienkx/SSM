using System;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class UnitModel
    {
        public long Id { get; set; }
        [Required]
        public String Unit1 { get; set; }
        public String Description { get; set; }
        public String ServiceType { get; set; }
        public static void RevertUnit(UnitModel Model, Unit Unit1)
        {
            if (Model.Id != 0)
            {
                Unit1.Id = Model.Id;
            }
            Unit1.Description = Model.Description;
            Unit1.Unit1 = Model.Unit1;
            Unit1.ServiceType = Model.ServiceType;
        }
    }
}