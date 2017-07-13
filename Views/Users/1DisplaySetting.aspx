<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.SettingModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EFS SSM System - Display Settting
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                <% using (Html.BeginForm("SaleTypesSettting", "Users"))
                   {
                       String Action = (String) ViewData["Action"]; %>
                    <h2>Bonus for sales</h2>
                    <table border="0" cellpadding="0" cellspacing="0" width="600px" class="grid">
                        <tr>
                            <th>No</th>
                            <th>Sales Type</th>
                            <th>Bonus(%)</th>
                            <th>Active</th>
                            <th>Edit</th>
                            <th>Delete</th>
                        </tr>
                        <% IEnumerable<SaleType> SaleTypes = (IEnumerable<SaleType>)ViewData["SaleTypes"];
                           if (SaleTypes != null && SaleTypes.Count() > 0)
                           {
                               int no = 0;
                               foreach (SaleType SaleType1 in SaleTypes)
                               {
                                   no++;
                                   bool ood = no % 2 == 0;
                        %>
                        <tr <%if(ood) {%>class="GridLight" <% }%>>
                            <td><%= no %></td>
                            <td><%= SaleType1.Name%></td>
                            <td><%= SaleType1.Value.Value.ToString("0.0")%> %</td>
                            <td>
                            <%if (SaleType1.Active!=null && SaleType1.Active.Value)
                              { %><a href="<%= Url.Action("DeActivateSaleType","Users",new { id=SaleType1.Id}) %>">
                            <img alt="Edit" src="../../Images/btn-activate.png"/>
                            </a>
                            <%}
                              else
                              { %>
                            <a href="<%= Url.Action("ActivateSaleType","Users",new { id=SaleType1.Id}) %>">
                            <img alt="Edit" src="../../Images/btn-deactivate.png"/>
                            </a>
                            <%} %>
                            </td>
                            <td><a href="<%= Url.Action("DisplaySetting","Users",new { id=SaleType1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a></td>
                            <td>
                                <%{Html.RenderAction("CheckDeleteSalesType", "Users", new { id = SaleType1.Id }); } %>
                               <%-- <a href="<%= Url.Action("DeleteSaleType","Users",new { id=SaleType1.Id}) %>"><img alt="Delete" src="../../Images/btn-delete.png"/></a>--%>
                            </td>
                        </tr>
                    <%}
                           }%>
                           </table>
                           <div style="height:auto;overflow:hidden;">
                            <%: Html.ActionLink("Create Sale Type", "DisplaySetting", "Users", new { id = -1 }, new { Class = "RevenueLink" })%>
                           </div>
                           <% if (Action != null && (Action.Equals("Update") || Action.Equals("Create")))
                              {%>
                           <table border="0" cellpadding="5" cellspacing="0" width="300px">
                                <tr><%: Html.HiddenFor(m => m.Id) %>
                                    <td><label class="ShipmentLabel">SaleType</label></td>
                                    <td><%: Html.TextBoxFor(m => m.SaleType, new { Class = "ShipmentInput" })%></td>
                                </tr>
                                <tr>
                                    <td><label class="ShipmentLabel">Bonus(%)</label></td>
                                    <td><%: Html.TextBoxFor(m => m.Bonus, new { Class = "ShipmentInput" })%></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><input type="submit" value="Update"/></td>
                                </tr>
                            </table>
                            <%} %>
                           <%
                    } %>
                    <% using (Html.BeginForm("TaxSettting", "Users"))
                       { %>
                       <table border="0" cellpadding="5" cellspacing="0" width="auto">
                       <tr style="height:15px"><td colspan="2"></td>
                       </tr>
                        <tr>
                        <td><label class="ShipmentLabel">Tax for commission</label></td>
                        <td><%: Html.TextBoxFor(m => m.TaxCommistion, new { Class = "ShipmentInput" })%></td>
                        </tr>
                        <tr><td ></td><td><input type="submit" value="Update Tax"/></td>
                       </tr>
                        </table>
                   <%} %>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript" language="javascript">
        jQuery(document).ready(function () {
            jQuery("#SettingTab").addClass("Active");
            jQuery('#SettingTab').activeThisNav();
        });
    </script>
</asp:Content>
