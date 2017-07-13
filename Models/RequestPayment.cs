using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SSM.Models
{
    public class RequestPaymentModel
    {
        public string DepartmentName { get; set; }

        public string ShipmentCode { get; set; }

        public long Id { get; set; }

        public long ShipmentId { get; set; }

        public string BillNumber { get; set; }

        public string CustomerName { get; set; }

        public string CarrierName { get; set; }

        public string Fee { get; set; }

        public string DocFee { get; set; }

        public string Total1 { get; set; }

        public string Total2 { get; set; }

        public string FeeCurrency { get; set; }

        public string DocFeeCurrency { get; set; }

        public string Total1Currency { get; set; }

        public string Total2Currency { get; set; }

        public string PaidDate { get; set; }

        public string PaidType { get; set; }

        public string PaidPerson { get; set; }
        public static void ConvertRequestPayment(RequestPaymentModel Model1, RequestPayment Payment)
        {
            Payment.ShipmentId = Model1.ShipmentId;
            Payment.ShipmentCode = Model1.ShipmentCode;
            Payment.DepartmentName = Model1.DepartmentName;
            Payment.PaidPerson = Model1.PaidPerson;
            Payment.PaidType = Model1.PaidType;
            Payment.PaidDate = Model1.PaidDate;
            Payment.Total2Currency = Model1.Total2Currency;
            Payment.Total1Currency = Model1.Total1Currency;
            Payment.DocFeeCurrency = Model1.DocFeeCurrency;
            Payment.FeeCurrency = Model1.FeeCurrency;
            Payment.Total2 = Model1.Total2;
            Payment.Total1 = Model1.Total1;
            Payment.DocFee = Model1.DocFee;
            Payment.Fee = Model1.Fee;
            Payment.CarrierName = Model1.CarrierName;
            Payment.CustomerName = Model1.CustomerName;
            Payment.BillNumber = Model1.BillNumber;
        }
        public static void ConvertRequestPayment(RequestPayment Model1, RequestPaymentModel Payment)
        {
            Payment.ShipmentId = Model1.ShipmentId;
            Payment.ShipmentCode = Model1.ShipmentCode;
            Payment.DepartmentName = Model1.DepartmentName;
            Payment.PaidPerson = Model1.PaidPerson;
            Payment.PaidType = Model1.PaidType;
            Payment.PaidDate = Model1.PaidDate;
            Payment.Total2Currency = Model1.Total2Currency;
            Payment.Total1Currency = Model1.Total1Currency;
            Payment.DocFeeCurrency = Model1.DocFeeCurrency;
            Payment.FeeCurrency = Model1.FeeCurrency;
            Payment.Total2 = Model1.Total2;
            Payment.Total1 = Model1.Total1;
            Payment.DocFee = Model1.DocFee;
            Payment.Fee = Model1.Fee;
            Payment.CarrierName = Model1.CarrierName;
            Payment.CustomerName = Model1.CustomerName;
            Payment.BillNumber = Model1.BillNumber;
            Payment.Id = Model1.Id;
        }
    }
    public class BookingRequestModel {
        public string ShipperName { get; set; }

        public string CneeName { get; set; }

        public string PlaceDelivery { get; set; }

        public System.Nullable<int> MainContract { get; set; }

        public System.Nullable<int> SubContract { get; set; }

        public System.Nullable<int> MainInvoice { get; set; }

        public System.Nullable<int> SubInvoice { get; set; }

        public System.Nullable<int> MainPL { get; set; }

        public System.Nullable<int> SubPL { get; set; }

        public System.Nullable<int> MainBL { get; set; }

        public System.Nullable<int> SubBL { get; set; }

        public System.Nullable<int> MainExtend1 { get; set; }

        public System.Nullable<int> MainExtend2 { get; set; }

        public System.Nullable<int> SubExtend1 { get; set; }

        public System.Nullable<int> SubExtend2 { get; set; }

        public string Commodity1 { get; set; }

        public string Commodity2 { get; set; }

        public string Commodity3 { get; set; }

        public string Commodity4 { get; set; }

        public string Commodity5 { get; set; }

        public string Commodity6 { get; set; }

        public string Commodity7 { get; set; }

        public string Commodity8 { get; set; }

        public string Commodity9 { get; set; }

        public string Commodity10 { get; set; }

        public string HSCode1 { get; set; }

        public string HSCode2 { get; set; }

        public string HSCode3 { get; set; }

        public string HSCode4 { get; set; }

        public string HSCode5 { get; set; }

        public string HSCode6 { get; set; }

        public string HSCode7 { get; set; }

        public string HSCode8 { get; set; }

        public string HSCode9 { get; set; }

        public string HSCode10 { get; set; }

        public string FreightFee { get; set; }

        public string DocFee { get; set; }

        public string CFSFee { get; set; }
        public string THCFee { get; set; }

        public string Tax { get; set; }

        public string OtherFee { get; set; }

        public string TotalFee { get; set; }

        public long Id { get; set; }

        public long ShipmentId { get; set; }
        public string PortFrom { get; set; }
        public string PortTo { get; set; }

        public static void ConvertBookingRequest(BookingRequestModel Model1, BookingRequest Request1) {

        Request1.ShipperName=Model1.ShipperName;

        Request1.CneeName=Model1.CneeName;

        Request1.PlaceDelivery=Model1.PlaceDelivery;

        Request1.MainContract=Model1.MainContract;

        Request1.SubContract=Model1.SubContract;

        Request1.MainInvoice=Model1.MainInvoice;

        Request1.SubInvoice=Model1.SubInvoice;

        Request1.MainPL=Model1.MainPL;

        Request1.SubPL = Model1.SubPL;

        Request1.MainBL = Model1.MainBL;

        Request1.SubBL = Model1.SubBL;

        Request1.MainExtend1 = Model1.MainExtend1;

        Request1.MainExtend2 = Model1.MainExtend2;

        Request1.SubExtend1 = Model1.SubExtend1;

        Request1.SubExtend2=Model1.SubExtend2;

        Request1.Commodity1=Model1.Commodity1;

        Request1.Commodity2=Model1.Commodity2;

        Request1.Commodity3=Model1.Commodity3;

        Request1.Commodity4=Model1.Commodity4;

        Request1.Commodity5=Model1.Commodity5;

        Request1.Commodity6=Model1.Commodity6;

        Request1.Commodity7=Model1.Commodity7;

        Request1.Commodity8=Model1.Commodity8;

        Request1.Commodity9=Model1.Commodity9;

        Request1.Commodity10=Model1.Commodity10;
        Request1.HSCode1 = Model1.HSCode1;

        Request1.HSCode2 = Model1.HSCode2;

        Request1.HSCode3 = Model1.HSCode3;

        Request1.HSCode4 = Model1.HSCode4;

        Request1.HSCode5 = Model1.HSCode5;

        Request1.HSCode6 = Model1.HSCode6;

        Request1.HSCode7 = Model1.HSCode7;

        Request1.HSCode8 = Model1.HSCode8;

        Request1.HSCode9 = Model1.HSCode9;

        Request1.HSCode10 = Model1.HSCode10;

        Request1.FreightFee=Model1.FreightFee;

        Request1.DocFee=Model1.DocFee;

        Request1.CFSFee=Model1.CFSFee;
        Request1.THCFee = Model1.THCFee;

        Request1.Tax=Model1.Tax;

        Request1.OtherFee=Model1.OtherFee;

        Request1.TotalFee=Model1.TotalFee;

        Request1.Id=Model1.Id;

        Request1.ShipmentId=Model1.ShipmentId;
        Request1.PortFrom = Model1.PortFrom;
        Request1.PortTo = Model1.PortTo;

        }
        public static void ConvertBookingRequest( BookingRequest Model1, BookingRequestModel Request1)
        {

            Request1.ShipperName = Model1.ShipperName;

            Request1.CneeName = Model1.CneeName;

            Request1.PlaceDelivery = Model1.PlaceDelivery;

            Request1.MainContract = Model1.MainContract;

            Request1.SubContract = Model1.SubContract;

            Request1.MainInvoice = Model1.MainInvoice;

            Request1.SubInvoice = Model1.SubInvoice;

            Request1.MainPL = Model1.MainPL;

            Request1.SubPL = Model1.SubPL;

            Request1.MainBL = Model1.MainBL;

            Request1.SubBL = Model1.SubBL;

            Request1.MainExtend1 = Model1.MainExtend1;

            Request1.MainExtend2 = Model1.MainExtend2;

            Request1.SubExtend1 = Model1.SubExtend1;

            Request1.SubExtend2 = Model1.SubExtend2;

            Request1.Commodity1 = Model1.Commodity1;

            Request1.Commodity2 = Model1.Commodity2;

            Request1.Commodity3 = Model1.Commodity3;

            Request1.Commodity4 = Model1.Commodity4;

            Request1.Commodity5 = Model1.Commodity5;

            Request1.Commodity6 = Model1.Commodity6;

            Request1.Commodity7 = Model1.Commodity7;

            Request1.Commodity8 = Model1.Commodity8;

            Request1.Commodity9 = Model1.Commodity9;

            Request1.Commodity10 = Model1.Commodity10;
            Request1.HSCode1 = Model1.HSCode1;

            Request1.HSCode2 = Model1.HSCode2;

            Request1.HSCode3 = Model1.HSCode3;

            Request1.HSCode4 = Model1.HSCode4;

            Request1.HSCode5 = Model1.HSCode5;

            Request1.HSCode6 = Model1.HSCode6;

            Request1.HSCode7 = Model1.HSCode7;

            Request1.HSCode8 = Model1.HSCode8;

            Request1.HSCode9 = Model1.HSCode9;

            Request1.HSCode10 = Model1.HSCode10;

            Request1.FreightFee = Model1.FreightFee;

            Request1.DocFee = Model1.DocFee;

            Request1.CFSFee = Model1.CFSFee;
            Request1.THCFee = Model1.THCFee;

            Request1.Tax = Model1.Tax;

            Request1.OtherFee = Model1.OtherFee;

            Request1.TotalFee = Model1.TotalFee;

            Request1.Id = Model1.Id;

            Request1.ShipmentId = Model1.ShipmentId;
            Request1.PortFrom = Model1.PortFrom;
            Request1.PortTo = Model1.PortTo;
        }
    }
    public class ManifestModel {
        public long Id{get;set;}

        public long ShipmentId{get;set;}

        public string MAWBNo{get;set;}

        public string FLTNo{get;set;}

        public string CONSIGNEDTO{get;set;}

        public string DEPARTURE{get;set;}

        public string DATE{get;set;}

        public string ESTINATION{get;set;}

        public string ORDNo{get;set;}

        public string HAWBNo{get;set;}

        public string NoOfPcs{get;set;}

        public string GWEIGHT{get;set;}

        public string DESCRIPTIONOFGOODS{get;set;}

        public string SHIPPER{get;set;}

        public string CONSIGNEE{get;set;}

        public string CHARGECODE{get;set;}
        public static void convertManifest(Manifest Manifest1, ManifestModel Model1)
        {
            Model1.Id = Manifest1.Id;
            Model1.ShipmentId = Manifest1.ShipmentId;

            Model1.MAWBNo = Manifest1.MAWBNo;

            Model1.FLTNo = Manifest1.FLTNo;

            Model1.CONSIGNEDTO = Manifest1.CONSIGNEDTO;

            Model1.DEPARTURE = Manifest1.DEPARTURE;

            Model1.DATE = Manifest1.DATE;

            Model1.ESTINATION = Manifest1.ESTINATION;

            Model1.ORDNo = Manifest1.ORDNo;

            Model1.HAWBNo = Manifest1.HAWBNo;

            Model1.NoOfPcs = Manifest1.NoOfPcs;

            Model1.GWEIGHT = Manifest1.GWEIGHT;

            Model1.DESCRIPTIONOFGOODS = Manifest1.DESCRIPTIONOFGOODS;

            Model1.SHIPPER = Manifest1.SHIPPER;

            Model1.CONSIGNEE = Manifest1.CONSIGNEE;

            Model1.CHARGECODE = Manifest1.CHARGECODE;
        }
        public static void convertManifest(ManifestModel Manifest1, Manifest Model1)
        {
            Model1.Id = Manifest1.Id;
            Model1.ShipmentId = Manifest1.ShipmentId;

            Model1.MAWBNo = Manifest1.MAWBNo;

            Model1.FLTNo = Manifest1.FLTNo;

            Model1.CONSIGNEDTO = Manifest1.CONSIGNEDTO;

            Model1.DEPARTURE = Manifest1.DEPARTURE;

            Model1.DATE = Manifest1.DATE;

            Model1.ESTINATION = Manifest1.ESTINATION;

            Model1.ORDNo = Manifest1.ORDNo;

            Model1.HAWBNo = Manifest1.HAWBNo;

            Model1.NoOfPcs = Manifest1.NoOfPcs;

            Model1.GWEIGHT = Manifest1.GWEIGHT;

            Model1.DESCRIPTIONOFGOODS = Manifest1.DESCRIPTIONOFGOODS;

            Model1.SHIPPER = Manifest1.SHIPPER;

            Model1.CONSIGNEE = Manifest1.CONSIGNEE;

            Model1.CHARGECODE = Manifest1.CHARGECODE;
        }
    }

    public class TransitmentAdvisedModel
    {
        public long Id { get; set; }

        public long ShipmentId { get; set; }

        public string AdvisedTo { get; set; }

        public string AdvisedATTN { get; set; }

        public string AdvisedBL { get; set; }

        public string Grossweight { get; set; }

        public string Measurement { get; set; }

        public string Vessel { get; set; }

        public string Portofloading { get; set; }

        public string ETD { get; set; }

        public string ETA { get; set; }

        public string Portofdischarge { get; set; }

        public string CustomerServices { get; set; }

        public string AdvisedTime { get; set; }

        public bool StatusOk { get; set; }

        public bool StatusReport { get; set; }

        public bool StatusNoReport { get; set; }

        public bool MachineNoReport { get; set; }

        public string ChargeFreight { get; set; }

        public string ChargeBill { get; set; }

        public string ChargeTHC { get; set; }

        public string ChargeAMS { get; set; }

        public string ChargeVAT { get; set; }
        public static void convertTransitment(TransitmentAdvised Advised1, TransitmentAdvisedModel Model1)
        {
            Model1.Id = Advised1.Id;

            Model1.ShipmentId = Advised1.ShipmentId;

            Model1.AdvisedTo = Advised1.AdvisedTo;

            Model1.AdvisedATTN = Advised1.AdvisedATTN;

            Model1.AdvisedBL = Advised1.AdvisedBL;

            Model1.Grossweight = Advised1.Grossweight;

            Model1.Measurement = Advised1.Measurement;

            Model1.Vessel = Advised1.Vessel;

            Model1.Portofloading = Advised1.Portofloading;

            Model1.ETD = Advised1.ETD;

            Model1.ETA = Advised1.ETA;

            Model1.Portofdischarge = Advised1.Portofdischarge;

            Model1.CustomerServices = Advised1.CustomerServices;

            Model1.AdvisedTime = Advised1.AdvisedTime;

            Model1.StatusOk = Advised1.StatusOk;

            Model1.StatusReport = Advised1.StatusReport;

            Model1.StatusNoReport = Advised1.StatusNoReport;

            Model1.MachineNoReport = Advised1.MachineNoReport;

            Model1.ChargeFreight = Advised1.ChargeFreight;

            Model1.ChargeBill = Advised1.ChargeBill;

            Model1.ChargeTHC = Advised1.ChargeTHC;

            Model1.ChargeAMS = Advised1.ChargeAMS;

            Model1.ChargeVAT = Advised1.ChargeVAT;
        }
        public static void convertTransitment(TransitmentAdvisedModel Advised1, TransitmentAdvised Model1)
        {
            Model1.Id = Advised1.Id;

            Model1.ShipmentId = Advised1.ShipmentId;

            Model1.AdvisedTo = Advised1.AdvisedTo;

            Model1.AdvisedATTN = Advised1.AdvisedATTN;

            Model1.AdvisedBL = Advised1.AdvisedBL;

            Model1.Grossweight = Advised1.Grossweight;

            Model1.Measurement = Advised1.Measurement;

            Model1.Vessel = Advised1.Vessel;

            Model1.Portofloading = Advised1.Portofloading;

            Model1.ETD = Advised1.ETD;

            Model1.ETA = Advised1.ETA;

            Model1.Portofdischarge = Advised1.Portofdischarge;

            Model1.CustomerServices = Advised1.CustomerServices;

            Model1.AdvisedTime = Advised1.AdvisedTime;

            Model1.StatusOk = Advised1.StatusOk;

            Model1.StatusReport = Advised1.StatusReport;

            Model1.StatusNoReport = Advised1.StatusNoReport;

            Model1.MachineNoReport = Advised1.MachineNoReport;

            Model1.ChargeFreight = Advised1.ChargeFreight;

            Model1.ChargeBill = Advised1.ChargeBill;

            Model1.ChargeTHC = Advised1.ChargeTHC;

            Model1.ChargeAMS = Advised1.ChargeAMS;

            Model1.ChargeVAT = Advised1.ChargeVAT;
        }
    }
    public class HouseAirWayBillModel{
        public long Id { get; set; }
        public long ShipmentId { get; set; }
        public String IssueBy { get; set; }
        public String ShipperAccount { get; set; }
        public String ShipperName { get; set; }
        public String CneeAccount { get; set; }
        public String CneeName { get; set; }
        public String AccountingInformation { get; set; }
        public String Issuing { get; set; }
        public String AgenIATACode { get; set; }
        public String AccountNo { get; set; }
        public String AirportofDeparture { get; set; }
        public String IssueTo { get; set; }
        public String ByfirstCarrier { get; set; }
        public String IssueTo2 { get; set; }
        public String IssueTo3 { get; set; }
        public String IssueBy2 { get; set; }
        public String IssueBy3 { get; set; }
        public String IssueCurrency { get; set; }
        public String IssueCHGSCode { get; set; }
        public String IssuePPD { get; set; }
        public String IssueCOLL { get; set; }
        public String IssuePPD2 { get; set; }
        public String IssueCOLL2 { get; set; }
        public String CarriageValue { get; set; }
        public String CustomsValue { get; set; }

        public String AirportofDestination { get; set; }
        public String Flight { get; set; }
        public String Date { get; set; }
        public String AmountofInsurance { get; set; }
        public String HandlingInformation { get; set; }
        public String NofPieces { get; set; }
        public String NofPieces2 { get; set; }
        public String GrossWeight { get; set; }
        public String GrossWeight2 { get; set; }
        public String Kglb { get; set; }
        public String RateClass { get; set; }
        public String CommodityItemNo { get; set; }
        public String ChargeableWeight { get; set; }
        public String RateCharge { get; set; }
        public String Total { get; set; }
        public String Total2 { get; set; }
        public String QuantityofGoods { get; set; }

        public String WeightPrepaid { get; set; }
        public String WeightCollect { get; set; }
        public String ValuationPrepaid { get; set; }
        public String ValuationCollect { get; set; }
        public String TaxPrepaid { get; set; }
        public String TaxCollect { get; set; }
        public String AgentPrepaid { get; set; }
        public String AgentCollect { get; set; }
        public String CarrierPrepaid { get; set; }
        public String CarrierCollect { get; set; }
        public String TotalPrepaid { get; set; }
        public String TotalCollect { get; set; }
        public String CurrencyConversion  { get; set; }
        public String CCChargesinDest { get; set; }
        public String ChargesatDestination { get; set; }
        public String OtherCharges { get; set; }

        public String OtherCharges1 { get; set; }
        public String OtherCharges2 { get; set; }
        public String SignatureShippe { get; set; }
        public String Executedon { get; set; }
        public String Executedat { get; set; }
        public String TotalCollectCharges { get; set; }
        public String MasterAWB { get; set; }
        public String HouseAWB { get; set; }
        public static void convertHAirWayBill(HouseAirWayBillModel Model1, HAirWayBill HAirWayBill1)
        {
            HAirWayBill1.Id = Model1.Id;
            HAirWayBill1.ShipmentId= Model1.ShipmentId;
            HAirWayBill1.IssueBy= Model1.IssueBy;
            HAirWayBill1.ShipperAccount= Model1.ShipperAccount;
            HAirWayBill1.ShipperName= Model1.ShipperName;
            HAirWayBill1.CneeAccount= Model1.CneeAccount;
            HAirWayBill1.CneeName= Model1.CneeName;
            HAirWayBill1.AccountingInformation= Model1.AccountingInformation;
            HAirWayBill1.Issuing= Model1.Issuing;
            HAirWayBill1.AgenIATACode= Model1.AgenIATACode;
            HAirWayBill1.AccountNo= Model1.AccountNo;
            HAirWayBill1.AirportofDeparture= Model1.AirportofDeparture;
            HAirWayBill1.IssueTo= Model1.IssueTo;
            HAirWayBill1.ByfirstCarrier= Model1.ByfirstCarrier;
            HAirWayBill1.IssueTo2= Model1.IssueTo2;
            HAirWayBill1.IssueTo3= Model1.IssueTo3;
            HAirWayBill1.IssueBy2= Model1.IssueBy2;
            HAirWayBill1.IssueBy3= Model1.IssueBy3;
            HAirWayBill1.IssueCurrency= Model1.IssueCurrency;
            HAirWayBill1.IssueCHGSCode= Model1.IssueCHGSCode;
            HAirWayBill1.IssuePPD= Model1.IssuePPD;
            HAirWayBill1.IssueCOLL= Model1.IssueCOLL;
            HAirWayBill1.IssuePPD2= Model1.IssuePPD2;
            HAirWayBill1.IssueCOLL2= Model1.IssueCOLL2;
            HAirWayBill1.CarriageValue= Model1.CarriageValue;
            HAirWayBill1.CustomsValue= Model1.CustomsValue;

            HAirWayBill1.AirportofDestination= Model1.AirportofDestination;
            HAirWayBill1.Flight= Model1.Flight;
            HAirWayBill1.Date= Model1.Date;
            HAirWayBill1.AmountofInsurance= Model1.AmountofInsurance;
            HAirWayBill1.HandlingInformation= Model1.HandlingInformation;
            HAirWayBill1.NofPieces= Model1.NofPieces;
            HAirWayBill1.NofPieces2= Model1.NofPieces2;
            HAirWayBill1.GrossWeight= Model1.GrossWeight;
            HAirWayBill1.GrossWeight2= Model1.GrossWeight2;
            HAirWayBill1.Kglb= Model1.Kglb;
            HAirWayBill1.RateClass= Model1.RateClass;
            HAirWayBill1.CommodityItemNo= Model1.CommodityItemNo;
            HAirWayBill1.ChargeableWeight= Model1.ChargeableWeight;
            HAirWayBill1.RateCharge= Model1.RateCharge;
            HAirWayBill1.Total= Model1.Total;
            HAirWayBill1.Total2= Model1.Total2;
            HAirWayBill1.QuantityofGoods= Model1.QuantityofGoods;

            HAirWayBill1.WeightPrepaid= Model1.WeightPrepaid;
            HAirWayBill1.WeightCollect= Model1.WeightCollect;
            HAirWayBill1.ValuationPrepaid= Model1.ValuationPrepaid;
            HAirWayBill1.ValuationCollect= Model1.ValuationCollect;
            HAirWayBill1.TaxPrepaid= Model1.TaxPrepaid;
            HAirWayBill1.TaxCollect= Model1.TaxCollect;
            HAirWayBill1.AgentPrepaid= Model1.AgentPrepaid;
            HAirWayBill1.AgentCollect= Model1.AgentCollect;
            HAirWayBill1.CarrierPrepaid= Model1.CarrierPrepaid;
            HAirWayBill1.CarrierCollect= Model1.CarrierCollect;
            HAirWayBill1.CurrencyConversion = Model1.CurrencyConversion;
            HAirWayBill1.CCChargesinDest= Model1.CCChargesinDest;
            HAirWayBill1.ChargesatDestination= Model1.ChargesatDestination;
            HAirWayBill1.OtherCharges= Model1.OtherCharges;

            HAirWayBill1.OtherCharges1= Model1.OtherCharges1;
            HAirWayBill1.OtherCharges2= Model1.OtherCharges2;
            HAirWayBill1.SignatureShippe= Model1.SignatureShippe;
            HAirWayBill1.Executedon= Model1.Executedon;
            HAirWayBill1.Executedat= Model1.Executedat;
            HAirWayBill1.TotalCollectCharges = Model1.TotalCollectCharges;
            HAirWayBill1.TotalCollect = Model1.TotalCollect;
            HAirWayBill1.AgentPrepaid = Model1.TotalPrepaid;
            HAirWayBill1.MasterAWB = Model1.MasterAWB;
            HAirWayBill1.HouseAWB = Model1.HouseAWB;

        }
        public static void convertHAirWayBill(HAirWayBill Model1, HouseAirWayBillModel HAirWayBill1)
        {
            HAirWayBill1.Id = Model1.Id;
            HAirWayBill1.ShipmentId = Model1.ShipmentId;
            HAirWayBill1.IssueBy = Model1.IssueBy;
            HAirWayBill1.ShipperAccount = Model1.ShipperAccount;
            HAirWayBill1.ShipperName = Model1.ShipperName;
            HAirWayBill1.CneeAccount = Model1.CneeAccount;
            HAirWayBill1.CneeName = Model1.CneeName;
            HAirWayBill1.AccountingInformation = Model1.AccountingInformation;
            HAirWayBill1.Issuing = Model1.Issuing;
            HAirWayBill1.AgenIATACode = Model1.AgenIATACode;
            HAirWayBill1.AccountNo = Model1.AccountNo;
            HAirWayBill1.AirportofDeparture = Model1.AirportofDeparture;
            HAirWayBill1.IssueTo = Model1.IssueTo;
            HAirWayBill1.ByfirstCarrier = Model1.ByfirstCarrier;
            HAirWayBill1.IssueTo2 = Model1.IssueTo2;
            HAirWayBill1.IssueTo3 = Model1.IssueTo3;
            HAirWayBill1.IssueBy2 = Model1.IssueBy2;
            HAirWayBill1.IssueBy3 = Model1.IssueBy3;
            HAirWayBill1.IssueCurrency = Model1.IssueCurrency;
            HAirWayBill1.IssueCHGSCode = Model1.IssueCHGSCode;
            HAirWayBill1.IssuePPD = Model1.IssuePPD;
            HAirWayBill1.IssueCOLL = Model1.IssueCOLL;
            HAirWayBill1.IssuePPD2 = Model1.IssuePPD2;
            HAirWayBill1.IssueCOLL2 = Model1.IssueCOLL2;
            HAirWayBill1.CarriageValue = Model1.CarriageValue;
            HAirWayBill1.CustomsValue = Model1.CustomsValue;

            HAirWayBill1.AirportofDestination = Model1.AirportofDestination;
            HAirWayBill1.Flight = Model1.Flight;
            HAirWayBill1.Date = Model1.Date;
            HAirWayBill1.AmountofInsurance = Model1.AmountofInsurance;
            HAirWayBill1.HandlingInformation = Model1.HandlingInformation;
            HAirWayBill1.NofPieces = Model1.NofPieces;
            HAirWayBill1.NofPieces2 = Model1.NofPieces2;
            HAirWayBill1.GrossWeight = Model1.GrossWeight;
            HAirWayBill1.GrossWeight2 = Model1.GrossWeight2;
            HAirWayBill1.Kglb = Model1.Kglb;
            HAirWayBill1.RateClass = Model1.RateClass;
            HAirWayBill1.CommodityItemNo = Model1.CommodityItemNo;
            HAirWayBill1.ChargeableWeight = Model1.ChargeableWeight;
            HAirWayBill1.RateCharge = Model1.RateCharge;
            HAirWayBill1.Total = Model1.Total;
            HAirWayBill1.Total2 = Model1.Total2;
            HAirWayBill1.QuantityofGoods = Model1.QuantityofGoods;

            HAirWayBill1.WeightPrepaid = Model1.WeightPrepaid;
            HAirWayBill1.WeightCollect = Model1.WeightCollect;
            HAirWayBill1.ValuationPrepaid = Model1.ValuationPrepaid;
            HAirWayBill1.ValuationCollect = Model1.ValuationCollect;
            HAirWayBill1.TaxPrepaid = Model1.TaxPrepaid;
            HAirWayBill1.TaxCollect = Model1.TaxCollect;
            HAirWayBill1.AgentPrepaid = Model1.AgentPrepaid;
            HAirWayBill1.AgentCollect = Model1.AgentCollect;
            HAirWayBill1.CarrierPrepaid = Model1.CarrierPrepaid;
            HAirWayBill1.CarrierCollect = Model1.CarrierCollect;
            HAirWayBill1.CurrencyConversion = Model1.CurrencyConversion;
            HAirWayBill1.CCChargesinDest = Model1.CCChargesinDest;
            HAirWayBill1.ChargesatDestination = Model1.ChargesatDestination;
            HAirWayBill1.OtherCharges = Model1.OtherCharges;

            HAirWayBill1.OtherCharges1 = Model1.OtherCharges1;
            HAirWayBill1.OtherCharges2 = Model1.OtherCharges2;
            HAirWayBill1.SignatureShippe = Model1.SignatureShippe;
            HAirWayBill1.Executedon = Model1.Executedon;
            HAirWayBill1.Executedat = Model1.Executedat;
            HAirWayBill1.TotalCollectCharges = Model1.TotalCollectCharges;
            HAirWayBill1.TotalCollect = Model1.TotalCollect;
            HAirWayBill1.AgentPrepaid = Model1.TotalPrepaid;
            HAirWayBill1.MasterAWB = Model1.MasterAWB;
            HAirWayBill1.HouseAWB = Model1.HouseAWB;
        }
    }
    public class PaymentVocherModel {
        public long Id { get; set; }
        public long ShipmentId { get; set; }
        public String PaidTo { get; set; }
        public String IdentifyNo { get; set; }
        public String BelongTo { get; set; }
        public String Address1 { get; set; }
        public String Address2 { get; set; }
        public String ContentPayment { get; set; }
        public String TotalAmountService { get; set; }
        public String DeliveryDate { get; set; }
        public String TotalAmount { get; set; }
        public String TotalWords { get; set; }
        public String CompanyName { get; set; }
        public static void convertPaymentVoucher(PaymentVoucher PaymentVoucher1, PaymentVocherModel Model1) {
            Model1.Id = PaymentVoucher1.Id;
            Model1.ShipmentId = PaymentVoucher1.ShipmentId;
            Model1.PaidTo = PaymentVoucher1.PaidTo;
            Model1.IdentifyNo = PaymentVoucher1.IdentifyNo;
            Model1.BelongTo = PaymentVoucher1.BelongTo;
            Model1.Address1 = PaymentVoucher1.Address1;
            Model1.Address2 = PaymentVoucher1.Address2;
            Model1.ContentPayment = PaymentVoucher1.ContentPayment;
            Model1.TotalAmountService = PaymentVoucher1.TotalAmountService;
            Model1.DeliveryDate = PaymentVoucher1.DeliveryDate;
            Model1.TotalAmount = PaymentVoucher1.TotalAmount;
            Model1.TotalWords = PaymentVoucher1.TotalWords;
            Model1.CompanyName = PaymentVoucher1.CompanyName;
        }
        public static void convertPaymentVoucher(PaymentVocherModel PaymentVoucher1, PaymentVoucher Model1)
        {
            Model1.Id = PaymentVoucher1.Id;
            Model1.ShipmentId = PaymentVoucher1.ShipmentId;
            Model1.PaidTo = PaymentVoucher1.PaidTo;
            Model1.IdentifyNo = PaymentVoucher1.IdentifyNo;
            Model1.BelongTo = PaymentVoucher1.BelongTo;
            Model1.Address1 = PaymentVoucher1.Address1;
            Model1.Address2 = PaymentVoucher1.Address2;
            Model1.ContentPayment = PaymentVoucher1.ContentPayment;
            Model1.TotalAmountService = PaymentVoucher1.TotalAmountService;
            Model1.DeliveryDate = PaymentVoucher1.DeliveryDate;
            Model1.TotalAmount = PaymentVoucher1.TotalAmount;
            Model1.TotalWords = PaymentVoucher1.TotalWords;
            Model1.CompanyName = PaymentVoucher1.CompanyName;
        }
    }
}
