using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;

namespace SSM.Models
{
    public class SalesModel
    {
        public long VoucherId { get; set; }

        public int? VoucherCode { get; set; }

        public DateTime? VoucherDate { get; set; }
        [Required]
        public string VoucherNo { get; set; }
        [Required]
        public Customer Customer { get; set; }

        public string Description { get; set; }
        [Required]
        public Curency Curency { get; set; }

        public decimal ExchangeRate { get; set; }

        public long CountryId { get; set; }

        public decimal Quantity { get; set; }

        public decimal Amount { get; set; }
        public decimal VnAmount { get; set; }

        public decimal TaxRate { get; set; }
        public decimal VAT { get; set; }

        public decimal TransportFee { get; set; }

        public decimal InlandFee { get; set; }

        public decimal Fee1 { get; set; }

        public decimal Fee2 { get; set; }

        public decimal Fee { get; set; }

        public decimal SumTotal { get; set; }
        public decimal Profit { get; set; }

        public decimal Amount0 { get; set; }

        public long? SubmittedBy { get; set; }

        public long? CheckedBy { get; set; }

        public long? ApprovedBy { get; set; }

        public VoucherStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateModify { get; set; }

        public long CreateBy { get; set; }

        public long? ModifyBy { get; set; }
        public User UserCreated { get; set; }
        public User UserChecked { get; set; }
        public User UserApproved { get; set; }
        public User UserSubmited { get; set; }
        public List<SalesDetailModel> DetailModels { get; set; }
        public DateTime? DateSubmited { get; set; }
        public DateTime? DateChecked { get; set; }
        public DateTime? DateApproved { get; set; }
        public Shipment Shipment { get; set; }
        public string NotePrints { get; set; }
        public string ProductView { get; set; }
    }

    public class SalesDetailModel
    {
        public long VoucherId { get; set; }

        public int RowId { get; set; }

        public long ProductId { get; set; }
        public string ProductCode { get; set; }

        public string UOM { get; set; }

        public int WarehouseId { get; set; }

        public decimal CurrentQty { get; set; }
        public decimal Quantity { get; set; }

        public decimal VnPrice { get; set; }
        public decimal Price { get; set; }

        public decimal Amount { get; set; }
        public decimal VnAmount { get; set; }

        public decimal VATTaxRate { get; set; }

        public decimal VATTax { get; set; }

        public decimal TransportFee { get; set; }

        public decimal Fee1 { get; set; }

        public decimal Fee2 { get; set; }

        public decimal TFee { get; set; }

        public decimal TT { get; set; }

        public decimal Price0 { get; set; }

        public decimal Amount0 { get; set; }

        public string Notes { get; set; }
        public Product Product { get; set; }
        public Warehouse Warehouse { get; set; }
    }
}