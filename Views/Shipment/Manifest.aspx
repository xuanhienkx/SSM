<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ManifestModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Manifest
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
       RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
   <div class="ManifestContainer">
        <div class="CompanyName">SANCO FREIGHT</div>
        <div class="Address">23B Ton Duc Thang St, Ben Nghe Ward, Dist. 01 </div>
        <div class="Address">Hochiminh City, Viet Nam</div>
        <div class="Address">Tel: 84.8.9104532 / 9104534 / 9104535 / 9104537 / 9104538     –  Fax: 84.8.9104533</div>
        <div class="ManifestName">CARGO  MANIFEST</div>
        
       <table style="width:100%">
           <tr>
               <td><label>MAWB No.:</label>
               </td>
               <td><%: Html.TextBoxFor(m => m.MAWBNo, new { Class = "Input" })%>
               </td>
               <td><label> FLT No.:</label>
               </td>
               <td><%: Html.TextBoxFor(m => m.FLTNo, new { Class = "Input" })%>
               </td>
           </tr>
           <tr>
               <td><label>CONSIGNED TO:</label>
               </td>
               <td><%: Html.TextBoxFor(m => m.CONSIGNEDTO, new { Class = "Input" })%>
               </td>
               <td><label> DATE:</label>
               </td>
               <td><%: Html.TextBoxFor(m => m.DATE, new { Class = "Input" })%>
               </td>
           </tr>
           <tr>
               <td><label>DEPARTURE :</label>
               </td>
               <td><%: Html.TextBoxFor(m => m.DEPARTURE, new { Class = "Input" })%>
               </td>
               <td><label>DESTINATION :</label>
               </td>
               <td><%: Html.TextBoxFor(m => m.ESTINATION, new { Class = "Input" })%>
               </td>
           </tr>
       </table>
       <div class="MainContent">
       <table  border="1px solid Silver" style="width:100%">
           <tr>
               <th>
                   ORD No.
               </th>
               <th>
                   HAWB No.
               </th>
               <th>
                   No. of Pcs
               </th>
               <th>
                   G.WEIGHT (KGS)
               </th>
               <th>
                   DESCRIPTION OF GOODS
               </th>
               <th>
                   SHIPPER
               </th>
               <th>
                   CONSIGNEE
               </th>
               <th>
                   CHARG CODE
               </th>
           </tr>
           <tr>
               <td>
                   <%: Html.TextBoxFor(m => m.ORDNo, new { Class = "InputShort" })%>
               </td>
               <td>
                   <%: Html.TextBoxFor(m => m.HAWBNo, new { Class = "InputShort" })%>
               </td>
               <td>
                   <%: Html.TextBoxFor(m => m.NoOfPcs, new { Class = "InputShort" })%>
               </td>
               <td>
                   <%: Html.TextBoxFor(m => m.GWEIGHT, new { Class = "InputShort" })%>
               </td>
               <td>
                   <%: Html.TextAreaFor(m => m.DESCRIPTIONOFGOODS, new { Class = "TextArea" })%>
               </td>
               <td>
                   <%: Html.TextAreaFor(m => m.SHIPPER, new { Class = "TextArea" })%>
               </td>
               <td>
                   <%: Html.TextAreaFor(m => m.CONSIGNEE, new { Class = "TextArea" })%>
               </td>
               <td>
                   <%: Html.TextBoxFor(m => m.CHARGECODE, new { Class = "InputShort" })%>
                   <%: Html.HiddenFor(m => m.Id)%>
               </td>
           </tr>
       </table>
       </div>
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
        <%: Html.ActionLink("Print", "PrintManifest", "Shipment", new { ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
    <%}
         %>
         <script language="javascript" type="text/javascript">
             jQuery(document).ready(function () {
                 jQuery("#FileTab").addClass("Active");
                 jQuery('#FileTab').activeThisNav();
                 new DateTimePicker('DebitDate', 'dd/MM/yyyy');
                 jQuery("#DocumentMenuContainer .LinkClose").hide();
                 jQuery("#Manifest").addClass("LinkActive");
             });
    </script>
</asp:Content>
