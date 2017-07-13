using System;
using SSM.Utils;

namespace SSM.Models
{
    public class ArriveNoticeModel
    {
        public long Id { get; set; }

        public long ShipmentId { get; set; }

        public string CompanyName { get; set; }

        public string BillNumber { get; set; }

        public string ShiperName { get; set; }

        public string ShiperNumber { get; set; }

        public string ETA { get; set; }

        public string PortTo { get; set; }

        public bool ShipperNote { get; set; }

        public bool IntroducePaper { get; set; }

        public bool ArrivePaper { get; set; }

        public string OrderDate { get; set; }
        public string BillDetailAction { get; set; }
        public long BillDetailId { get; set; }
        public string ShippingMark { get; set; }
        public string NoCTNS { get; set; }
        public string GoodsDescription { get; set; }
        public string GrossWeight { get; set; }
        public string CBM { get; set; }
        public string NoticeTel { get; set; }
        public string NoticeAttn { get; set; }
        public string Notification { get; set; }
        public bool Logo { get; set; }
        public bool Footer { get; set; }
        public bool Header { get; set; }
        public bool DOLogo { get; set; }
        public bool DOFooter { get; set; }
        public bool DOHeader { get; set; }
        public String DeliveryDate { get; set; }
        public String CompanyAddress { get; set; }
        public String DOAddress { get; set; }
        public String DOCompanyAddress { get; set; }
        public String DOVNTitle { get; set; }
        public String DOENTitle { get; set; }
        public String ToVN { get; set; }
        public String ToEN { get; set; }
        public String AddressOfSign { get; set; }
        public static void ConvertArriveNotice(ArriveNoticeModel NoticeModel, ArriveNotice Notice)
        {
            Notice.ShipmentId = NoticeModel.ShipmentId;
            Notice.CompanyName = NoticeModel.CompanyName;
            Notice.BillNumber = NoticeModel.BillNumber;
            Notice.ShiperName = NoticeModel.ShiperName;
            Notice.ShiperNumber = NoticeModel.ShiperNumber;

            Notice.ShippingMark = NoticeModel.ShippingMark;
            Notice.NoCTNS = NoticeModel.NoCTNS;
            Notice.GoodsDescription = NoticeModel.GoodsDescription;
            Notice.GrossWeight = NoticeModel.GrossWeight;
            Notice.CBM = NoticeModel.CBM;
            Notice.Notification = NoticeModel.Notification;

            Notice.ETA = DateUtils.Convert2DateTime(NoticeModel.ETA);
            Notice.PortTo = NoticeModel.PortTo;
            Notice.ShipperNote = NoticeModel.ShipperNote;
            Notice.IntroducePaper = NoticeModel.IntroducePaper;

            Notice.ArrivePaper = NoticeModel.ArrivePaper;
            Notice.OrderDate = DateUtils.Convert2DateTime(NoticeModel.OrderDate);
            Notice.Tel = NoticeModel.NoticeTel;
            Notice.Attn = NoticeModel.NoticeAttn;

            Notice.Logo = NoticeModel.Logo;
            Notice.Header = NoticeModel.Header;
            Notice.Footer = NoticeModel.Footer;
            Notice.CompanyAddress = NoticeModel.CompanyAddress;

            Notice.DOENTitle = NoticeModel.DOENTitle;
            Notice.DOVNTitle = NoticeModel.DOVNTitle;
            Notice.ToEN = NoticeModel.ToEN;
            Notice.ToVN = NoticeModel.ToVN;
            Notice.AddressOfSign = NoticeModel.AddressOfSign;
        }
        public static void ConvertDeliveryOrder(ArriveNoticeModel NoticeModel, ArriveNotice Notice)
        {
            Notice.DOENTitle = NoticeModel.DOENTitle;
            Notice.DOVNTitle = NoticeModel.DOVNTitle;
            Notice.ToEN = NoticeModel.ToEN;
            Notice.ToVN = NoticeModel.ToVN;
            Notice.AddressOfSign = NoticeModel.AddressOfSign;

            Notice.ShipmentId = NoticeModel.ShipmentId;
            Notice.CompanyName = NoticeModel.CompanyName;
            Notice.BillNumber = NoticeModel.BillNumber;
            Notice.ShiperName = NoticeModel.ShiperName;
            Notice.ShiperNumber = NoticeModel.ShiperNumber;

            Notice.ShippingMark = NoticeModel.ShippingMark;
            Notice.NoCTNS = NoticeModel.NoCTNS;
            Notice.GoodsDescription = NoticeModel.GoodsDescription;
            Notice.GrossWeight = NoticeModel.GrossWeight;
            Notice.CBM = NoticeModel.CBM;
            Notice.Notification = NoticeModel.Notification;

            Notice.IntroducePaper = NoticeModel.IntroducePaper;

            Notice.ArrivePaper = NoticeModel.ArrivePaper;
            Notice.OrderDate = DateUtils.Convert2DateTime(NoticeModel.OrderDate);

            Notice.Logo = NoticeModel.Logo;
            Notice.Header = NoticeModel.Header;
            Notice.Footer = NoticeModel.Footer;
            Notice.DeliveryDate = NoticeModel.DeliveryDate;
            Notice.DOCompanyAddress = NoticeModel.DOCompanyAddress;
            Notice.DOAddress = NoticeModel.DOAddress;

        }
        public static void ConvertArriveNotice(ArriveNotice NoticeModel, ArriveNoticeModel Notice)
        {
            Notice.Id = NoticeModel.Id;
            Notice.ShipmentId = NoticeModel.ShipmentId != null ? NoticeModel.ShipmentId.Value : 0;
            Notice.CompanyName = NoticeModel.CompanyName;
            Notice.BillNumber = NoticeModel.BillNumber;
            Notice.ShiperName = NoticeModel.ShiperName;
            Notice.ShiperNumber = NoticeModel.ShiperNumber;

            Notice.ShippingMark = NoticeModel.ShippingMark;
            Notice.NoCTNS = NoticeModel.NoCTNS;
            Notice.GoodsDescription = NoticeModel.GoodsDescription;
            Notice.GrossWeight = NoticeModel.GrossWeight;
            Notice.CBM = NoticeModel.CBM;
            Notice.Notification = NoticeModel.Notification;

            Notice.ETA = NoticeModel.ETA != null ? NoticeModel.ETA.Value.ToString("dd/MM/yyyy") : "";
            Notice.PortTo = NoticeModel.PortTo;
            Notice.ShipperNote = NoticeModel.ShipperNote;
            Notice.IntroducePaper = NoticeModel.IntroducePaper;

            Notice.ArrivePaper = NoticeModel.ArrivePaper;
            Notice.OrderDate = NoticeModel.OrderDate != null ? NoticeModel.OrderDate.Value.ToString("dd/MM/yyyy") : "";
            Notice.NoticeTel = NoticeModel.Tel;
            Notice.NoticeAttn = NoticeModel.Attn;

            Notice.Logo = NoticeModel.Logo;
            Notice.Header = NoticeModel.Header;
            Notice.Footer = NoticeModel.Footer;

            Notice.DOLogo = NoticeModel.DOLogo;
            Notice.DOHeader = NoticeModel.DOHeader;
            Notice.DOFooter = NoticeModel.DOFooter;

            Notice.DeliveryDate = NoticeModel.DeliveryDate;
            Notice.CompanyAddress = NoticeModel.CompanyAddress;
            Notice.DOCompanyAddress = NoticeModel.DOCompanyAddress;
            Notice.DOAddress = NoticeModel.DOAddress;

            Notice.DOENTitle = NoticeModel.DOENTitle;
            Notice.DOVNTitle = NoticeModel.DOVNTitle;
            Notice.ToEN = NoticeModel.ToEN;
            Notice.ToVN = NoticeModel.ToVN;
            Notice.AddressOfSign = NoticeModel.AddressOfSign;
        }
    }
}