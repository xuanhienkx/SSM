using System;
using SSM.Models;

namespace SSM.ViewModels
{
    public class RevenueCalculateViewModel
    {
        public long Id { get; set; }
        public double BRate { get; set; } 
        public double ARate { get; set; }
        public double SRate { get; set; }
        public double INTransportRate { get; set; }
        public double INInlandService { get; set; }
        public double INCreditDebit { get; set; }
        public double INDocumentFee { get; set; }
        public double INHandingFee { get; set; }
        public double INTHC { get; set; }
        public double INCFS { get; set; }
        //public string AutoName1 { get; set; }
        //public string AutoName2 { get; set; }
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
        //public string EXManualName1 { get; set; }
        //public string EXManualName2 { get; set; }
        public double EXManualValue1 { get; set; }
        public double ExManualValue2 { get; set; }
        public double EXmanualValue1OS { get; set; }
        public double EXmanualValue2OS { get; set; }
        public double InvoiceCustom { get; set; }
        //public string Description4Invoice { get; set; }
        //public string INRemark { get; set; }
        //public string EXRemark { get; set; }
        //public string INRemarkHidden { get; set; }
        //public string EXRemarkHidden { get; set; }
        //public double Income { get; set; }
        //public double INVI { get; set; }
        //public double INOS { get; set; }
        //public double Expense { get; set; }
        //public double EXVI { get; set; }
        //public double EXOS { get; set; }
        //public double Earning { get; set; }
        //public double EarningVI { get; set; }
        //public double EarningOS { get; set; }
        public decimal InvAmount1 { get; set; }
        //public string InvType1 { get; set; }
        //public String InvCurrency1 { get; set; }
        public long? InvAgentId1 { get; set; }
        public decimal? InvAmount2 { get; set; }
        //public string InvType2 { get; set; }
       // public String InvCurrency2 { get; set; }
        public long? InvAgentId2 { get; set; }
        public double BonRequest { get; set; }
        public double BonApprove { get; set; }
        public decimal AmountBonus1 { get; set; }
        public decimal AmountBonus2 { get; set; }
        //public string AccInv1 { get; set; }
        //public string AccInv2 { get; set; }
        //public string AccInv3 { get; set; }
        //public string AccInv4 { get; set; }
        //public String AccInvDate1 { get; set; }
        //public String AccInvDate2 { get; set; }
        //public String AccInvDate3 { get; set; }
        //public String AccInvDate4 { get; set; }
        //public long? ApproveId { get; set; }
        //public String ApproveName { get; set; }
        //public long? AccountId { get; set; }
        public int Tax { get; set; }
        public long PaidToCarrier { get; set; }
        //public String SaleType { get; set; }
        public static RevenueCalculateViewModel ConvertModel(RevenueModel revenueModel)
        {
            var model = new RevenueCalculateViewModel
            {
                Id = revenueModel.Id,
                BRate = revenueModel.BRate,
                ARate = revenueModel.ARate,
                SRate = revenueModel.SRate,
                INTransportRate = revenueModel.INTransportRate,
                INInlandService = revenueModel.INInlandService,
                INCreditDebit = revenueModel.INCreditDebit,
                INDocumentFee = revenueModel.INDocumentFee,
                INHandingFee = revenueModel.INHandingFee,
                INTHC = revenueModel.INTHC,
                INCFS = revenueModel.INCFS,
                //AutoName1 = revenueModel.AutoName1,
                //AutoName2 = revenueModel.AutoName2,
                INAutoValue1 = revenueModel.INAutoValue1,
                INAutoValue2 = revenueModel.INAutoValue2,
                OUTAutoValue1 = revenueModel.OUTAutoValue1,
                OUTAutoValue2 = revenueModel.OUTAutoValue2,
                INTransportRateOS = revenueModel.INTransportRateOS,
                INInlandServiceOS = revenueModel.INInlandServiceOS,
                INCreditDebitOS = revenueModel.INCreditDebitOS,
                INDocumentFeeOS = revenueModel.INDocumentFeeOS,
                INHandingFeeOS = revenueModel.INHandingFeeOS,
                INAutoValue1OS = revenueModel.INAutoValue1OS,
                INAutoValue2OS = revenueModel.INAutoValue2OS,
                EXAutoValue1OS = revenueModel.EXAutoValue1OS,
                EXAutoValue2OS = revenueModel.EXAutoValue2OS,
                EXTransportRateOS = revenueModel.EXTransportRateOS,
                EXTransportRate = revenueModel.EXTransportRate,
                EXInlandServiceOS = revenueModel.EXInlandServiceOS,
                EXInlandService = revenueModel.EXInlandService,
                EXCommision2Shipper = revenueModel.EXCommision2Shipper,
                EXCommision2Carrier = revenueModel.EXCommision2Carrier,
                EXTax = revenueModel.EXTax,
                EXCreditDebit = revenueModel.EXCreditDebit,
                EXCreditDebitOS = revenueModel.EXCreditDebitOS,
                EXDocumentFee = revenueModel.EXDocumentFee,
                EXHandingFee = revenueModel.EXHandingFee,
                EXTHC = revenueModel.EXTHC,
                EXCFS = revenueModel.EXCFS,
                EXManualValue1 = revenueModel.EXManualValue1,
                ExManualValue2 = revenueModel.ExManualValue2,
                EXmanualValue1OS = revenueModel.EXmanualValue1OS,
                EXmanualValue2OS = revenueModel.EXmanualValue2OS,
                InvoiceCustom = revenueModel.InvoiceCustom,
                //Income = revenueModel.Income,
                //INVI = revenueModel.INVI,
                //INOS = revenueModel.INOS,
                //Expense = revenueModel.Expense,
                //EXVI = revenueModel.EXVI,
                //EXOS = revenueModel.EXOS,
                //Earning = revenueModel.Earning,
                //EarningVI = revenueModel.EarningVI,
                //EarningOS = revenueModel.EarningOS,
                //InvType1 = revenueModel.InvType1,
                //InvType2 = revenueModel.InvType2,
                InvAgentId1 = revenueModel.InvAgentId1,
                InvAgentId2 = revenueModel.InvAgentId2,
                InvAmount1 = revenueModel.InvAmount1,
                InvAmount2 = revenueModel.InvAmount2,
                AmountBonus1 = revenueModel.AmountBonus1,
                AmountBonus2 = revenueModel.AmountBonus2,
                BonRequest = revenueModel.BonRequest,
                BonApprove = revenueModel.BonApprove,
                PaidToCarrier = revenueModel.PaidToCarrier,
                Tax = revenueModel.Tax,
                
                
            }; 
           
            return model;
        }
    }
}