<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ArriveNoticeModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Arrive Notice
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="ArriveDetailFormZone" class="ArriveNoticeBody">
<% using( Html.BeginForm()) { %>
<% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
    <div style="height:auto;overflow:hidden;padding:15px 0 15px 0;font-family:Arial Baltic;font-weight:bold;background-image:none url(../../Images/embassy.gif)">
    </div>
    <table border="0" cellpadding="3" width="100%">
        <tr>
            <td colspan="2">
                <table width="100%">
                    <tr>
                        <td style="text-align: right">
                            <label style="font-family: Arial Baltic; font-size: 17px; font-weight: bold;">
                                REF #:
                                <%="S "+ Request.Params.Get(0) %></label>
                        </td>
                        <td rowspan="2" style="width:150px;">
                             <div style="height: auto; overflow: hidden;padding:0 10px;text-align:left;font-size:20px;font-weight:bold;">
                        <%: Html.CheckBoxFor(m=>m.Logo) %><span>Logo</span><br />
                        <%: Html.CheckBoxFor(m=>m.Header) %><span>Header</span><br />
                    </div>
                        </td>
                    </tr>
                    <tr>
                        <td  style="text-align: center">
                            <label style="font-family: Arial Baltic; font-size: 20px; font-weight: bold;">
                                GIẤY BÁO HÀNG ĐẾN</label>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        
        <tr>
            <td colspan="2"><%: Html.TextAreaFor(m => m.CompanyAddress, new { Class = "ShipmentTextArea", style="width:350px;" })%>
            </td>
        </tr>
        <tr>
            <td><label class="Label">Xin thông báo đến Quý Công Ty : </label>
            </td>
            <td><%: Html.TextAreaFor(m => m.CompanyName, new { Class = "ShipmentTextArea", style="width:350px;" })%>
            </td>
        </tr>
    </table>
    <table border="0" cellpadding="3" >
        <tr>
            <td><label class="Label">Lô hàng thuộc Bill số :</label>
            </td>
            <td>
            <%: Html.TextBoxFor(m => m.BillNumber, new { Class = "ShipmentInput" })%>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td><label class="Label">Tên tàu / Chuyến bay :</label>
            </td>
            <td>
            <%: Html.TextBoxFor(m => m.ShiperName, new { Class = "ShipmentInput" })%>
            </td>
            <td>
            </td>
            <td>
            <label class="Label">Số Chuyến</label>
            </td>
            <td>
            <%: Html.TextBoxFor(m => m.ShiperNumber, new { Class = "ShipmentInput" })%>
            </td>
        </tr>
        <tr>
            <td><label class="Label">ETA :</label>
            </td>
            <td>
            <%: Html.TextBoxFor(m => m.ETA, new { Class = "ShipmentInput" })%>
            </td>
            <td><label for="ETA" class="DateInput"></label>
            </td>
            <td>
                <label class="Label">Cập cảng / Sân bay :</label>
            </td>
            <td>
            <%: Html.TextBoxFor(m => m.PortTo, new { Class = "ShipmentInput" })%>
            </td>
        </tr>
    </table>
    <div style="height:auto;overflow:hidden;padding:15px 0 15px 0;font-family:Arial Baltic;font-weight:bold;">
    <table border="1px solid #BFBFBF" class="TableNormal" width="700px" cellpadding="3">
        <tr style="background: #338899;">
            <th>
                Số Cont / Seat
            </th>
            <th>
                Số Kiện
            </th>
            <th>
                Tên Hàng Hóa
            </th>
            <th>
                T.Lượng
            </th>
            <th>
                K.Lượng
            </th>
        </tr>
      <tr>
                <td>
                <%: Html.TextAreaFor(m => m.ShippingMark, new { Class = "Bill3TextArea" })%>
                </td>
                <td>
                <%: Html.TextAreaFor(m => m.NoCTNS, new { Class = "Bill3TextArea" })%>
                </td>
                <td>
                <%: Html.TextAreaFor(m => m.GoodsDescription, new { Class = "Bill1TextArea" })%>
                </td>
                <td>
                <%: Html.TextAreaFor(m => m.GrossWeight, new { Class = "Bill3TextArea" })%>
                </td>
                <td>
                <%: Html.TextAreaFor(m => m.CBM, new { Class = "Bill3TextArea" })%>
                </td>
            </tr>
    </table>
    </div>
    
    <div style="height:auto;overflow:hidden;padding:10px 0 10px;">
    <span class="Label">Đề nghị quý khách đến công ty chúng tôi để nhận lệnh giao hàng</span>
    <br />
    <span class="Label">Vui lòng mang theo các giấy tờ sau:</span>
    </div>
    <div style="height:auto;overflow:hidden">
    <table border="0" cellpadding="3" width="500px">
        <tr>
            <td>
            <label class="Label">1. Vận tải đơn góc.  </label><%: Html.CheckBoxFor(m=>m.ShipperNote)%>
            </td>
            <td>
            <label class="Label">2. Giấy giới thiệu.  </label><%: Html.CheckBoxFor(m=>m.IntroducePaper) %>
            </td>
            <td>
            <label class="Label">3. Giấy báo hàng đến.  </label><%: Html.CheckBoxFor(m=>m.ArrivePaper) %>
            </td>
        </tr>
    </table>
    </div>
    <div style="height:auto;overflow:hidden">
        <span class="Label">Và số tiền thanh toán bằng USD không bao gồm VAT: </span>
    </div>
    <% Revenue Revenue1 = (Revenue)ViewData["Revenue"];
       if (Revenue1 == null) { Revenue1 = RevenueModel.InitRevenue(); }%>
    <table border="0" cellpadding="5" width="750px">
        <tr><td><label class="Label">1. Cước phí : </label></td><td><label class="Label">$<%= Revenue1.INTransportRate != null ? Revenue1.INTransportRate.Value.ToString("0.00") : "0.00"%></label></td>
        <td rowspan="8" style="width:450px;text-align:center">
        <div style="overflow:hidden;padding-left:120px">
            <%: Html.TextAreaFor(m => m.Notification, new { Class = "Bill2TextArea" })%>
            </div>
        </td>
        </tr>
        <tr><td><label class="Label">2. Phí EXW : </label></td><td><label class="Label">$<%= Revenue1.INInlandService.Value.ToString("0.00")%></label></td></tr>
        <tr><td><label class="Label">3. Phí giao Door : </label></td><td><label class="Label">$<%= Revenue1.INCreditDebit.Value.ToString("0.00")%></label></td></tr>
        <tr><td><label class="Label">4. Phí chứng từ : </label></td><td><label class="Label">$<%= Revenue1.INDocumentFee.Value.ToString("0.00")%></label></td></tr>
        <tr><td><label class="Label">5. Phí handling : </label></td><td><label class="Label">$<%= Revenue1.INHandingFee.Value.ToString("0.00")%></label></td></tr>
        <tr><td><label class="Label">6. THC : </label></td><td><label class="Label">$<%= Revenue1.INTHC.Value.ToString("0.00")%></label></td><td></td><td></td></tr>
        <tr><td><label class="Label">7. CFS : </label></td><td><label class="Label">$<%= Revenue1.INCFS.Value.ToString("0.00")%></label></td></tr>
        <tr><td><label class="Label">8. <%= Revenue1.AutoName1 %> : </label></td><td><label class="Label">$<%= Revenue1.INAutoValue1.Value.ToString("0.00")%></label></td></tr>
        <tr><td><label class="Label">9. <%= Revenue1.AutoName2 %> : </label></td><td><label class="Label">$<%= Revenue1.INAutoValue2.Value.ToString("0.00")%></label></td></tr>
    </table>
    <div style="height:auto;overflow:hidden">
        <span class="Label">Liên hệ điện thoại trước khi đến nhận lệnh giao hàng. Cảm ơn!</span><br />
        </div>
        <div style="height:auto;overflow:hidden">
        <table>
            <tr>
                <td>
                    <span class="Label">Tel : </span>
                </td>
                <td>
                <%: Html.TextBoxFor(m => m.NoticeTel, new { Class = "BillDetailInput" })%>
                </td>
                <td>
                <span class="Label"> / Attn : </span>
                </td>
                <td>
                <%: Html.TextBoxFor(m => m.NoticeAttn, new { Class = "BillDetailInput" })%>
                </td>
            </tr>
        </table>
    </div>
    </div>
     <table border="0" cellpadding="3">
        <tr>
            <td colspan="2" style="text-align:center">
               <label style="font-family:Arial Baltic;font-size:16px;font-weight:bold;">CÔNG TY CHÚNG TÔI CÓ SẴN DỊCH VỤ KHAI BÁO HẢI QUAN VÀ VẬN CHUYỂN ĐẾN KHO</label> 
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <label style="font-family:Arial Baltic;font-size:14px;">Rất hân hạnh được phục vụ quý khách!</label>
                <br />
                <br />
            </td>
        </tr>
        </table>
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
        <%: Html.ActionLink("Print", "PrintArriveNotice", "Shipment", new { id = 0, ShipmentId = Model.ShipmentId, BillDetailId = 0,typedoc=ViewData["Typedoc"] }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
    </div>
    <%} %>
    <script language="javascript" type="text/javascript">
        function submitForm() {
            var form = jQuery("#BillDetailAction").parents("form:first");
            form.submit();
        }
        jQuery(document).ready(function () {
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            new DateTimePicker('ETA', 'dd/MM/yyyy');
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#ArriveNotice").addClass("LinkActive");
        });
    </script>
</asp:Content>
