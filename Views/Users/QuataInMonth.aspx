<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.SalePlanModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace ="SSM.Controllers"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EFS SSM System - QuataInMonth
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<% User User1 = (User)Session[AccountController.USER_SESSION_ID];
    List<Int32> MonthList = new List<Int32>(12); 
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
                    <h2>Quata in month</h2>
                    <div style="height:auto;overflow:hidden">
                    <label class="ShipmentLabel">Chose the month</label><%: Html.DropDownListFor(m => m.Month, new SelectList(MonthList), new { Class = "ShipmentSelectShort" })%>
                    <label class="ShipmentLabelShort">Year</label><%: Html.DropDownListFor(m => m.Year, new SelectList(YearList), new { Class = "ShipmentSelectShort" })%>
                    <input type="submit" value="Ok"/>
                 </div>
                    <p />
                    <div style="height:auto;overflow:hidden">
                    <label class="ShipmentLabel">Plan for <%= ViewData["PlanFor"] %></label><%: Html.DropDownListFor(m => m.PlanOfficeType, (SelectList)ViewData["PlanTypeList"], new { Class = "ShipmentSelect" })%>
                    </div>
                    <div id="saleComZone">
                        <div style="padding:15px 0 30px 50px">
                        <table border="1px solid #BFBFBF" class="tableCom">
                            <tr><th>No</th><th>Companies</th><th>Sale plan ($)</th><th>Edit</th></tr>
                            <% IEnumerable<UserSalePlan> SalePlans = (IEnumerable<UserSalePlan>)ViewData["SalePlans4Com"];
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
                                       <td> <%if (UsersModel.isAdminOrDirctor(User1))
                                              { %>
                                       <a href="<%= Url.Action("DirectorPlan4Sales","Users",new { id=SalePlan1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a>
                                       <%} %></td>
                                       </tr>
                                       <tr><td colspan="4">
                                           <div style="padding:15px 0 30px 50px">
                    <table border="1px solid #BFBFBF" class="tableCom">
                        <tr><th>No</th><th>Departments</th><th>Sale plan ($)</th><th>Edit</th></tr>
                        <% IEnumerable<UserSalePlan> SalePlans4Dept = (IEnumerable<UserSalePlan>)ViewData["SalePlans4Dept"];
                           if (SalePlans4Dept != null && SalePlans4Dept.Count() > 0)
                       {
                           int dno = 0;
                           double dsum = 0.0;
                           foreach (UserSalePlan SalePlan14Dept in SalePlans4Dept)
                           {
                               if (SalePlan14Dept.ComId == SalePlan1.Id)
                               {
                                   dno++;
                                   bool dood = no % 2 == 0;
                                   dsum += SalePlan14Dept.PlanValue != null ? Convert.ToDouble(SalePlan14Dept.PlanValue.Value) : 0.00;
                                   %>
                                   <tr <%if(dood) {%>class="GridLight" <% }%> >
                                   <td><%= dno%></td>
                                   <td><%= SalePlan14Dept.FullName%></td>
                                   <td><%= SalePlan14Dept.PlanValue != null ? SalePlan14Dept.PlanValue.Value.ToString("0.00") : "0.00"%></td>
                                   <td>
                                    <%if (UsersModel.isAdminOrDirctor(User1))
                                      { %>
                                     <a href="<%= Url.Action("Plan4Depts","Users",new { id=SalePlan14Dept.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a>
                                     <%} %></td>
                                   </tr>
                                   <tr>
                                    <td colspan="4">
                                        
                                        <div style="padding:15px 0 30px 50px">
                    <table border="1px solid #BFBFBF" class="tableNormal">
                        <tr><th>No</th><th>Sales</th><th>Sale plan ($)</th><th>Edit</th></tr>
                        <% 
                           IEnumerable<UserSalePlan> SalePlans4Sales = (IEnumerable<UserSalePlan>)ViewData["SalePlans4Salses"];
                           if (SalePlans4Sales != null && SalePlans4Sales.Count() > 0)
                           {
                               int sno = 0;
                               double ssum = 0.0;
                               foreach (UserSalePlan SalePlan1Sale in SalePlans4Sales)
                               {
                                   if (SalePlan14Dept.Id == SalePlan1Sale.DeptId)
                                   {
                                       sno++;
                                       bool sood = no % 2 == 0;
                                       ssum += SalePlan1Sale.PlanValue != null ? Convert.ToDouble(SalePlan1Sale.PlanValue.Value) : 0.00;
                                   %>
                                   <tr <%if(sood) {%>class="GridLight" <% }%> >
                                   <td><%= sno%></td>
                                   <td><%= SalePlan1Sale.FullName%></td>
                                   <td><%= SalePlan1Sale.PlanValue != null ? SalePlan1Sale.PlanValue.Value.ToString("0.00") : "0.00"%></td>
                                   <td>
                                   <% if (UsersModel.isDeptManager(User1))
                                      {%>
                                   <a href="<%= Url.Action("ManagerPlan4Sales","Users",new { id=SalePlan1Sale.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a>
                                   <%} %>
                                   </td>
                                   </tr>
                        <%}
                               }%>
                        <tr>
                            <td colspan="2" style="font-weight:bold">
                                Total plan in month
                            </td>
                            <td colspan="2">
                            <%= ssum %> $
                            </td>
                           
                        </tr>
                       <%}%>

                    </table>
                    </div>

                                    </td>
                                   </tr>
                           <%}
                           }%>
                        <tr>
                            <td colspan="2" style="font-weight:bold">
                                Total plan in month
                            </td>
                            <td colspan="2">
                            <%= dsum %> $
                            </td>
                           
                        </tr>
                       <%}%>
                    </table>
                    </div></td>
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
                    </div>
                   
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
