<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ArriveNoticeModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delivery Order
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <% using( Html.BeginForm()) { %>
    <div class="DivBody">
    <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
   <table>
            <tr>
                <td>
                    <table border="0" cellpadding="3" width="750px">
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <%: Html.TextBoxFor(m => m.DOVNTitle, new { style = "font-family:Arial Baltic;font-size:20px;font-weight:bold;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: center">
                                <%: Html.TextBoxFor(m => m.DOENTitle, new { style = "font-family:Arial Baltic;font-size:14px;" })%>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="text-align: right">
                                <div style="font-weight: bold; padding-right: 100px;font-size:17px">
                                    REF #:
                                    <%="S "+ Request.Params.Get(0) %></div>
                            </td>
                        </tr>
                         <tr>
            <td colspan="2"><%: Html.TextAreaFor(m => m.CompanyAddress, new { Class = "ShipmentTextArea", style="width:350px;" })%>
            </td>
        </tr>
                        <tr>
                            <td style="text-align: right; width: 120px">
                                <label style="font-weight: bold;">
                                    Kính gửi:
                                </label>
                            </td>
                            <td style="text-align: left">
                                <%: Html.TextBoxFor(m => m.ToVN, new { Class = "ShipmentInput", style = "font-family:Arial Baltic;font-size:14px;" })%>
                                <label style="font-family: Arial Baltic; font-size: 14px; padding-left: 5px">
                                    <%= Model.PortTo%></label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right;">
                                <label style="font-family: Arial Baltic;">
                                    To:
                                </label>
                            </td>
                            <td colspan="2">
                                <%: Html.TextBoxFor(m => m.ToEN)%>
                            </td>
                        </tr>
                        <tr style="height: 15px">
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                CÔNG TY GIAO NHẬN VÀ VẬN TẢI QUỐC TẾ SAO NAM (SANCO FREIGHT LTD) ĐỀ NGHỊ GIAO CHO :
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="font-family: Arial Baltic; font-size: 14px; font-weight: bold">
                                <%: Html.TextAreaFor(m => m.DOAddress, new { Class = "ShipmentTextArea", style="width:350px;" })%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="vertical-align: top;">
                    <div style="height: auto; overflow: hidden; padding: 0 10px; text-align: left; font-size: 20px;
                        font-weight: bold;">
                        <%: Html.CheckBoxFor(m=>m.DOLogo) %><span>Logo</span><br />
                        <%: Html.CheckBoxFor(m=>m.DOHeader) %><span>Header</span><br />
                        <%: Html.CheckBoxFor(m=>m.DOFooter) %><span>Footer</span>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table border="0" cellpadding="3" width="700px">
            <tr>
                <td style="width: 170px">
                    <label>
                        Lô hàng thuộc B/L số :</label>
                    <br />
                    <label>
                        (Shipment under B/L No.)</label>
                </td>
                <td>
                    <%= Model.BillNumber %>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Tên tàu / Chuyến bay :</label>
                    <br />
                    <label>
                        (Vessel)</label>
                </td>
                <td>
                    <%= Model.ShiperName%>
                </td>
                <td>
                    <label>
                        Số Chuyến</label>
                    <br />
                    <label>
                        (Voyage)</label>
                </td>
                <td>
                    <%= Model.ShiperNumber%>
                </td>
            </tr>
            <tr>
                <td>
                    <label>
                        Ngày cập cảng</label><br />
                    <label>
                        (ETA)</label>
                </td>
                <td>
                    <%= Model.ETA%>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <div style="height: auto; overflow: hidden; padding: 15px 0 15px 0;">
            <table border="1px solid #BFBFBF" class="TableNormal" width="700px" style="text-align: center">
                <tr style="background: #338899;">
                    <th>
                        Số Cont / Seal
                        <br />
                        <label style="font-family: Arial Baltic; font-size: 12px;">
                            (Cont / Seal No.)</label>
                    </th>
                    <th>
                        Số Kiện
                        <br />
                        <label style="font-family: Arial Baltic; font-size: 12px;">
                            (No.of Packages)</label>
                    </th>
                    <th>
                        Tên Hàng Hóa
                        <br />
                        <label style="font-family: Arial Baltic; font-size: 12px;">
                            (Description of goods)</label>
                    </th>
                    <th>
                        T.Lượng
                        <br />
                        <label style="font-family: Arial Baltic; font-size: 12px;">
                            (Weight)</label>
                    </th>
                    <th>
                        Số khối
                        <br />
                        <label style="font-family: Arial Baltic; font-size: 12px;">
                            (Volume)</label>
                    </th>
                </tr>
                <tr style="border: none; height: 200px; text-align: center; vertical-align: top;">
                    <td width="150px">
                        <%: Html.TextAreaFor(m => m.ShippingMark, new { Class = "Bill3TextArea" })%>
                    </td>
                    <td width="150px">
                        <%: Html.TextAreaFor(m => m.NoCTNS, new { Class = "Bill3TextArea" })%>
                    </td>
                    <td width="200px">
                        <%: Html.TextAreaFor(m => m.GoodsDescription, new { Class = "Bill1TextArea" })%>
                        <td width="150px">
                            <%: Html.TextAreaFor(m => m.GrossWeight, new { Class = "Bill3TextArea" })%>
                        </td>
                        <td width="150px">
                            <%: Html.TextAreaFor(m => m.CBM, new { Class = "Bill3TextArea" })%>
                        </td>
                </tr>
            </table>
        </div>
        <div style="height: auto; overflow: hidden; padding: 10px 0 10px;">
            <span style="padding-left: 30px;">Kính mong các cơ quan hữu quan tạo điều kiện thuận
                lợi cho cơ quan trên trong việc làm thủ tục</span>
            <br />
            <span>nhận hàng (Pls kindly assist the company to take delivery of the goods smoothly)</span>
        </div>
        <div style="height: auto; overflow: hidden; padding: 10px 0;">
            <span class="Label">Chân thành cảm ơn!</span>
        </div>
        <div style="height: auto; overflow: hidden; padding: 10px 0;">
            <span class="Label">(Yours Sincerely !)</span><br />
        </div>
        <% Revenue Revenue1 = (Revenue)ViewData["Revenue"];
       if (Revenue1 == null) { Revenue1 = RevenueModel.InitRevenue(); }%>
        <table border="0" cellpadding="3" width="750px">
            <tr>
                <td>
                    <label style="font-family: Arial Baltic; font-weight: bold;">
                        NGƯỜI NHẬN CHỨNG TỪ
                    </label>
                    <label>
                        (Reveiver's signature)
                    </label>
                </td>
                <td>
                    <label style="font-family: Arial Baltic; float: left; font-weight: bold; line-height: 30px;
                        vertical-align: middle; padding-right: 5px;">
                        Address
                    </label>
                    <%: Html.TextBoxFor(m => m.AddressOfSign, new { Class = "SOAInputShort" })%>
                    <label style="font-family: Arial Baltic; font-weight: bold; float: left; line-height: 30px;
                        vertical-align: middle; padding-right: 20px;">
                        , Ngày (Date)</label><%: Html.TextBoxFor(m => m.DeliveryDate, new { Class = "SOAInputShort" })%>
                </td>
            </tr>
        </table>
        <div style="font-size: 11px">
            <table border="0" cellpadding="3">
                <tr>
                    <td colspan="4">
                        <label>
                            Đã nhận đủ giấy tờ kèm theo (Received docs as below) :</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            1- Vận đơn(AWB, B/L):</label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                    <td>
                        <label>
                            4- Chứng thư bảo hiểm</label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            2- Invoice / Parking list:</label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                    <td>
                        <label>
                            5- Khác</label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>
                            3- C/O</label>
                    </td>
                    <td>
                        <input type="checkbox" name="Vandon" />
                    </td>
                </tr>
                <tr></tr>
                <tr>
                    <td colspan="4">
                        <label>
                            Ngày (date):</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <label>
                            Ký tên (Signature):</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <label>
                            Họ và tên (Full Name):</label>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="4">
                        ___________________________
                    </td>
                </tr>
            </table>
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
        <%: Html.ActionLink("Print", "PrintDeliveryOrder", "Shipment", new { ShipmentId = Model.ShipmentId ,typedoc=ViewData["Typedoc"]}, new { Class = "LinkForm", target = "_blank" })%>
        </div>
    </div>
     <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#DeliveryOrder").addClass("LinkActive");
        });
    </script>
</asp:Content>
