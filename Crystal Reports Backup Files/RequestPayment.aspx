<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.RequestPaymentModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 RequestPayment
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
    <div class="RequestPaymentContent">
        <div class="Title">PHIẾU YÊU CẦU THANH TOÁN CƯỚC PHÍ
        </div>
        <div class="RefNO"><label class="PaymentLabel">REF: </label> <label class="Label">#<%= Model.ShipmentId %></label></div>
        <div class="DearTo">Kính gửi: Phòng kế toán</div>
        <div class="Element"><label class="PaymentLabel">Phòng</label><div class="PaymentDiv"><%= Html.TextBoxFor(m => m.DepartmentName, new { Class="ShipmentInput"})%></div><label class="PaymentLabel">Xin trân trọng thông báo :</label></div>
        <div class="Element"></div>
        <div class="Element"><label class="PaymentLabel">Lô hàng với số lượng và B/L số hoặc AWB số :</label><div class="PaymentDivShipmentNo"><%= Html.TextBoxFor(m => m.ShipmentCode, new { Class="ShipmentInput"})%></div>
        <label class="PaymentLabel">HBL: </label><div class="PaymentDivBHL"><%= Html.TextBoxFor(m => m.BillNumber, new { Class="ShipmentInput"})%></div>
        </div>
        <div class="Element"><label class="PaymentLabel">Của khách hàng .</label><div class="PaymentDivCustomer"><%= Html.TextBoxFor(m => m.CustomerName, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"><label class="PaymentLabel">Đi qua hãng tàu\ Hãng hàng không \ Co-loade</label><div class="PaymentDivCarrier"><%= Html.TextBoxFor(m => m.CarrierName, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"><label class="PaymentLabel">Cước</label><div class="PaymentDivFee"><%= Html.TextBoxFor(m => m.Fee, new { Class="ShipmentInput"})%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Html.TextBoxFor(m => m.FeeCurrency, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"><label class="PaymentLabel" style="width:100px">Phí chứng từ</label><div class="PaymentDivTotal"><%= Html.TextBoxFor(m => m.DocFee, new { Class="ShipmentInput"})%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Html.TextBoxFor(m => m.DocFeeCurrency, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"><label class="PaymentLabel" style="width:100px">Tổng cộng   </label><div class="PaymentDivTotal"><%= Html.TextBoxFor(m => m.Total1, new { Class="ShipmentInput"})%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Html.TextBoxFor(m => m.Total1Currency, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"><div class="PaymentDivTotal" style="margin-left:140px;"><%= Html.TextBoxFor(m => m.Total2, new { Class="ShipmentInput"})%></div><label class="PaymentLabel">Loại tiền</label><div class="PaymentDivCurrency"><%= Html.TextBoxFor(m => m.Total2Currency, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"></div>
        <div class="Element"><label class="PaymentLabel">Đề nghị phòng tài vụ kế toán sắp xếp thanh toán cước phí trước ngày</label><div class="PaymentDivDate"><%= Html.TextBoxFor(m => m.PaidDate, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"><label class="PaymentLabel">Phương thức thanh toán</label><div class="PaymentDivType"><%= Html.TextBoxFor(m => m.PaidType, new { Class="ShipmentInput"})%></div></div>
        <div class="Element"></div>
        <div class="Element"><label class="PaymentLabel">Ngày</label><label class="PaymentLabel"><%= DateTime.Now.Day %></label>
        <label class="PaymentLabel">Tháng</label><label class="PaymentLabel"><%= DateTime.Now.Month %></label>
        <label class="PaymentLabel">Năm</label><label class="PaymentLabel"><%=  DateTime.Now.Year %></label>
        <label class="PaymentLabel" style="margin-left:200px">Người đề nghị</label>
        </div>
        <div class="Element">
        <div class="PaymentDiv" style="margin-left:500px;padding-left:50px"><%= Html.TextBoxFor(m => m.PaidPerson, new { Class="ShipmentInput"})%></div>
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
        <%: Html.ActionLink("Print", "PrintRequestPayment", "Shipment", new { id = 0, ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
    <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            new DateTimePicker('DebitDate', 'dd/MM/yyyy');
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#RequestPayment").addClass("LinkActive");

        });
    </script>
</asp:Content>
