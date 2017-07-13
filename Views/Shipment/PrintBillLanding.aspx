<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BillLandingModel>" %>
<%@ Import Namespace="SSM" %>
<%@ Import Namespace="SSM.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System - Print Bill Lading</title>
    <link id="Link1" href="~/Content/PrintPage/Site.css" rel="stylesheet" type="text/css" runat="server"/>
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/PrintPage/global.css" media="all" runat="server"/>
    <link id="Link3" type="text/css" rel="stylesheet" href="~/Content/PrintPage/top-nav-bar.css" media="all" runat="server"/>
    <link id="Link4" type="text/css" rel="stylesheet" href="~/Content/PrintPage/main-nav-bar.css" media="all" runat="server"/>
    <link id="Link5" type="text/css" rel="stylesheet" href="~/Content/PrintPage/homepage.css" media="all" runat="server"/>
     <link id="Link6" type="text/css" rel="stylesheet" href="~/Content/PrintPage/section-block.css" media="all" runat="server"/>
      <link id="Link7" type="text/css" rel="stylesheet" href="~/Content/PrintPage/footer-bar.css" media="all" runat="server"/>
      <link id="Link8" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server"/>
      <link id="Link9" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Shipment.css" media="all" runat="server" />
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
    .TableNormal td {
        border-right: none;
        padding: 5px;
    }
    </style>
</head>
<body style="margin-left:30px; margin-top:15px">
<div class="BillLandingZone" style="font-size:14px;padding-top:20px;color: black;">
    <table width="850px" border="0">
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
                <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: auto;">
                <% if (Model.Header)
                   { %>
                    <%= ViewData["CompanyHeader"]%>
                    <%} %>
                </div>
                </td>
                <td>
                    <div style="float: right; width: 200px;">
                        <label style="font-family: Arial Baltic; font-size: 24px; font-weight: bold;">
                            BILL OF LADING</label></div>
                </td>
            </tr>
        </table>
       
   <table cellpadding="3" width="870px">
        <tr>
            <td style="width: 420px; border-right: 1px solid #000; border-top: 1px solid #000">
                <div class="BOLShipperLabel" style="margin-top: -48px;">
                    <label class="BillLabel">
                        Shipper</label></div>
                <div class="BOLShipper">
                    <%= StringUtils.HtmlFilter(Model.PhipperName)%>
                </div>
            </td>
            <td rowspan="2" valign="bottom"; style="border-top: 1px solid #000">
                <div class="BOLLeftLabel">
                    <label class="BillLabel">
                        Bill of Lading No.
                    </label>
                    <label class="NormalLabel" style="padding-left: 30px">
                        <%= Model.BillLandingNo%></label></div>
                <div style="width: 100%; overflow: hidden; height: auto; border-bottom: 1px solid #000;">
                    <br />
                </div>
                <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: auto">
                    <% if (Model.Footer)
                      { %>
                    <%= ViewData["CompanyFooter"]%>
                    <%} %>
                </div>
                <div style="min-height: 150px; overflow: hidden; clear: both;">
                    <div style="height: 120px; overflow: hidden; padding: 100px 0 10px; text-align: center;
                        font-size: 30px; font-weight: bold;">
                        <%= StringUtils.HtmlFilter(Model.Original) %>
                    </div>
                    <div class="BOLLeftLabel">
                        <span class="Label" style="padding-right: 30px">Ref. No. </span>
                        <label class="Label">
                            #
                            <%= Model.ShipmentId %></label></div>
                </div>
            </td>
        </tr>
        <tr style="border-top:1px solid #000;">
            <td style="border-right: 1px solid #000">
                <div class="BOLShipperLabel" style="margin-top: -48px;">
                    <label class="BillLabel">
                        Consignee</label></div>
                <div class="BOLShipper">
                    <%= StringUtils.HtmlFilter(Model.ConsignedOrder)%></div>
            </td>
        </tr>
        <tr style="border-top:1px solid #000;">
            <td style="border-right: 1px solid #000">
                <div class="BOLShipperLabel">
                    <label class="BillLabel">
                        Notify Party</label></div>
                <div class="BOLShipper">
                    <%= StringUtils.HtmlFilter(Model.NotifyAddress)%></div>
            </td>
            <td>
                <div class="BOLLeftLabel">
                    <label class="BillLabel">
                        For release of goods apply to</label></div>
                <div class="BOLShipper">
                    <%= StringUtils.HtmlFilter(Model.ForRelease)%></div>
            </td>
        </tr>
        <tr>
            <td style="border-top:1px solid #000; border-right:1px solid #000">
                <div class="BOLShipperLabel">
                    <label class="BillLabel">
                      Place of receipt</label><span style="padding-left:30px"><%= Model.PlaceOfReceipt%></span></div>
            </td>
        </tr>
        <tr>
            <td style="border-top:1px solid #000; border-right: 1px solid #000">
                <table width="100%">
                    <tr>
                        <td>
                        <div class="BOLShipperLabel"><label class="BillLabel">Ocean vessel</label></div>
                        <div class="BOLShipperText"><%= Model.OceanVessel%></div>
                        </td>
                        <td>
                        <div class="BOLShipperLabel"><label class="BillLabel">Voy no.</label></div>
                        <div class="BOLShipperText"><%= Model.VoyNo%></div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="border-top:1px solid #000">
            <div class="BOLLeftLabel"><label class="BillLabel">Port of loading</label></div>
            <div class="BOLShipperText"><%= Model.PortLoading %></div>
            </td>
        </tr>
        <tr style="border-top:1px solid #000;">
            <td style="border-top:1px solid #000; border-right: 1px solid #000">
                <table width="100%">
                    <tr>
                        <td>
                        <div class="BOLShipperLabel"><label class="BillLabel">Port of discharge</label></div>
                        <div class="BOLShipperText"><%= Model.PortDischarge%></div>
                        </td>
                        <td>
                        <div class="BOLShipperLabel"><label class="BillLabel">Place of delivery</label></div>
                        <div class="BOLShipperText"><%= Model.PlaceDelivery%></div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            <div class="BOLLeftLabel"><label class="BillLabel">Final destination     </label><label style="font-size:13">  ( For the merchant's reference onlly)</label></div>
            <div class="BOLShipperText"><%= Model.FinalDestination%></div>
            </td>
        </tr>
        <tr style="border-top:1px solid #000;">
            <td colspan="2">
                <table class="TableNormal" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <td valign="top" align="center">
                            <div class="GoodsLabel">
                                <label class="BillLabel">
                                    Marks and Nos</label></div>
                        </td>
                        <td valign="top" align="center">
                            <div class="GoodsLabel">
                                <label class="BillLabel">
                                    Quantity and description of goods</label></div>
                        </td>
                        <td valign="top" align="center">
                        <div class="GoodsLabel"><label class="BillLabel">Gross weight (Kgs)</label></div>
                        </td>
                        <td valign="top" align="center" style="border-right: 1px solid #fff;">
                        <div class="GoodsLabel"><label class="BillLabel">Measurement (M3)</label></div>
                        </td>
                    </tr>
                    <tr style="height:220px;">
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.MarksNos)%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.QuanlityDescription)%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.GrossWeight)%></div>
                        </td>
                        <td valign="top" style="border-right: 1px solid #fff;">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.Measurement)%></div>
                        </td>
                    </tr>
                    
                </table>
            </td>
        </tr>
        <tr style="border-top:1px solid #000;">
            <td style="height:100px;vertical-align:top; border-right:1px solid #000">
            <div class="BOLShipperLabel"><label class="BillLabel">Freight and charges</label></div>
            <div class="BOLShipperText"><%= StringUtils.HtmlFilter(Model.FreightCharge)%></div>
            </td>
            <td rowspan="2" style="font-size:12px">
            <div style="height:auto;overflow:hidden">
            RECEIVED the goods in apparent good order and condition and, as far as ascertained by reasonable means of checking, as specified above unless otherwise stated.</div>
            <div style="height:auto;overflow:hidden">The undesigned, in accordance with the provisions contained in this document</div>
            <div style="height:auto;overflow:hidden;padding-left:30px;">* Undertakes to perform or to procure the performance of the entire transport from the place at which the goods are taken in charge to the place designated for delivery in this document, and</div>
            <div style="height:auto;overflow:hidden;padding-left:30px;">* Assumes liability as prescribeb in this document for such transport. One of the B/Ls must be surrendered duly endorsed in exchange for the goods or delivery order.</div>
            <div style="height:auto;overflow:hidden">IN WITNESS whereof THERE (3) original B/Ls have been signed, if not otherwise stated above, one of which being acconplished the other(s) to be void</div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align:top; border-top:1px solid #000; border-right:1px solid #000">
            <div class="BOLShipperLabel"><label class="BillLabel">Place and date of issue</label></div>
            <div class="BOLShipperText"><%= Model.PlaceDateIssue%></div>
            </td>
        </tr>
        <tr style="border-top:1px solid #000">
            <td style="border-right:1px solid #000">
             <table width="100%">
                    <tr>
                        <td>
                        <div class="BOLShipperLabel"><label class="BillLabel">Freight payable at</label></div>
                        <div class="BOLShipperText"><%= Model.FreightPayable%></div>
                        </td>
                        <td>
                        <div class="BOLShipperLabel"><label class="BillLabel">Number of original B/Ls</label></div>
                        <div class="BOLShipperText"><%= Model.NumberOriginal%></div>
                        </td>
                    </tr>
                </table>
            </td>
             <td valign="top">
              <div class="BOLLeftLabel"><label class="BillLabel">Stamp and authorized signature</label></div>
              <div>.</label></div>
              <div>.</div>
              <div>.</div>
              <div>.</div>
               <div class="BOLShipperText"><%= Model.StampAuthorized%></div>
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
