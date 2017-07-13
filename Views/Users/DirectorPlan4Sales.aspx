<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.SalePlanModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SCF SSM System - Director Plan 4 Sales
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% User User1 = (User)Session[SSM.Controllers.AccountController.USER_SESSION_ID];%>
<% List<Int32> MonthList = new List<Int32>(12); 
    for(int i=1; i<=12; i++){
        MonthList.Add(i);
    }
    DateTime currentDate = DateTime.Now;
    List<String> YearList = new List<String>(12);
    for(int i=-5; i<= 5; i++) {
        String item = currentDate.AddYears(i).ToString("yyyy");
        YearList.Add(item);
    }
     String Action = (String) ViewData["Action"];
    %>
    <% using (Html.BeginForm())
        { %>
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <h2>Plan for sales department</h2>
                    <div style="height:auto;overflow:hidden">
                    <label class="ShipmentLabel">Chose the month</label><%: Html.DropDownListFor(m => m.Month, new SelectList(MonthList), new { Class = "ShipmentSelectShort" })%>
                    <label class="ShipmentLabelShort">Year</label><%: Html.DropDownListFor(m => m.Year, new SelectList(YearList), new { Class = "ShipmentSelectShort" })%>
                    <input type="submit" value="Ok"/>
                    
                    </div>
                    <p />
                    <div style="height:auto;overflow:hidden">
                    <label class="ShipmentLabel">Plan for <%= ViewData["PlanFor"] %></label><%: Html.DropDownListFor(m => m.PlanOfficeType, (SelectList)ViewData["PlanTypeList"], new { Class = "ShipmentSelect" })%>
                    </div>
                    <div style="padding:15px 0 30px 50px">
                        <table border="1px solid #BFBFBF" class="tableNormal">
                            <tr><th>No</th><th>Sales</th><th>Sale plan ($)</th><th>Edit</th></tr>
                            <% IEnumerable<UserSalePlan> SalePlans = (IEnumerable<UserSalePlan>)ViewData["SalePlans"];
                           if (SalePlans != null && SalePlans.Count() > 0)
                           {
                               int no = 0;
                               double sum = 0.0;
                               foreach (UserSalePlan SalePlan1 in SalePlans)
                               {
                                   no++;
                                   bool ood = no % 2 == 0;
                                   sum += SalePlan1.PlanValue != null ? Convert.ToDouble(SalePlan1.PlanValue.Value) : 0.00;
                                       %>
                                       <tr <%if(ood) {%>class="GridLight" <% }%> >
                                       <td><%= no%></td>
                                       <td><%= SalePlan1.FullName%></td>
                                       <td><%= SalePlan1.PlanValue != null ? SalePlan1.PlanValue.Value.ToString("0.00"):"0.00"%></td>
                                       <td>
                                       <%if (UsersModel.isAdminOrDirctor(User1))
                                         { %>
                                       <a href="<%= Url.Action("DirectorPlan4Sales","Users",new { id=SalePlan1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a>
                                       <%} %></td>
                                       </tr>
                                <%}%>
                        <tr>
                            <td colspan="2" style="font-weight:bold">
                                Total plan in month
                            </td>
                            <td colspan="2">
                            <%= sum %> $
                            </td>
                           
                        </tr>
                       <%}%>
                        </table>
                    </div>
                    <% if (Action != null && Action.Equals("Update"))
                              {%>
                              <div>
                                   <table border="0" cellpadding="5" cellspacing="0" width="300px">
                                        <tr><%: Html.HiddenFor(m => m.SaleId) %>
                                            <td><label class="ShipmentLabel">Office Name</label></td>
                                            <td><label class="ShipmentLabelValue"><%= Model.SaleName%></label> </td>
                                        </tr>
                                        <tr>
                                            <td><label class="ShipmentLabel">Sale plan ($)</label></td>
                                            <td><%: Html.TextBoxFor(m => m.PlanValue, new { Class = "ShipmentSelect" })%></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td><input type="submit" value="Update"/></td>
                                        </tr>
                                    </table>
                            </div>
                            <%} %>
                </div>
            </div>
        </div>
    </div>
    <%} %>
    <script type="text/javascript" language="javascript">
        jQuery(document).ready(function () {
            jQuery("#SalesTab").addClass("Active");
            jQuery('#SalesTab').activeThisNav();
            var $PlanValue = jQuery('#PlanValue');

            if ($PlanValue.length > 0) {
               window.location = window.location.href + '#PlanValue';
               jQuery('#PlanValue').focus();
            }
        });
    </script>
</asp:Content>
