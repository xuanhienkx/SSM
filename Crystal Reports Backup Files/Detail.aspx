<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.UsersModel>" %>

<%@ Import Namespace="SSM.Models" %>

<asp:Content ID="Content3" ContentPlaceHolderID="TitleContent" runat="server">
    EFS SSM System - My profile
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm())
       { %>
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                        <fieldset>
                            <legend class="Subject AddUserLegend">My Profile</legend>
                            <% User User1 = (User)Session[SSM.Controllers.AccountController.USER_SESSION_ID];%>
                            <%: Html.ValidationSummary(true, "Add User was unsuccessful. Please correct the errors and try again.") %>
                            <div class="SSMRowFormItem">
                                        <%: Html.LabelFor(m => m.FullName) %>
                                        <%: Html.TextBoxFor(m => m.FullName)%>
                                        <%: Html.ValidationMessageFor(m => m.FullName)%>
                                </div>
                                <div class="SSMRowFormItem">
                                        <%: Html.LabelFor(m => m.UserName) %>
                                        <%: Html.TextBoxFor(m => m.UserName, new { ReadOnly=true })%>
                                        <%: Html.ValidationMessageFor(m => m.UserName) %>
                                </div>
                                <div class="SSMRowFormItem">
                                <%if (UsersModel.isSetPassword(User1))
                                  {%>
                                       <%: Html.LabelFor(m => m.Password)%>
                                       <%: Html.PasswordFor(m => m.Password, new { value = Model.Password})%>
                                       <%: Html.ValidationMessageFor(m => m.Password)%>
                                        <%}
                                  else
                                  {%>
                                        <%: Html.LabelFor(m => m.Password)%>
                                        <%: Html.PasswordFor(m => m.Password, new { value = Model.Password, ReadOnly = true })%>
                                        <% }%>
                                </div>
                                <div class="SSMRowFormItem">
                                        <%: Html.LabelFor(m => m.RoleName) %>
                                        <%= Html.DropDownList("RoleName", (SelectList)ViewData["Positions"], new { disabled = "disabled" })%>
                                </div>
                                
                             <div class="SSMRowFormItem">
                                        <%: Html.LabelFor(m => m.DeptId) %>
                                        <%= Html.DropDownList("DeptId", (SelectList)ViewData["Departments"], new { disabled = "disabled" })%>
                                </div>
                                <div class="SSMRowFormItem" id="showLevelZone">
                                        <%: Html.LabelFor(m => m.Level) %>
                                        <%= Html.DropDownList("Level", (SelectList)ViewData["Levels"], new { disabled = "disabled" })%>
                                </div>
                                <div class="SSMRowFormItem">
                                        <%: Html.LabelFor(m => m.ComId) %>
                                        <%= Html.DropDownList("ComId", (SelectList)ViewData["Companies"], new { disabled = "disabled" })%>
                                </div>
                                <div class="SSMRowFormItem">
                                        <%: Html.LabelFor(m => m.Note) %>
                                        <%: Html.TextAreaFor(m => m.Note, new { disabled = "disabled" })%>
                                </div>
                                <div class="SSMRowFormItem">
                                       <label></label>
                                       <input style="width:100px;color:White" type="submit" value="Create" />
                                </div>
                        </fieldset>
                </div>
            </div>
        </div>
    </div>
    <div id="Div2">
        <div class="SectionBlock Expanded BoxL1">
            <div class="BoxL2">
                <div class="BoxL3">
                    <div class="BoxL4">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width:400px">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td colspan="2" class="SettingUserTd" style="color:#6D6D65;padding:15px 36px 15px 0;">
                                                Setting for User
                                            </td>
                                         </tr>
                                        <tr>
                                            <td class="SettingUserTd">
                                                <%: Html.LabelFor(m => m.SetPass) %>
                                            </td>
                                            <td>
                                                <%: Html.CheckBoxFor(m => m.SetPass, new { disabled = "disabled" })%>
                                            </td>
                                         </tr>
                                         <tr>   
                                            <td class="SettingUserTd">
                                                <%: Html.LabelFor(m => m.AllowUpdateSeaRate)%>
                                            </td>
                                            <td>
                                                <%: Html.CheckBoxFor(m => m.AllowUpdateSeaRate, new { disabled = "disabled" })%>
                                            </td>
                                         </tr>
                                         <tr>   
                                            <td class="SettingUserTd">
                                                <%: Html.LabelFor(m => m.AllowUpdateAirRate)%>
                                            </td>
                                            <td>
                                                <%: Html.CheckBoxFor(m => m.AllowUpdateAirRate, new { disabled = "disabled" })%>
                                            </td>
                                        </tr>
                                    </table>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td colspan="2" class="SettingUserTd" style="color:#6D6D65;padding:15px 36px 15px 0;">
                                                        Setting for additional function
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SettingUserTd">
                                                        <%: Html.LabelFor(m => m.AllowUpdateAgent)%>
                                                    </td>
                                                    <td>
                                                        <%: Html.CheckBoxFor(m => m.AllowUpdateAgent, new { disabled = "disabled" })%>
                                                    </td>
                                                </tr>
                                                <tr>   
                                                    <td class="SettingUserTd">
                                                        <%: Html.LabelFor(m => m.AllowUpdatePartner)%>
                                                    </td>
                                                    <td>
                                                        <%: Html.CheckBoxFor(m => m.AllowUpdatePartner, new { disabled = "disabled" })%>
                                                    </td>
                                                </tr>
                                                <tr>   
                                                    <td class="SettingUserTd">
                                                        <%: Html.LabelFor(m => m.AllowSeaPort)%>
                                                    </td>
                                                    <td>
                                                        <%: Html.CheckBoxFor(m => m.AllowSeaPort, new { disabled = "disabled" })%>
                                                    </td>
                                                </tr>
                                                <tr>   
                                                    <td class="SettingUserTd">
                                                        <%: Html.LabelFor(m => m.AllowAirPort)%>
                                                    </td>
                                                    <td>
                                                        <%: Html.CheckBoxFor(m => m.AllowAirPort, new { disabled = "disabled" })%>
                                                    </td>
                                                </tr>
                                                <tr>   
                                                    <td class="SettingUserTd">
                                                        <%: Html.LabelFor(m => m.AllowCustomer)%>
                                                    </td>
                                                    <td>
                                                        <%: Html.CheckBoxFor(m => m.AllowCustomer, new { disabled = "disabled" })%>
                                                    </td>
                                                </tr>
                                            </table>
                                </td>
                                <td style="vertical-align:top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            
                                            <td colspan="2" class="SettingUserTdRight" style="color:#6D6D65;padding:15px 36px 15px 0;">
                                            Allow follow up works
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td  style="width:40px">
                                            <%: Html.CheckBoxFor(m => m.AllowTrackSeaIn, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrackSeaIn)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrackSeaOut, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                             <%: Html.LabelFor(m => m.AllowTrackSeaOut)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrackAirIn, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrackAirIn)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrackAirOut, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrackAirOut)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrackInlandSer, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrackInlandSer)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrackProjectSer, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrackProjectSer)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrackOtherSer, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrackOtherSer)%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                          <td style="vertical-align: top">
                                    <table width="100%"  style="vertical-align: text-top; border: none; ">
                                         <tr>
                                            
                                            <td colspan="2" class="SettingUserTdRight" style="color:#6D6D65;padding-bottom:15px;padding-top:15px;">
                                            TRADING
                                            </td>
                                        </tr>
                                         <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowTrading)%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowTrading)%>
                                            </td>
                                        </tr>
                                        <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowApprovedStockCard)%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                            <%: Html.LabelFor(m => m.AllowApprovedStockCard)%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                  </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="DirectorRight" style="height: auto">
    <div class="SectionBlock Expanded BoxL1" id="showOfficeControlZone">
            <div class="BoxL2">
                <div class="BoxL3">
                    <div class="BoxL4">
                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td style="width:400px;vertical-align:top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                                    <td colspan="2" class="SettingUserTd" style="color:#6D6D65;padding:15px 36px 15px 0;">
                                                        Allow control office
                                                    </td>
                                                </tr>
                                        <tr>
                                            <td class="SettingUserTd"><label>Sellec All</label></td>
                                            <td><input type="checkbox" id="Checkbox2" value="0" name="CompanyControls" disabled="disabled"/></td>
                                        </tr>
                                        <%
                                            IEnumerable<Company> AllCompanies = (IEnumerable<Company>)ViewData["AllCompanies"];
                                            foreach (Company CompanyItem in AllCompanies)
                                            {
                                         %>
                                        <tr>
                                            <td class="SettingUserTd"><label><%= CompanyItem.CompanyName %></label></td>
                                            <td><input type="checkbox" checked="checked" id="Checkbox3" value="<%= CompanyItem.Id %>" name="CompanyControls" disabled="disabled"/></td>
                                        </tr>
                                        <%} %>
                                    </table>
                                </td>
                                <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr>
                                                   
                                                    <td colspan="2" class="SettingUserTdRight" style="color:#6D6D65;padding:15px 36px 15px 0;">
                                                       Setting for Director
                                                    </td>
                                                </tr>
                                            <tr>
                                            
                                            <td>
                                            <%: Html.CheckBoxFor(m => m.AllowApproRevenue, new { disabled = "disabled" })%>
                                            </td>
                                            <td class="SettingUserTdRight">
                                             <%: Html.LabelFor(m => m.AllowApproRevenue)%>
                                            </td>
                                        </tr>
                                        <tr>
                                           
                                            <td><%: Html.CheckBoxFor(m => m.AllowQuotaOffice, new { disabled = "disabled" })%></td>
                                             <td class="SettingUserTdRight"><%: Html.LabelFor(m => m.AllowQuotaOffice)%></td>
                                        </tr>
                                        <tr>
                                            <td><%: Html.CheckBoxFor(m => m.AllowQuotaDept, new { disabled = "disabled" })%></td>
                                            <td class="SettingUserTdRight"><%: Html.LabelFor(m => m.AllowQuotaDept)%></td>
                                            
                                        </tr>
                                         <tr>
                                           
                                            <td><%: Html.CheckBoxFor(m => m.AllowSetSales, new { disabled = "disabled" })%></td>
                                             <td class="SettingUserTdRight"><%: Html.LabelFor(m => m.AllowSetSales)%></td>
                                        </tr>
                                        <tr>
                                            
                                            <td><%: Html.CheckBoxFor(m => m.AllowSettingSystem, new { disabled = "disabled" })%></td>
                                            <td class="SettingUserTdRight"><%: Html.LabelFor(m => m.AllowSettingSystem)%></td>
                                        </tr>
                                      
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#StaffManagementTab").addClass("Active");
            jQuery('#StaffManagementTab').activeThisNav();
            if (jQuery('#RoleName').val() == 'Director') {
                jQuery('#showLevelZone').show();
                jQuery('#showOfficeControlZone').show();
            }
            else {
                jQuery('#showLevelZone').hide();
                jQuery('#showOfficeControlZone').hide();
            }
        });
    </script>
</asp:Content>
