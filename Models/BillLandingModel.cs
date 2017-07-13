namespace SSM.Models
{
    public class BillLandingModel
    {
        public long Id { get; set; }

        public long ShipmentId { get; set; }

        public string PhipperName { get; set; }

        public string ConsignedOrder { get; set; }

        public string NotifyAddress { get; set; }

        public string PlaceOfReceipt { get; set; }

        public string OceanVessel { get; set; }

        public string VoyNo { get; set; }

        public string BillLandingNo { get; set; }

        public string ForRelease { get; set; }

        public string PortLoading { get; set; }

        public string PortDischarge { get; set; }

        public string PlaceDelivery { get; set; }

        public string MarksNos { get; set; }

        public string QuanlityDescription { get; set; }

        public string GrossWeight { get; set; }

        public string Measurement { get; set; }

        public string FreightCharge { get; set; }

        public string PlaceDateIssue { get; set; }

        public string FreightPayable { get; set; }

        public string NumberOriginal { get; set; }

        public string BillTo { get; set; }
        public string FinalDestination { get; set; }
        public string StampAuthorized { get; set; }
        public string Original { get; set; }
        public string BKG { get; set; }
        public bool Logo { get; set; }
        public bool Header { get; set; }
        public bool Footer { get; set; }
        public string BillFrom { get; set; }
        public string BLComAddress { get; set; }

        public static void ConvertBillLanding(BillLandingModel Model, BillLanding Bill)
        {
            if (Bill == null) { Bill = new BillLanding(); }
            if (Model.Id > 0) { Bill.Id = Model.Id; }
            Bill.BillLandingNo = Model.BillLandingNo;
            Bill.BillTo = Model.BillTo;
            Bill.BKG = Model.BKG;
            Bill.ConsignedOrder = Model.ConsignedOrder;
            Bill.ForRelease = Model.ForRelease;
            Bill.FreightCharge = Model.FreightCharge;
            Bill.FreightPayable = Model.FreightPayable;
            Bill.GrossWeight = Model.GrossWeight;
            Bill.MarksNos = Model.MarksNos;
            Bill.Measurement = Model.Measurement;
            Bill.NotifyAddress = Model.NotifyAddress;
            Bill.NumberOriginal = Model.NumberOriginal;
            Bill.OceanVessel = Model.OceanVessel;
            Bill.PhipperName = Model.PhipperName;
            Bill.PlaceDateIssue = Model.PlaceDateIssue;
            Bill.PlaceDelivery = Model.PlaceDelivery;
            Bill.PlaceOfReceipt = Model.PlaceOfReceipt;
            Bill.PortDischarge = Model.PortDischarge;

            Bill.PortLoading = Model.PortLoading;
            Bill.QuanlityDescription = Model.QuanlityDescription;
            Bill.ShipmentId = Model.ShipmentId;
            Bill.VoyNo = Model.VoyNo;
            Bill.FinalDestination = Model.FinalDestination;
            Bill.StampAuthorized = Model.StampAuthorized;
            Bill.Original = Model.Original;
            Bill.Logo = Model.Logo;
            Bill.Header = Model.Header;
            Bill.Footer = Model.Footer;
        }
        public static void ConvertBillLanding(BillLanding Bill, BillLandingModel Model)
        {
            if (Model == null) { Model = new BillLandingModel(); }
            if (Bill.Id > 0) { Model.Id = Bill.Id; }
            Model.BillLandingNo = Bill.BillLandingNo;
            Model.BillTo = Bill.BillTo;
            Model.BKG = Bill.BKG;
            Model.ConsignedOrder = Bill.ConsignedOrder;
            Model.ForRelease = Bill.ForRelease;
            Model.FreightCharge = Bill.FreightCharge;
            Model.FreightPayable = Bill.FreightPayable;
            Model.GrossWeight = Bill.GrossWeight;
            Model.MarksNos = Bill.MarksNos;
            Model.Measurement = Bill.Measurement;
            Model.NotifyAddress = Bill.NotifyAddress;
            Model.NumberOriginal = Bill.NumberOriginal;
            Model.OceanVessel = Bill.OceanVessel;
            Model.PhipperName = Bill.PhipperName;
            Model.PlaceDateIssue = Bill.PlaceDateIssue;
            Model.PlaceDelivery = Bill.PlaceDelivery;
            Model.PlaceOfReceipt = Bill.PlaceOfReceipt;
            Model.PortDischarge = Bill.PortDischarge;

            Model.PortLoading = Bill.PortLoading;
            Model.QuanlityDescription = Bill.QuanlityDescription;
            Model.ShipmentId = Bill.ShipmentId;
            Model.VoyNo = Bill.VoyNo;
            Model.FinalDestination = Bill.FinalDestination;
            Model.StampAuthorized = Bill.StampAuthorized;
            Model.Original = Bill.Original;
            Model.Logo = Bill.Logo;
            Model.Header = Bill.Header;
            Model.Footer = Bill.Footer;
            Model.BillFrom = Bill.BillFrom;
            Model.BLComAddress = Bill.BLComAddress;
        }
    }
}