using System;
using System.ComponentModel.DataAnnotations;

namespace SSM.ViewModels
{
    public class ServicesViewModel
    {
        public int Id { get; set; }
        [Required]
        public string SerivceName { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public long? CountryId { get; set; } 
        public DateTime? DateCreate { get; set; }
        public DateTime? DateModify { get; set; }
        public bool IsActive { get; set; }
    }
}