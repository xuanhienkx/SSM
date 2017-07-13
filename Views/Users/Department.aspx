<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.DepartmentModel>" %>

<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    EFS SSM System - Department Management
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <div class="DivHeader">
                        <h2>
                            List Department</h2>
                     </div>
                     <%IEnumerable<Department> DepartmentList1 = (IEnumerable<Department>)ViewData["DepartmentList1"];
                       if (DepartmentList1 != null && DepartmentList1.Count() > 0)
                       { %>
                    <table width="100%" class="grid">
                        <tr>
                            <th>
                                Name
                            </th>
                             <th>
                                Function
                            </th>
                            <th>
                                Description
                            </th>
                            <th>
                                Edit
                            </th>
                            <th>
                                Delete
                            </th>
                            <th>List Active</th>
                        </tr>
                        <% 
int no = 0;
foreach (Department Department1 in DepartmentList1)
{
    no++;
    bool HightLine = no % 2 == 0;
                        %>
                        <tr <% if(HightLine) {%> class="GridLight" <% }%> >
                            <td>
                                <%= Department1.DeptName%>
                            </td>
                            <td>
                                <%= Department1.DeptFunction%>
                            </td>
                            <td>
                                <%= Department1.Description%>
                            </td>
                            <td>
                                <a href="<%= Url.Action("Department","Users",new { id=Department1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a>
                            </td>
                            <td>
                                <a href="<%= Url.Action("DepartmentDelete","Users",new { id=Department1.Id}) %>"><img alt="Edit" src="../../Images/btn-delete.png"/></a>
                            </td>
                            <td>
                                <%=Html.CheckBox("IsActive", Department1.IsActive, new { @class = "serviceActive", @id = "IsActive_" + Department1.Id }) %>
                            </td>
                        </tr>
                        <%}
           %>
                    </table>
                    <%}
                       else
                       { %>
                       Data is not found!!!
                    <%} %>
                    <div style="height:auto;overflow: hidden;">
                     <input type="button" id="CreateDept" value="Create Department" />
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
                        <div>
                              <%: Html.ValidationSummary()%>
                        </div>
                        <table>
                            <tr>
                               <td class="TDClass"><label>Name</label></td>
                                <td class="TDClass">
                                    <%: Html.HiddenFor(m => m.Id)%>
                                    <%: Html.HiddenFor(m => m.IsActive)%>
                                    <%: Html.TextBoxFor(m => m.DeptName, new { maxlength="100"})%>
                                    <%: Html.ValidationMessageFor(m => m.DeptName)%>
                                </td>
                            </tr>
                             <tr>
                               <td class="TDClass"><label>Function</label></td>
                                <td class="TDClass">
                                    <%: Html.DropDownListFor(m => m.DeptFunction, (SelectList)ViewData["Functions"])%>
                                </td>
                            </tr>
                            <tr>
                               <td class="TDClass"><label>Company</label></td>
                                <td class="TDClass">
                                    <%: Html.DropDownListFor(m => m.ComId, (SelectList)ViewData["Companies"])%>
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
            jQuery("#StaffManagementTab").addClass("Active");
            jQuery('#StaffManagementTab').activeThisNav();
            jQuery('#ModifyDeptZone').hide();
            if (jQuery('input[name="ModifyDept"]').val() == 'ModifyDept') {
                jQuery('#ModifyDeptZone').show();
            }
            jQuery('#CreateDept').click(function () {
                jQuery('#ModifyDeptZone').show();
                jQuery('#Id').val('-1');
                jQuery('#DeptName').val('');
                jQuery('#DeptFunction').val('');
                jQuery('#Description').val('');
            });
            jQuery(".serviceActive").on("click", function () {
                var id = jQuery(this).attr("id").split("_")[1];
                var check = jQuery(this).is(":checked");
                var url = '<%=Url.Action("SetDepartActive", "Users")%>';
                var data = JSON.stringify({ "id": parseInt(id), "isActive": check });
                jQuery.mbqAjax({
                    url: url,
                    type: 'post',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: data,
                    success: function (result) {
                    }
                });
            });
        });
    </script>
</asp:Content>
