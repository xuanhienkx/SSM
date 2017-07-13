<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BookingNoteModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Booking Note
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm())
   { %>
       <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
   <div style="width:835px; text-align:center; padding:15px 0 15px;"><label style="font-family:Arial Baltic;font-size:20px;font-weight:bold;">BOOKING NOTE</label></div>
    <table class="BookingTable" border="1px solid Silver" cellpadding="3">
        <tr>
            <td>
                <label class="BookingLabel">
                    Date:</label><%: Html.TextBoxFor(m => m.Date, new { Class = "ShipmentInput" })%><label for="Date" class="DateInput"></label>
            </td>
            <td colspan="2">
                <label class="BookingLabel">
                    Booking No:</label><%: Html.TextBoxFor(m => m.BookingNo, new { Class = "ShipmentInput" })%>
            </td>
        </tr>
    <tr><td><label class="BookingLabel">Shipper:</label><%: Html.TextAreaFor(m => m.ShipperName, new { Class = "ShipmentTextArea" })%></td><td rowspan="3" colspan="2">
        <table width="100%">
            <tr>
                <td colspan="2">
                    <div style="height: auto; overflow: hidden; padding: 0 10px; text-align: center;
                        font-size: 20px; font-weight: bold;">
                        <%: Html.CheckBoxFor(m=>m.Logo) %><span>Logo</span>
                        <%: Html.CheckBoxFor(m=>m.Header) %><span>Header</span>
                     </div>
                </td>
            </tr>
            
        </table>
    </td></tr>
    <tr><td><label class="BookingLabel">Consignee:</label> <%: Html.TextAreaFor(m => m.Consignee, new { Class = "ShipmentTextArea" })%></td></tr>
    <tr><td><label class="BookingLabel">Notify address:</label><%: Html.TextAreaFor(m => m.NotifyAddress, new { Class = "ShipmentTextArea" })%></td></tr>
        <tr>
            <td>
                <label class="BookingLabel">
                    Place of receipt:</label><%: Html.TextAreaFor(m => m.PlaceOfReceipt, new { Class = "ShipmentTextArea" })%>
            </td>
            <td><div style="height:auto;overflow:hidden">
                <label class="BookingLabel">
                    Port of loading:</label>
                    </div>
                    <div style="height:auto;overflow:hidden">
                    <%: Html.TextAreaFor(m => m.PortToLoading, new { Class = "ShipmentTextArea" })%>
                    </div>
            </td>
            <td><div style="height:auto;overflow:hidden">
                <label class="BookingLabel">
                    Vessel:</label></div> 
                    <div style="height:auto;overflow:hidden">
                    <%: Html.TextAreaFor(m => m.Vesel, new { Class = "ShipmentTextArea" })%>
                    </div>
            </td>
        </tr>
        <tr>
            <td><label class="BookingLabel">
                    ETD:</label><%: Html.TextBoxFor(m => m.ETD, new { Class = "ShipmentInput" })%><label for="ETD" class="DateInput"></label>
            </td>
            <td><div style="height:auto;overflow:hidden"><label class="BookingLabel">
                    Port of destination:</label></div>
                    <div style="height:auto;overflow:hidden">
                    <%: Html.TextBoxFor(m => m.PortOfDestination, new { Class = "ShipmentInput" })%>
                    </div>
            </td>
            <td><div style="height:auto;overflow:hidden">
            <label class="BookingLabel">
                    Place of Delivery:</label>
                    </div>
                    <div style="height:auto;overflow:hidden">
                    <%: Html.TextBoxFor(m => m.PlaceOfDelivery, new { Class = "ShipmentInput" })%>
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
             <table class="TableNormal" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <td valign=top>
                            <div style="height: auto; overflow: hidden">
                                <label class="BillLabel">
                                   Shipping mark</label></div>
                        </td>
                        <td>
                            <div style="height: auto; overflow: hidden">
                                <label class="BillLabel">
                                    No. of ctns</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel"> Description of goods</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Gross weight</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">CBM</label></div>
                        </td>
                    </tr>
                    <tr>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.ShippingMark, new { Class = "Bill1TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.NoCTNS, new { Class = "Bill3TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.GoodsDescription, new { Class = "Bill1TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.GrossWeight, new { Class = "Bill3TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.CBM, new { Class = "Bill3TextArea" })%></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3"><label class="BookingLabel">
                    Place of stuffing:</label><%: Html.TextBoxFor(m => m.PlaceOfStuffing, new { Class = "BillInputLong" })%>
            </td>
        </tr>
        <tr>
            <td colspan="3"><label class="BookingLabel">
                    Person in charge:</label><%: Html.TextBoxFor(m => m.PersonInCharge, new { Class = "BillInputLong" })%>
            </td>
        </tr>
        <tr>
            <td colspan="3"><label class="BookingLabel">
                    Closing time:</label><%: Html.TextBoxFor(m => m.ClosingTime, new { Class = "ShipmentInput" })%><label for="ClosingTime" class="DateInput"></label>
            </td>
        </tr>
    </table>
    <div style="padding:20px 0;">
    <table width="100%">
        <tr>
            <td style="text-align: center; font-weight: bold; font-size: 12px">
                SHIPPER
            </td>
            <td style="text-align: center; font-weight: bold; font-size: 12px">
                SANCO FREIGHT LTD
            </td>
        </tr>
        <tr>
            <td style="text-align: center; font-weight: bold; font-size: 12px">
            <div style="padding-left:150px;">
                <%: Html.TextBoxFor(m => m.Shipper, new { Class = "ShipmentInput" })%>
                </div>
            </td>
            <td style="text-align: center; font-weight: bold; font-size: 12px">
            <div style="padding-left:150px;">
                <%: Html.TextBoxFor(m => m.SancoFreight, new { Class = "ShipmentInput" })%>
                </div>
            </td>
        </tr>
    </table>
    </div>
     <div class="ButtonZone">
         <div class="DocLinkButton">
        <%: Html.ActionLink("Shipment", "Edit", "Shipment", new { id = Model.ShipmentId }, new { Class = "LinkForm" })%>
        </div>
        <div class="DocLinkButton">
        <%: Html.ActionLink("Revenue", "Revenue", "Shipment", new { id = Model.ShipmentId }, new { Class = "LinkForm" })%>
        </div>
        <div class="DocLinkButton">
        <a href="#" onclick="jQuery('#submitButton').click();" class="LinkForm" title="Update Document">Update</a>
        <input id="submitButton" type="submit" value="Update" title="Update Document" style="display:none"/>
        </div>
        <div class="DocLinkButton">
        <%: Html.ActionLink("Print", "PrintBookingNote", "Shipment", new { ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
    </div>
    <%} %>
    <script language="javascript" type="text/javascript">
        function submitForm() {
            var form = jQuery("#BillDetailAction").parents("form:first");
            form.submit();
        }
        jQuery(document).ready(function () {
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();

            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#BookingNote").addClass("LinkActive");

            new DateTimePicker('ETD', 'dd/MM/yyyy');
            new DateTimePicker('Date', 'dd/MM/yyyy');
            new DateTimePicker('ClosingTime', 'dd MMM yyyy HH:mm:ss');
            
        });
    </script>
</asp:Content>
