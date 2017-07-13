<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ShipperModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Shipper
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <div class="DivHeader">
                        <h2>
                            List Shipper</h2>
                    </div>
                    <table width="100%" class="grid">
                        <tr>
                            <th>
                                Delete
                            </th>
                            <th>
                                Edit
                            </th>
                            <th>
                                ShipperName
                            </th>
                            <th>
                                Address
                            </th> <th>
                                Phone Number
                            </th> <th>
                                Email
                            </th>
                             <th>
                                Description
                            </th>
                        </tr>
                        <% IEnumerable<Shipper> ShipperList1 = (IEnumerable<Shipper>)ViewData["ShipperList1"];
                           if (ShipperList1 != null && ShipperList1.Count() > 0)
                           {
                               int no = 0;
                               foreach (Shipper Shipper1 in ShipperList1)
                               {
                                   no++;
                                   bool hightline = no % 2 == 0;
                        %>
                        <tr <%if(hightline) {%>class="GridLight" <% }%> >
                            <td>
                                <a href="<%= Url.Action("Shipper","Shipment",new { id=Shipper1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a>
                            </td>
                            <td>
                                <a href="<%= Url.Action("ShipperDelete","Shipment",new { id=Shipper1.Id}) %>"><img alt="Edit" src="../../Images/btn-delete.png"/></a>
                            </td>
                            <td>
                                <%= Shipper1.ShipperName%>
                            </td>
                            <td>
                                <%= Shipper1.Address%>
                            </td>
                            <td>
                                <%= Shipper1.PhoneNumber%>
                            </td>
                            <td>
                                <%= Shipper1.Email%>
                            </td>
                            <td>
                                <%= Shipper1.Description%>
                            </td>
                        </tr>
                        <%}
           }%>
                    </table>
                    <div style="height:auto;overflow: hidden;">
                     <input type="button" id="CreateDept" value="Create a Shipper" />
                            <input type="hidden" name="ModifyDept" value="<%= ViewData["ModifyDept"] %>" />
                            </div>
                </div>
            </div>
        </div>
    </div>
    <div class="SectionBlock Expanded BoxL1" id="ModifyDeptZone">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <div class="DivHeader">
                        <h2>
                            Modify Department</h2>
                    </div>
                    <div style="width: 100%" id="ModifyForm">
                        <% using (Html.BeginForm())
                           { %>
                        <table>
                            <tr>
                               <td class="TDClass"><label>Shipper Name</label></td>
                                <td class="TDClass">
                                <%: Html.HiddenFor(m => m.Id)%>
                                    <%: Html.TextBoxFor(m => m.ShipperName, new { maxlength = "200" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClass"><label>
                                    Address</label>
                                </td>
                                <td class="TDClass">
                                    <%: Html.TextBoxFor(m => m.Address, new { maxlength = "200"})%>
                                </td>
                            </tr>
                            <tr>
                               <td class="TDClass"><label>Phone Number</label></td>
                                <td class="TDClass">
                                    <%: Html.TextBoxFor(m => m.PhoneNumber, new { maxlength = "50", Class = "PhoneNumber" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClass"><label>
                                    Email</label>
                                </td>
                                <td class="TDClass">
                                    <%: Html.TextBoxFor(m => m.Email, new { maxlength = "100" })%>
                                </td>
                            </tr>
                            <tr>
                                <td class="TDClass"><label>
                                    Description</label>
                                </td>
                                <td class="TDClass">
                                    <%: Html.TextAreaFor(m => m.Description)%>
                                </td>
                            </tr>
                            <tr>
                            <td></td>
                                <td>
                                    <input type="submit" value="Create" />
                                    <input type="button" value="Cancel" onclick="javascript:jQuery('#ModifyDeptZone').hide();" style="width:75px; background-color:#ED1B2E;"/>
                                </td>
                            </tr>
                        </table>
                        <%} %>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#databaseTab").addClass("Active");
            jQuery('#databaseTab').activeThisNav();
            jQuery('#ModifyDeptZone').hide();
            if (jQuery('input[name="ModifyDept"]').val() == 'ModifyDept') {
                jQuery('#ModifyDeptZone').show();
            }
            jQuery('#CreateDept').click(function () {
                jQuery('#ModifyDeptZone').show();
                jQuery('#ShipperName').val('');
                jQuery('#Address').val('');
                jQuery('#PhoneNumber').val('');
                jQuery('#Email').val('');
                jQuery('#Description').val('');
                jQuery('#Id').val('0');
            });
            jQuery(".PhoneNumber").each(function (index) {
                jQuery(this).blur(function () {
                    jQuery(this).val(jQuery.trim(jQuery(this).val()));
                    var $phone = jQuery(this).val();
                    if (!validatePhoneNumber($phone)) {
                        jQuery(this).val('');
                    }
                });
            });
        });
    </script>

</asp:Content>
