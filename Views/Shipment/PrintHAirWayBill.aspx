<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.HouseAirWayBillModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Air Way Bill</title>
    <link id="Link1" href="~/Content/PrintPage/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/PrintPage/global.css" media="all" runat="server" />
    <link id="Link12" type="text/css" rel="stylesheet" href="~/Content/PrintPage/top-nav-bar.css" media="all" runat="server" />
    <link id="Link13" type="text/css" rel="stylesheet" href="~/Content/PrintPage/main-nav-bar.css" media="all" runat="server" />
    <link id="Link14" type="text/css" rel="stylesheet" href="~/Content/PrintPage/homepage.css" media="all" runat="server" />
    <link id="Link15" type="text/css" rel="stylesheet" href="~/Content/PrintPage/section-block.css" media="all" runat="server" />
    <link id="Link16" type="text/css" rel="stylesheet" href="~/Content/PrintPage/footer-bar.css" media="all" runat="server" />
    <link id="Link17" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server" />
    <link id="Link18" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Shipment.css" media="all" runat="server" />
      <link id="Link10" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Freights.css" media="all" runat="server" />
        <link id="Link11" type="text/css" rel="stylesheet" href="~/Content/PrintPage/BookingConfirm.css" media="all" runat="server" />
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-2.1.4.min.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-ui-core.js") %>"></script>
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-ui.js") %>"></script>
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
<% using (Html.BeginForm())
   { %>

   <div class="HouseAirWayBill HouseAirWayBillPrint">
        <table class="HAirWayBill" border="1px solid #BFBFBF">
            <tr>
                <td style="vertical-align:top;">
                    <table class="HAirWayBillCommon">
                        <tr>
                            <td>Shipper's Name and Address
                            </td>
                            <td style="width:230px;border:1px solid #BFBFBF"><div>Shipper's Account Number</div>
                            <div><%= Model.ShipperAccount%></div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                            <div><%= Model.ShipperName %></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td class="AirWaybill">
                    <%= Model.IssueBy%>
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    <table class="HAirWayBillCommon">
                        <tr>
                            <td>Consignee's Name and Address
                            </td>
                            <td style="width:230px;border:1px solid #BFBFBF"><div>Consignee's Account Number</div>
                            <div><%= Model.CneeAccount%></div>
                            </td>
                        </tr>
                        <tr>
                             <td colspan="2">
                            <div><%= Model.CneeName%></div>
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
                <td style="vertical-align:top;">
                    <table class="HAirWayBillCommon">
                        <tr style="min-height:70px;">
                            <td colspan="2" style="vertical-align:top;">
                            <div>Issuing Carrier's Agent Name and City</div>
                            <br />
                            <div><%= Model.Issuing%></div>
                            </td>
                        </tr>
                        <tr style="border-bottom:1px solid #BFBFBF;border-top:1px solid #BFBFBF;">
                            <td><span>Agent's IATA Code </span><%= Model.AgenIATACode%>
                            </td>
                            <td style="width:230px;border-left:1px solid #BFBFBF;height:30px;"><span>Account No. </span><%= Model.AccountNo%>
                            </td>
                        </tr>
                        <tr style="min-height:40px;">
                            <td colspan="2">
                            <div>Airport of Departure (Addr. of First Carrier) and Requested Routing</div>
                            <div><%= Model.AirportofDeparture%></div>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align:top;">
                <div>Accounting Information</div>
                <div><%= Model.AccountingInformation%></div>
                <div style="padding-top:70px;"><span style="font-weight:bold;font-size:18px;">MASTER AWB : </span><%= Model.MasterAWB%></div>
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
        <tr style="vertical-align:top;">
            <td style="padding:0;border-top: 1px solid #fff;width:30px;height:40px;"><%= Model.IssueTo%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;">
                <%= Model.ByfirstCarrier%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;width:30px;"><%= Model.IssueTo2%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;width:30px;"><%= Model.IssueBy2%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;width:30px;"><%= Model.IssueTo3%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;width:30px;"><%= Model.IssueBy3%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;"><%= Model.IssueCurrency%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;"><%= Model.IssueCHGSCode%>
            </td>
            <td>PPD<br /><%= Model.IssuePPD%>
            </td>
            <td>COLL<br /><%= Model.IssueCOLL%>
            </td>
            <td>PPD<br /><%= Model.IssuePPD2%>
            </td>
            <td>COLL<br /><%= Model.IssueCOLL2%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;"><%= Model.CarriageValue%>
            </td>
            <td style="padding:0;border-top: 1px solid #fff;"><%= Model.CustomsValue%>
            </td>
        </tr>
    </table>
        <table class="HAirWayBill" border="1px solid #BFBFBF">
            <tr>
                <td style="vertical-align:top;width:200px;text-align: center;">
                    Airport of Destination
                    <br />
                    <br />
                    <%= Model.AirportofDestination%>
                </td>
                <td colspan="2" style="vertical-align:top;height:20px;width:400px;padding-top:0;text-align: center;">
                    <span>Flight/Date</span>
                    <span style="padding: 0 30px 5px 30px;border:1px solid #BFBFBF;background-color:#cee2fa;">For Carrier Use only</span>
                    <span>Flight/Date</span>
                    <br />
                    <div style="padding-right:50px;width:100px;height:30px;border-right:1px solid #BFBFBF;"><%= Model.Flight%></div>  <%= Model.Date%>
                </td>
                <td style="vertical-align:top;text-align: center;width:140px">
                    Amount of Insurance
                    <br />
                    <br />
                    <%= Model.AmountofInsurance%>
                </td>
                <td style="width:350px;">
                    INSURANCE: If Carrier offers insurance and such insurance is requested in accordance
                    with conditions thereof, indicate amount to be insured in figures in box marked
                    "amount of insurance".
                </td>
            </tr>
        </table>
        <table  class="HAirWayBill" border="1px solid #BFBFBF">
        <tr style="height:80px;vertical-align:top;"><td><span>Handling Information</span><br />
        <%= Model.HandlingInformation%>
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
            <tr style="height:210px;">
                <td style="width:50px;"><%= Model.NofPieces%>
                </td>
                <td style="width:100px;"><%= Model.GrossWeight%>
                </td>
                <td rowspan="2" style="width:30px;"><%= Model.Kglb%>
                </td>
                <td rowspan="2" style="width:30px;"><%= Model.RateClass%>
                </td>
                <td rowspan="2" style="width:100px;"><%= Model.CommodityItemNo%>
                </td>
                <td rowspan="2" style="width:100px;"><%= Model.ChargeableWeight%>
                </td>
                <td rowspan="2" style="width:100px;"><%= Model.RateCharge%>
                </td>
                <td style="width:150px;"><%= Model.Total%>
                </td>
                <td rowspan="2"><%= Model.QuantityofGoods%>
                </td>
                
            </tr>
            <tr style="height:30px;">
                <td><%= Model.NofPieces2%>
                </td>
                <td><%= Model.GrossWeight2%>
                </td>
                <td><%= Model.Total2%>
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
            <tr style="height:25px;">
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.WeightPrepaid%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.WeightCollect%></div>
                </td>
                <td style="border-top:1px solid #fff;"><%= Model.OtherCharges%>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding:0;"> 
                <div style="margin: 0 80px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Valuation Charge</div>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height:25px;">
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.ValuationPrepaid%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.ValuationCollect%></div>
                </td>
                <td style="border-top:1px solid #fff;"><%= Model.OtherCharges1%>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="padding:0;"> <div style="margin: 0 120px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Tax</div>
                </td>
                <td>
                </td>
            </tr>
            <tr style="height:25px;">
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.TaxPrepaid%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.TaxCollect%></div>
                </td>
                <td style="border-top:1px solid #fff;"><%= Model.OtherCharges2%>
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
                <div style="margin-left:150px;border-bottom: 1px dotted silver;text-align:center;"><%= Model.SignatureShippe%></div>
                <div style="margin-left:150px;text-align:center;">Signature of Shipper or his Agent</div>
                </td>
            </tr>
            <tr style="height:25px;">
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.AgentPrepaid%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.AgentCollect%></div>
                </td>
            </tr>
            <tr>
               <td colspan="4" style="padding:0;">
                <div style="margin: 0 60px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Total Other Charges Due Carrier</div>
                </td>
                
            </tr>
            <tr style="height:25px;">
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.CarrierPrepaid%></div>
                </td>
                <td colspan="2" style="border-top:1px solid #fff;text-align:center;"><div><%= Model.CarrierCollect%></div>
                </td>
            </tr>
            <tr style="Height:30px;">
                <td colspan="2" style="background-color:#cee2fa;">
                </td>
                <td colspan="2" style="background-color:#cee2fa;">
                </td>
            </tr>
            <tr style="height:30px;">
                <td colspan="2" style="padding:0;"><div style="margin: 0 20px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Total Prepaid</div>
                <div style="text-align:center;height:25px;"><%= Model.TotalPrepaid%></div>
                </td>
                <td colspan="2" style="padding:0;"><div style="margin: 0 20px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Total Collect</div>
                <div style="text-align:center;height:25px;"><%= Model.TotalCollect%></div>
                </td>
                <td rowspan="2">
                    <div style="height:40px;border-bottom:1px dotted #BFBFBF;width:100%;"></div>
                    <div>
                        <span>Executed on (date)</span><span style="width:180px;text-align:center;padding:0 30px;font-weight:bold;"><%= Model.Executedon%></span>
                        <span>at (place)</span><span style="width:180px;text-align:center;padding:0 30px;font-weight:bold;"><%= Model.Executedat%></span>
                        <span>Signature of Issuing Carrier or its Agent</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="padding:0;background-color:#cee2fa;"><div style="margin: 0 10px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">Currency Conversion Rates</div>
                <div style="text-align:center;height:25px;"><%= Model.CurrencyConversion%></div>
                </td>
                <td colspan="2" style="padding:0;background-color:#cee2fa;"><div style="margin: 0 10px;border:1px solid #BFBFBF;text-align:center;background-color:#cee2fa;">CC Charges in Dest. Currency</div>
                <div style="text-align:center;height:30px;"><%= Model.CCChargesinDest%></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;background-color:#cee2fa;">For Carriers Use only <br /> at Destination
                </td>
                <td colspan="2" style="background-color:#cee2fa;"><div>Charges at Destinatio</div>
                <div style="text-align:center;height:25px;"><%= Model.ChargesatDestination%></div>
                </td>
                <td>
                <div style="float:left;width:160px;background-color:#cee2fa;min-height:40px;">Total Collect Charges
                <br />
                <div style="text-align:center;background-color:#cee2fa;"><%= Model.TotalCollectCharges%></div>
                </div>
                <div style="float:left;width:160px;vertical-align:middle;height:30px;"><br /><span style="font-weight:bold;font-size:18px;">HOUSE AWB :</span></div>
                <div  style="float:left;width:160px;vertical-align:middle;font-size:16px;padding-top:15px;"><span style="font-weight:bold;font-size:16px;"><%= Model.HouseAWB%></span></div>
                </td>
            </tr>
        </table>
    </div>

   <%} %>
      <script language="javascript" type="text/javascript">
          jQuery(document).ready(function () {
             //window.print();
          });
   </script>
</body>
</html>