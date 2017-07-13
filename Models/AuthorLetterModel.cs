namespace SSM.Models
{
    public class AuthorLetterModel
    {
        public long Id { get; set; }
        public long ShipmentId { get; set; }
        public string DearTo { get; set; }
        public string BenA { get; set; }
        public string BenB { get; set; }
        public string MAWBNo { get; set; }
        public string HAWNNo { get; set; }
        public string Flight { get; set; }
        public string Weight { get; set; }
        public string NoOfPackage { get; set; }
        public string DescriptionOfGoods { get; set; }
        public string FlightDate { get; set; }
        public string CompanyAddress { get; set; }
        public string PublicTitle { get; set; }
        public static void convertAuthorLetter(AuthorLetterModel Model, AuthorLetter Letter)
        {
            Letter.ShipmentId = Model.ShipmentId;
            Letter.DearTo = Model.DearTo;
            Letter.BenA = Model.BenA;
            Letter.BenB = Model.BenB;
            Letter.MAWBNo = Model.MAWBNo;
            Letter.HAWNNo = Model.HAWNNo;
            Letter.Flight = Model.Flight;
            Letter.Weight = Model.Weight;
            Letter.NoOfPackage = Model.NoOfPackage;
            Letter.DescriptionOfGoods = Model.DescriptionOfGoods;
            Letter.FlightDate = Model.FlightDate;
            Letter.CompanyAddress = Model.CompanyAddress;
            Letter.PublicTitle = Model.PublicTitle;
        }
        public static void convertAuthorLetter(AuthorLetter Letter, AuthorLetterModel Model)
        {
            Model.Id = Letter.Id;
            Model.ShipmentId = Letter.ShipmentId;
            Model.DearTo = Letter.DearTo;
            Model.BenA = Letter.BenA;
            Model.BenB = Letter.BenB;
            Model.MAWBNo = Letter.MAWBNo;
            Model.HAWNNo = Letter.HAWNNo;
            Model.Flight = Letter.Flight;
            Model.Weight = Letter.Weight;
            Model.NoOfPackage = Letter.NoOfPackage;
            Model.DescriptionOfGoods = Letter.DescriptionOfGoods;
            Model.FlightDate = Letter.FlightDate;
            Model.CompanyAddress = Letter.CompanyAddress;
            Model.PublicTitle = Letter.PublicTitle;
        }
    }
}