<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BookingNoteModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System-Print Booking Note</title>
    <link id="Link1" href="~/Content/PrintPage/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/PrintPage/global.css" media="all" runat="server" />
    <link id="Link12" type="text/css" rel="stylesheet" href="~/Content/PrintPage/top-nav-bar.css" media="all" runat="server" />
    <link id="Link13" type="text/css" rel="stylesheet" href="~/Content/PrintPage/main-nav-bar.css" media="all" runat="server" />
    <link id="Link14" type="text/css" rel="stylesheet" href="~/Content/PrintPage/homepage.css" media="all" runat="server" />
    <link id="Link15" type="text/css" rel="stylesheet" href="~/Content/PrintPage/section-block.css" media="all" runat="server" />
    <link id="Link16" type="text/css" rel="stylesheet" href="~/Content/PrintPage/footer-bar.css" media="all" runat="server" />
    <link id="Link17" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server" />
    <link id="Link18" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Shipment.css" media="all" runat="server" />
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
</head>
<body style="margin-left:40px; margin-top:15px">
<div class="NormalLabel">
    <div style="width:835px; text-align:center; padding:15px 0 15px;"><label style="font-family:Arial Baltic;font-size:26px;font-weight:bold;">BOOKING NOTE</label></div>
    <table width="835px" border="1px solid #000000" cellpadding="3">
        <tr>
            <td width="50%">
                <label class="BookingLabel">
                    Date: </label> <label style="font-size:15px"><%= Model.Date%></label>
            </td>
            <td colspan="2">
                <label class="BookingLabel">
                    Booking No: </label> <label style="font-size:15px"><%= Model.BookingNo%></label>
            </td>
        </tr>
    <tr style="height:75px"><td><label class="BookingLabel">Shipper: </label> <div class="BookinNoteShipper"; style="font-size:15px"><%= StringUtils.HtmlFilter(Model.ShipperName)%></div></td>
    <td rowspan="3" colspan="2">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <div style="overflow: hidden; line-height: 1px; height: auto; text-align:center;">
                    <% if (Model.Logo)
                       { %>
                        <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
                        <%} %>
                    </div>
                </td>
            </tr>
            <tr></tr>
            <tr></tr>
            <tr class="CompanyAddress">
                <td colspan="2">
                   <div style="overflow: hidden; height: auto; float: left; padding: 5px 0px 0px 5px; line-height:normal">
                <% if (Model.Header)
                   { %>
                    <%= ViewData["CompanyHeader"]%>
                    <%} %>
                </div>
                </td>
            </tr>
        </table>
    </td></tr>
    <tr style="height:75px"><td><label class="BookingLabel">Consignee:</label> <div class="BookinNoteShipper"; style="font-size:15px"><%= StringUtils.HtmlFilter(Model.Consignee)%></div></td></tr>
    <tr style="height:75px"><td><label class="BookingLabel">Notify address:</label> <div class="BookinNoteShipper"; style="font-size:15px"><%= StringUtils.HtmlFilter(Model.NotifyAddress)%></div></td></tr>
        <tr>
            <td>
                <label class="BookingLabel">
                    Place of receipt:</label> <div class="BookinNoteShipper"; style="font-size:15px"><%= StringUtils.HtmlFilter(Model.PlaceOfReceipt)%></div>
            </td>
            <td><div style="height:auto;overflow:hidden">
                <label class="BookingLabel">
                    Port of loading:</label>
                    </div>
                    <div class="BookinNoteVesel"; style="font-size:15px">
                     <label><%= StringUtils.HtmlFilter(Model.PortToLoading)%></label>
                    </div>
            </td>
            <td><div style="height:auto;overflow:hidden">
                <label class="BookingLabel">
                    Vessel:</label></div> 
                    <div class="BookinNoteVesel"; style="font-size:15px">
                     <label><%= StringUtils.HtmlFilter(Model.Vesel)%></label>
                    </div>
            </td>
        </tr>
        <tr>
            <td><label class="BookingLabel">
                    ETD:</label> <label style="font-size:15px"><%= Model.ETD%></label>
            </td>
            <td><div style="height:auto;overflow:hidden"><label class="BookingLabel2">
                    Port of destination:</label></div>
                    <div class="BookinNoteVesel">
                     <label style="font-size:15px"><%= Model.PortOfDestination%></label>
                    </div>
            </td>
            <td><div style="height:auto;overflow:hidden">
            <label class="BookingLabel2">
                    Place of Delivery:</label>
                    </div>
                    <div class="BookinNoteVesel">
                     <label style="font-size:15px"><%= Model.PlaceOfDelivery%></label>
                    </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" style="text-align:center;">
            <label style="font-family:Arial Baltic;font-size:14px;font-weight:bold;">PARTICULAR FURNISHED BY CUSTOMER - CARRIE NOT RESPONSIBLE</label>
            </td>
        </tr>
        <tr>
            <td colspan="3">
            <table class="TableNormal" width="100%">
        <tr style="background: #338899;">
            <th>
               Shipping mark
            </th>
            <th>
                No. of ctns
            </th>
            <th>
                Description of goods
            </th>
            <th>
                Gross weight
            </th>
            <th>
                CBM
            </th>
        </tr>
     <tr>
            <td style="font-size:15px; vertical-align:top"><%= StringUtils.HtmlFilter(Model.ShippingMark)%>
            </td>
            <td style="font-size:15px; vertical-align:top"><%= StringUtils.HtmlFilter(Model.NoCTNS)%>
            </td>
            <td style="font-size:15px; vertical-align:top"><%= StringUtils.HtmlFilter(Model.GoodsDescription)%>
            </td>
            <td style="font-size:15px; vertical-align:top"><%= StringUtils.HtmlFilter(Model.GrossWeight)%>
            </td>
            <td style="font-size:15px; vertical-align:top"><%= StringUtils.HtmlFilter(Model.CBM)%>
            </td>
        </tr>
       <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
       <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
       <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
       <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
       <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>
        <tr><td ></td><td ></td><td ></td><td ></td><td ></td></tr>

    </table>
        </td>
        </tr>
        <tr style="height:35px">
            <td colspan="3"><label class="BookingLabel2">
                    Place of stuffing:</label> <label style="font-size:15px"><%= Model.PlaceOfStuffing%></label>
            </td>
        </tr>
        <tr style="height:35px">
            <td colspan="3"><label class="BookingLabel2">
                    Person in charge:</label> <label style="font-size:15px"><%= Model.PersonInCharge%></label>
            </td>
        </tr>
        <tr style="height:35px">
            <td colspan="3"><label class="BookingLabel2">
                    Closing time:</label> <label style="font-size:15px"><%= Model.ClosingTime%></label>
            </td>
        </tr>
    </table>
    <div style="padding:20px 0;">
    <table width="100%">
        <tr>
            <td style="text-align: center; font-weight: bold; font-size: 15px">
                SHIPPER
            </td>
            <td style="text-align: center; font-weight: bold; font-size: 15px">
                SANCO FREIGHT LTD
            </td>
        </tr>
        <tr><td>.</td></tr>
        <tr><td>.</td></tr>
        <tr><td>.</td></tr>
        <tr><td>.</td></tr>
        <tr><td>.</td></tr>
        <tr>
            <td style="text-align: center; font-weight:normal ; font-size: 15px">
           <div style="height:auto">
                <label><%= Model.Shipper%></label>
                </div>
            </td>
            <td style="text-align: center; font-weight:normal; font-size: 15px">
            <div style="height:auto">
                <label><%= Model.SancoFreight%></label>
                </div>
            </td>
        </tr>
    </table>
    </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
   </script>
</body>
</html>
