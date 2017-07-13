<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BillLandingModel>" %>
<%@ Import Namespace="SSM" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	BLDetail
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="NormalLabel">
<% using (Html.BeginForm())
      { %>
      <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
   <div style="width:400px; text-align:right; padding:25px 50px 15px;float:left"><label style="font-family:Arial Baltic;font-size:20px;font-weight:bold;">SHIPPING INSTRUCTION</label></div>
   <table width="100%">
   <tr>
   <td>
   <div style="height: auto; overflow: hidden; font-weight: bolder;padding: 15px;">
                   <div style="height: auto; overflow: hidden">
                       <div style="height: auto; overflow: hidden; float: left;">TO :    </div>
                       <%: Html.TextBoxFor(m => m.BillTo, new { Class = "ShipmentInput" })%>
                   </div>
    </div>
    </td>
   <td>
   <div style="height: auto; overflow: hidden; font-weight: bolder">
                   <div style="height: auto; overflow: hidden">
                       <div style="height: auto; overflow: hidden; float: left;">BKG # </div>
                       <%: Html.TextBoxFor(m => m.BKG, new { Class = "ShipmentInput" })%>
                    </div>
   </div>
   </td>   
   </tr>
   <tr>
   <td></td>
   <td>
   <div style="width:150px;float:left">
   <label style="font-family:Arial Baltic;font-size:14px;">Ref No:   #</label>
   <label style="font-family:Arial Baltic;font-size:14px;font-weight:bold;"> <%= Model.ShipmentId %></label>
   </div>
   </td>
   </tr>
    </table>
    <table class="BillDetailTable" cellpadding="3" width="750px">
       <tr>
            <td colspan="2">
            <label class="Label">Shipper:</label>
            </td>
       </tr>
       <tr>
           <td style="font-weight: bolder;" width="450px"  colspan="2">
           <%: Html.TextAreaFor(m => m.BLComAddress, new { Class = "BillTextArea" })%>
           </td>
           <td width="300px">           
           </td>
       </tr>
       <tr>
            <td colspan="2">
            <label class="Label">Consignee:</label>
            </td>
       </tr>
       <tr>
           <td colspan="2">
           <%= StringUtils.HtmlFilter(Model.ForRelease)%>
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td colspan="2">
            <label class="Label">Notify:</label>
            </td>           
       </tr>
       <tr>
           <td>
           <label class="Label">SAME AS CONSIGNEE</label>
           </td>
           <td>
           </td>
       </tr>
       <tr>
            <td>
            <label class="Label">Vessel</label>
            </td>
            <td>
            <label class="Label">Voy no.</label>
            </td>
       </tr>
       <tr>
           <td><%= Model.OceanVessel %>
           </td>
            <td>
                <%= Model.VoyNo %>
           </td>
           <td>
           </td>
       </tr>
       <tr>
            <td>
            <label class="Label">Port of loading</label>
            </td>
            <td>
            <label class="Label">Port Discharge</label>
            </td>
            <td>
            <label class="Label">Final Destination</label>
            </td>
       </tr>
       <tr>
           <td>
           <%= Model.PortLoading %>
           </td>
           <td>
           <%= Model.PortDischarge %>
           </td>
            <td>
           <%= Model.PlaceDelivery %>
           </td>
       </tr>
       <tr>
          <td colspan="3">
                <table class="BillDetailTable" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <td>
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
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.MarksNos)%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.QuanlityDescription)%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= Model.GrossWeight%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= Model.Measurement%></div>
                        </td>
                </table>
            </td>
       </tr>
       <tr>
           <td>
           <table width="100%">
                <tr>
                    <td colspan="2" width="300px">
                        <div style="overflow:hidden; width:450px"> <%: Html.TextBoxFor(m => m.BillFrom, new { Class = "ShipmentInput" })%></div>
                    </td>
                </tr>
           </table>
           </td>
       </tr>
       <tr>
           <td colspan="2"><label class="Label">PLEASE EMAIL TO :  export-docs@sancofreight.com.vn</label>
           </td>
           <td>
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
        <%: Html.ActionLink("Print", "PrintBLDetail", "Shipment", new { ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
    </div>
   <%} %>
</div>
    <script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#FileTab").addClass("Active");
        jQuery('#FileTab').activeThisNav();
        jQuery("#DocumentMenuContainer .LinkClose").hide();
        jQuery("#BLDetail").addClass("LinkActive");
    });
   </script>
</asp:Content>
