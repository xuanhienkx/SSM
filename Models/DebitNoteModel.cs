using System;
using SSM.Utils;

namespace SSM.Models
{
    public class DebitNoteModel
    {
        public String CompanyTo { get; set; }
        public String DebitNo { get; set; }
        public String DebitDate { get; set; }
        public String DebitTerms { get; set; }
        public String FlightNo { get; set; }
        public String HAWB_HBL { get; set; }
        public String Origin { get; set; }
        public String Destination { get; set; }
        public String Pieces { get; set; }
        public String Weight { get; set; }
        public String Description { get; set; }
        public double DebitAmount { get; set; }
        public double DebitUSD { get; set; }
        public String AuthName { get; set; }
        public long ShipmentId { get; set; }
        public String CompanyFrom { get; set; }
        public String InWords { get; set; }
        public bool Logo { get; set; }
        public bool Header { get; set; }
        public bool Footer { get; set; }
        public string Wearehouse { get; set; }
        public string FlyNo { get; set; }
        public static void ConvertDebitNote(DebitNoteModel DebitNoteModel1, DebitNote DebitNote1)
        {
            DebitNote1.CompanyTo = DebitNoteModel1.CompanyTo;
            DebitNote1.CompanyFrom = DebitNoteModel1.CompanyFrom;
            DebitNote1.InWords = DebitNoteModel1.InWords;
            DebitNote1.DebitNo = DebitNoteModel1.DebitNo;
            DebitNote1.DebitDate = DateUtils.Convert2DateTime(DebitNoteModel1.DebitDate);
            DebitNote1.DebitTerms = DebitNoteModel1.DebitTerms;
            DebitNote1.FlightNo = DebitNoteModel1.FlightNo;
            DebitNote1.HAWB_HBL = DebitNoteModel1.HAWB_HBL;
            DebitNote1.Origin = DebitNoteModel1.Origin;
            DebitNote1.Destination = DebitNoteModel1.Destination;
            DebitNote1.Pieces = DebitNoteModel1.Pieces;
            DebitNote1.Weight = DebitNoteModel1.Weight;
            DebitNote1.Description = DebitNoteModel1.Description;
            DebitNote1.DebitUSD = Convert.ToDecimal(DebitNoteModel1.DebitUSD);
            DebitNote1.DebitAmount = Convert.ToDecimal(DebitNoteModel1.DebitAmount);
            DebitNote1.AuthName = DebitNoteModel1.AuthName;
            DebitNote1.ShipmentId = DebitNoteModel1.ShipmentId;

            DebitNote1.Logo = DebitNoteModel1.Logo;
            DebitNote1.Header = DebitNoteModel1.Header;
            DebitNote1.Footer = DebitNoteModel1.Footer;
          
        }
        public static void ConvertDebitNote(DebitNote DebitNote1, DebitNoteModel DebitNoteModel1)
        {
            DebitNoteModel1.CompanyTo = DebitNote1.CompanyTo;
            DebitNoteModel1.CompanyFrom = DebitNote1.CompanyFrom;
            DebitNoteModel1.InWords = DebitNote1.InWords;
            DebitNoteModel1.DebitNo = DebitNote1.DebitNo;
            DebitNoteModel1.DebitDate = DebitNote1.DebitDate != null ? DebitNote1.DebitDate.Value.ToString("dd/MM/yyyy") : "";
            DebitNoteModel1.DebitTerms = DebitNote1.DebitTerms;
            DebitNoteModel1.FlightNo = DebitNote1.FlightNo;
            DebitNoteModel1.HAWB_HBL = DebitNote1.HAWB_HBL;
            DebitNoteModel1.Origin = DebitNote1.Origin;
            DebitNoteModel1.Destination = DebitNote1.Destination;
            DebitNoteModel1.Pieces = DebitNote1.Pieces;
            DebitNoteModel1.Weight = DebitNote1.Weight;
            DebitNoteModel1.Description = DebitNote1.Description;
            DebitNoteModel1.DebitUSD = Convert.ToDouble(DebitNote1.DebitUSD);
            DebitNoteModel1.DebitAmount = Convert.ToDouble(DebitNote1.DebitAmount);
            DebitNoteModel1.AuthName = DebitNote1.AuthName;
            DebitNoteModel1.ShipmentId = DebitNote1.ShipmentId;
            DebitNoteModel1.Logo = DebitNote1.Logo;
            DebitNoteModel1.Header = DebitNote1.Header;
            DebitNoteModel1.Footer = DebitNote1.Footer; 
        }
    }
}