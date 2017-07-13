<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.PaymentVocherModel>" %>

<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Payment Voucher</title>
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
    <div class="PaymentVoucherContent PaymentVoucherContentPrint">
        <div class="Header">
            <div class="CompanyIcon">
                <div style="overflow: hidden; line-height: 1px; height: auto; float: left; width: auto;">
                    <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
                </div>
            </div>
            <div class="Title">
                PAYMENT VOUCHER (PHIẾU CHI TRẢ)
            </div>
        </div>
        <div class="CompanyTitle"><%= Model.CompanyName%></div>
        <% Shipment Shipment1 = (Shipment)ViewData["Shipment"]; %>
        <div class="Element">
            <div class="ELabel">Trả cho</div>
            <div class="EContent"><%= Model.PaidTo%></div>
        </div>
        <div class="Element">
            <div class="ELabel">Số CMND</div>
            <div class="EContent"><%= Model.IdentifyNo%></div>
        </div>
        <div class="Element">
            <div class="ELabel">Thuộc Công Ty</div>
            <div class="EContent"><%= Model.BelongTo%></div>
        </div>
        <div class="Element">
            <div class="ELabel">Địa chỉ</div>
            <div class="EContent"><%= Model.Address1%></div>
        </div>
        <div class="Element">
            <div class="ELabel"></div>
            <div class="EContent"><%= Model.Address2%></div>
        </div>
        <div class="Element">
            <div class="ELabel2">Nội dung chi trả</div>
            <div class="EContent2"><%= Model.ContentPayment%></div>
        </div>
        <div class="Element">
            <div class="ELabel2">
                Lô hàng vận đơn số(HB\L) :
            </div>
            <div class="EContent2">
                <span><%= Shipment1.HouseNum %></span><span style="padding-left: 30px;">Số SSM ref : #<%= Model.ShipmentId %></span>
                <span style="padding-left: 100px;">Số lượng : <%= Shipment1.QtyNumber %> x <%=  Shipment1.QtyUnit%></span>
            </div>
        </div>
        <div class="Element">
            <div class="ELabel2">Tổng số tiền dịch vụ lô hàng</div>
            <div class="EContent2"><%= Model.TotalAmountService%></div>
        </div>
        <div class="Element">
            <div class="ELabel3">
                Đi từ
            </div>
            <div class="EContent3">
                <span><%= Shipment1.Area.AreaAddress %></span><span style="padding-left: 50px;">đến</span><span style="padding-left: 50px;"><%= Shipment1.Area1.AreaAddress %></span>
                <span style="padding-left: 50px;">Ngày giao hàng : </span><%= Model.DeliveryDate%>
            </div>
        </div>
        <div class="Element">
            <div class="ELabel">Số tiền</div>
            <div class="EContent"><%= Model.TotalAmount%></div>
        </div>
        <div class="Element">
            <div class="ELabel">Bằng chữ</div>
            <div class="EContent"><%= Model.TotalWords%></div>
        </div>
        <div class="Element">
            <div class="DateRegion"><span style="padding-right: 50px;">Ngày</span><span style="padding-right: 50px;">Tháng</span><span style="padding-right: 50px;">Năm</span></div>
        </div>
        <div class="Footer">
            <span>Approved</span>
            <span style="padding-left: 180px;">Người nhận</span>
            <span style="padding-left: 180px;">Người bán hàng</span>
        </div>
        <div class="Footer">
            <span></span>
            <span style="padding-left: 320px;"></span>
            <span style="padding-left: 160px;"><%= Shipment1.User.FullName %></span>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>
