using System;
using System.ComponentModel.DataAnnotations;

namespace SSM.Models
{
    public class RevenueModel
    {
        public long Id { get; set; }
        [Required]
        public double BRate { get; set; }
        [Required]
        public double ARate { get; set; }
        public double SRate { get; set; }
        public double INTransportRate { get; set; }
        public double INInlandService { get; set; }
        public double INCreditDebit { get; set; }
        public double INDocumentFee { get; set; }
        public double INHandingFee { get; set; }
        public double INTHC { get; set; }
        public double INCFS { get; set; }
        public string AutoName1 { get; set; }
        public string AutoName2 { get; set; }
        public double INAutoValue1 { get; set; }
        public double INAutoValue2 { get; set; }
        public double OUTAutoValue1 { get; set; }
        public double OUTAutoValue2 { get; set; }
        public double INTransportRateOS { get; set; }
        public double INInlandServiceOS { get; set; }
        public double INCreditDebitOS { get; set; }
        public double INDocumentFeeOS { get; set; }
        public double INHandingFeeOS { get; set; }
        public double INAutoValue1OS { get; set; }
        public double INAutoValue2OS { get; set; }
        public double EXAutoValue1OS { get; set; }
        public double EXAutoValue2OS { get; set; }
        public double EXTransportRateOS { get; set; }
        public double EXTransportRate { get; set; }
        public double EXInlandServiceOS { get; set; }
        public double EXInlandService { get; set; }
        public double EXCommision2Shipper { get; set; }
        public double EXCommision2Carrier { get; set; }
        public double EXTax { get; set; }
        public double EXCreditDebit { get; set; }
        public double EXCreditDebitOS { get; set; }
        public double EXDocumentFee { get; set; }
        public double EXHandingFee { get; set; }
        public double EXTHC { get; set; }
        public double EXCFS { get; set; }
        public string EXManualName1 { get; set; }
        public string EXManualName2 { get; set; }
        public double EXManualValue1 { get; set; }
        public double ExManualValue2 { get; set; }
        public double EXmanualValue1OS { get; set; }
        public double EXmanualValue2OS { get; set; }
        public double InvoiceCustom { get; set; }
        public string Description4Invoice { get; set; }
        public string INRemark { get; set; }
        public string EXRemark { get; set; }
        public string INRemarkHidden { get; set; }
        public string EXRemarkHidden { get; set; }
        public double Income { get; set; }
        public double INVI { get; set; }
        public double INOS { get; set; }
        public double Expense { get; set; }
        public double EXVI { get; set; }
        public double EXOS { get; set; }
        public double Earning { get; set; }
        public double EarningVI { get; set; }
        public double EarningOS { get; set; }

        public decimal InvAmount1 { get; set; }
        public string InvType1 { get; set; }
        public String InvCurrency1 { get; set; }
        public System.Nullable<long> InvAgentId1 { get; set; }
        public System.Nullable<decimal> InvAmount2 { get; set; }
        public string InvType2 { get; set; }
        public String InvCurrency2 { get; set; }
        public System.Nullable<long> InvAgentId2 { get; set; }
        public double BonRequest { get; set; }
        public double BonApprove { get; set; }
        public decimal AmountBonus1 { get; set; }
        public decimal AmountBonus2 { get; set; }
        public string AccInv1 { get; set; }
        public string AccInv2 { get; set; }
        public string AccInv3 { get; set; }
        public string AccInv4 { get; set; }
        public String AccInvDate1 { get; set; }
        public String AccInvDate2 { get; set; }
        public String AccInvDate3 { get; set; }
        public String AccInvDate4 { get; set; }
        public System.Nullable<long> ApproveId { get; set; }
        public String ApproveName { get; set; }
        public System.Nullable<long> AccountId { get; set; }
        public int Tax { get; set; }
        public long PaidToCarrier { get; set; }
        public String SaleType { get; set; }
        public bool IsTrading { get; set; }
        public bool IsControl { get; set; }
        public bool IsRequest { get; set; }
        public bool IsRevised { get; set; }
        public Shipment Shipment { get; set; }
        public long SaleTypeId { get; set; }

        public static Revenue ConvertModel(RevenueModel RevenueModel1)
        {
            Revenue Revenue1 = new Revenue();
            Revenue1.Id = RevenueModel1.Id;
            Revenue1.BRate = Convert.ToDecimal(RevenueModel1.BRate);
            Revenue1.ARate = Convert.ToDecimal(RevenueModel1.ARate);
            Revenue1.SRate = Convert.ToDecimal(RevenueModel1.SRate);
            Revenue1.INTransportRate = Convert.ToDecimal(RevenueModel1.INTransportRate);
            Revenue1.INInlandService = Convert.ToDecimal(RevenueModel1.INInlandService);
            Revenue1.INCreditDebit = Convert.ToDecimal(RevenueModel1.INCreditDebit);
            Revenue1.INDocumentFee = Convert.ToDecimal(RevenueModel1.INDocumentFee);
            Revenue1.INHandingFee = Convert.ToDecimal(RevenueModel1.INHandingFee);
            Revenue1.INTHC = Convert.ToDecimal(RevenueModel1.INTHC);
            Revenue1.INCFS = Convert.ToDecimal(RevenueModel1.INCFS);
            Revenue1.AutoName1 = RevenueModel1.AutoName1;
            Revenue1.AutoName2 = RevenueModel1.AutoName2;
            Revenue1.INAutoValue1 = Convert.ToDecimal(RevenueModel1.INAutoValue1);
            Revenue1.INAutoValue2 = Convert.ToDecimal(RevenueModel1.INAutoValue2);
            Revenue1.OUTAutoValue1 = Convert.ToDecimal(RevenueModel1.OUTAutoValue1);
            Revenue1.OUTAutoValue2 = Convert.ToDecimal(RevenueModel1.OUTAutoValue2);
            Revenue1.INTransportRate_OS = Convert.ToDecimal(RevenueModel1.INTransportRateOS);
            Revenue1.INInlandService_OS = Convert.ToDecimal(RevenueModel1.INInlandServiceOS);
            Revenue1.INCreditDebit_OS = Convert.ToDecimal(RevenueModel1.INCreditDebitOS);
            Revenue1.INDocumentFee_OS = Convert.ToDecimal(RevenueModel1.INDocumentFeeOS);
            Revenue1.INHandingFee_OS = Convert.ToDecimal(RevenueModel1.INHandingFeeOS);
            Revenue1.INAutoValue1_OS = Convert.ToDecimal(RevenueModel1.INAutoValue1OS);
            Revenue1.INAutoValue2_OS = Convert.ToDecimal(RevenueModel1.INAutoValue2OS);

            Revenue1.EXAutoValue1_OS = Convert.ToDecimal(RevenueModel1.EXAutoValue1OS);
            Revenue1.EXAutoValue2_OS = Convert.ToDecimal(RevenueModel1.EXAutoValue2OS);
            Revenue1.EXTransportRate_OS = Convert.ToDecimal(RevenueModel1.EXTransportRateOS);
            Revenue1.EXTransportRate = Convert.ToDecimal(RevenueModel1.EXTransportRate);
            Revenue1.EXInlandService_OS = Convert.ToDecimal(RevenueModel1.EXInlandServiceOS);
            Revenue1.EXInlandService = Convert.ToDecimal(RevenueModel1.EXInlandService);
            Revenue1.EXCommision2Shipper = Convert.ToDecimal(RevenueModel1.EXCommision2Shipper);
            Revenue1.EXCommision2Carrier = Convert.ToDecimal(RevenueModel1.EXCommision2Carrier);
            Revenue1.EXTax = Convert.ToDecimal(RevenueModel1.EXTax);
            Revenue1.EXCreditDebit = Convert.ToDecimal(RevenueModel1.EXCreditDebit);
            Revenue1.EXCreditDebit_OS = Convert.ToDecimal(RevenueModel1.EXCreditDebitOS);
            Revenue1.EXDocumentFee = Convert.ToDecimal(RevenueModel1.EXDocumentFee);
            Revenue1.EXHandingFee = Convert.ToDecimal(RevenueModel1.EXHandingFee);
            Revenue1.EXTHC = Convert.ToDecimal(RevenueModel1.EXTHC);
            Revenue1.EXCFS = Convert.ToDecimal(RevenueModel1.EXCFS);
            Revenue1.EXManualName1 = RevenueModel1.EXManualName1;
            Revenue1.EXManualName2 = RevenueModel1.EXManualName2;
            Revenue1.EXManualValue1 = Convert.ToDecimal(RevenueModel1.EXManualValue1);
            Revenue1.ExManualValue2 = Convert.ToDecimal(RevenueModel1.ExManualValue2);
            Revenue1.EXmanualValue1_OS = Convert.ToDecimal(RevenueModel1.EXmanualValue1OS);
            Revenue1.EXmanualValue2_OS = Convert.ToDecimal(RevenueModel1.EXmanualValue2OS);

            Revenue1.InvoiceCustom = Convert.ToDecimal(RevenueModel1.InvoiceCustom);
            Revenue1.Description4Invoice = RevenueModel1.Description4Invoice;
            Revenue1.INRemark = RevenueModel1.INRemark;
            Revenue1.EXRemark = RevenueModel1.EXRemark;

            Revenue1.Income = Convert.ToDecimal(RevenueModel1.Income);
            Revenue1.INVI = Convert.ToDecimal(RevenueModel1.INVI);
            Revenue1.INOS = Convert.ToDecimal(RevenueModel1.INOS);
            Revenue1.Expense = Convert.ToDecimal(RevenueModel1.Expense);
            Revenue1.EXVI = Convert.ToDecimal(RevenueModel1.EXVI);
            Revenue1.EXOS = Convert.ToDecimal(RevenueModel1.EXOS);
            Revenue1.Earning = Convert.ToDecimal(RevenueModel1.Earning);
            Revenue1.EarningVI = Convert.ToDecimal(RevenueModel1.EarningVI);
            Revenue1.EarningOS = Convert.ToDecimal(RevenueModel1.EarningOS);

            Revenue1.InvType1 = RevenueModel1.InvType1;
            Revenue1.InvType2 = RevenueModel1.InvType2;
            Revenue1.InvAgentId1 = RevenueModel1.InvAgentId1;
            Revenue1.InvAgentId2 = RevenueModel1.InvAgentId2;
            Revenue1.InvAmount1 = RevenueModel1.InvAmount1;
            Revenue1.InvAmount2 = RevenueModel1.InvAmount2;
            Revenue1.Currency1 = RevenueModel1.InvCurrency1;
            Revenue1.Currency2 = RevenueModel1.InvCurrency2;
            Revenue1.AmountBonus1 = RevenueModel1.AmountBonus1;
            Revenue1.AmountBonus2 = RevenueModel1.AmountBonus2;

            Revenue1.BonRequest = (int)RevenueModel1.BonRequest;
            Revenue1.BonApprove = Convert.ToDecimal(RevenueModel1.BonApprove);
            Revenue1.AmountBonus1 = RevenueModel1.AmountBonus1;
            Revenue1.AmountBonus2 = RevenueModel1.AmountBonus2;
            Revenue1.SaleType = RevenueModel1.SaleType;
            if (RevenueModel1.ApproveId != null)
            {
                Revenue1.ApproveId = RevenueModel1.ApproveId.Value;
            }
            if (RevenueModel1.AccountId != null)
            {
                Revenue1.AccountId = RevenueModel1.AccountId.Value;
            }
            Revenue1.Tax = RevenueModel1.Tax;
            Revenue1.PaidToCarrier = RevenueModel1.PaidToCarrier;
            Revenue1.IsControl = RevenueModel1.IsControl;
            Revenue1.IsRequest = RevenueModel1.IsRequest;
            Revenue1.IsRevised = RevenueModel1.IsRevised;
            return Revenue1;
        }
        public static void ConvertModel(RevenueModel RevenueModel1, Revenue Revenue1)
        {
            Revenue1.Id = RevenueModel1.Id;
            Revenue1.BRate = Convert.ToDecimal(RevenueModel1.BRate);
            Revenue1.ARate = Convert.ToDecimal(RevenueModel1.ARate);
            Revenue1.SRate = Convert.ToDecimal(RevenueModel1.SRate);
            Revenue1.INTransportRate = Convert.ToDecimal(RevenueModel1.INTransportRate);
            Revenue1.INInlandService = Convert.ToDecimal(RevenueModel1.INInlandService);
            Revenue1.INCreditDebit = Convert.ToDecimal(RevenueModel1.INCreditDebit);
            Revenue1.INDocumentFee = Convert.ToDecimal(RevenueModel1.INDocumentFee);
            Revenue1.INHandingFee = Convert.ToDecimal(RevenueModel1.INHandingFee);
            Revenue1.INTHC = Convert.ToDecimal(RevenueModel1.INTHC);
            Revenue1.INCFS = Convert.ToDecimal(RevenueModel1.INCFS);
            Revenue1.AutoName1 = RevenueModel1.AutoName1;
            Revenue1.AutoName2 = RevenueModel1.AutoName2;
            Revenue1.INAutoValue1 = Convert.ToDecimal(RevenueModel1.INAutoValue1);
            Revenue1.INAutoValue2 = Convert.ToDecimal(RevenueModel1.INAutoValue2);
            Revenue1.OUTAutoValue1 = Convert.ToDecimal(RevenueModel1.OUTAutoValue1);
            Revenue1.OUTAutoValue2 = Convert.ToDecimal(RevenueModel1.OUTAutoValue2);
            Revenue1.INTransportRate_OS = Convert.ToDecimal(RevenueModel1.INTransportRateOS);
            Revenue1.INInlandService_OS = Convert.ToDecimal(RevenueModel1.INInlandServiceOS);
            Revenue1.INCreditDebit_OS = Convert.ToDecimal(RevenueModel1.INCreditDebitOS);
            Revenue1.INDocumentFee_OS = Convert.ToDecimal(RevenueModel1.INDocumentFeeOS);
            Revenue1.INHandingFee_OS = Convert.ToDecimal(RevenueModel1.INHandingFeeOS);
            Revenue1.INAutoValue1_OS = Convert.ToDecimal(RevenueModel1.INAutoValue1OS);
            Revenue1.INAutoValue2_OS = Convert.ToDecimal(RevenueModel1.INAutoValue2OS);

            Revenue1.EXAutoValue1_OS = Convert.ToDecimal(RevenueModel1.EXAutoValue1OS);
            Revenue1.EXAutoValue2_OS = Convert.ToDecimal(RevenueModel1.EXAutoValue2OS);
            Revenue1.EXTransportRate_OS = Convert.ToDecimal(RevenueModel1.EXTransportRateOS);
            Revenue1.EXTransportRate = Convert.ToDecimal(RevenueModel1.EXTransportRate);
            Revenue1.EXInlandService_OS = Convert.ToDecimal(RevenueModel1.EXInlandServiceOS);
            Revenue1.EXInlandService = Convert.ToDecimal(RevenueModel1.EXInlandService);
            Revenue1.EXCommision2Shipper = Convert.ToDecimal(RevenueModel1.EXCommision2Shipper);
            Revenue1.EXCommision2Carrier = Convert.ToDecimal(RevenueModel1.EXCommision2Carrier);
            Revenue1.EXTax = Convert.ToDecimal(RevenueModel1.EXTax);
            Revenue1.EXCreditDebit = Convert.ToDecimal(RevenueModel1.EXCreditDebit);
            Revenue1.EXCreditDebit_OS = Convert.ToDecimal(RevenueModel1.EXCreditDebitOS);
            Revenue1.EXDocumentFee = Convert.ToDecimal(RevenueModel1.EXDocumentFee);
            Revenue1.EXHandingFee = Convert.ToDecimal(RevenueModel1.EXHandingFee);
            Revenue1.EXTHC = Convert.ToDecimal(RevenueModel1.EXTHC);
            Revenue1.EXCFS = Convert.ToDecimal(RevenueModel1.EXCFS);
            Revenue1.EXManualName1 = RevenueModel1.EXManualName1;
            Revenue1.EXManualName2 = RevenueModel1.EXManualName2;
            Revenue1.EXManualValue1 = Convert.ToDecimal(RevenueModel1.EXManualValue1);
            Revenue1.ExManualValue2 = Convert.ToDecimal(RevenueModel1.ExManualValue2);
            Revenue1.EXmanualValue1_OS = Convert.ToDecimal(RevenueModel1.EXmanualValue1OS);
            Revenue1.EXmanualValue2_OS = Convert.ToDecimal(RevenueModel1.EXmanualValue2OS);

            Revenue1.InvoiceCustom = Convert.ToDecimal(RevenueModel1.InvoiceCustom);
            Revenue1.Description4Invoice = RevenueModel1.Description4Invoice;
            Revenue1.INRemark = RevenueModel1.INRemark;
            Revenue1.EXRemark = RevenueModel1.EXRemark;

            Revenue1.Income = Convert.ToDecimal(RevenueModel1.Income);
            Revenue1.INVI = Convert.ToDecimal(RevenueModel1.INVI);
            Revenue1.INOS = Convert.ToDecimal(RevenueModel1.INOS);
            Revenue1.Expense = Convert.ToDecimal(RevenueModel1.Expense);
            Revenue1.EXVI = Convert.ToDecimal(RevenueModel1.EXVI);
            Revenue1.EXOS = Convert.ToDecimal(RevenueModel1.EXOS);
            Revenue1.Earning = Convert.ToDecimal(RevenueModel1.Earning);
            Revenue1.EarningVI = Convert.ToDecimal(RevenueModel1.EarningVI);
            Revenue1.EarningOS = Convert.ToDecimal(RevenueModel1.EarningOS);

            Revenue1.InvType1 = RevenueModel1.InvType1;
            Revenue1.InvType2 = RevenueModel1.InvType2;
            Revenue1.InvAgentId1 = RevenueModel1.InvAgentId1;
            Revenue1.InvAgentId2 = RevenueModel1.InvAgentId2;
            Revenue1.InvAmount1 = RevenueModel1.InvAmount1;
            Revenue1.InvAmount2 = RevenueModel1.InvAmount2;
            Revenue1.Currency1 = RevenueModel1.InvCurrency1;
            Revenue1.Currency2 = RevenueModel1.InvCurrency2;
            Revenue1.AmountBonus1 = RevenueModel1.AmountBonus1;
            Revenue1.AmountBonus2 = RevenueModel1.AmountBonus2;

            Revenue1.BonRequest = (int)RevenueModel1.BonRequest;
            Revenue1.BonApprove = Convert.ToDecimal(RevenueModel1.BonApprove);
            Revenue1.AmountBonus1 = RevenueModel1.AmountBonus1;
            Revenue1.AmountBonus2 = RevenueModel1.AmountBonus2;
            Revenue1.SaleType = RevenueModel1.SaleType;
            if (RevenueModel1.ApproveId != null)
            {
                Revenue1.ApproveId = RevenueModel1.ApproveId.Value;
            }
            if (RevenueModel1.AccountId != null)
            {
                Revenue1.AccountId = RevenueModel1.AccountId.Value;
            }
            Revenue1.Tax = RevenueModel1.Tax;
            Revenue1.IsControl = RevenueModel1.IsControl;
            Revenue1.IsRevised = RevenueModel1.IsRevised;
            Revenue1.IsRequest = RevenueModel1.IsRequest;
            Revenue1.PaidToCarrier = RevenueModel1.PaidToCarrier;
            Revenue1.SaleTypeId = RevenueModel1.SaleTypeId;
        }
        public static RevenueModel ConvertModel(Revenue Revenue1)
        {
            RevenueModel RevenueModel1 = new RevenueModel();
            RevenueModel1.Id = Revenue1.Id;
            RevenueModel1.BRate = Convert.ToDouble(Revenue1.BRate);
            RevenueModel1.ARate = Convert.ToDouble(Revenue1.ARate);
            RevenueModel1.SRate = Convert.ToDouble(Revenue1.SRate);
            RevenueModel1.INTransportRate = Convert.ToDouble(Revenue1.INTransportRate);
            RevenueModel1.INInlandService = Convert.ToDouble(Revenue1.INInlandService);
            RevenueModel1.INCreditDebit = Convert.ToDouble(Revenue1.INCreditDebit);
            RevenueModel1.INDocumentFee = Convert.ToDouble(Revenue1.INDocumentFee);
            RevenueModel1.INHandingFee = Convert.ToDouble(Revenue1.INHandingFee);
            RevenueModel1.INTHC = Convert.ToDouble(Revenue1.INTHC);
            RevenueModel1.INCFS = Convert.ToDouble(Revenue1.INCFS);
            RevenueModel1.AutoName1 = Revenue1.AutoName1;
            RevenueModel1.AutoName2 = Revenue1.AutoName2;
            RevenueModel1.INAutoValue1 = Convert.ToDouble(Revenue1.INAutoValue1);
            RevenueModel1.INAutoValue2 = Convert.ToDouble(Revenue1.INAutoValue2);
            RevenueModel1.OUTAutoValue1 = Convert.ToDouble(Revenue1.OUTAutoValue1);
            RevenueModel1.OUTAutoValue2 = Convert.ToDouble(Revenue1.OUTAutoValue2);
            RevenueModel1.INTransportRateOS = Convert.ToDouble(Revenue1.INTransportRate_OS);
            RevenueModel1.INInlandServiceOS = Convert.ToDouble(Revenue1.INInlandService_OS);
            RevenueModel1.INCreditDebitOS = Convert.ToDouble(Revenue1.INCreditDebit_OS);
            RevenueModel1.INDocumentFeeOS = Convert.ToDouble(Revenue1.INDocumentFee_OS);
            RevenueModel1.INHandingFeeOS = Convert.ToDouble(Revenue1.INHandingFee_OS);
            RevenueModel1.INAutoValue1OS = Convert.ToDouble(Revenue1.INAutoValue1_OS);
            RevenueModel1.INAutoValue2OS = Convert.ToDouble(Revenue1.INAutoValue2_OS);

            RevenueModel1.EXAutoValue1OS = Convert.ToDouble(Revenue1.EXAutoValue1_OS);
            RevenueModel1.EXAutoValue2OS = Convert.ToDouble(Revenue1.EXAutoValue2_OS);
            RevenueModel1.EXTransportRateOS = Convert.ToDouble(Revenue1.EXTransportRate_OS);
            RevenueModel1.EXTransportRate = Convert.ToDouble(Revenue1.EXTransportRate);
            RevenueModel1.EXInlandServiceOS = Convert.ToDouble(Revenue1.EXInlandService_OS);
            RevenueModel1.EXInlandService = Convert.ToDouble(Revenue1.EXInlandService);
            RevenueModel1.EXCommision2Shipper = Convert.ToDouble(Revenue1.EXCommision2Shipper);
            RevenueModel1.EXCommision2Carrier = Convert.ToDouble(Revenue1.EXCommision2Carrier);
            RevenueModel1.EXTax = Convert.ToDouble(Revenue1.EXTax);
            RevenueModel1.EXCreditDebit = Convert.ToDouble(Revenue1.EXCreditDebit);
            RevenueModel1.EXCreditDebitOS = Convert.ToDouble(Revenue1.EXCreditDebit_OS);
            RevenueModel1.EXDocumentFee = Convert.ToDouble(Revenue1.EXDocumentFee);
            RevenueModel1.EXHandingFee = Convert.ToDouble(Revenue1.EXHandingFee);
            RevenueModel1.EXTHC = Convert.ToDouble(Revenue1.EXTHC);
            RevenueModel1.EXCFS = Convert.ToDouble(Revenue1.EXCFS);
            RevenueModel1.EXManualName1 = Revenue1.EXManualName1;
            RevenueModel1.EXManualName2 = Revenue1.EXManualName2;
            RevenueModel1.EXManualValue1 = Convert.ToDouble(Revenue1.EXManualValue1);
            RevenueModel1.ExManualValue2 = Convert.ToDouble(Revenue1.ExManualValue2);
            RevenueModel1.EXmanualValue1OS = Convert.ToDouble(Revenue1.EXmanualValue1_OS);
            RevenueModel1.EXmanualValue2OS = Convert.ToDouble(Revenue1.EXmanualValue2_OS);

            RevenueModel1.InvoiceCustom = Convert.ToDouble(Revenue1.InvoiceCustom);
            RevenueModel1.Description4Invoice = Revenue1.Description4Invoice;
            RevenueModel1.INRemark = Revenue1.INRemark;
            RevenueModel1.EXRemark = Revenue1.EXRemark;
            RevenueModel1.INRemarkHidden = Revenue1.INRemark;
            RevenueModel1.EXRemarkHidden = Revenue1.EXRemark;

            RevenueModel1.Income = Convert.ToDouble(Revenue1.Income);
            RevenueModel1.INVI = Convert.ToDouble(Revenue1.INVI);
            RevenueModel1.INOS = Convert.ToDouble(Revenue1.INOS);
            RevenueModel1.Expense = Convert.ToDouble(Revenue1.Expense);
            RevenueModel1.EXVI = Convert.ToDouble(Revenue1.EXVI);
            RevenueModel1.EXOS = Convert.ToDouble(Revenue1.EXOS);
            RevenueModel1.Earning = Convert.ToDouble(Revenue1.Earning);
            RevenueModel1.EarningVI = Convert.ToDouble(Revenue1.EarningVI);
            RevenueModel1.EarningOS = Convert.ToDouble(Revenue1.EarningOS);

            RevenueModel1.InvType1 = Revenue1.InvType1;
            RevenueModel1.InvType2 = Revenue1.InvType2;
            RevenueModel1.InvAgentId1 = Revenue1.InvAgentId1;
            RevenueModel1.InvAgentId2 = Revenue1.InvAgentId2;
            RevenueModel1.InvAmount1 = Revenue1.InvAmount1;
            RevenueModel1.InvAmount2 = Revenue1.InvAmount2;
            RevenueModel1.InvCurrency1 = Revenue1.Currency1;
            RevenueModel1.InvCurrency2 = Revenue1.Currency2;

            RevenueModel1.BonRequest = Convert.ToDouble(Revenue1.BonApprove);
            RevenueModel1.BonApprove = Convert.ToDouble(Revenue1.BonApprove);
            RevenueModel1.AmountBonus1 = Revenue1.AmountBonus1;
            RevenueModel1.AmountBonus2 = Revenue1.AmountBonus2;

            RevenueModel1.AccInv1 = Revenue1.AccInv1;
            RevenueModel1.AccInv2 = Revenue1.AccInv2;
            RevenueModel1.AccInv3 = Revenue1.AccInv3;
            RevenueModel1.AccInv4 = Revenue1.AccInv4;

            RevenueModel1.AccInvDate1 = Revenue1.AccInvDate1 != null ? Revenue1.AccInvDate1.Value.ToString("dd/MM/yyyy") : "";
            RevenueModel1.AccInvDate2 = Revenue1.AccInvDate2 != null ? Revenue1.AccInvDate2.Value.ToString("dd/MM/yyyy") : "";
            RevenueModel1.AccInvDate3 = Revenue1.AccInvDate3 != null ? Revenue1.AccInvDate3.Value.ToString("dd/MM/yyyy") : "";
            RevenueModel1.AccInvDate4 = Revenue1.AccInvDate4 != null ? Revenue1.AccInvDate4.Value.ToString("dd/MM/yyyy") : "";
            RevenueModel1.SaleType = Revenue1.SaleType;
            RevenueModel1.SaleTypeId = Revenue1.SaleTypeId;

            RevenueModel1.ApproveId = Revenue1.ApproveId;
            if (Revenue1.User != null)
            {
                RevenueModel1.ApproveName = Revenue1.User.FullName;
            }
            RevenueModel1.Tax = Revenue1.Tax;
            RevenueModel1.AccountId = Revenue1.AccountId;
            if (Revenue1.PaidToCarrier == null)
            {
                RevenueModel1.PaidToCarrier = Revenue1.Shipment.CarrierAirId.Value;
            }
            else
            {
                RevenueModel1.PaidToCarrier = Revenue1.PaidToCarrier.Value;
            }
            if (Revenue1.Shipment != null && Revenue1.Shipment.IsTrading != null && Revenue1.Shipment.IsTrading == true)
            {
                RevenueModel1.IsTrading = true;
            }
            else
            {
                RevenueModel1.IsTrading = false;
            }
            RevenueModel1.Shipment = Revenue1.Shipment;
            RevenueModel1.IsControl = Revenue1.IsControl ?? false;
            RevenueModel1.IsRequest = Revenue1.IsRequest;
            RevenueModel1.IsRevised = Revenue1.IsRevised;
            return RevenueModel1;
        }
        public static Revenue InitRevenue()
        {
            Revenue Revenue1 = new Revenue();
            Revenue1.Id = 0;
            Revenue1.BRate = 0;
            Revenue1.ARate = 0;
            Revenue1.SRate = 0;
            Revenue1.INTransportRate = 0;
            Revenue1.INInlandService = 0;
            Revenue1.INCreditDebit = 0;
            Revenue1.INDocumentFee = 0;
            Revenue1.INHandingFee = 0;
            Revenue1.INTHC = 0;
            Revenue1.INCFS = 0;
            Revenue1.INAutoValue1 = 0;
            Revenue1.INAutoValue2 = 0;
            Revenue1.OUTAutoValue1 = 0;
            Revenue1.OUTAutoValue2 = 0;
            Revenue1.INTransportRate_OS = 0;
            Revenue1.INInlandService_OS = 0;
            Revenue1.INCreditDebit_OS = 0;
            Revenue1.INDocumentFee_OS = 0;
            Revenue1.INHandingFee_OS = 0;
            Revenue1.INAutoValue1_OS = 0;
            Revenue1.INAutoValue2_OS = 0;

            Revenue1.EXAutoValue1_OS = 0;
            Revenue1.EXAutoValue2_OS = 0;
            Revenue1.EXTransportRate_OS = 0;
            Revenue1.EXTransportRate = 0;
            Revenue1.EXInlandService_OS = 0;
            Revenue1.EXInlandService = 0;
            Revenue1.EXCommision2Shipper = 0;
            Revenue1.EXCommision2Carrier = 0;
            Revenue1.EXTax = 0;
            Revenue1.EXCreditDebit = 0;
            Revenue1.EXCreditDebit_OS = 0;
            Revenue1.EXDocumentFee = 0;
            Revenue1.EXHandingFee = 0;
            Revenue1.EXTHC = 0;
            Revenue1.EXCFS = 0;
            Revenue1.EXManualValue1 = 0;
            Revenue1.ExManualValue2 = 0;
            Revenue1.EXmanualValue1_OS = 0;
            Revenue1.EXmanualValue2_OS = 0;

            Revenue1.InvoiceCustom = 0;
            Revenue1.Description4Invoice = "";
            Revenue1.INRemark = "";
            Revenue1.EXRemark = "";

            Revenue1.Income = 0;
            Revenue1.INVI = 0;
            Revenue1.INOS = 0;
            Revenue1.Expense = 0;
            Revenue1.EXVI = 0;
            Revenue1.EXOS = 0;
            Revenue1.Earning = 0;
            Revenue1.EarningVI = 0;
            Revenue1.EarningOS = 0;

            Revenue1.InvAmount1 = 0;
            Revenue1.InvAmount2 = 0;
            Revenue1.AmountBonus1 = 0;
            Revenue1.AmountBonus2 = 0;

            Revenue1.BonRequest = 0;
            Revenue1.BonApprove = 0;
            Revenue1.AmountBonus1 = 0;
            Revenue1.AmountBonus2 = 0;

            Revenue1.Tax = 0;
            return Revenue1;
        }



        public enum InvTypes
        {
            [StringLabel("VN Debit")]
            VNDebit,
            [StringLabel("Vn Credit")]
            VNCredit,
            [StringLabel("Agent Debit")]
            AgentDebit,
            [StringLabel("Agent Credit")]
            AgentCredit
        }
        public enum CurrencyTypes
        {
            [StringLabel("USD")]
            USD,
            [StringLabel("EUR")]
            EUR,
            [StringLabel("GBP")]
            GBP,
            [StringLabel("AUD")]
            AUD
        }
    }
}