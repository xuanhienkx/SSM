<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.HouseAirWayBillModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 HAirWayBill
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
                                    <%:Html.HiddenFor(m=>m.Id) %>
    <div class="HouseAirWayBill">
        <table class="HAirWayBill" border="1px solid #BFBFBF">
            <tr>
                <td style="vertical-align:top;">
                    <table class="HAirWayBillCommon">
                        <tr>
                            <td>Shipper's Name and Address
                            </td>
                            <td style="width:230px;border:1px solid #BFBFBF"><div>Shipper's Account Number</div>
                            <div><%: Html.TextBoxFor(m => m.ShipperAccount, new { Class="ShipperAccount"})%></div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <div><%: Html.TextAreaFor(m=>m.ShipperName, new{Class="ShipperAddress"}) %></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="AirWaybill">
                    <%: Html.TextAreaFor(m => m.IssueBy, new { Class = "ShipperAddress" })%>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="HAirWayBillCommon">
                        <tr>
                            <td>Consignee's Name and Address
                            </td>
                            <td style="width:230px;border:1px solid #BFBFBF"><div>Consignee's Account Number</div>
                            <div><%: Html.TextBoxFor(m => m.CneeAccount, new { Class = "ShipperAccount" })%></div>
                            </td>
                        </tr>
                        <tr>
                             <td colspan="2">
                            <div><%: Html.TextAreaFor(m => m.CneeName, new { Class = "ShipperAddress" })%></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td><div style="padding-left:10px;padding-top:10px;">It is agreed that the goods described herein are accepted in apparent good order and condition (except as 
                    noted) for carriage SUBJECT TO THE CONDITIONS OF CONTRACT ON THE REVERSE HEREOF. ALL 
                    GOODS MAY BE CARRIED BY ANY OTHER MEANS INCLUDING ROAD OR ANY OTHER CARRIER 
                    UNLESS SPECIFIC  CONTRARY INSTRUCTIONS ARE GIVEN HEREON BY THE SHIPPER, AND 
                    SHIPPER AGREES THAT THE SHIPMENT MAY BE CARRIED VIA INTERMEDIATE STOPPING PLACES 
                    WHICH THE CARRIER DEEMS APPROPRIATE. THE SHIPPER'S ATTENTION IS DRAWN TO THE 
                    NOTICE CONCERNING CARRIERS' LIMITATION OF LIABILITY. Shipper may increase such limitation of 
                    liability by declaring a higher value for carriage and paying a supplemental charge if required.
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table class="HAirWayBillCommon">
                        <tr>
                            <td colspan="2">
                            <div>Issuing Carrier's Agent Name and City</div>
                            <div><%: Html.TextAreaFor(m => m.Issuing, new { Class = "ShipperAddress" })%></div>
                            </td>
                        </tr>
                        <tr style="border-bottom:1px solid #BFBFBF;border-top:1px solid #BFBFBF;">
                            <td><span>Agent's IATA Code </span><%: Html.TextBoxFor(m => m.AgenIATACode, new { Class = "ByfirstCarrier" })%>
                            </td>
                            <td style="width:230px;border-left:1px solid #BFBFBF;"><span>Account No. </span><%: Html.TextBoxFor(m => m.AccountNo, new { Class = "ByfirstCarrier" })%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <div>Airport of Departure (Addr. of First Carrier) and Requested Routing</div>
                            <div><%: Html.TextAreaFor(m => m.AirportofDeparture, new { Class = "ShipperAddress" })%></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align:top;">
                <div>Accounting Information</div>
                <div><%: Html.TextAreaFor(m => m.AccountingInformation, new { Class = "ShipperAddress" })%></div>
                <div style="padding-top:70px;"><span style="font-weight:bold;font-size:18px;">MASTER AWB : </span><%: Html.TextBoxFor(m => m.MasterAWB, new { Class = "ByfirstCarrier" })%></div>
                </td>
            </tr>
        </table>
    <table class="HAirWayBill" border="1px solid #BFBFBF">
        <tr>
            <td>
                To
            </td>
            <td>By first Carrier Routing and Destination
            </td>
            <td>to
            </td>
            <td>by
            </td>
            <td>to
            </td>
            <td>by
            </td>
            <td>Currency
            </td>
            <td>CHGS<br />Code
            </td>
            <td colspan="2" style="text-align:center">WT/VAL
            </td>
            <td colspan="2" style="text-align:center">Other
            </td>
            <td>Declared Value for Carriage
            </td>
            <td>Declared Value for Customs
            </td>
        </tr>
        <tr>
            <td><%: Html.TextBoxFor(m => m.IssueTo, new { Class="IssueTo"})%>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.ByfirstCarrier, new { Class = "ByfirstCarrier" })%>
            </td>
            <td><%: Html.TextBoxFor(m => m.IssueTo2, new { Class="IssueTo"})%>
            </td>
            <td><%: Html.TextBoxFor(m => m.IssueBy2, new { Class="IssueTo"})%>
            </td>
            <td><%: Html.TextBoxFor(m => m.IssueTo3, new { Class="IssueTo"})%>
            </td>
            <td><%: Html.TextBoxFor(m => m.IssueBy3, new { Class="IssueTo"})%>
            </td>
            <td><%: Html.TextBoxFor(m => m.IssueCurrency, new { Class = "IssueCurrency" })%>
            </td>
            <td><%: Html.TextBoxFor(m => m.IssueCHGSCode, new { Class="IssueTo"})%>
            </td>
            <td>PPD<br /><%: Html.TextBoxFor(m => m.IssuePPD, new { Class="IssueTo"})%>
            </td>
            <td>COLL<br /><%: Html.TextBoxFor(m => m.IssueCOLL, new { Class="IssueTo"})%>
            </td>
            <td>PPD<br /><%: Html.TextBoxFor(m => m.IssuePPD2, new { Class = "IssueTo" })%>
            </td>
            <td>COLL<br /><%: Html.TextBoxFor(m => m.IssueCOLL2, new { Class = "IssueTo" })%>
            </td>
            <td><%: Html.TextBoxFor(m => m.CarriageValue, new { Class = "ByfirstCarrier" })%>
            </td>
            <td><%: Html.TextBoxFor(m => m.CustomsValue, new { Class = "ByfirstCarrier" })%>
            </td>
        </tr>
    </table>
        <table class="HAirWayBill" border="1px solid #BFBFBF">
            <tr>
                <td style="vertical-align:top;">
                    Airport of Destination
                    <br />
                    <br />
                    <%: Html.TextBoxFor(m => m.AirportofDestination, new { Class = "AirportofDestination" })%>
                </td>
                <td colspan="2" style="vertical-align:top;height:20px;width:400px;padding-top:0;">
                    <span>Flight/Date</span>
                    <span style="padding: 0 30px 5px 30px;border:1px solid #BFBFBF;background-color:#cee2fa;">For Carrier Use only</span>
                    <span>Flight/Date</span>
                    <br />
                    <br />
                    <%: Html.TextBoxFor(m => m.Flight, new { Class = "ByfirstCarrier" })%>  <%: Html.TextBoxFor(m => m.Date, new { Class = "ByfirstCarrier" })%>
                </td>
                <td style="vertical-align:top;">
                    Amount of Insurance
                    <br />
                    <br />
                    <%: Html.TextBoxFor(m => m.AmountofInsurance, new { Class = "ByfirstCarrier" })%>
                </td>
                <td style="width:350px;">
                    INSURANCE: If Carrier offers insurance and such insurance is requested in accordance
                    with conditions thereof, indicate amount to be insured in figures in box marked
                    "amount of insurance".
                </td>
            </tr>
        </table>
        <table  class="HAirWayBill" border="1px solid #BFBFBF">
        <tr><td><span>Handling Information</span><br />
        <%: Html.TextAreaFor(m => m.HandlingInformation, new { Class = "ShipperAddress HandlingInformation" })%>
        </td></tr>
        </table>
        <table class="HAirWayBill" border="1px solid #BFBFBF">
            <tr>
                <td rowspan="2">
                    No. of Pieces RCP
                </td>
                <td rowspan="2">
                    Gross Weight
                </td>
                <td rowspan="2">
                    kg lb
                </td>
                <td rowspan="4" style="width:5px;background-color:#cee2fa;">
                </td>
                <td colspan="2">Rate Class
                </td>
                <td rowspan="4" style="width:5px;background-color:#cee2fa;">
                </td>
                <td rowspan="2">Chargeable Weight
                </td>
                <td rowspan="4" style="width:5px;background-color:#cee2fa;">
                </td>
                <td rowspan="2">Rate / Charge
                </td>
                <td rowspan="4" style="width:5px;background-color:#cee2fa;">
                </td>
                <td rowspan="2">Total
                </td>
                <td rowspan="4" style="width:5px;background-color:#cee2fa;">
                </td>
                <td rowspan="2">Nature and Quantity of Goods 
                    (incl. Dimensions or Volume)
                </td>
            
            </tr>
            <tr>
                <td style="border-top:1px solid #fff;">
                </td>
                <td>Commodity
                </td>
            </tr>
            <tr>
                <td><%: Html.TextAreaFor(m => m.NofPieces, new { Class = "ShipperAddress TextSmall TextHeight" })%>
                </td>
                <td><%: Html.TextAreaFor(m => m.GrossWeight, new { Class = "ShipperAddress TextMedium TextHeight" })%>
                </td>
                <td rowspan="2"><%: Html.TextAreaFor(m => m.Kglb, new { Class = "ShipperAddress TextVerySmall TextHeighter" })%>
                </td>
                <td rowspan="2"><%: Html.TextAreaFor(m => m.RateClass, new { Class = "ShipperAddress TextVerySmall TextHeighter" })%>
                </td>
                <td rowspan="2"><%: Html.TextAreaFor(m => m.CommodityItemNo, new { Class = "ShipperAddress TextMedium TextHeighter" })%>
                </td>
                <td rowspan="2"><%: Html.TextAreaFor(m => m.ChargeableWeight, new { Class = "ShipperAddress TextMedium TextHeighter" })%>
                </td>
                <td rowspan="2"><%: Html.TextAreaFor(m => m.RateCharge, new { Class = "ShipperAddress TextMedium TextHeighter" })%>
                </td>
                <td ><%: Html.TextAreaFor(m => m.Total, new { Class = "ShipperAddress TextRatherBig TextHeight" })%>
                </td>
                <td rowspan="2"><%: Html.TextAreaFor(m => m.QuantityofGoods, new { Class = "ShipperAddress TextBig TextHeighter" })%>
                </td>
                
            </tr>
            <tr>
                <td><%: Html.TextBoxFor(m => m.NofPieces2, new { Class = "ShipperAccount TextSmall" })%>
                </td>
                <td><%: Html.TextBoxFor(m => m.GrossWeight2, new { Class = "ShipperAccount TextMedium" })%>
                </td>
                <td><%: Html.TextBoxFor(m => m.Total2, new { Class = "ShipperAccount TextRatherBig" })%>
                </td>
            </tr>
        </table>
        <table class="HAirWayBill" border="1px solid #BFBFBF">
            <tr>
                <td colspan="2" style="font-weight:bold;text-align:center;font-size:11px;background-color:#cee2fa;">
                    Prepaid
                </td>
                <td colspan="2" style="font-weight:bold;text-align:center;font-size:11px;background-color:#cee2fa;">
                    Collect
                </td>
                <td style="width:610px;">
                    Other Charges
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding:0;">
                   <div style="margin: 0 100px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;"> Weight Charge</div>
                </td>
               
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.WeightPrepaid, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.WeightCollect, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td style="border-top:1px solid #fff;"><%: Html.TextBoxFor(m => m.OtherCharges, new { Class = "OtherCharges" })%>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding:0;"> 
                <div style="margin: 0 80px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Valuation Charge</div>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.ValuationPrepaid, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.ValuationCollect, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td style="border-top:1px solid #fff;"><%: Html.TextBoxFor(m => m.OtherCharges1, new { Class = "OtherCharges" })%>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding:0;"> <div style="margin: 0 120px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Tax</div>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.TaxPrepaid, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.TaxCollect, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td style="border-top:1px solid #fff;"><%: Html.TextBoxFor(m => m.OtherCharges2, new { Class = "OtherCharges" })%>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding:0;"> <div style="margin: 0 60px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">
                Total Other Charges Due Agent</div>
                </td>
                <td rowspan="5" style="width:600px;vertical-align:top;">
                <div>I hereby certify that the particulars on the face hereof are correct and that insofar as any part of the consignment 
                    contains dangerous goods, I hereby certify that the contents of the consignment are fully and accurately described 
                    above by proper shipping names and are classified, packaged, marked and labeled, and in proper condition for 
                    carriage by air according to the applicable national governmental regulations.</div>
                    <br />
                    <br />
                <div style="margin-left:150px;border-bottom: 1px dotted silver;text-align:center;"><%: Html.TextBoxFor(m => m.SignatureShippe, new { Class = "ShipperAccount" })%></div>
                <div style="margin-left:150px;text-align:center;">Signature of Shipper or his Agent</div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.AgentPrepaid, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.AgentCollect, new { Class = "ByfirstCarrier" })%></div>
                </td>
            </tr>
            <tr>
               <td colspan="4" style="padding:0;">
                <div style="margin: 0 60px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Total Other Charges Due Carrier</div>
                </td>
                
            </tr>
            <tr>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.CarrierPrepaid, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%: Html.TextBoxFor(m => m.CarrierCollect, new { Class = "ByfirstCarrier" })%></div>
                </td>
            </tr>
            <tr style="Height:30px;">
                <td colspan="2" style="background-color:#cee2fa;">
                </td>
                <td colspan="2" style="background-color:#cee2fa;">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:0;"><div style="margin: 0 20px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Total Prepaid</div>
                <div style="text-align:center;"><%: Html.TextBoxFor(m => m.TotalPrepaid, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="padding:0;"><div style="margin: 0 20px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Total Collect</div>
                <div style="text-align:center;"><%: Html.TextBoxFor(m => m.TotalCollect, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td rowspan="2">
                    <div style="height:40px;border-bottom:1px dotted #BFBFBF;width:100%;"></div>
                    <div>
                        <span>Executed on (date)</span><%: Html.TextBoxFor(m => m.Executedon, new { Class = "ByfirstCarrier" })%>
                        <span>at (place)</span><%: Html.TextBoxFor(m => m.Executedat, new { Class = "ByfirstCarrier" })%>
                        <span>Signature of Issuing Carrier or its Agent</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:0;background-color:#cee2fa;"><div style="margin: 0 10px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Currency Conversion Rates</div>
                <div style="text-align:center;"><%: Html.TextBoxFor(m => m.CurrencyConversion, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td colspan="2" style="padding:0;background-color:#cee2fa;"><div style="margin: 0 10px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">CC Charges in Dest. Currency</div>
                <div style="text-align:center;"><%: Html.TextBoxFor(m => m.CCChargesinDest, new { Class = "ByfirstCarrier" })%></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="background-color:#cee2fa;">For Carriers Use only at Destination
                </td>
                <td colspan="2" style="background-color:#cee2fa;"><div>Charges at Destinatio</div>
                <div><%: Html.TextBoxFor(m => m.ChargesatDestination, new { Class = "ByfirstCarrier" })%></div>
                </td>
                <td>
                <div style="float:left;width:160px;background-color:#cee2fa;">Total Collect Charges
                <br />
                <%: Html.TextBoxFor(m => m.TotalCollectCharges, new { Class = "ByfirstCarrier" })%>
                </div>
                <div style="float:left;width:160px;vertical-align:middle;height:30px;overflow:hidden;"><br /><span style="font-weight:bold;font-size:18px;">HOUSE AWB :</span></div>
                <div  style="float:left;width:160px;vertical-align:middle;height:30px;overflow:hidden;"><br /><%: Html.TextBoxFor(m => m.HouseAWB, new { Class = "ByfirstCarrier" })%></div>
                </td>
            </tr>
        </table>
    </div>
    <div class="ButtonZone">
        <div class="DocLinkButton">
        <%: Html.ActionLink("Shipment", "Edit", "Shipment", new { id = Model.ShipmentId }, new { Class = "LinkForm" })%>
        </div>
        <div class="DocLinkButton">
        <%: Html.ActionLink("Revenue", "Revenue", "Shipment", new { id = Model.ShipmentId }, new { Class = "LinkForm" })%>
        </div>
        <div class="DocLinkButton">
        <a href="#" onclick="jQuery('#submitButton').click();" class="LinkForm" title="Update Document">Update</a>
        <input id="submitButton" type="submit" value="Update" title="Update Document" style="display:none"/>
        </div>
        <div class="DocLinkButton">
        <%: Html.ActionLink("Print", "PrintHAirWayBill", "Shipment", new { id = 0, ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
      <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            CKEDITOR.replace('IssueBy',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            new DateTimePicker('DebitDate', 'dd/MM/yyyy');
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#HAirWayBill").addClass("LinkActive");
        });
    </script>
</asp:Content>
