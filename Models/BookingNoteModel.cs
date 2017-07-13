using SSM.Utils;

namespace SSM.Models
{
    public class BookingNoteModel
    {
        public long Id { get; set; }
        public long ShipmentId { get; set; }
        public string Date { get; set; }
        public string ShipperName { get; set; }
        public string Consignee { get; set; }
        public string NotifyAddress { get; set; }
        public string PlaceOfReceipt { get; set; }
        public string ETD { get; set; }
        public string BookingNo { get; set; }
        public string PortToLoading { get; set; }
        public string Vesel { get; set; }
        public string PortOfDestination { get; set; }
        public string PlaceOfDelivery { get; set; }
        public string PlaceOfStuffing { get; set; }
        public string PersonInCharge { get; set; }
        public string ClosingTime { get; set; }
        public string BillDetailAction { get; set; }
        public string ShippingMark { get; set; }
        public string NoCTNS { get; set; }
        public string GoodsDescription { get; set; }
        public string GrossWeight { get; set; }
        public string CBM { get; set; }
        public string Shipper { get; set; }
        public string SancoFreight { get; set; }
        public bool Logo { get; set; }
        public bool Header { get; set; }
        public bool Footer { get; set; }
        public static void ConvertBookingNote(BookingNoteModel Model, BookingNote Note)
        {
            if (Note == null) { Note = new BookingNote(); }
            if (Model.Id > 0)
            {
                Note.Id = Model.Id;
            }
            Note.BookingNo = Model.BookingNo;
            Note.ShippingMark = Model.ShippingMark;
            Note.NoCTNS = Model.NoCTNS;
            Note.GoodsDescription = Model.GoodsDescription;
            Note.GrossWeight = Model.GrossWeight;
            Note.CBM = Model.CBM;
            Note.Shipper = Model.Shipper;
            Note.SancoFreight = Model.SancoFreight;

            Note.ClosingTime = Model.ClosingTime;
            Note.Consignee = Model.Consignee;
            Note.Date = DateUtils.Convert2DateTime(Model.Date);
            Note.ETD = DateUtils.Convert2DateTime(Model.ETD);
            Note.NotifyAddress = Model.NotifyAddress;
            Note.PersonInCharge = Model.PersonInCharge;
            Note.PlaceOfDelivery = Model.PlaceOfDelivery;
            Note.PlaceOfReceipt = Model.PlaceOfReceipt;
            Note.PlaceOfStuffing = Model.PlaceOfStuffing;
            Note.PortOfDestination = Model.PortOfDestination;
            Note.PortToLoading = Model.PortToLoading;
            Note.ShipmentId = Model.ShipmentId;
            Note.ShipperName = Model.ShipperName;
            Note.Vesel = Model.Vesel;

            Note.Logo = Model.Logo;
            Note.Header = Model.Header;
            Note.Footer = Model.Footer;
        }
        public static void ConvertBookingNote(BookingNote Note, BookingNoteModel Model)
        {
            Model.Id = Model.Id;

            Model.BookingNo = Note.BookingNo;
            Model.ShippingMark = Note.ShippingMark;
            Model.NoCTNS = Note.NoCTNS;
            Model.GoodsDescription = Note.GoodsDescription;
            Model.GrossWeight = Note.GrossWeight;
            Model.CBM = Note.CBM;
            Model.Shipper = Note.Shipper;
            Model.SancoFreight = Note.SancoFreight;

            Model.ClosingTime = Note.ClosingTime;
            Model.Consignee = Note.Consignee;
            Model.Date = Note.Date != null ? Note.Date.Value.ToString("dd/MM/yyyy") : "";
            Model.ETD = Note.Date != null ? Note.ETD.Value.ToString("dd/MM/yyyy") : "";
            Model.NotifyAddress = Note.NotifyAddress;
            Model.PersonInCharge = Note.PersonInCharge;
            Model.PlaceOfDelivery = Note.PlaceOfDelivery;
            Model.PlaceOfReceipt = Note.PlaceOfReceipt;
            Model.PlaceOfStuffing = Note.PlaceOfStuffing;
            Model.PortOfDestination = Note.PortOfDestination;
            Model.PortToLoading = Note.PortToLoading;
            Model.ShipmentId = Note.ShipmentId;
            Model.ShipperName = Note.ShipperName;
            Model.Vesel = Note.Vesel;
            Model.Logo = Note.Logo;
            Model.Header = Note.Header;
            Model.Footer = Note.Footer;
        }
    }
}