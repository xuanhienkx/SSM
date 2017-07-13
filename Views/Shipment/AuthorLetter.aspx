<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.AuthorLetterModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Author Letter
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

   <% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
    <div class="AuthorLetterContent">
        <div class="Title1" visible="true"><%: Html.TextAreaFor(m => m.PublicTitle, new { Class="ShipmentDearTo"})%></div>
        <div class="Title3" visible="true"></div>
        <div class="Title4">
            <div class="TitleLeft">
            <%: Html.TextAreaFor(m => m.CompanyAddress, new { Class="ShipmentDearTo"})%>
            </div>
            <div class="TitleRight" style="text-align:right">..............., Ngày ............... tháng ............ năm <%= DateTime.Now.Year %>
            </div>
        </div>
        <div class="RefNo"><div class="NormalLabel">Số : </div><div class="Label"><%= Model.ShipmentId %></div></div>
        <div class="DearTo"><div class="DearLeft">Kính gửi    : </div><div class="DearRight"><%: Html.TextAreaFor(m => m.DearTo, new { Class="ShipmentDearTo"})%></div></div>
        <div class="SideA"><span>A. BÊN ỦY QUYỀN (BÊN A):</span></div>
        <div class="SideAInput"><%: Html.TextAreaFor(m => m.BenA, new { Class="ShipmentSideA"})%></div>
        <div class="SideA"><span>B. BÊN ĐƯỢC ỦY QUYỀN (BÊN B):</span></div>
        <div class="SideBInput"><%: Html.TextAreaFor(m => m.BenB, new { Class = "ShipmentSideA" })%></div>
        <div class="SideA"><span>C. NỘI DUNG ỦY QUYỀN</span></div>
        <div class="DetailAuthor1">1- Bên A ủy quyền cho bên B thay mặt bên A thực hiện các giao dịch tại Công ty TCS/SCSC như sau:</div>
        <div class="DetailAuthor1">- Bên B được quyền làm các thủ tục và nhận hàng tại Công Ty TCS/SCSC chi tiết lô hàng như sau:</div>
        <div class="SoMAWB"><label class="ContentTitle">* Số MAWB (MAWB No)</label><%: Html.TextBoxFor(m => m.MAWBNo, new { Class="ShipmentInput"})%></div>
        <div class="SoMAWB"><label class="ContentTitle">* Số HAWB (HAWB No)</label><%: Html.TextBoxFor(m => m.HAWNNo, new { Class="ShipmentInput"})%></div>
        <div class="SoMAWB"><label class="ContentTitle">* Chuyến bay (Flight)</label><%: Html.TextBoxFor(m => m.Flight, new { Class="ShipmentInput"})%><label class="NormalLabel" style="padding:0 20px;">NGÀY CHUYẾN BAY</label><%: Html.TextBoxFor(m => m.FlightDate, new { Class="ShipmentInput"})%></div>
        <div class="SoMAWB"><label class="ContentTitle">* Trọng lượng (Weight)</label><%: Html.TextBoxFor(m => m.Weight, new { Class="ShipmentInput"})%></div>
        <div class="SoMAWB"><label class="ContentTitle">* Số kiện (No of packages)</label><%: Html.TextBoxFor(m => m.NoOfPackage, new { Class="ShipmentInput"})%></div>
        <div class="SoMAWB"><label class="ContentTitle">* Loại hàng (Description of goods)</label><%: Html.TextBoxFor(m => m.DescriptionOfGoods, new { Class="ShipmentInput"})%></div>
        <div class="DetailAuthor1">- Bên B chịu trách nhiệm thanh toán mọi chi phí liên quan đến việc nhận hàng tại Công Ty TCS/SCSC theo MST (nếu có) của bên B</div>
        <div class="DetailAuthor1">2- Bên A cam kết chịu mọi trách nhiệm trước pháp luật về việc ủy quyền này.</div>
        <div class="DetailAuthor1">Kính mong cơ quan hữu quan tạo điều kiện thuận lợi cho cơ quan trên trong việc làm thủ tục nhận hàng.</div>
        <div class="DetailAuthor1">Trân trọng kính chào!</div>
        <div class="BehalfAuthor">ĐẠI DIỆN BÊN ỦY QUYỀN</div>
        <div class="BehalfAuthorName">Giám Đốc (Director)</div>
        <div class="Title5">Người nhận chứng từ (Receiver's D.O Signature):</div>
        <div class="Title6"><label class="NormalLabel">Ngày (Date)</label></div>
        <div class="Title6"><label class="NormalLabel">Ngày (Signature)</label></div>
        <div class="Title6"><label class="NormalLabel">Họ và tên (Full Name)</label></div>
        <div class="Title6"><label class="NormalLabel">1- Vận đơn(AWB, B/L) <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 2- Invoice / Packing list: <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 3- C/O <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 4- Chứng thư bảo hiểm <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 5- Khác <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> </label></div>
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
        <%: Html.ActionLink("Print", "PrintAuthorLetter", "Shipment", new { id = 0, ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
    <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            CKEDITOR.replace('DearTo',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
					CKEDITOR.replace('PublicTitle',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
					CKEDITOR.replace('BenA',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
					CKEDITOR.replace('BenB',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            new DateTimePicker('DebitDate', 'dd/MM/yyyy');
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#AuthorLetter").addClass("LinkActive");
        });
    </script>

</asp:Content>
