<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.TransitmentAdvisedModel>" %>

<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Transitment Advised</title>
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
    <% using (Html.BeginForm())
        { %>
    <div class="TransitmentAdvisedContrainer">
        <div class="CompanyIcon">
            <img src="../../Images/transitment.png" /></div>
        <div class="RefZone">Ref #: <span style="font-weight: bold;"><%= Model.ShipmentId %></span></div>
        <div class="TransitmentTo"><span>To</span><div style="float: left;"><%= StringUtils.HtmlFilter(Model.AdvisedTo) %></div>
        </div>
        <div class="TransitmentATTN"><span>ATTN:</span><%: Model.AdvisedATTN %></div>
        <div class="TransitmentTitle">TRANSITMENT  ADVISED</div>
        <div class="WouldLike">We would like to inform that your cargo is loaded on the vessel as below</div>
        <div class="TransitmentBL"><span>* B\L :</span><%: Model.AdvisedBL %></div>
        <div class="GrossWeight">
            <div class="GrossWeightLeft">*</div>
            <div class="GrossWeightRight">
                <table>
                    <tr>
                        <td class="Title">Gross weight.kg</td>
                        <td>Measurement M3</td>
                    </tr>
                    <tr>
                        <td style="text-align: center;"><%: Model.Grossweight %></td>
                        <td style="text-align: center;"><%: Model.Measurement %></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="MainContent">
            <table border="1px solid Silver" style="width: 100%">
                <tr>
                    <th>Vessel
                    </th>
                    <th>Port of loading
                    </th>
                    <th>ETD
                    </th>
                    <th>Port of discharge
                    </th>
                    <th>ETA
                    </th>
                </tr>
                <tr>
                    <td><%= StringUtils.HtmlFilter(Model.Vessel)%>
                    </td>
                    <td><%=StringUtils.HtmlFilter(Model.Portofloading)%>
                    </td>
                    <td><%: Model.ETD %>
                    </td>
                    <td><%=StringUtils.HtmlFilter(Model.Portofdischarge) %>
                    </td>
                    <td><%: Model.ETA %>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div class="Note"><span>Note : the above information is provided by carrier.</span></div>
        <div class="Note"><span>Any change will be up-date to customer soon.</span></div>
        <div class="Thanhks"><span>Please contact us for detailed information</span></div>
        <div class="Thanhks"><span>Thanhks and best regards,</span></div>
        <div class="CustomerServices"><span>Customer Services</span></div>
        <div class="Tell"><span>Tell : </span><%: Model.CustomerServices %></div>
        <div class="PayCharge">
            <table>
                <tr>
                    <td style="width: 500px;">
                        <div class="PayChargeLeft">
                            <table>
                                <tr>
                                    <td colspan="2" style="font-weight: bold;">FAX OUT</td>
                                    <td colspan="3"></td>
                                </tr>
                                <tr>
                                    <td colspan="2">Times \ Date:</td>
                                    <td colspan="3"><%: Model.AdvisedTime %></td>
                                </tr>
                                <tr>
                                    <td>Status
                                    </td>
                                    <td>
                                        <%:Html.CheckBoxFor(m=>m.StatusOk) %>
                                    </td>
                                    <td>Ok
                                    </td>
                                    <td><%:Html.CheckBoxFor(m=>m.StatusNoReport) %>
                                    </td>
                                    <td>No Report 
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td><%:Html.CheckBoxFor(m=>m.StatusReport) %> 
                                    </td>
                                    <td>Report 
                                    </td>
                                    <td><%:Html.CheckBoxFor(m=>m.MachineNoReport) %>
                                    </td>
                                    <td>Machine no report
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <div class="PayChargeRight">
                            <div class="PayTitle">PLS PAY CHARGE AS BELOW</div>
                            <div class="PayContent">
                                <table>
                                    <tr>
                                        <td>* Freight:</td>
                                        <td><%:Model.ChargeFreight %></td>
                                    </tr>
                                    <tr>
                                        <td>* Bill: </td>
                                        <td><%:Model.ChargeBill %></td>
                                    </tr>
                                    <tr>
                                        <td>* THC:</td>
                                        <td><%:Model.ChargeTHC %></td>
                                    </tr>
                                    <tr>
                                        <td>* AMS:</td>
                                        <td><%:Model.ChargeAMS %></td>
                                    </tr>
                                    <tr>
                                        <td>* + VAT for above</td>
                                        <td><%:Model.ChargeVAT %></td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>
