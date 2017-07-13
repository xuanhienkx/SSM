namespace SSM.Models
{
    public class BookingConfirmModel
    {
        public long Id { get; set; }
        public long ShipmentId { get; set; }
        public string RefNo { get; set; }
        public string BookDate { get; set; }
        public string BookTo { get; set; }
        public string BookFrom { get; set; }
        public string Destination { get; set; }
        public string Commodity { get; set; }
        public string Quantity { get; set; }
        public string FlightDate { get; set; }
        public string LoadingDate { get; set; }
        public string ClosingDate { get; set; }
        public string AirportCharge { get; set; }
        public string XPray { get; set; }
        public string AWBFee { get; set; }
        public string HandingCharge { get; set; }
        public string AMSCharge { get; set; }
        public string Contact { get; set; }
        public string AuthoWord { get; set; }
        public string Wearehouse { get; set; }
        public string FlyNo { get; set; }

        public static void ConvertBookingConfirm(BookingConfirmModel Model1, BookingConfirm Book1)
        {
            Book1.ShipmentId = Model1.ShipmentId;
            Book1.RefNo = Model1.RefNo;
            Book1.BookDate = Model1.BookDate;
            Book1.BookTo = Model1.BookTo;
            Book1.BookFrom = Model1.BookFrom;
            Book1.Destination = Model1.Destination;
            Book1.Commodity = Model1.Commodity;
            Book1.Quantity = Model1.Quantity;
            Book1.FlightDate = Model1.FlightDate;
            Book1.LoadingDate = Model1.LoadingDate;
            Book1.ClosingDate = Model1.ClosingDate;

            Book1.AirportCharge = Model1.AirportCharge;
            Book1.XPray = Model1.XPray;
            Book1.AWBFee = Model1.AWBFee;
            Book1.HandingCharge = Model1.HandingCharge;
            Book1.AMSCharge = Model1.AMSCharge;
            Book1.Contact = Model1.Contact;
            Book1.AuthoWord = Model1.AuthoWord;  
            Book1.Wearehouse = Model1.Wearehouse;  
            Book1.FlyNo = Model1.FlyNo;  
        }
        public static void ConvertBookingConfirm(BookingConfirm Book1, BookingConfirmModel Model1)
        {
            Model1.Id = Book1.Id;
            Model1.ShipmentId = Book1.ShipmentId;
            Model1.RefNo = Book1.RefNo;
            Model1.BookDate = Book1.BookDate;
            Model1.BookTo = Book1.BookTo;
            Model1.BookFrom = Book1.BookFrom;
            Model1.Destination = Book1.Destination;
            Model1.Commodity = Book1.Commodity;
            Model1.Quantity = Book1.Quantity;
            Model1.FlightDate = Book1.FlightDate;
            Model1.LoadingDate = Book1.LoadingDate;
            Model1.ClosingDate = Book1.ClosingDate;

            Model1.AirportCharge = Book1.AirportCharge;
            Model1.XPray = Book1.XPray;
            Model1.AWBFee = Book1.AWBFee;
            Model1.HandingCharge = Book1.HandingCharge;
            Model1.AMSCharge = Book1.AMSCharge;
            Model1.Contact = Book1.Contact;
            Model1.AuthoWord = Book1.AuthoWord;
            Model1.FlyNo = Book1.FlyNo;
            Model1.Wearehouse = Book1.Wearehouse;
        }
    }
}