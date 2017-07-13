<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.RevenueModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Revenue</title>
    <link id="Link1" href="~/Content/PrintPage/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/PrintPage/global.css" media="all" runat="server" />
    <link id="Link3" type="text/css" rel="stylesheet" href="~/Content/PrintPage/top-nav-bar.css" media="all" runat="server" />
    <link id="Link4" type="text/css" rel="stylesheet" href="~/Content/PrintPage/main-nav-bar.css" media="all" runat="server" />
    <link id="Link14" type="text/css" rel="stylesheet" href="~/Content/PrintPage/homepage.css" media="all" runat="server" />
    <link id="Link15" type="text/css" rel="stylesheet" href="~/Content/PrintPage/section-block.css" media="all" runat="server" />
    <link id="Link16" type="text/css" rel="stylesheet" href="~/Content/PrintPage/footer-bar.css" media="all" runat="server" />
    <link id="Link17" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server" />
    <link id="Link18" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Shipment.css" media="all" runat="server" />
    <link id="Link10" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Freights.css" media="all" runat="server" />
    <link id="Link11" type="text/css" rel="stylesheet" href="~/Content/PrintPage/BookingConfirm.css" media="all" runat="server" />
   
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-2.1.4.min.js") %>" ></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-ui-1.11.4.min.js") %>"></script>
     <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-cookie.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-mousewheel.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/j-select.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/j-select.external.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/main.js") %>"></script>

    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/global.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/top-nav-bar.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/main-nav-bar.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/homepage.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/prototype.js")%>"></script>
	<script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/ui.datetimepicker.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/calendar-time-custom.js")%>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/date-validator.js")%>"></script>
</head>
<body style="margin-left:40px; margin-top:15px">
<div style="font-size:12px;color:Black;">
<div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
        <% using (Html.BeginForm()) {%>
        <%: Html.ValidationSummary(true) %>
   
            <table style="margin-left:50px;" width="850px">
                            <tr><td colspan="4">
                                    <div class="ButtonZone1" style="font-size:20px;font-weight:bold;text-align:center">
                                        <span>REVENUE</span>
                                    </div>
                                    <% Html.RenderPartial("_DocumentMenu", Model); %>
                            </td></tr>
                            <tr>

                                <td style="width: 130px;">
                                   <label class="RevenueLabel">Ref</label>
                                </td>
                                <td>
                                <%: Html.TextBoxFor(m => m.Id, new { disabled = "disabled", Class = "RevenueInput" })%>
                                </td>
                                <td style="width: 130px;"></td>
                                <td></td>
                            </tr>
                            <tr>

                                <td style="width: 130px;">
                                   <label class="RevenueLabel">Service</label>
                                </td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["ServiceName"] %>"/>
                                </td>
                                <td style="width: 130px;"><label class="RevenueLabel"><%= ViewData["DateShpLabel"]%></label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["DateShp"] %>"/></td>
                            </tr>
                            <tr>
                                <td><label class="RevenueLabel">Shipper</label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["ShipperRevenue"] %>"/></td>
                                <td><label class="RevenueLabel">Cnee</label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["CneeRevenue"] %>"/></td>
                            </tr>
                            <tr>
                                <td><label class="RevenueLabel">House bill</label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["HBill"] %>"/></td>
                                <td><label class="RevenueLabel">POL / POD</label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["POLPOD"] %>"/></td>
                                
                            </tr>
                            <tr>
                                <td><label class="RevenueLabel">Master bill</label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["MBill"] %>"/></td>
                                <td><label class="RevenueLabel">Quantity</label></td>
                                <td><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["Quantity"] %>"/></td>
                            </tr>
                            <tr>
                                <td><label class="RevenueLabel">B.Rate($) / A.Rate(%)</label></td>
                                <td style="font-size:1.2em;"><%: Html.TextBoxFor(model => model.BRate, new { Class = "Shipment RevenueInputShort", OnBlur = "formatMoneyNumber(this);" })%>
                                / <%: Html.TextBoxFor(model => model.ARate, new { Class = "RevenueInputShort", OnBlur = "formatMoneyNumber(this);" })%>
                                <%: Html.ValidationMessageFor(m => m.BRate)%>
                                <%: Html.ValidationMessageFor(m => m.ARate)%></td>
                                <td><label class="RevenueLabel">S.Rate($)</label></td>
                                <td><%: Html.TextBoxFor(m => m.SRate, new { Class = "Shipment RevenueInputShort", OnBlur = "formatMoneyNumber(this);" })%>
                                <%: Html.ValidationMessageFor(m => m.SRate)%></td>
                            </tr>
                            <tr>
                                <td ><label class="RevenueLabel">Agent</label></td>
                                <td ><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["AgentRev"] %>"/></td>
                                <td ><label class="RevenueLabel">Carrier</label></td>
                                <td ><input type="text" class="RevenueInput" disabled="disabled" value="<%= ViewData["CarrierRev"] %>"/></td>
                            </tr>
            </table>
            <br />
            <table width="926px" style="background-color:#e6edf9;border:1px solid #BFBFBF;">
                <tr style="background-color:#338899; font-size:14px;font-family:Arial; font-weight:bold;color:White;height:35px;"><td style="width:50px"></td>
                    <td style="width:161px">Descriptions</td>
                    <td style="width:200px">Payable in Viet Nam</td>
                    <td style="width:200px">Payable in Oversea</td>
                    <td>Remark</td>
                </tr>
                <tr style="background-color:#e3f7e5;  font-weight:bold"><td>A</td>
                    <td>INCOME</td>
                    <td><%: Html.TextBoxFor(model => model.INVI, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeader", OnBlur = "formatMoneyNumber(this);" })%><%: Html.ValidationMessageFor(m => m.INVI)%></td></td>
                    <td><%: Html.TextBoxFor(model => model.INOS, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeader", OnBlur = "formatMoneyNumber(this);" })%><%: Html.ValidationMessageFor(m => m.INOS)%></td></td>
                    <td><%: Html.TextBoxFor(model => model.Income, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeaderLong", OnBlur = "formatMoneyNumber(this);" })%><%: Html.ValidationMessageFor(m => m.Income)%></td></td>
                </tr>
                <tr><td>1</td>
                    <td>Transport Rate</td>
                    <td><%: Html.TextBoxFor(model => model.INTransportRate, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%><%: Html.ValidationMessageFor(m => m.INTransportRate)%></td>
                    <td><%: Html.TextBoxFor(model => model.INTransportRateOS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%><%: Html.ValidationMessageFor(m => m.INTransportRateOS)%></td>
                    <td rowspan="9"><%: Html.TextAreaFor(model => model.INRemark, new { Class = "ShipmentPayableRemark", OnBlur = "convertValue(this, 'INRemarkHidden')" })%></td>
                </tr>
                <tr><td>2</td>
                    <td>Inland Service</td>
                    <td><%: Html.TextBoxFor(model => model.INInlandService, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%> <%: Html.ValidationMessageFor(m => m.INInlandService)%></td>
                    <td><%: Html.TextBoxFor(model => model.INInlandServiceOS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%><%: Html.ValidationMessageFor(m => m.INInlandServiceOS)%></td>
                </tr>
                <tr><td>3</td>
                    <td>Oversea Income</td>
                    <td></td>
                    <td><%: Html.TextBoxFor(model => model.INCreditDebitOS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>4</td>
                    <td>Document fee</td>
                    <td><%: Html.TextBoxFor(model => model.INDocumentFee, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.INDocumentFeeOS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>5</td>
                    <td>Handling fee</td>
                    <td><%: Html.TextBoxFor(model => model.INHandingFee, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.INHandingFeeOS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>6</td>
                    <td>THC</td>
                    <td><%: Html.TextBoxFor(model => model.INTHC, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>7</td>
                    <td>CFS</td>
                    <td><%: Html.TextBoxFor(model => model.INCFS, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>8</td>
                    <td><%: Html.TextBoxFor(model => model.AutoName1, new { Class = "ShipmentPayableText" })%></td>
                    <td><%: Html.TextBoxFor(model => model.INAutoValue1, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.INAutoValue1OS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>9</td>
                    <td><%: Html.TextBoxFor(model => model.AutoName2, new { Class = "ShipmentPayableText" })%></td>
                    <td><%: Html.TextBoxFor(model => model.INAutoValue2, new { Class = "Shipment ShipmentPayable IncomVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.INAutoValue2OS, new { Class = "Shipment ShipmentPayable IncomOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr style="background-color:#e3f7e5; font-weight:bold"><td>B</td>
                    <td>EXPENSES</td>
                    <td><%: Html.TextBoxFor(model => model.EXVI, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeader", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.EXOS, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeader", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.Expense, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeaderLong", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>10</td>
                    <td>Buying Rate</td>
                    <td><%: Html.TextBoxFor(model => model.EXTransportRate, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.EXTransportRateOS, new { Class = "Shipment ShipmentPayable ExpenseOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td>Freight & local charge paid to</td>
                </tr>
                <tr><td>11</td>
                    <td>Inland Service</td>
                    <td><%: Html.TextBoxFor(model => model.EXInlandService, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.EXInlandServiceOS, new { Class = "Shipment ShipmentPayable ExpenseOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%= Html.DropDownList("PaidToCarrier", (SelectList)ViewData["Carriers"], new { Class="ShipmentSelect" })%></td>
                    <td></td>
                </tr>
                <tr><td>12</td>
                    <td>Commision to shipper</td>
                    <td><%: Html.TextBoxFor(model => model.EXCommision2Shipper, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                    <td></td>
                </tr>
                <tr><td>13</td>
                    <td>Commision to carrier</td>
                    <td><%: Html.TextBoxFor(model => model.EXCommision2Carrier, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>14</td>
                    <td>Tax <input type="hidden" id="TaxCommission" value='<%= ViewData["TaxCommission"]%>' /></td>
                    <td><%: Html.TextBoxFor(model => model.EXTax, new { ReadOnly = "true", Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                    <td rowspan="8"><%: Html.TextAreaFor(model => model.EXRemark, new { Class = "ShipmentPayableRemark2", OnBlur = "convertValue(this, 'EXRemarkHidden')" })%></td>
                </tr>
                <tr><td>15</td>
                    <td>Oversea Expense</td>
                    <td></td>
                    <td><%: Html.TextBoxFor(model => model.EXCreditDebitOS, new { Class = "Shipment ShipmentPayable ExpenseOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>16</td>
                    <td>Document fee</td>
                    <td><%: Html.TextBoxFor(model => model.EXDocumentFee, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>17</td>
                    <td>Handling fee</td>
                    <td><%: Html.TextBoxFor(model => model.EXHandingFee, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>18</td>
                    <td>THC</td>
                    <td><%: Html.TextBoxFor(model => model.EXTHC, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>19</td>
                    <td>CFS</td>
                    <td><%: Html.TextBoxFor(model => model.EXCFS, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td></td>
                </tr>
                <tr><td>20</td>
                    <td><%: Html.TextBoxFor(model => model.EXManualName1, new { Class = "ShipmentPayableText"})%></td>
                    <td><%: Html.TextBoxFor(model => model.EXManualValue1, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.EXmanualValue1OS, new { Class = "Shipment ShipmentPayable ExpenseOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td>21</td>
                    <td><%: Html.TextBoxFor(model => model.EXManualName2, new { Class = "ShipmentPayableText"})%></td>
                    <td><%: Html.TextBoxFor(model => model.ExManualValue2, new { Class = "Shipment ShipmentPayable ExpenseVI", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.EXmanualValue2OS, new { Class = "Shipment ShipmentPayable ExpenseOS", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr style="background-color:#e3f7e5; font-weight:bold"><td>C</td>
                    <td>EARNING</td>
                    <td><%: Html.TextBoxFor(model => model.EarningVI, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeader", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.EarningOS, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeader", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td><%: Html.TextBoxFor(model => model.Earning, new { ReadOnly = "true", Class = "Shipment ShipmentPayableHeaderLong", OnBlur = "formatMoneyNumber(this);" })%></td>
                </tr>
                <tr><td colspan="2">Issue Invoice to customer</td>
                    <td><%: Html.TextBoxFor(model => model.InvoiceCustom, new { ReadOnly = "true", Class = "Shipment InvoiveVietnam", OnBlur = "formatMoneyNumber(this);" })%></td>
                    <td>Currency to Detail</td>
                    <td><%: Html.TextBoxFor(model => model.Description4Invoice, new { Class = "ShipmentPayableLong"})%></td>
                </tr>
            </table>
            <br /><br />
            <table width="630px" style="float:left;position:relative;height:274px;">
                <tr style="height:263px;"><td style="width:618px; background-color:#f5ebd3;border:2px solid #BFBFBF;">
                <table width="100%">
                <tr><td></td>
                    <td style="width:120px;"></td>
                    <td style="width:120px;"></td>
                    <td style="width:120px;"></td>
                    <td style="width:120px;"></td>
                </tr>
                <tr><td colspan="5" style="font-weight:bold">Credit / Debit to/from Overseas Agents</td>
                </tr>
                <tr><td ></td>
                    <td><%: Html.TextBoxFor(m => m.InvAmount1, new { Class = "Shipment RevenueInv" })%></td>
                    <td ><%: Html.DropDownList("InvCurrency1", (SelectList)ViewData["CurrencyTypes"], new { Class = "RevenueInvSelect" })%></td>
                    <td><%: Html.DropDownList("InvType1", (SelectList)ViewData["InvTypes"], new { Class = "RevenueInvSelect" })%></td>
                    <td><select id="InvAgentId1" name="InvAgentId1" class = "RevenueInvSelect">
                                            <% IEnumerable<Agent> Agents = (IEnumerable<Agent>)ViewData["Agents"];%>
                                            <% if (Agents != null && Agents.Count() > 0)
                                               {
                                                   foreach (Agent Agent1 in Agents)
                                                   {%>
                                                       <option class="CustomizeTooltip" <% if(Agent1.Id == Model.InvAgentId1) { %> selected="selected"<%} %> value="<%= Agent1.Id %>" title="<%= Agent1.Description %>"> <%=Agent1.AbbName%> </option>
                                            <%}
                                               }%>
                                        </select></td>
                </tr>
                <tr><td ></td>
                    <td><%: Html.TextBoxFor(m => m.InvAmount2, new { Class = "Shipment RevenueInv" })%></td>
                    <td ><%: Html.DropDownList("InvCurrency2", (SelectList)ViewData["CurrencyTypes"], new { Class = "RevenueInvSelect" })%></td>
                    <td><%: Html.DropDownList("InvType2", (SelectList)ViewData["InvTypes"], new { Class = "RevenueInvSelect" })%></td>
                    <td><select id="InvAgentId2" name="InvAgentId2" class = "RevenueInvSelect">
                                            <% if (Agents != null && Agents.Count() > 0)
                                               {
                                                   foreach (Agent Agent1 in Agents)
                                                   {%>
                                                       <option class="CustomizeTooltip" <% if(Agent1.Id == Model.InvAgentId2) { %> selected="selected"<%} %> value="<%= Agent1.Id %>" title="<%= Agent1.Description %>"> <%=Agent1.AbbName%> </option>
                                            <%}
                                               }%>
                                        </select></td>
                </tr>
                <tr><td class="Label" colspan="5">Bonus</td>
                </tr>
                <tr><td></td>
                    <td>Request</td>
                    <td>Approved</td>
                    <td colspan="2" style="text-align:center">Bonus</td>
                </tr>
                <tr><td></td>
                    <td><input disabled="disabled" value="<%= ViewData["SaleType"] %>" class="ShipmentInputShort"/></td>
                    <td> <select id="BonRequest" name="BonRequest" ReadOnly="true" Class="RevenueInv">
                                            <%IEnumerable<SaleType> SaleTypes = (IEnumerable<SaleType>)ViewData["RevenueST"];%>
                                            <% if (SaleTypes != null && SaleTypes.Count() > 0)
                                               {
                                                   foreach (SaleType SaleType1 in SaleTypes)
                                                   {%>
                                                   
                                                       <option class="CustomizeTooltip"  <% if(Convert.ToDouble(SaleType1.Value).Equals(Convert.ToDouble(Model.BonRequest))) { %> selected="selected" <%} %> value="<%= SaleType1.Value %>"> <%=SaleType1.Name%> </option>
                                            <%}
                                               }%>
                                        </select></td>
                    <td><%: Html.TextBoxFor(m => m.BonApprove, new { ReadOnly = "true", Class = "RevenueInvText" })%>%</td>
                    <td><%: Html.TextBoxFor(m => m.AmountBonus2, new { ReadOnly = "true", Class = "RevenueInv Shipment" })%></td>
                </tr>
            </table>
            <table width="100%">
            <% bool userAction = (bool)ViewData["SalesAction"];
               bool directorAction = (bool)ViewData["DirectorAction"];
            %>
                <tr style="height:115px">
                    <td style="width: 150px; border:2px solid #BFBFBF;text-align:center;">
                        <input id="RevenueAction" type="hidden" name="RevenueAction" value="Submit" />
                        <label>User Action</label><br/><br/>
                        <% if (userAction)
                           {%>
                        <input type="button" value="Get Back" onclick="javascript:submitForm('GetBack');" style="width:75px;background-color:#ED1B2E;"/>
                        <br/>
                        <input type="submit" value="Submit" style="width:75px;"/>
                        <%}
                           else
                           { %>
                           <input type="button" class="DisabledButton" disabled="disabled" value="Get Back" onclick="#" style="width:75px;"/>
                           <br/>
                           <input type="button" class="DisabledButton" disabled="disabled" value="Submit" style="width:75px;"/>
                           <% }%>
                    </td>
                    <td style="width: 150px;border:2px solid #BFBFBF;text-align:center">
                        <label>Director Action</label><br /><br/>
                        <% if (directorAction)
                           {%>
                        <input type="button" value="Cancel" onclick="javascript:submitForm('Cancel');" style="width:75px; background-color:#ED1B2E;"/>
                        <br/>
                        <input type="button" value="Approve" onclick="javascript:submitForm('Approve');" style="width:75px;"/>
                        <%}else { %>
                        <input type="button" class="DisabledButton" disabled="disabled"  value="Cancel" onclick="#" style="width:75px;"/>
                        <br/>
                        <input type="button" class="DisabledButton" disabled="disabled"  value="Approve" onclick="#" style="width:75px;"/>
                        <%} %>
                    </td>
                    <td style="border:2px solid #BFBFBF;">
                    <label><%= ViewData["SubmitName"]%></label><br />
                     <label><%= ViewData["SubmitDate"]%></label><br />
                     <label><%= ViewData["ApproveName"]%></label><br />
                    <label><%= ViewData["ApprovedDate"]%></label>
                    </td>
                </tr>
            </table>

            </td></tr>
            </table>
         <% } %>
         <% using (Html.BeginForm("RevenueAccount", "Shipment"))
            {%>
        <%: Html.ValidationSummary(true)%>
         <% bool accountAction = (bool)ViewData["AccountAction"]; %>
             <table width="300px" style="height:274px; background-color:#e3f7e5;border:2px solid #BFBFBF;text-align:center;">
                <tr>
                    <td colspan="3"><label>Account Invoice NO. / Date</label>
                    </td>
                </tr>
                <tr>
                    <td>1
                    </td>
                    <td style="width: 140px;">
                    <%: Html.HiddenFor(model => model.Id)%>
                    <%: Html.HiddenFor(model => model.EXRemarkHidden)%>
                    <%: Html.HiddenFor(model => model.INRemarkHidden)%>
                    <%: Html.TextBoxFor(model => model.AccInv1, new { Class = "RevenueInvDate" })%>
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInvDate1, new { Class = "RevenueInvDate" })%>
                    <%: Html.ValidationMessageFor(model => model.AccInvDate1)%>
                    <label for="AccInvDate1" class="RevenueInputDate"></label>
                    </td>
                </tr>
                <tr>
                    <td>2
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInv2, new { Class = "RevenueInvDate" })%>
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInvDate2, new { Class = "RevenueInvDate" })%>
                    <label for="AccInvDate2" class="RevenueInputDate"></label>
                    </td>
                </tr>
                <tr>
                    <td>3
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInv3, new { Class = "RevenueInvDate" })%>
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInvDate3, new { Class = "RevenueInvDate" })%>
                    <label for="AccInvDate3" class="RevenueInputDate"></label>
                    </td>
                </tr>
                <tr>
                    <td>4
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInv4, new { Class = "RevenueInvDate" })%>
                    </td>
                    <td style="width: 140px;">
                    <%: Html.TextBoxFor(model => model.AccInvDate4, new { Class = "RevenueInvDate" })%>
                    <label for="AccInvDate4" class="RevenueInputDate"></label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3"><label><%= ViewData["AccountName"]%></label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    <% if (accountAction)
                       {%>
                    <input type="submit" value="Accountant update"/><br/>
                    <%}
                       else
                       { %>
                       <input type="button" disabled="disabled" class="DisabledButton" value="Accountant update"/><br/>
                    <%} %>
                    </td>
                </tr>
            </table>
        <%} %>
          </div>
       </div>
     </div>
   </div>
   </div>
   <script type="text/javascript" language="javascript">
       function submitForm(action) {
           jQuery("#RevenueAction").val(action);
           var form = jQuery("#RevenueAction").parents("form:first");
           form.submit();
       }
       function convertValue(_this, destination) {
           jQuery("#" + destination).val(jQuery(_this).val());
       }

       jQuery(document).ready(function () {
           jQuery("#FileTab").addClass("Active");
           jQuery('#FileTab').activeThisNav();
           new DateTimePicker('AccInvDate1', 'dd/MM/yyyy');
           new DateTimePicker('AccInvDate2', 'dd/MM/yyyy');
           new DateTimePicker('AccInvDate3', 'dd/MM/yyyy');
           new DateTimePicker('AccInvDate4', 'dd/MM/yyyy');
           jQuery('#DocumentMenuContainer').hide();

           jQuery("#DocumentMenuContainer .Close").click(function () {
               jQuery('#DocumentMenuContainer').hide();
           });
           jQuery("#DocumentsButton").click(function () {
               jQuery('#DocumentMenuContainer').show();
           });

           jQuery("input[type='text']").each(function () {
               if (jQuery(this).hasClass("Shipment")) {
                   var number = jQuery(this).val();
                   if (jQuery.trim(number) != "" && !isNaN(number)) {
                       jQuery(this).val(parseFloat(number).toFixed(2));
                   }
                   else {
                       jQuery(this).val("0.00");
                   }

               }
           });

           function calculateRevenue(ClassName) {
               var $IncomVis = jQuery("." + ClassName);
               var i = 0;
               var sumIncomVI = 0.0;
               for (i = 0; i < $IncomVis.length; i++) {
                   var $eachVi = $IncomVis[i];
                   sumIncomVI += parseFloat(jQuery($eachVi).val());
               }
               return sumIncomVI;
           }

           jQuery("#BonRequest").change(function () {
               jQuery("#BonApprove").val(parseInt(jQuery("#BonRequest").val()));
               jQuery("#AmountBonus2").val(parseFloat(parseFloat(jQuery("#Earning").val()) * parseFloat(jQuery("#BonApprove").val()) / 100).toFixed(2));
           });
           jQuery("#BonApprove").change(function () {
               jQuery("#AmountBonus2").val(parseFloat(parseFloat(jQuery("#Earning").val()) * parseFloat(jQuery("#BonApprove").val()) / 100).toFixed(2));
           });
           function calculatorAll() {
               jQuery("#Income").val(parseFloat(jQuery("#INVI").val()) + parseFloat(jQuery("#INOS").val()));
               jQuery("#Expense").val(parseFloat(jQuery("#EXVI").val()) + parseFloat(jQuery("#EXOS").val()));
               jQuery("#EarningVI").val(parseFloat(parseFloat(jQuery("#INVI").val()) - parseFloat(jQuery("#EXVI").val())).toFixed(2));
               jQuery("#EarningOS").val(parseFloat(parseFloat(jQuery("#INOS").val()) - parseFloat(jQuery("#EXOS").val())).toFixed(2));
               jQuery("#Earning").val(parseFloat(parseFloat(jQuery("#EarningVI").val()) + parseFloat(jQuery("#EarningOS").val())).toFixed(2));
               jQuery("#InvoiceCustom").val(jQuery("#INVI").val());
               jQuery("#AmountBonus1").val(parseFloat(parseFloat(jQuery("#Earning").val()) * parseFloat(jQuery("#BonRequest").val()) / 100).toFixed(2));
               jQuery("#AmountBonus2").val(parseFloat(parseFloat(jQuery("#Earning").val()) * parseFloat(jQuery("#BonApprove").val()) / 100).toFixed(2));
           }

           var $IncomVis = jQuery(".IncomVI");
           var i = 0;
           for (i = 0; i < $IncomVis.length; i++) {
               var $eachVi = $IncomVis[i];
               jQuery($eachVi).change(function () {
                   var sumIncomVI = 0.0;
                   var number = jQuery(this).val();
                   if (jQuery.trim(number) != "" && !isNaN(number)) {
                       jQuery(this).val(parseFloat(number).toFixed(2));
                   }
                   else {
                       jQuery(this).val("0.00");
                   }
                   sumIncomVI = calculateRevenue("IncomVI");
                   jQuery("#INVI").val(sumIncomVI);
                   calculatorAll();
               });
           }
           var $IncomOSs = jQuery(".IncomOS");
           for (i = 0; i < $IncomOSs.length; i++) {
               var $eachOS = $IncomOSs[i];
               jQuery($eachOS).change(function () {
                   var sumIncomVI = 0.0;
                   var number = jQuery(this).val();
                   if (jQuery.trim(number) != "" && !isNaN(number)) {
                       jQuery(this).val(parseFloat(number).toFixed(2));
                   }
                   else {
                       jQuery(this).val("0.00");
                   }
                   sumIncomVI = calculateRevenue("IncomOS");
                   jQuery("#INOS").val(sumIncomVI);
                   calculatorAll();
               });
           }
           var $ExpenseVis = jQuery(".ExpenseVI");
           for (i = 0; i < $ExpenseVis.length; i++) {
               var $eachVi = $ExpenseVis[i];
               jQuery($eachVi).change(function () {
                   var sumIncomVI = 0.0;
                   var number = jQuery(this).val();
                   if (jQuery.trim(number) != "" && !isNaN(number)) {
                       jQuery(this).val(parseFloat(number).toFixed(2));
                   }
                   else {
                       jQuery(this).val("0.00");
                   }

                   jQuery("#EXTax").val(parseFloat(jQuery("#EXCommision2Shipper").val()) + parseFloat(jQuery("#EXCommision2Carrier").val()));
                   jQuery("#EXTax").val(parseFloat(jQuery("#EXTax").val() * parseFloat(jQuery("#TaxCommission").val()) / 100).toFixed(2));
                   sumIncomVI = calculateRevenue("ExpenseVI");
                   jQuery("#EXVI").val(sumIncomVI);
                   calculatorAll();
               });
           }
           var $ExpenseOSs = jQuery(".ExpenseOS");
           for (i = 0; i < $ExpenseOSs.length; i++) {
               var $eachVi = $ExpenseOSs[i];
               jQuery($eachVi).change(function () {
                   var sumIncomVI = 0.0;
                   var number = jQuery(this).val();
                   if (jQuery.trim(number) != "" && !isNaN(number)) {
                       jQuery(this).val(parseFloat(number).toFixed(2));
                   }
                   else {
                       jQuery(this).val("0.00");
                   }
                   sumIncomVI = calculateRevenue("ExpenseOS");
                   jQuery("#EXOS").val(sumIncomVI);
                   calculatorAll();
               });
           }
       });
    </script>
    <% if (!(bool)ViewData["SalesAction"])
       { %>
            <script type="text/javascript" language="javascript">
                jQuery(document).ready(function () {
                    jQuery(".IncomVI").attr('ReadOnly', 'true');
                    jQuery(".IncomOS").attr('ReadOnly', 'true');
                    jQuery(".ExpenseVI").attr('ReadOnly', 'true');
                    jQuery(".ExpenseOS").attr('ReadOnly', 'true');
                });
            </script>
       <%} %>
   <script language="javascript" type="text/javascript">
       jQuery(document).ready(function () {
           window.print();
       });
   </script>
</body>
</html>
