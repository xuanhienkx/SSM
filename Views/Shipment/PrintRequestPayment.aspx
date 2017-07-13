<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.RequestPaymentModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Request Payment</title>
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
   <div class="RequestPaymentContent Print">
   <table width="850px">
            <tr>
                <td>
                    <div style="overflow: hidden; line-height: 1px; height: auto; float: left; width: auto;">
                       <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
                     </div>
                </td>
                <td>
                <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: 400px">
                   <%= ViewData["CompanyHeader"]%>
                </div>
                </td>
                <td>
              
                </td>
            </tr>
        </table>
        <div class="Title">PHIẾU YÊU CẦU THANH TOÁN CƯỚC PHÍ
        </div>
        <div class="RefNO"><label class="PaymentLabel">REF: </label> <label class="Label">#<%= Model.ShipmentId %></label></div>
        <div class="DearTo">Kính gửi: Phòng kế toán</div>
        <div class="Element"><label class="PaymentLabel">Phòng</label><div class="PaymentDiv"><%= Model.DepartmentName%></div><label class="PaymentLabel">Xin trân trọng thông báo :</label></div>
        <div style="height:15px;"></div>
        <div class="Element"><label class="PaymentLabel">Lô hàng với số lượng và B/L số hoặc AWB số :</label><div class="PaymentDivShipmentNo"><%= Model.ShipmentCode%></div>
        <label class="PaymentLabel">HBL: </label><div class="PaymentDivBHL"><%= Model.BillNumber%></div>
        </div>
        <div class="Element"><label class="PaymentLabel">Của khách hàng .</label><div class="PaymentDivCustomer"><%=Model.CustomerName%></div></div>
        <div class="Element"><label class="PaymentLabel">Đi qua hãng tàu\ Hãng hàng không \ Co-loade</label><div class="PaymentDivCarrier"><%= Model.CarrierName%></div></div>
        <div class="Element"><label class="PaymentLabel">Cước</label><div class="PaymentDivFee"><%=Model.Fee%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Model.FeeCurrency%></div></div>
        <div class="Element"><label class="PaymentLabel" style="width:100px">Phí chứng từ</label><div class="PaymentDivTotal"><%= Model.DocFee%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Model.DocFeeCurrency%></div></div>
        <div class="Element"><label class="PaymentLabel" style="width:100px">Tổng cộng   </label><div class="PaymentDivTotal"><%= Model.Total1%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Model.Total1Currency%></div></div>
        <div class="Element"><div class="PaymentDivTotal" style="margin-left:140px;"><%= Model.Total2%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%=Model.Total2Currency%></div></div>
        <div class="Element"></div>
        <div class="Element"><label class="PaymentLabel">Đề nghị phòng tài vụ kế toán sắp xếp thanh toán cước phí trước ngày</label><div class="PaymentDivDate"><%= Model.PaidDate%></div></div>
        <div class="Element"><label class="PaymentLabel">Phương thức thanh toán</label><div class="PaymentDivType"><%= Model.PaidType%></div></div>
        <div style="height:15px;">
        <div class="Element"><label class="PaymentLabel">Ngày</label><label class="PaymentLabel"><%= DateTime.Now.Day %></label>
        <label class="PaymentLabel">Tháng</label><label class="PaymentLabel"><%= DateTime.Now.Month %></label>
        <label class="PaymentLabel">Năm</label><label class="PaymentLabel"><%=  DateTime.Now.Year %></label>
        <label class="PaymentLabel" style="margin-left:150px">Người đề nghị</label>
        </div>
        <div class="Element"></div>
        <div class="PaymentDiv" style="margin-left:400px;padding-left:50px"><%= Model.PaidPerson%></div>
        </div>
    </div>
   <script language="javascript" type="text/javascript">
       jQuery(document).ready(function () {
           window.print();
       });
   </script>
</body>
</html>

