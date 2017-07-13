<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ArriveNoticeModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System-Print Delivery Order</title>
    <link id="Link1" href="~/Content/Site.css" rel="stylesheet" type="text/css" runat="server"/>
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/global.css" media="all" runat="server"/>
    <link id="Link3" type="text/css" rel="stylesheet" href="~/Content/top-nav-bar.css" media="all" runat="server"/>
    <link id="Link4" type="text/css" rel="stylesheet" href="~/Content/main-nav-bar.css" media="all" runat="server"/>
    <link id="Link5" type="text/css" rel="stylesheet" href="~/Content/homepage.css" media="all" runat="server"/>
     <link id="Link6" type="text/css" rel="stylesheet" href="~/Content/section-block.css" media="all" runat="server"/>
      <link id="Link7" type="text/css" rel="stylesheet" href="~/Content/footer-bar.css" media="all" runat="server"/>
      <link id="Link8" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server"/>
      <link id="Link9" type="text/css" rel="stylesheet" href="~/Content/Shipment.css" media="all" runat="server" />
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-2.1.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-ui-core.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-ui.js") %>"></script>
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
        table tr 
        {
            height:20px;
            }
 
         .PrintArriveNotice th :last-child {
        border-color:-moz-use-text-color -moz-use-text-color #000;
        border-style:none none none;
        border-width:medium medium 1px;
        }
        .PrintArriveNotice th {
        border-bottom:1px solid #000;
        border-right:1px solid #000;
        font-size:14px;
        padding:10px 5px;
        text-align:left;
        }
    </style>
</head>
<body style="margin-left:40px; margin-top:15px">
    <div class="PrintDivBody">
   <table border="0" cellpadding="3" width="850px">
            <tr>
                <% if (Model.DOLogo)
               { %>
                <td>
                    <div style="top: auto; overflow: hidden; line-height: 1px; height: auto; float: left;
                        width: auto;">
                        <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
                    </div>
                </td>
                <%}
               else
               {%>
                <td rowspan="3" style="width: auto;">
                </td>
                <%} %>
                <td style="text-align: left">
                    <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: 400px">
                        <% if (Model.DOHeader)
                   { %>
                        <%= ViewData["CompanyHeader"]%>
                        <%} %>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <div style="text-align: center; clear: both; padding-right: 50px;">
                        <label style="font-family: Arial Baltic; font-size: 30px; font-weight: bold;">
                            <%=Model.DOVNTitle %></label>
                        <br />
                        <label style="font-family: Arial Baltic; font-size: 16px;">
                            <%=Model.DOENTitle %></label>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: right; font-size:17px">
                    <div style="font-weight: bold; padding-right: 100px">
                        REF #:
                        <%="S "+ Request.Params.Get(0) %></div>
                </td>
            </tr>
            <tr>
                <td style="text-align: right; width: 120px; font-size:16px">
                    <label style="font-weight: bold;">
                        Kính gửi:
                    </label>
                </td>
                <td style="text-align: left">
                    <label style="font-family: Arial Baltic; font-size: 16px;">
                        <%=Model.ToVN %>
                        <%= Model.PortTo%></label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    <label style="font-family: Arial Baltic; font-size:16px">
                        To:
                    </label>
                </td>
                <td colspan="2" style="font-size:16px">
                    <label>
                        <%=Model.ToEN %></label>
                </td>
            </tr>
            <tr style="height: 17px">
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:16px">
                    CÔNG TY GIAO NHẬN VÀ VẬN TẢI QUỐC TẾ SAO NAM (SANCO FREIGHT LTD) ĐỀ NGHỊ GIAO CHO :
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="font-family: Arial Baltic; font-size: 16px; font-weight: bold;">
                    <%= StringUtils.HtmlFilter(Model.DOAddress)%>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="3" width="700px" style="font-size:16px">
            <tr>
                <td style="width: 200px">
                    <label>
                        Lô hàng thuộc B/L số :</label>
                    <br />
                    <label>
                        (Shipment under B/L No.)</label>
                </td>
                <td style="width: 150px" valign="top">
                    <%= Model.BillNumber %>
                </td>
            </tr>
            <tr>
                <td>
                    <label style="font-size: 16px">
                        Tên tàu / Chuyến bay :</label>
                    <br />
                    <label>
                        (Vessel)</label>
                </td>
                <td valign="top" style="width: 200px">
                    <%= Model.ShiperName%>
                </td>
                <td align="right">
                    <label>
                        Số Chuyến :</label>
                    <br />
                    <label>
                        (Voyage)</label>
                </td>
                <td align="center" valign="top">
                    <%= Model.ShiperNumber%>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Ngày cập cảng :</label><br />
                    <label>
                        (ETA)</label>
                </td>
                <td valign="top">
                    <%= Model.ETA%>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div style="height: auto; overflow: hidden; margin: 15px 0; border: 1px solid #000;
            width: 95%; border-right: 1px solid #fff;">
            <table class="PrintArriveNotice" width="100%" style="text-align: center;">
                <tr>
                    <th align="center">
                        Số Cont / Seal
                        <br />
                        <label style="font-size: 16px;">
                            (Cont / Seal No.)</label>
                    </th>
                    <th align="center">
                        Số Kiện
                        <br />
                        <label style="font-size: 16px;">
                            (No.of Packages)</label>
                    </th>
                    <th align="center">
                        Tên Hàng Hóa
                        <br />
                        <label style="font-size: 16px;">
                            (Description of goods)</label>
                    </th>
                    <th align="center">
                        T.Lượng
                        <br />
                        <label style="font-size: 16px;">
                            (Weight)</label>
                    </th>
                    <th align="center">
                        Số khối
                        <br />
                        <label style="font-size: 16px;">
                            (Volume)</label>
                    </th>
                </tr>
                <tr style="border: none; height: 120px; text-align: center; vertical-align: text-top; font-size:14px">
                    <td width="150px">
                        <%= StringUtils.HtmlFilter(Model.ShippingMark)%>
                    </td>
                    <td width="150px">
                        <%= StringUtils.HtmlFilter(Model.NoCTNS)%>
                    </td>
                    <td width="200px">
                        <%= StringUtils.HtmlFilter(Model.GoodsDescription)%>
                    </td>
                    <td width="150px">
                        <%= StringUtils.HtmlFilter(Model.GrossWeight)%>
                    </td>
                    <td width="150px">
                        <%= StringUtils.HtmlFilter(Model.CBM)%>
                    </td>
                </tr>
            </table>
        </div>
        <div style="height: auto; overflow: hidden; padding: 10px 0 10px;font-size:16px">
            <span style="padding-left: 30px;">Kính mong các cơ quan hữu quan tạo điều kiện thuận
                lợi cho cơ quan trên trong việc làm thủ tục nhận hàng </span>
            <br />
            <span> (Pls kindly assist the company to take delivery of the goods smoothly)</span>
        </div>
        <div style="height: auto; overflow: hidden; padding: 10px 0; font-size:16px">
            <span>Chân thành cảm ơn!</span>
        </div>
        <div style="height: auto; overflow: hidden; padding: 10px 0; font-size:16px">
            <span>(Yours Sincerely !)</span><br />
        </div>
        <% Revenue Revenue1 = (Revenue)ViewData["Revenue"];
       if (Revenue1 == null) { Revenue1 = RevenueModel.InitRevenue(); }%>
        <table border="0" cellpadding="3" width="100%" style="font-size:16px">
            <tr>
                <td>
                    <label style="font-family: Arial Baltic; font-weight: bold;">
                        NGƯỜI NHẬN CHỨNG TỪ
                    </label>
                    <label>
                        (Reveiver's signature)
                    </label>
                </td>
                <td align="right">
                    <label style="font-family: Arial Baltic; font-weight: bold; float: right; line-height: 20px;
                        vertical-align: middle; border-bottom: 1px dotted Silver; padding-right: 30px;
                        width: 100px;">
                        <%= "&nbsp;" + Model.DeliveryDate%></label>
                    <label style="font-family: Arial Baltic; font-weight: bold; float: right; line-height: 25px;
                        vertical-align: middle; padding-right: 30px;">
                        <%= Model.AddressOfSign %>, Ngày (Date)</label>
                </td>
            </tr>
        </table>
        <div style="font-size: 16px">
            <table border="0" cellpadding="0">
                <tr>
                    <td colspan="4">
                        <label>
                            Đã nhận đủ giấy tờ kèm theo (Received docs as below) :</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            1- Vận đơn(AWB, B/L) </label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                    <td>
                        <label>
                            4- Chứng thư bảo hiểm </label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            2- Invoice / Parking list </label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                    <td>
                        <label>
                            5- Khác </label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            3- C/O </label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                </tr>
                <tr></tr>
                <tr>
                    <td colspan="4">
                        <label>
                            Ngày (date):</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <label>
                            Ký tên (Signature):</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <label>
                            Họ và tên (Full Name):</label>
                    </td>
                    <td>
                        <div style="margin-left: 220px">
                            CAO TRAN NGOC DIEP
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="4">
                        ___________________________
                    </td>
                </tr>
            </table>
    </div>
    <div style="overflow: hidden; height: auto; clear: both; padding: 0 15px; width: 400px">
                <% if (Model.DOFooter)
                   { %>
                    <%= ViewData["CompanyFooter"]%>
                    <%} %>
    </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
   </script>
</body>
</html>
