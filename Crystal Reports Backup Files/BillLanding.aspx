<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BillLandingModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Bill Lading
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

  
   <% using (Html.BeginForm())
      { %>
      <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
     <div style="width:720px; text-align:right; padding:15px 50px 15px;"><label style="font-family:Arial Baltic;font-size:20px;font-weight:bold;">BILL OF LADING</label></div>
    <table class="BookingTable" border="1px solid Silver" cellpadding="3" width="835px">
        <tr>
            <td style="width:420px">
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Shipper</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.PhipperName, new { Class = "BillTextArea" })%></div>
            </td>
            <td rowspan="2" valign="top">
            <div style="height:auto;overflow:hidden">
            <label class="BillLabel">Bill of Lading No.</label><%: Html.TextBoxFor(m => m.BillLandingNo, new { Class = "ShipmentInput" })%>
            </div>
            <div style="width:100%;overflow:hidden;height:auto;border-bottom:1px solid #BFBFBF;">
            <br />
            </div>
                <div style="min-height: 150px; overflow: hidden">
                    <div style="height: auto; overflow: hidden;padding:0 10px;text-align:center;font-size:20px;font-weight:bold;">
                        <%: Html.CheckBoxFor(m=>m.Logo) %><span>Logo</span>
                        <%: Html.CheckBoxFor(m=>m.Footer) %><span>Footer</span>
                    </div>
                    <div style="height: auto; overflow: hidden;padding:100px 0 10px;text-align:center;font-size:20px;font-weight:bold;">
                          <%: Html.TextAreaFor(m => m.Original, new { Class = "BillTextArea" })%>
                    </div>
                    <div style="height: auto; overflow: hidden">
                        <span class="Label" style="padding-right:30px">
                            Ref. No.      </span><label crlass="Label"># <%= Model.ShipmentId %></label></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Consignee</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.ConsignedOrder, new { Class = "BillTextArea" })%></div>
            </td>
        </tr>
        <tr>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Notify Party</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.NotifyAddress, new { Class = "BillTextArea" })%></div>
            </td>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">For release of goods apply to</label></div>
            <div style="height:auto;overflow:hidden"> <%: Html.TextAreaFor(m => m.ForRelease, new { Class = "BillTextArea" })%></div>
            </td>
        </tr>
        <tr>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Place of receipt</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.PlaceOfReceipt, new { Class = "BillInput" })%></div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Ocean vessel</label></div>
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.OceanVessel, new { Class = "ShipmentInput" })%></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Voy no.</label></div>
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.VoyNo, new { Class = "ShipmentInput" })%></div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Port of loading</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.PortLoading, new { Class = "BillInput" })%></div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Port of discharge</label></div>
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.PortDischarge, new { Class = "ShipmentInput" })%></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Place of delivery</label></div>
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.PlaceDelivery, new { Class = "ShipmentInput" })%></div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Final destination     </label><label class="NormalLabel">  ( For the merchant's reference onlly)</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.FinalDestination, new { Class = "ShipmentInput" })%></div>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <table class="TableNormal" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <td valign=top>
                            <div style="height: auto; overflow: hidden">
                                <label class="BillLabel">
                                    Marks and Nos</label></div>
                        </td>
                        <td>
                            <div style="height: auto; overflow: hidden">
                                <label class="BillLabel">
                                    Quantity and description of goods</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Gross weight (Kgs)</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Measurement (M3)</label></div>
                        </td>
                    </tr>
                    <tr>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.MarksNos, new { Class = "Bill1TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.QuanlityDescription, new { Class = "Bill2TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.GrossWeight, new { Class = "Bill3TextArea" })%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.Measurement, new { Class = "Bill3TextArea" })%></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Freight and charges</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.FreightCharge, new { Class = "BillTextArea" })%></div>
            </td>
            <td rowspan="2">
            <div style="height:auto;overflow:hidden">
            RECEIVED the goods in apparent good order and condition and, as far as ascertained by reasonable means of checking, as specified above unless otherwise stated.</div>
            <div style="height:auto;overflow:hidden">The undesigned, in accordance with the provisions contained in this document</div>
            <div style="height:auto;overflow:hidden;padding-left:30px;">* Undertakes to perform or to procure the performance of the entire transport from the place at which the goods are taken in charge to the place designated for delivery in this document, and</div>
            <div style="height:auto;overflow:hidden;padding-left:30px;">* Assumes liability as prescribeb in this document for such transport. One of the B/Ls must be surrendered duly endorsed in exchange for the goods or delivery order.</div>
            <div style="height:auto;overflow:hidden">IN WITNESS whereof THERE (3) original B/Ls have been signed, if not otherwise stated above, one of which being acconplished the other(s) to be void</div>
            </td>
        </tr>
        <tr>
            <td>
            <div style="height:auto;overflow:hidden"><label class="BillLabel">Place and date of issue</label></div>
            <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.PlaceDateIssue, new { Class = "BillInputLong" })%></div>
            </td>
        </tr>
        <tr>
            <td>
             <table width="100%">
                    <tr>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Freight payable at</label></div>
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.FreightPayable, new { Class = "ShipmentInput" })%></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Number of original B/Ls</label></div>
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.NumberOriginal, new { Class = "ShipmentInput" })%></div>
                        </td>
                    </tr>
                </table>
            </td>
             <td valign="top">
              <div style="height:auto;overflow:hidden"><label class="BillLabel">Stamp and authorized signature</label></div>
              <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.StampAuthorized, new { Class = "BillInputLong" })%></div>
             </td>
        </tr>
    </table>
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
        <%: Html.ActionLink("Print", "PrintReportBillLanding", "Shipment", new {id = Model.ShipmentId}, new { Class = "LinkForm", target = "_blank" })%>
        </div>
    </div>
   <%} %>
   <script language="javascript" type="text/javascript">
       jQuery(document).ready(function () {
           jQuery("#FileTab").addClass("Active");
           jQuery('#FileTab').activeThisNav();
           jQuery("#DocumentMenuContainer .LinkClose").hide();
           jQuery("#BillLangding").addClass("LinkActive");
       });
   </script>
</asp:Content>
