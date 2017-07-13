<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.PersonReportModel>" %>

<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     Personal Report
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%
        User User1 = (User)Session[AccountController.USER_SESSION_ID];
        DateTime currentDate = DateTime.Now; 
         
    
    %>
    <% using (Html.BeginForm())
       { %>
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <div style="height: auto; overflow: hidden; padding-bottom: 10px;">
                        <%: Html.HiddenFor(m=>m.Action) %>
                        <table width="auto" cellpadding="2">
                            <tr>
                                <td>
                                    <label class="Label">Select a year</label></td>
                                <td><%: Html.DropDownListFor(m => m.Year,new SelectList(Enumerable.Range(DateTime.Now.Year - 10, 11)), new { Class = "ShipmentInputShort" })%></td>
                                <td>
                                    <label class="Label">Select a Sale</label></td>
                                <td>
                                    <%if (ViewData["ListUser"] != null)
                                      {%>
                                    <%: Html.DropDownListFor(m => m.UserId, (SelectList)ViewData["ListUser"], new { Class = "ShipmentSelect" })%>
                                    <%: Html.ValidationMessageFor(m => m.UserId)%>
                                    <%}
                                      else
                                      { %>
                                    <select class="ShipmentSelect">
                                        <option>-- Please select --</option>
                                    </select>
                                    <%} %></td>
                                <td>
                                    <input type="submit" value="Report for select sale" onclick="SearchReport();" /></td>
                            </tr>
                        </table>
                    </div>

                    <% List<PerformanceReport> ReportProcess = (List<PerformanceReport>)ViewData["ReportProcess"];
                       int totalSaleType = ReportProcess.ElementAt(0).PerformModels.Count;
                       double[] TotalProfiltSaleTypes = new double[totalSaleType];
                       double[] totalBonusSaleTypes = new double[totalSaleType];
                       double TotalPlan = 0, TotalProfilt = 0, totalBonus = 0;
                       for (int i = 0; i < 12; i++)
                       {
                           TotalPlan += ReportProcess.ElementAt(i).Plan;
                           //TotalHandel += ReportProcess.ElementAt(i).ProfitHandel;
                           //TotalSale += ReportProcess.ElementAt(i).ProfitSale;
                           for (int ti = 0; ti < totalSaleType; ti++)
                           {
                               TotalProfiltSaleTypes[ti] += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                               totalBonusSaleTypes[ti] += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Bonus;
                               TotalProfilt += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Profit;
                               totalBonus += ReportProcess.ElementAt(i).PerformModels.ElementAt(ti).Bonus;
                           }
                       }
                       
                    %>
                    <table id="ResultList" border="" cellpadding="2" cellspacing="0" class="tableNormal reportsummer">
                        <tr>
                            <th></th>
                            <th>Jan</th>
                            <th>Feb</th>
                            <th>Mar</th>
                            <th>Apr</th>
                            <th>May</th>
                            <th>Jun</th>
                            <th>Jul</th>
                            <th>Aug</th>
                            <th>Sep</th>
                            <th>Oct</th>
                            <th>Nov</th>
                            <th>Dec</th>
                            <th>Total</th>
                        </tr>
                        <tr style="border-bottom: 2px solid #000000">
                            <td>
                                <label class="Label">Plan of <%= Model.Year %></label></td>
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
                            <td>
                                <label class="Label">Profit of <%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).SaleType %></label></td>
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
                            <td>
                                <label class="Label">Bonus of <%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).SaleType %></label></td>
                            <td><%= ReportProcess.ElementAt(0).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(2).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(3).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(4).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(5).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(6).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(7).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(8).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(9).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= ReportProcess.ElementAt(10).PerformModels.ElementAt(ti).Bonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(11).PerformModels.ElementAt(ti).Bonus.ToString("0.00") %></td>
                            <td><%= totalBonusSaleTypes[ti].ToString("0.00")%></td>
                        </tr>
                        <tr style="border-bottom: 2px solid #000000">
                            <td>
                                <label class="Label">Perform of <%= ReportProcess.ElementAt(1).PerformModels.ElementAt(ti).SaleType %>(%)</label></td>
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
                            <td>
                                <label class="Label">Total profit</label></td>
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
                            <td>
                                <label class="Label">Total Bonus</label></td>
                            <td><%= ReportProcess.ElementAt(0).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(1).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(2).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(3).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(4).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(5).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(6).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(7).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(8).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(9).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(10).SumBonus.ToString("0.00")%></td>
                            <td><%= ReportProcess.ElementAt(11).SumBonus.ToString("0.00")%></td>
                            <td><%= totalBonus%></td>
                        </tr>
                        <tr style="border-bottom: 2px solid #000000">
                            <td>
                                <label class="Label">Total perform(%)</label></td>
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
                            <td><%= (TotalPlan == 0 ? TotalPlan.ToString("0.00") : ((TotalProfilt) * 100 / TotalPlan).ToString("0.00")) + "%"%></td>
                        </tr>

                    </table>
                    <div style="overflow: hidden;">
                        <%: Html.ActionLink("Report to Excel", "PersonalReportExcel", "Report", new { Class = "RevenueLink" })%>
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
