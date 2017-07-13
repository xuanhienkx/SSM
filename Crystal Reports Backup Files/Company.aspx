<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.CompanyModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EFS SSM System - Company
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="SectionBlock Expanded BoxL1"
            <div class="BoxL2">
                <div class="BoxL3">
                    <div class="BoxL4">
                    <div class="DivHeader"><h2> List Companies</h2></div>
                    <% IEnumerable<Company> CompanyList1 = (IEnumerable<Company>)ViewData["CompanyList1"];
                       if (CompanyList1 != null && CompanyList1.Count() > 0)
                       { %>
    <table width="100%" class="grid">
        <tr><th>Name</th><th>Phone Number</th><th>Description</th><th>Edit</th><th>Delete</th></tr>
        <%
int no = 0;
foreach (Company Company1 in CompanyList1)
{
    no++;
    bool HightLine = no % 2 == 0;
           %>
                <tr <%if(HightLine) {%>class="GridLight" <% }%>>
                      <td><%= Company1.CompanyName%></td>
                      <td><%= Company1.PhoneNumber%></td>
                      <td><%= Company1.Description%></td>
                      <td><a href="<%= Url.Action("Company","Users",new { id=Company1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a></td>
                      <td><a href="<%= Url.Action("CompanyDelete","Users",new { id=Company1.Id}) %>"><img alt="Delete a company" src="../../Images/btn-delete.png"/></a></td>
                      </tr>
        <%}%>
    </table>
    <%}
                       else
                       { %>Data is not found!!!
    <%} %>
    <div style="height:auto;overflow: hidden;">
                     <input type="button" id="CreateCom" value="Create a company" />
                            <input type="hidden" name="ModifyCom" value="<%= ViewData["ModifyCom"] %>" />
                            </div>
    </div>
    </div>
    </div>
    </div>
    <div class="SectionBlock Expanded BoxL1" id="ModifyCompanyZone">
            <div class="BoxL2">
                <div class="BoxL3">
                    <div class="BoxL4">
                    <div class="DivHeader"><h2> Modify Company</h2></div>
    <div style="width:100%" id="ModifyForm">
    <% using (Html.BeginForm())
       { %>
        <table>
            <%: Html.HiddenFor(m => m.Id)%>
            <tr>
                <td class="TDClass">
                    <label>
                        Name</label>
                </td>
                <td class="TDClass">
                    <%: Html.TextBoxFor(m => m.CompanyName, new { maxlength = "200"})%>
                    <%: Html.ValidationMessageFor(m => m.CompanyName)%>
                </td>
            </tr>
            <tr>
                <td class="TDClass">
                    <label>
                        Address</label>
                </td>
                <td class="TDClass">
                    <%: Html.TextBoxFor(m => m.Address, new { maxlength = "500" })%>
                </td>
            </tr>
            <tr>
                <td class="TDClass">
                    <label>
                        PhoneNumber</label>
                </td>
                <td class="TDClass">
                    <%: Html.TextBoxFor(m => m.PhoneNumber, new { maxlength = "20", Class = "PhoneNumber" })%><br />
                    <span>Please input include numeric and space key(' ') or '-'</span>
                </td>
            </tr>
            <tr>
                <td class="TDClass">
                    <label>
                        Description</label>
                </td>
                <td class="TDClass">
                    <%: Html.TextAreaFor(m => m.Description)%>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <input type="submit" value="Create" />
                     <input type="button" value="Cancel" onclick="javascript:jQuery('#ModifyCompanyZone').hide();" style="width:75px; background-color:#ED1B2E;"/>
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
            jQuery("#StaffManagementTab").addClass("Active");
            jQuery('#StaffManagementTab').activeThisNav();
            jQuery('#ModifyCompanyZone').hide();
            if (jQuery('input[name="ModifyCom"]').val() == 'ModifyCom') {
                jQuery('#ModifyCompanyZone').show();
            }
            jQuery('#CreateCom').click(function () {
                jQuery('#ModifyCompanyZone').show();
                jQuery('#Id').val('-1');
                jQuery('#CompanyName').val('');
                jQuery('#Address').val('');
                jQuery('#PhoneNumber').val('');
                jQuery('#Description').val('');
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
