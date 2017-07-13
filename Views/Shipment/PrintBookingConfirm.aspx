<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BookingConfirmModel>" %>

<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Booking Confirm</title>
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
<body style="margin-left: 40px; margin-top: 15px">

    <div class="Header">
        <div class="CompanyIcon">
            <div style="overflow: hidden; line-height: 1px; height: auto; float: left; width: auto;">
                <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
            </div>


            <div class="BookDivBody">
                <div class="NormalZone BookRefNo">
                    <div class="BookRefNoText NormalZone"><%= Model.ShipmentId %></div>
                    <div class="BookRefNoLabel NormalZone">Ref No:</div>
                </div>
                <div class="NormalZone">
                    <div class="BookDateText NormalZone"><%= DateTime.Now.ToString("dd/MM/yyyy") %></div>
                    <div class="BookDate NormalZone">DATE:</div>
                </div>
                <div class="BookTo NormalZone">
                    <div class="BookToLabel NormalZone">To:</div>
                    <div class="BookToContent NormalZone"><%= StringUtils.HtmlFilter(Model.BookTo)%></div>
                </div>
                <div class="BookFrom NormalZone">
                    <div class="BookFromLabel NormalZone">From:</div>
                    <div class="BookFromContent NormalZone"><%= StringUtils.HtmlFilter(Model.BookFrom)%></div>
                </div>
                <div class="BookConfirmText NormalZone">
                    BOOKING CONFIRMATION
                </div>
                <div class="NormalZone ThankFor">Thank you very much for your support to our service</div>
                <div class="NormalZone WouldLike">
                    We would like to inform you that we have booked space for you shipment on the flight<br />
                    with the details as follows
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">DESTINATION:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.Destination%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">COMMODITY:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.Commodity%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">QUANTITY / WEIGHT:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.Quantity%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">FLIGHT No:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.FlyNo%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">FLIGHT / DATE:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.FlightDate%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">LOADING DATE:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.LoadingDate%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">CLOSING TIME:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.ClosingDate%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookDestination">Wearehouse:</div>
                    <div class="NormalZone BookDestinationText"><%= Model.Wearehouse%></div>
                </div>

                <div class="NormalZone BookFobCharge">FOB CHARGE:</div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookFobChargeLabel">1 - AIRPORT CHARGES:</div>
                    <div class="NormalZone BookFobChargeText"><%= Model.AirportCharge%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookFobChargeLabel">2 - X_PRAY:</div>
                    <div class="NormalZone BookFobChargeText"><%= Model.XPray%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookFobChargeLabel">3 - AWB FEE</div>
                    <div class="NormalZone BookFobChargeText"><%= Model.AWBFee%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookFobChargeLabel">4 - HANDLING</div>
                    <div class="NormalZone BookFobChargeText"><%= Model.HandingCharge%></div>
                </div>
                <div class="NormalZone">
                    <div class="NormalZone PrintBookFobChargeLabel">5 - AMS</div>
                    <div class="NormalZone BookFobChargeText"><%= Model.AMSCharge%></div>
                </div>
                <div class="NormalZone BookContact">
                    <div class="NormalZone BookContactLabel">For loading cargo, please contact:</div>
                    <div class="NormalZone BooKConfirmText"><%= Model.Contact%></div>
                </div>
                <div class="NormalZone BookRegard">
                    Best Regards
                </div>
                <div class="NormalZone">
                    <%= Model.AuthoWord%>
                </div>
                <div style="clear: both; padding-top: 30px;">
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>
