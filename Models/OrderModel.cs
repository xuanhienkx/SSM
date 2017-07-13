using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class OrderModel
    {
        public long VoucherId { get; set; }

        public int VoucherCode { get; set; }
          [Required]
        public DateTime? VoucherDate { get; set; }
        [Required]
        public string VoucherNo { get; set; }
        [Required]
        public Supplier Supplier { get; set; }

        public string Description { get; set; }
        [Required]
        public Curency Curency { get; set; }
        [Required]
        public decimal ExchangeRate { get; set; }
        [Required]
        public string HBL { get; set; }
        [Required]
        public string MBL { get; set; }
         [Required]
        public Country Country { get; set; }
        [Required]
        public string DeclaraNo { get; set; }
        [Required]
        public DateTime? DeclaraDate { get; set; }
        [Required]
        public DateTime? ReceiptDate { get; set; }

        public decimal QuantityTotal { get; set; }

        public decimal TAmount { get; set; }

        public decimal TVATTax { get; set; }

        public decimal TransportFee { get; set; }

        public decimal InlnadFee { get; set; }

        public decimal Fee1 { get; set; }

        public decimal Fee2 { get; set; }

        public decimal Fee { get; set; }

        public decimal TTT { get; set; }
        public decimal VnTTT { get; set; }

        public long SubmittedBy { get; set; }

        public long CheckedBy { get; set; }

        public long ApprovedBy { get; set; }

        public VoucherStatus Status { get; set; }

        public DateTime? DateCreate { get; set; }

        public DateTime? DateModify { get; set; }

        public long CreateBy { get; set; }

        public long ModifyBy { get; set; }
        public List<OrderDetailModel> OrderDetails { get; set; }
        public User UserCreated { get; set; }
        public User UserSubmited { get; set; }
        public User UserChecked  { get; set; }
        public User UserApproved{ get; set; }

        public DateTime? DateSubmited { get; set; }
        public DateTime? DateChecked { get; set; }
        public DateTime? DateApproved { get; set; }
        public string NotePrints { get; set; }
        public string ProductView { get; set; }
    }

    public class OrderDetailModel
    {
        public long VoucherId { get; set; }

        public int RowId { get; set; }
        [Required]
        public long ProductId{ get; set; }
        public string ProductCode { get; set; }
        [Required]
        public string UOM { get; set; }

        public int WarehouseId { get; set; } 

        public decimal Quantity { get; set; }

        public decimal Price { get; set; }
        public decimal VnPrice { get; set; }

        public decimal Amount { get; set; }

        public decimal ImportTaxRate { get; set; }

        public decimal ImportTax { get; set; }

        public decimal TaxRate { get; set; }

        public decimal Tax { get; set; }

        public decimal TransportFee { get; set; }

        public decimal InlandFee { get; set; }

        public decimal Fee1 { get; set; }

        public decimal Fee2 { get; set; }

        public decimal TFee { get; set; }
        public decimal PriceReceive { get; set; }

        public decimal Total { get; set; } 
        public decimal VnTotal { get; set; } 
        public string Note { get; set; }
        public int TabIndex { get; set; }
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }


    }
}