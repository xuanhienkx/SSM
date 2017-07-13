<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ArriveNoticeModel>" %>

<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System-Print Arrive Notice</title>
    <link id="Link1" href="~/Content/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/global.css" media="all" runat="server" />
    <link id="Link3" type="text/css" rel="stylesheet" href="~/Content/top-nav-bar.css" media="all" runat="server" />
    <link id="Link4" type="text/css" rel="stylesheet" href="~/Content/main-nav-bar.css" media="all" runat="server" />
    <link id="Link5" type="text/css" rel="stylesheet" href="~/Content/homepage.css" media="all" runat="server" />
    <link id="Link6" type="text/css" rel="stylesheet" href="~/Content/section-block.css" media="all" runat="server" />
    <link id="Link7" type="text/css" rel="stylesheet" href="~/Content/footer-bar.css" media="all" runat="server" />
    <link id="Link8" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server" />
    <link id="Link9" type="text/css" rel="stylesheet" href="~/Content/Shipment.css" media="all" runat="server" />
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-2.1.4.min.js") %>" ></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-ui-1.11.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-cookie.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-mousewheel.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/j-select.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/j-select.external.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/main.js") %>"></script>

    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/global.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/top-nav-bar.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/main-nav-bar.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/homepage.js") %>"></script>
    <style type="text/css">
        table tr {
            height: 25px;
        }

        .PrintArriveNotice th :last-child {
            border-color: -moz-use-text-color -moz-use-text-color #000;
            border-style: none none none;
            border-width: medium medium 1px;
        }

        .PrintArriveNotice th {
            border-bottom: 1px solid #000;
            border-right: 1px solid #000;
            font-size: 14px;
            padding: 10px 5px;
            text-align: left;
        }
    </style>

</head>
<body style="margin-left: 40px; margin-top: 15px">
    <div class="PrintDivBody" style="color: Black !important;">
        <table width="800px">
            <tr>
                <td>
                    <div style="overflow: hidden; line-height: 1px; height: auto; float: left; width: auto;">
                        <% if (Model.Logo)
                           { %>
                        <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
                        <%} %>
                    </div>
                </td>
                <td>
                    <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: 550px; font-size: 17px">
                        <% if (Model.Header)
                           { %>
                        <%= ViewData["CompanyHeader"]%>
                        <%} %>
                    </div>
                </td>
                <td></td>
            </tr>
        </table>
        <table border="0" cellpadding="3" width="800px">
            <tr>
                <td colspan="2" style="text-align: right;">
                    <div style="overflow: hidden; padding-right: 50px;">
                        <label style="font-family: Arial Baltic; font-size: 17px; font-weight: bold;">REF #: <%="S " + Request.Params.Get(0) %></label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <label style="font-family: Arial Baltic; font-size: 20px; font-weight: bold;">GIẤY BÁO HÀNG ĐẾN</label>
                    <br />
                </td>
            </tr>
            <tr>
                <td width="220px" valign="top">
                    <label style="font-size: 15px">SANCO FREIGHT CO., LTD : </label>

                </td>
            </tr>
            <tr>
                <td width="220px" valign="top">
                    <label>Xin thông báo đến Quý Công Ty : </label>
                </td>
                <td>
                    <label class="Label"><%= StringUtils.HtmlFilter(Model.CompanyName)%></label>
                </td>
            </tr>
            <tr>
            </tr>
        </table>
        <table border="0" cellpadding="1" width="800px">
            <tr>
                <td width="170px" style="font-size: 14px">
                    <label>Lô hàng thuộc Bill số :</label>
                </td>
                <td width="200px" style="font-size: 16px; font-weight: bold">
                    <%= Model.BillNumber %>
                </td>
                <td width="150px"></td>
                <td></td>
            </tr>
            <tr>
                <td style="font-size: 14px">
                    <label>Tên tàu / Chuyến bay :</label>
                </td>
                <td style="font-size: 14px">
                    <%= Model.ShiperName%>
                </td>
                <td style="font-size: 14px">
                    <label>Số Chuyến</label>
                </td>
                <td style="font-size: 14px">
                    <%= Model.ShiperNumber%>
                </td>
            </tr>
            <tr>
                <td>
                    <label>ETA :</label>
                </td>
                <td style="font-size: 14px">
                    <%= Model.ETA%>
                </td>
                <td style="font-size: 14px">
                    <label>Cập cảng / Sân bay :</label>
                </td>
                <td style="font-size: 14px">
                    <%= Model.PortTo%>
                </td>
            </tr>
        </table>
        <div style="height: auto; overflow: hidden; margin: 10px 0; width: 750px; border: 1px solid #000; border-right: 1px solid #fff">
            <table width="750px" class="PrintArriveNotice">
                <tr>
                    <th width="150px">Số Cont / Seat
                    </th>
                    <th width="150px">Số Kiện
                    </th>
                    <th width="200px">Tên Hàng Hóa
                    </th>
                    <th width="150px">T.Lượng
                    </th>
                    <th width="150px">K.Lượng
                    </th>
                </tr>
                <tr style="border: none; height: 140px;">
                    <td width="150px" style="vertical-align: top; text-align: center;"><%= StringUtils.HtmlFilter(Model.ShippingMark)%>
                    </td>
                    <td width="150px" style="vertical-align: top; text-align: center;"><%= StringUtils.HtmlFilter(Model.NoCTNS)%>
                    </td>
                    <td width="200px" style="vertical-align: top; text-align: center;"><%= StringUtils.HtmlFilter(Model.GoodsDescription)%>
                    </td>
                    <td width="150px" style="vertical-align: top; text-align: center;"><%= StringUtils.HtmlFilter(Model.GrossWeight)%>
                    </td>
                    <td width="150px" style="vertical-align: top; text-align: center;"><%= StringUtils.HtmlFilter(Model.CBM)%>
                    </td>
                </tr>
            </table>
        </div>

        <div style="height: auto; overflow: hidden; padding: 10px 0 10px; font-size: 15px">
            <span>Đề nghị quý khách đến công ty chúng tôi để nhận lệnh giao hàng. Vui lòng mang theo các giấy tờ sau:</span>
        </div>
        <div style="height: auto; overflow: hidden; font-size: 13px">
            <table border="0" cellpadding="3" width="550px">
                <tr>
                    <td style="font-size: 14px">
                        <label>1. Vận tải đơn góc.  </label>
                        <%: Html.CheckBoxFor(m => m.ShipperNote, new { ReadOnly="true"})%>
                    </td>
                    <td style="font-size: 14px">
                        <label>2. Giấy giới thiệu.  </label>
                        <%: Html.CheckBoxFor(m=>m.IntroducePaper, new { ReadOnly="true"}) %>
                    </td>
                    <td style="font-size: 14px">
                        <label>3. Giấy báo hàng đến.  </label>
                        <%: Html.CheckBoxFor(m => m.ArrivePaper, new { ReadOnly = "true" })%>
                    </td>
                </tr>
            </table>
        </div>
        <br>
        <br />

        <div style="height: auto; overflow: hidden; padding: 5px 0; font-size: 15px">
            <span>Và số tiền thanh toán bằng USD : </span>
        </div>
        <% Revenue Revenue1 = (Revenue)ViewData["Revenue"];
           if (Revenue1 == null) { Revenue1 = RevenueModel.InitRevenue(); }%>

        <table border="0" cellpadding="4" width="650px">
            <tr>
                <td>
                    <label style="font-size: 16px">
                        1. Cước phí :
                    </label>
                </td>
                <td>$<label style="font-size: 15px"> <%= Revenue1.INTransportRate.Value.ToString("0.00")%> </label>
                </td>
                <td>
                    <label>+ VAT: 0% </label>
                </td>

                <td>$<label style="font-weight: bold; font-size: 15px"> <%= Revenue1.INTransportRate.Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>

                <td>
                    <label style="font-size: 16px">
                        2. Phí EXW :
                    </label>
                </td>
                <td>$<label style="font-size: 15px"> <%= Revenue1.INInlandService.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 0% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INInlandService)*100/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        3. Phí giao Door :
                    </label>
                </td>
                <td>$
                    <label style="font-size: 15px"><%= Revenue1.INCreditDebit.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INCreditDebit)*100/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        4. Phí chứng từ :
                    </label>
                </td>
                <td>$
                    <label style="font-size: 15px"><%= Revenue1.INDocumentFee.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INDocumentFee)*110/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        5. Phí handling :
                    </label>
                </td>
                <td>$
                    <label style="font-size: 15px"><%= Revenue1.INHandingFee.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INHandingFee)*110/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        6. THC :
                    </label>
                </td>
                <td>$<label style="font-size: 15px"> <%= Revenue1.INTHC.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INTHC)*110/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        7. CFS :
                    </label>
                </td>
                <td>$<label style="font-size: 15px"> <%= Revenue1.INCFS.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INCFS)*110/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        8.
                        <%= Revenue1.AutoName1 %>
                        :
                    </label>
                </td>
                <td>$<label style="font-size: 15px"> <%= Revenue1.INAutoValue1.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INAutoValue1)*110/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        9.
                        <%= Revenue1.AutoName2 %>
                        :
                    </label>
                </td>
                <td>$<label style="font-size: 15px"> <%= Revenue1.INAutoValue2.Value.ToString("0.00")%></label>
                </td>
                <td>
                    <label>+ VAT: 10% </label>
                </td>
                <td>$<label style="font-weight: bold; font-size: 15px"> <%= ((Revenue1.INAutoValue2)*110/100).Value.ToString("0.00")%></label>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px; font-weight: bold">
                        **. Total Amount (with VAT) :
                    </label>
                </td>
                <td></td>
                <td></td>
                <td>$<label style="font-weight: bold; font-size: 16px"> <%= (Revenue1.INTransportRate + Revenue1.INInlandService + ((Revenue1.INCreditDebit)*110/100)+((Revenue1.INDocumentFee)*110/100)+((Revenue1.INHandingFee)*110/100)+((Revenue1.INTHC)*110/100)+((Revenue1.INCFS)*110/100)+((Revenue1.INAutoValue1)*110/100)+ ((Revenue1.INAutoValue2)*110/100)).Value.ToString("0.00")%></label>

                </td>
            </tr>
        </table>
        <br>
        <br />
        <div style="height: auto; overflow: hidden; padding-top: 10px;">
            <span style="font-size: 16px">Liên hệ điện thoại trước khi đến nhận lệnh giao hàng. Cảm ơn!</span><br />
        </div>
        <div style="height: auto; overflow: hidden; padding-bottom: 10px;">
            <table>
                <tr>
                    <td style="font-size: 16px">
                        <span>Tel : 08-39104532 </span>
                    </td>
                    <td style="width: 100px; font-size: 17px">
                        <%= Model.NoticeTel%>
                    </td>
                    <td style="font-size: 16px">
                        <span>/ Attn : Baodung </span>
                    </td>
                    <td style="width: 200px; font-size: 16px">
                        <%= Model.NoticeAttn%>
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellpadding="3">
            <tr>
                <td colspan="2" style="text-align: center">
                    <label style="font-family: Arial Baltic; font-size: 16px; font-weight: bold;">
                        CÔNG TY CHÚNG TÔI CÓ SẴN DỊCH VỤ KHAI BÁO HẢI QUAN VÀ VẬN CHUYỂN ĐẾN KHO</label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <label style="font-family: Arial Baltic; font-size: 16px;">
                        Rất hân hạnh được phục vụ quý khách!</label>
                    <br />
                    <br />
                    <label style="font-family: Arial Baltic; font-size: 12px; font-weight: bold; text-align: center">
                        THỜI GIAN LÀM VIỆC CỦA CHÚNG TÔI :
     * SÁNG : 8H - 12H / CHIỀU : 13H - 17H (TỪ THỨ 2 - 6). 
     * THỨ 7 : LÀM VIỆC BUỔI SÁNG.    </label>
                <br />
                        <br />


                        <label style="font-family: Arial Baltic; font-size: 15px; font-weight: bold; text-align: left">
                            Ghi chú: quí khách vui lòng kiểm tra trước với cảng / sân bay hoặc Sanco Freight Ltd
        </label>
     <br />
                            <label style="font-family: Arial Baltic; font-size: 15px; font-weight: bold">
                                trước khi điều xe chở hàng để tránh trường hợp cảng / sân bay  chưa khai thác hàng kịp. Cảm ơn!
                            </label>
                </td>
            </tr>
        </table>






    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>
