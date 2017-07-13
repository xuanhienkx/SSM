<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BookingRequestModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 BookingRequest
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
       RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1);%>

                                    <%: Html.HiddenFor(m=>m.Id) %>
    <div class="BookingRequest">
        <div class="BElement" style="border-bottom:1px solid Silver;">
            <div class="BElement" style="float:left;width:499px;border-right:1px solid Silver;">
                <div class="Title">INLAND BOOKING / PHIẾU ĐẶT DỊCH VỤ</div>
                <div class="BElement" style="padding: 0;">
                    <label class="Label" style="padding:0 10px;width:120px;">Date / Ngày : </label>
                    <label class="NormalLabel" style="padding:0 10px;width:100px;"><%= DateTime.Now.ToString("dd/MM/yyyy") %></label>
                    <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                    <label class="Label" style="padding:0 10px;">OutBound / XK </label>
                </div>
                <div class="BElement" style="padding: 0;">
                    <label class="Label" style="padding:0 10px;width:120px;">No / Số : </label>
                    <label class="NormalLabel" style="padding:0 10px;width:100px;"><%= Model.ShipmentId %></label>
                    <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                    <label class="Label" style="padding:0 10px;">InBound / NK </label>
                </div>
            </div>
            <div  class="BElement" style="float:left;width:499px;">
               <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto"/>
            </div>
        </div>
        <div class="BElement" style="border-bottom: 1px solid Silver;">
            <div style="float:left;height:100%;overflow:hidden;width:499px;border-right:1px solid Silver;">
                <div class="Shipper">
                    <div style="font-weight:bold;">Shipper / Người gửi hàng :</div>
                    <div>
                        <%: Html.TextAreaFor(m=>m.ShipperName) %>
                    </div>
                </div>
                <div class="Shipper">
                    <div style="font-weight:bold;">Cnee / Người nhận hàng :</div>
                    <div>
                        <%: Html.TextAreaFor(m=>m.CneeName) %>
                    </div>
                </div>
                <div class="Shipper">
                    <div style="font-weight:bold;">Place of Delivery / Địa điểm giao nhận hàng :</div>
                    <div>
                        <%: Html.TextAreaFor(m=>m.PlaceDelivery) %>
                    </div>
                </div>
            </div>
            <div style="float:left;height:auto;overflow:hidden;width:500px;">
                <div class="BElement" style="border-bottom: 1px solid Silver;">
                    <div style="font-weight:bold;padding-left:5px;padding-top:10px;"> -Mode / Phương thức vận chuyển :</div>
                    <div style="padding-left:5px;padding-top:10px;">
                        <label class="Label" style="width:200px;"> -POL/Nơi đi</label><%: Html.TextBoxFor(m=>m.PortFrom) %>
                    </div>
                    <div style="padding-left:5px;padding-top:10px;">
                        <label class="Label" style="width:200px;"> -POD/Nơi đến</label><%: Html.TextBoxFor(m=>m.PortTo) %>
                    </div>
                    <div style="font-weight:bold;padding-left:5px;padding-top:10px;"> -Term of Delivery / Điều kiện giao hàng :</div>
                    <div style="padding-left:5px;padding-top:10px;">
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;width:200px;">At site / Tại công trường </label>
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;">FOT / Trên xe </label>
                    </div>
                    <div style="font-weight:bold;clear:both;padding-left:5px;padding-top:10px;">  </div>
                    <div style="font-weight:bold;clear:both;padding-left:5px;padding-top:10px;"> -Các dịch vụ khác :</div>
                    <div style="clear:both;padding-left:5px;padding-top:10px;">
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;width:200px;">C/O </label>
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;">Insurance / Bảo hiểm </label>
                    </div>
                    <div style="clear:both;padding:10px 0 50px 5px;">
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;width:200px;">Warehouse  / Lưu kho </label>
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;">Courier / Gửi chứng từ </label>
                    </div>
                    <div style="clear:both;padding:10px 0 50px 5px;">
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;width:200px;"> Other  / khác </label>
                        <input type="checkbox" class="CheckBox" style="padding:0 10px;float:left;"/>
                        <label class="Label" style="padding:0 10px;"> Fumigation / Hun Trùng </label>
                    </div>
                </div>
                <div class="BElement">
                    <div style="float: left; width: 200px; overflow: hidden;">
                        <div style="font-weight: bold;padding-left:5px;" class="Commodity">
                            Relevant doc. / Chứng từ</div>
                            <div class="Commodity" style="padding-left:5px;">
                            1  Contract / Hợp đông</div>
                            <div class="Commodity" style="padding-left:5px;">
                            2  Invoice / Hóa đơn TM</div>
                            <div class="Commodity" style="padding-left:5px;">
                            3  PL / Bảng kê chi tiết</div>
                            <div class="Commodity" style="padding-left:5px;">
                            4  BL / Vận đơn</div>
                            <div class="Commodity" style="padding-left:5px;">
                            5 Giấy giới thiệu
                            </div>
                            <div class="Commodity" style="padding-left:5px;">
                            6
                            </div>
                    </div>
                    <div style="float: left; width: 150px; overflow: hidden;">
                        <div class="Commodity">
                            Bản chính</div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.MainContract) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.MainInvoice) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.MainPL) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.MainBL) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.MainExtend1) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.MainExtend2) %></div>
                    </div>
                    <div style="float: left; width: 149px; overflow: hidden;">
                        <div class="Commodity">
                            Bản sao</div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.SubContract) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.SubInvoice) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.SubPL) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.SubBL) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.SubExtend1) %></div>
                        <div class="Commodity">
                            <%: Html.TextBoxFor(m=>m.SubExtend2) %></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="BElement">
            <div style="float:left;height:100%;overflow:hidden;width:499px;border-right:1px solid Silver;">
                 <div style="float: left; width: 50px; overflow: hidden;border-right:1px solid Silver;">
                        <div style="font-weight: bold;padding:15px 10px;border-bottom:1px solid Silver;" class="Commodity">
                            No</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;" class="Commodity">
                            1</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;"  class="Commodity">
                            2</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;"  class="Commodity">
                            3</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;"  class="Commodity">
                            4</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;"  class="Commodity">
                            5</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;"  class="Commodity">
                            6</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;"  class="Commodity">
                            7</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;" class="Commodity">
                            8</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;" class="Commodity">
                            9</div>
                            <div style="padding-left:10px;border-bottom:1px solid Silver;" class="Commodity">
                            10</div>
                    </div>
                    <div style="float: left; width: 280px; overflow: hidden;border-right:1px solid Silver;">
                        <div class="Commodity" style="font-weight: bold;padding:15px 10px;border-bottom:1px solid Silver;">
                            Commodity in Viet Nam/ Dịch tên hàng</div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity1, new { Style="width:200px;" })%></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity2, new { Style = "width:200px;" })%></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity3, new { Style = "width:200px;" })%></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity4, new { Style = "width:200px;" })%></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity5, new { Style = "width:200px;" })%></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity6, new { Style = "width:200px;" })%></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity7, new { Style = "width:200px;" })%></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity8, new { Style = "width:200px;" })%></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity9, new { Style = "width:200px;" })%></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.Commodity10, new { Style = "width:200px;" })%></div>
                    </div>
                    <div style="float: left; width: 167px; overflow: hidden;">
                        <div class="Commodity" style="font-weight: bold;padding:15px 10px;border-bottom:1px solid Silver;">
                            HS Code / Mã HS</div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode1) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode2) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode3) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode4) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode5) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode6) %></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode7) %></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode8) %></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode9) %></div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.HSCode10) %></div>
                    </div>
                <div style="clear: both; font-weight: bold;padding-top:20PX;">
                    <span>Total / TC:</span><span style="width:15px;border-bottom:1px dotted silver;padding:0 30px;"></span><span>PLTS/</span>
                    <span style="width:15px;border-bottom:1px dotted silver;padding:0 30px;"></span><span>Con't</span>
                    <span style="width:15px;border-bottom:1px dotted silver;padding:0 30px;"></span><span>/</span>
                    <span style="width:15px;border-bottom:1px dotted silver;padding:0 30px;"></span><span>KGS</span>
                    </div>
            </div>
            <div style="float:left;height:auto;overflow:hidden;width:500px;">
                <div style="width:500px;clear:both;font-weight: bold;border-bottom:1px solid Silver;padding:15px 0;HEIGHT:25PX;"><span style="padding:10px 20px;">Advanced Expenses / Chi phí ứng trước</span></div>
                <div style="float: left; width: 250px; overflow: hidden;">
                        <div class="Commodity" style="border-bottom:1px solid Silver;padding-left:5px;">
                            1.Freight / Cước</div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;border-right:1px solid Silver;padding-left:5px;">
                            2.Doc Fee / Phí chứng từ</div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;border-right:1px solid Silver;padding-left:5px;">
                            3.CFS</div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;border-right:1px solid Silver;padding-left:5px;">
                            4.THC</div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;border-right:1px solid Silver;padding-left:5px;">
                            5.Tax / Thuế</div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;border-right:1px solid Silver;padding-left:5px;">
                            6.Other / Khác</div>
                            <div class="Commodity" style="border-bottom:1px solid Silver;border-right:1px solid Silver;padding-left:5px;">
                            Total / TC:</div>
                            <div class="CustomerSignature">
                            Customer's Signature / <br /> Đại diện khách hàng</div>
                            
                    </div>
                    <div style="float: left; width: 249px; overflow: hidden;">
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.FreightFee) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.DocFee) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.CFSFee) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m => m.THCFee)%></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.Tax) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.OtherFee) %></div>
                        <div class="Commodity" style="border-bottom:1px solid Silver;">
                            <%: Html.TextBoxFor(m=>m.TotalFee) %></div>
                        <div class="CustomerSignature">
                            SANCO's Signature / <br /> Đại diện SANCO</div>
                    </div>
            </div>
        </div>
        <div>
        ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        </div>
        
        <div>
        </div>
        <div>
        </div>
        
        <div class="BElement Title">AFTER SERVICES CONFIRMATION / XÁC NHẬN KẾT THÚC DỊCH VỤ
        </div>
        <div class="BElement" style="border-bottom: 1px solid silver;">
            <div class="FinishLeft">
                <div  style="clear:both;font-weight: bold;">Proof of Delivery / Xác nhận giao nhận hàng</div>
                <div class="Commodity">We confrim that the goods is delivery at / Chúng tôi xác nhận lô</div>
                <div class="Commodity"><span>hàng trên được giao/nhận tại</span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span>Condition / Tình trạng hàng hóa</span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span>Quantity / Số lượng thực tế</span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div style="padding:50px 0;"><span style="margin: 10px 100px 10px 50px;font-weight:bold;">Người giao hàng</span><span style="margin:10px 100px 10px 50px;font-weight:bold;">Người nhận hàng</span></div>
            </div>
            <div class="FinishRight">
                <div  style="clear:both;font-weight: bold;">Document release / Giao chứng từ</div>
                <div class="Commodity"><span> 1 </span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span> 2 </span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span> 3 </span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span> 4 </span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span> 5 </span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div class="Commodity"><span> 6 </span> <span style="width:15px;border-bottom:1px dotted silver;padding:0 100px;"></span></div>
                <div style="padding:10px 0 50px;"><span style="margin: 10px 10px 50px 50px;font-weight:bold;">Bên giao</span><span style="margin:10px 10px 10px 50px;font-weight:bold;">Bên nhận</span></div>
            </div>
        </div>
        <div class="BElement">
            <div class="EndLeft">
                <div  style="clear:both;font-weight: bold;">Payment Confirmation / Xác nhận thanh toán</div>
                <div  style="clear:both;font-weight: bold;">Total / Số tiền</div>
                <div  style="clear:both;font-weight: bold;">Date / Ngày thanh toán</div>
            </div>
            <div class="EndRight">
            <!--
                <div  style="clear:both;font-weight: bold;">Pls contact us at / Vui lòng liên hệ chúng tôi tại</div>
                <div  style="clear:both;font-weight: bold;">EMBASSY FREIGHT SERVICES (VN)</div>
                <div  style="clear:both;text-align:center;width:300px;">23 B Tôn Đức Thắng, Q1, Tp HCM</div>
                <div  style="clear:both;text-align:center;width:300px;">Tel: 9104536  Fax:  9104750</div>
                -->
            </div>
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
        <%: Html.ActionLink("Print", "PrintBookingRequest", "Shipment", new { ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
    <%}
         %>


    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            CKEDITOR.replace('ShipperName',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
                    
                     CKEDITOR.replace('CneeName',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
                    
                     CKEDITOR.replace('PlaceDelivery',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
            
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#BookingRequest").addClass("LinkActive");
        });
    </script>
</asp:Content>
