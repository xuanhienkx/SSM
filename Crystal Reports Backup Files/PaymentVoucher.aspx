<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.PaymentVocherModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Payment Voucher
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 <% using( Html.BeginForm()) { %>
<% RevenueModel RevenueModel1 = new RevenueModel();
   RevenueModel1.Id = (long)Model.ShipmentId;
   Shipment Shipment1 = (Shipment)ViewData["Shipment"];
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
    <div class="PaymentVoucherContent">
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
        <div style="font-style:italic;font-weight:bold">Company Name</div>
        <div class="CompanyTitle"><%: Html.TextBoxFor(m => m.CompanyName, new { Class = "PaymentVoucherInputLong" })%></div>
        <div class="Element"><div class="ELabel">Trả cho</div><div class="EContent"><%: Html.TextBoxFor(m => m.PaidTo, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="ELabel">Số CMND</div><div class="EContent"><%: Html.TextBoxFor(m => m.IdentifyNo, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="ELabel">Thuộc Công Ty</div><div class="EContent"><%: Html.TextBoxFor(m => m.BelongTo, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="ELabel">Địa chỉ</div><div class="EContent"><%: Html.TextBoxFor(m => m.Address1, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="ELabel"></div><div class="EContent"><%: Html.TextBoxFor(m => m.Address2, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="ELabel2">Nội dung chi trả</div><div class="EContent2"><%: Html.TextBoxFor(m => m.ContentPayment, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element">
            <div class="ELabel2">
                Lô hàng vận đơn số(HB\L) :</div>
            <div class="EContent2">
                <span> <%= Shipment1.HouseNum %></span><span style="padding-left:30px;">Số SSM ref : #<%= Model.ShipmentId %></span>
                <span style="padding-left:100px;">Số lượng : <%= Shipment1.QtyNumber %> x <%=  Shipment1.QtyUnit%></span>
            </div>
        </div>
        <div class="Element"><div class="ELabel2">Tổng số tiền dịch vụ lô hàng</div><div class="EContent2"><%: Html.TextBoxFor(m => m.TotalAmountService, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element">
            <div class="ELabel3">
                Đi từ </div>
            <div class="EContent3">
                 <span><%= Shipment1.Area.AreaAddress %></span><span style="padding-left:50px;">đến</span><span style="padding-left:50px;"><%= Shipment1.Area1.AreaAddress %></span>
                <span style="padding-left:50px;"> Ngày giao hàng : </span><%: Html.TextBoxFor(m => m.DeliveryDate, new { Class = "PaymentVoucherDeliveryDate" })%>
            </div>
        </div>
        <div class="Element"><div class="ELabel">Số tiền</div><div class="EContent"><%: Html.TextBoxFor(m => m.TotalAmount, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="ELabel">Bằng chữ</div><div class="EContent"><%: Html.TextBoxFor(m => m.TotalWords, new { Class = "PaymentVoucherInputLong" })%></div></div>
        <div class="Element"><div class="DateRegion"><span style="padding-right:50px;">Ngày</span><span style="padding-right:50px;">Tháng</span><span style="padding-right:50px;">Năm</span></div></div>
        <div class="Footer">
            <span>Approved</span>
            <span style="padding-left:180px;">Người nhận</span>
            <span style="padding-left:180px;">Người bán hàng</span>
        </div>
        <div class="Footer">
            <span></span>
            <span style="padding-left:320px;"></span>
            <span style="padding-left:180px;"><%= Shipment1.User.FullName %></span>
        </div>
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
        <%: Html.ActionLink("Print", "PrintPaymentVoucher", "Shipment", new { ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
 <%} %>
 <script language="javascript" type="text/javascript">
     jQuery(document).ready(function () {
         jQuery("#FileTab").addClass("Active");
         jQuery('#FileTab').activeThisNav();
         jQuery("#DocumentMenuContainer .LinkClose").hide();
         jQuery("#PaymentVoucher").addClass("LinkActive");
     });
    </script>
</asp:Content>
