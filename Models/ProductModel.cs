using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class ProductModel
    {
        public long Id { get; set; }
        [Required]
        [DisplayName("Code")] 
        public string Code { get; set; }
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string SupplierName { get; set; } 
        [Display(Name = "English Name")]
        public string NameEnglish { get; set; }
        [Required]
        [Display(Name = "Unit")]
        public string Uom { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }

        public string Category1 { get; set; }

        public string Category2 { get; set; }

        public string Category3 { get; set; }
        [Required]
        [DisplayName("SupplierId")]
        public long SupplierId { get; set; }

        public long? CreatedBy { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateModify { get; set; }
    }
}