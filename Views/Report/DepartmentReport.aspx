<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.DepartmentportModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Department Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    User User1 = (User)Session[AccountController.USER_SESSION_ID];
    DateTime currentDate = DateTime.Now;
    List<String> YearList = new List<String>(12);
    for(int i=-5; i<= 5; i++) {
        String item = currentDate.AddYears(i).ToString("yyyy");
        YearList.Add(item);
    }
    %>
    <% using (Html.BeginForm())
       { %>
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <table width="auto" cellpadding="2">
                     <%: Html.HiddenFor(m=>m.Action) %>
                        <tr>
                            <td><label class="Label">Select a year</label></td>
                            <td><%: Html.DropDownListFor(m => m.Year,new SelectList(YearList), new { Class = "ShipmentInputShort" })%></td>
                            <td><label class="Label">Select a Sale</label></td>
                            <td><%: Html.DropDownListFor(m => m.DeptId, (SelectList)ViewData["ListDepts"], new { Class = "ShipmentSelect" })%>
                            <%: Html.ValidationMessageFor(m=>m.DeptId) %></td>
                            <td><input type="submit" value="Report for select department"/></td>
                        </tr>
                    </table>
                    <% List<PerformanceReport> ReportProcess = (List<PerformanceReport>)ViewData["ReportProcess"];
                       int totalSaleType = ReportProcess.ElementAt(0).PerformModels.Count;
                       double[] TotalProfiltSaleTypes = new double[totalSaleType];
                       double TotalPlan = 0, TotalProfilt = 0;
                       for (int i = 0; i < 12; i++)
                       {
                           TotalPlan += ReportProcess.ElementAt(i).Plan;
                           //TotalHandel += ReportProcess.ElementAt(i).ProfitHandel;
                           //TotalSale += ReportProcess.ElementAt(i).ProfitSale;
                           for (int ti = 0; ti < totalSaleType; ti++)
                           {
                               TotalProfiltSaleTypes[ti] += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                               TotalProfilt += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                           }
                       }
                        %>
                    <table id="ResultList" border="" cellpadding="2" cellspacing="0" class="tableNormal">
                        <tr>
                            <th></th><th>Jan</th><th>Feb</th><th>Mar</th><th>Apr</th><th>May</th><th>Jun</th><th>Jul</th><th>Aug</th><th>Sep</th><th>Oct</th><th>Nov</th><th>Dec</th><th>Total</th>
                        </tr>
                        <tr>
                        <td><label class="Label">Plan of <%= Model.Year %></label></td>
                        <td><%= ReportProcess.ElementAt(0).Plan %></td>
                        <td><%= ReportProcess.ElementAt(1).Plan %></td>
                        <td><%= ReportProcess.ElementAt(2).Plan %></td>
                        <td><%= ReportProcess.ElementAt(3).Plan %></td>
                        <td><%= ReportProcess.ElementAt(4).Plan %></td>
                        <td><%= ReportProcess.ElementAt(5).Plan %></td>
                        <td><%= ReportProcess.ElementAt(6).Plan %></td>
                        <td><%= ReportProcess.ElementAt(7).Plan %></td>
                        <td><%= ReportProcess.ElementAt(8).Plan %></td>
                        <td><%= ReportProcess.ElementAt(9).Plan %></td>
                        <td><%= ReportProcess.ElementAt(10).Plan %></td>
                        <td><%= ReportProcess.ElementAt(11).Plan %></td>
                        <td><%= TotalPlan%></td>
                        </tr>
                        <%
                            for (int ti = 0; ti < totalSaleType; ti++)
                            {
                                %>
                                <tr>
                        <td><label class="Label">Profit of <%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).SaleType %></label></td>
                        <td><%= ReportProcess.ElementAt(0).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(2).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(3).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(4).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(5).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(6).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(7).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(8).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(9).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(10).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= ReportProcess.ElementAt(11).PerformModels.ElementAt(ti).Profit%></td>
                        <td><%= TotalProfiltSaleTypes[ti]%></td>
                        </tr>
                        <tr>
                        <td><label class="Label">Perform of <%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).SaleType %>(%)</label></td>
                        <td><%= ReportProcess.ElementAt(0).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(2).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(3).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(4).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(5).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(6).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(7).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(8).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(9).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(10).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(11).PerformModels.ElementAt(ti).Perform.ToString("0.00") + "%"%></td>
                        <td><%= (TotalPlan == 0 ? TotalPlan.ToString("0.00") : (TotalProfiltSaleTypes[ti] * 100 / TotalPlan).ToString("0.00")) + "%"%></td>
                        </tr>
                               <%
                            }
                        %>
                        
                        <tr>
                        <td><label class="Label">Total profit</label></td>
                        <td><%= ReportProcess.ElementAt(0).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(1).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(2).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(3).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(4).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(5).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(6).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(7).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(8).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(9).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(10).SumProfit%></td>
                        <td><%= ReportProcess.ElementAt(11).SumProfit%></td>
                        <td><%= TotalProfilt%></td>
                        </tr>
                        <tr>
                        <td><label class="Label">Total perform(%)</label></td>
                        <td><%= ReportProcess.ElementAt(0).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(1).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(2).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(3).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(4).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(5).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(6).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(7).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(8).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(9).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(10).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%= ReportProcess.ElementAt(11).PerformSumProfit.ToString("0.00") + "%"%></td>
                        <td><%=  (TotalPlan == 0 ? TotalPlan.ToString("0.00") : ((TotalProfilt) * 100 / TotalPlan).ToString("0.00")) + "%"%></td>
                        </tr>
                    </table>

                    <div style="height:auto;overflow:hidden; padding:30px 0 15px 50px"><label class="Label"> Theo doi loi nhuan ban hang</label></div>
                    <% List<ReportYearModel> ListReport1 = (List<ReportYearModel>)ViewData["ListReport1"];
                       List<ReportYearModel> ListReport2 = (List<ReportYearModel>)ViewData["ListReport2"]; %>
                    <table class="tableNormal" border="">
                        <tr>
                        <th>Sales</th><th>Plan</th><th>USD/Month</th><th>Jan</th><th>Feb</th><th>Mar</th><th>Apr</th><th>May</th><th>Jun</th><th>Jul</th><th>Aug</th><th>Sep</th><th>Oct</th><th>Nov</th><th>Dec</th><th>Total</th><th>Remain</th>
                        </tr>
                        <% foreach (ReportYearModel ReportYearModel1 in ListReport1)
                           {%>
                           <tr>
                           <td><%= ReportYearModel1.SaleName %></td>
                           <td><%= ReportYearModel1.Plan %></td>
                           <td><%= ReportYearModel1.PlanPerMonth %></td>
                           <td><%= ReportYearModel1.Jan %></td>
                           <td><%= ReportYearModel1.Feb %></td>
                           <td><%= ReportYearModel1.Mar %></td>
                           <td><%= ReportYearModel1.Apr %></td>
                           <td><%= ReportYearModel1.May %></td>
                           <td><%= ReportYearModel1.Jun %></td>
                           <td><%= ReportYearModel1.Jul %></td>
                           <td><%= ReportYearModel1.Aug %></td>
                           <td><%= ReportYearModel1.Sep %></td>
                           <td><%= ReportYearModel1.Oct %></td>
                           <td><%= ReportYearModel1.Nov %></td>
                           <td><%= ReportYearModel1.Dec %></td>
                           <td><%= ReportYearModel1.Total %></td>
                           <td><%= ReportYearModel1.Remain %></td>
                           </tr>
                        <%} %>
                        <tr>
                        <th colspan="3">%</th><th>Jan</th><th>Feb</th><th>Mar</th><th>Apr</th><th>May</th><th>Jun</th><th>Jul</th><th>Aug</th><th>Sep</th><th>Oct</th><th>Nov</th><th>Dec</th><th>Total</th><th>Remain</th>
                        </tr>
                        <% foreach (ReportYearModel ReportYearModel1 in ListReport2)
                           {%>
                           <tr>
                           <td colspan="3"><%= ReportYearModel1.SaleName %></td>
                           <td><%= ReportYearModel1.Jan %></td>
                           <td><%= ReportYearModel1.Feb %></td>
                           <td><%= ReportYearModel1.Mar %></td>
                           <td><%= ReportYearModel1.Apr %></td>
                           <td><%= ReportYearModel1.May %></td>
                           <td><%= ReportYearModel1.Jun %></td>
                           <td><%= ReportYearModel1.Jul %></td>
                           <td><%= ReportYearModel1.Aug %></td>
                           <td><%= ReportYearModel1.Sep %></td>
                           <td><%= ReportYearModel1.Oct %></td>
                           <td><%= ReportYearModel1.Nov %></td>
                           <td><%= ReportYearModel1.Dec %></td>
                           <td><%= ReportYearModel1.Total %></td>
                           <td><%= ReportYearModel1.Remain %></td>
                           </tr>
                        <%} %>
                    </table>
                    <div style="overflow:hidden;">
                    <%: Html.ActionLink("Report to Excel", "DepartmentReportExcel", "Report", new { Class = "RevenueLink" })%>
                    </div>
                </div>
            </div>
        </div>
    </div>
   <%} %>
      <script type="text/javascript">
          jQuery(document).ready(function () {
              jQuery("#ReportTab").addClass("Active");
              jQuery('#ReportTab').activeThisNav();
          });
   </script>
</asp:Content>
