<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BookingRequestModel>" %>

<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Print Booking Request</title>
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

    <div class="PrintBookingRequest">
        <div class="BElement" style="border-bottom: 1px solid #000;">
            <div class="BElement" style="float: left; width: 449px; border-right: 1px solid #000;">
                <div class="Title">INLAND BOOKING / PHIẾU ĐẶT DỊCH VỤ</div>
                <div class="BElement" style="padding: 0;">
                    <label class="Label" style="padding: 0 10px; width: 120px;">Date / Ngày : </label>
                    <label class="NormalLabel" style="padding: 0 10px; width: 100px;"><%= DateTime.Now.ToString("dd/MM/yyyy") %></label>
                    <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                    <label class="Label" style="padding: 0 10px;">OutBound / XK </label>
                </div>
                <div class="BElement" style="padding: 0;">
                    <label class="Label" style="padding: 0 10px; width: 120px;">No / Số : </label>
                    <label class="NormalLabel" style="padding: 0 10px; width: 100px;"><%= Model.ShipmentId %></label>
                    <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                    <label class="Label" style="padding: 0 10px;">InBound / NK </label>
                </div>
            </div>
            <div class="BElement" style="float: left; padding-left: 5px; padding-bottom: 3px; width: 444px;">
                <img src="../../Images/SCF.jpg" width="430px" />
            </div>
        </div>
        <div class="BElement" style="border-bottom: 1px solid #000;">
            <div style="float: left; height: 100%; overflow: hidden; width: 449px; border-right: 1px solid #000;">
                <div class="Shipper">
                    <div style="font-weight: bold;">Shipper / Người gửi hàng :</div>
                    <div>
                        <%= Model.ShipperName %>
                    </div>
                </div>
                <div class="Shipper">
                    <div style="font-weight: bold;">Cnee / Người nhận hàng :</div>
                    <div>
                        <%= Model.CneeName %>
                    </div>
                </div>
                <div class="ShipperEnd">
                    <div style="font-weight: bold;">Place of Delivery / Địa điểm giao nhận hàng :</div>
                    <div>
                        <%= Model.PlaceDelivery %>
                    </div>
                </div>
            </div>
            <div style="float: left; height: auto; overflow: hidden; width: 450px;">
                <div class="BElement" style="border-bottom: 1px solid #000;">
                    <div style="font-weight: bold; padding-left: 5px; padding-top: 5px;">-Mode / Phương thức vận chuyển :</div>
                    <div style="height: auto; overflow: hidden; padding-left: 5px; padding-top: 10px;">
                        <label class="Label" style="width: 200px;">-POL/Nơi đi</label><%= Model.PortFrom %>
                    </div>
                    <div style="height: auto; overflow: hidden; padding-left: 5px; padding-top: 10px;">
                        <label class="Label" style="width: 200px;">-POD/Nơi đến</label><%= Model.PortTo %>
                    </div>
                    <div style="font-weight: bold; padding-left: 5px; padding-top: 5px; height: auto; overflow: hidden;">-Term of Delivery / Điều kiện giao hàng :</div>
                    <div style="padding-left: 5px; padding-top: 5px;">
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px; width: 200px;">At site / Tại công trường </label>
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px;">FOT / Trên xe </label>
                    </div>
                    <div style="font-weight: bold; clear: both; padding-left: 5px; padding-top: 5px;">-Các dịch vụ khác :</div>
                    <div style="clear: both; padding-left: 5px; padding-top: 5px 0 20px 5px;">
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px; width: 200px;">C/O </label>
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px;">Insurance / Bảo hiểm </label>
                    </div>
                    <div style="clear: both; height: auto; padding: 0px 0 20px 5px;">
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px; width: 200px;">Warehouse  / Lưu kho </label>
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px;">Courier / Gửi chứng từ </label>
                    </div>
                    <div style="clear: both; height: auto; padding: 0px 0 20px 5px;">
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px; width: 200px;">Wooden case  / Ðóng kiện gỗ </label>
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px;">Fumigation / Hun trùng </label>
                    </div>
                    <div style="clear: both; height: auto; padding: 0px 0 30px 5px;">
                        <input type="checkbox" class="CheckBox" style="padding: 0 10px; float: left;" />
                        <label class="Label" style="padding: 0 10px; width: 200px;">Other  / Khác </label>
                    </div>
                </div>
                <div class="BElement" style="">
                    <div style="float: left; width: 200px; overflow: hidden;">
                        <div style="font-weight: bold; padding-left: 5px;" class="Commodity">
                            Relevant doc. / Chứng từ
                        </div>
                        <div class="Commodity" style="padding-left: 5px;">
                            1  Contract / Hợp đông
                        </div>
                        <div class="Commodity" style="padding-left: 5px;">
                            2  Invoice / Hóa đơn TM
                        </div>
                        <div class="Commodity" style="padding-left: 5px;">
                            3  PL / Bảng kê chi tiết
                        </div>
                        <div class="Commodity" style="padding-left: 5px;">
                            4  BL / Vận đơn
                        </div>
                        <div class="Commodity" style="padding-left: 5px;">
                            5 Giấy giới thiệu
                        </div>
                        <div class="Commodity" style="padding-left: 5px;">
                            6
                        </div>
                    </div>
                    <div style="float: left; width: 130px; overflow: hidden;">
                        <div class="Commodity">
                            Bản chính
                        </div>
                        <div class="Commodity">
                            <%: Model.MainContract %>
                        </div>
                        <div class="Commodity">
                            <%:  Model.MainInvoice %>
                        </div>
                        <div class="Commodity">
                            <%:  Model.MainPL %>
                        </div>
                        <div class="Commodity">
                            <%:  Model.MainBL %>
                        </div>
                        <div class="Commodity">
                            <%:  Model.MainExtend1 %>
                        </div>
                        <div class="Commodity">
                            <%:  Model.MainExtend2 %>
                        </div>
                    </div>
                    <div style="float: left; width: 119px; overflow: hidden;">
                        <div class="Commodity">
                            Bản sao
                        </div>
                        <div class="Commodity">
                            <%:  Model.SubContract %>
                        </div>
                        <div class="Commodity">
                            <%: Model.SubInvoice %>
                        </div>
                        <div class="Commodity">
                            <%: Model.SubPL %>
                        </div>
                        <div class="Commodity">
                            <%: Model.SubBL %>
                        </div>
                        <div class="Commodity">
                            <%: Model.SubExtend1 %>
                        </div>
                        <div class="Commodity">
                            <%: Model.SubExtend2 %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="BElement">
            <div style="float: left; height: 100%; overflow: hidden; width: 449px; border-right: 1px solid #000;">
                <div style="float: left; width: 50px; overflow: hidden; border-right: 1px solid #000;">
                    <div style="font-weight: bold; padding: 5px 10px 15px; border-bottom: 1px solid #000;" class="Commodity">
                        No
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        1
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        2
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        3
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        4
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        5
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        6
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        7
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        8
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        9
                    </div>
                    <div style="padding-left: 10px; border-bottom: 1px solid #000;" class="Commodity">
                        10
                    </div>
                </div>
                <div style="float: left; width: 250px; overflow: hidden; border-right: 1px solid #000;">
                    <div class="Commodity" style="font-weight: bold; padding: 5px 10px 15px; border-bottom: 1px solid #000;">
                        Commodity in Viet Nam/ Tên hàng
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity1%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity2%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity3%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity4%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity5%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity6%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity7%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity8%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity9%></span>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <span style="width: 200px;"><%: Model.Commodity10%></span>
                    </div>
                </div>
                <div style="float: left; width: 147px; overflow: hidden;">
                    <div class="Commodity" style="font-weight: bold; padding: 5px 10px 15px; border-bottom: 1px solid #000;">
                        HS Code / Mã HS
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode1 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode2 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode3 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode4 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode5 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode6 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode7 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode8 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode9 %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.HSCode10 %>
                    </div>
                </div>
                <div style="clear: both; font-weight: bold; padding-top: 20PX; height: 25PX;">
                    <span>Total / TC:</span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 30px;"></span><span>PLTS/</span>
                    <span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 30px;"></span><span>Con't</span>
                    <span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 30px;"></span><span>/</span>
                    <span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 30px;"></span><span>KGS</span>
                </div>
            </div>
            <div style="float: left; height: auto; overflow: hidden; width: 450px;">
                <div style="width: 450px; clear: both; font-weight: bold; border-bottom: 1px solid #000; padding: 5px 0 15px; height: 15PX;"><span style="padding: 10px 20px;">Advanced Expenses / Chi phí ứng trước</span></div>
                <div style="float: left; width: 220px; overflow: hidden;">
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        1.Freight / Cước
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        2.Doc Fee / Phí chứng từ
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        3.CFS
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        4.THC
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        5.Tax / Thuế
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        6.Other / Khác
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000; border-right: 1px solid #000; padding-left: 5px;">
                        Total / TC:
                    </div>
                    <div class="CustomerSignature">
                        Customer's Signature /
                        <br />
                        Đại diện khách hàng
                    </div>

                </div>
                <div style="float: left; width: 230px; overflow: hidden;">
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.FreightFee %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.DocFee %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.CFSFee %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.THCFee%>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.Tax %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.OtherFee %>
                    </div>
                    <div class="Commodity" style="border-bottom: 1px solid #000;">
                        <%: Model.TotalFee %>
                    </div>
                    <div class="CustomerSignature">
                        SANCO's Signature /
                        <br />
                        Đại diện SANCO
                    </div>
                </div>
            </div>
        </div>
        <div>
            ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        </div>
        <div class="BElement Title">
            AFTER SERVICES CONFIRMATION / XÁC NHẬN KẾT THÚC DỊCH VỤ
        </div>
        <div class="BElement" style="border-bottom: 1px solid #000;">
            <div class="FinishLeft">
                <div style="clear: both; font-weight: bold;">Proof of Delivery / Xác nhận giao nhận hàng</div>
                <div class="Commodity">We confrim that the goods is delivery at / Chúng tôi xác nhận lô</div>
                <div class="Commodity"><span>hàng trên được giao/nhận tại</span> <span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>Condition / Tình trạng hàng hóa</span> <span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>Quantity / Số lượng thực tế</span> <span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div style="padding: 50px 0;"><span style="margin: 10px 70px 10px 50px; font-weight: bold;">Người giao hàng</span><span style="margin: 10px 100px 10px 50px; font-weight: bold;">Người nhận hàng</span></div>
            </div>
            <div class="FinishRight">
                <div style="clear: both; font-weight: bold;">Document release / Giao chứng từ</div>
                <div class="Commodity"><span>1 </span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>2 </span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>3 </span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>4 </span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>5 </span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div class="Commodity"><span>6 </span><span style="width: 15px; border-bottom: 1px dotted silver; padding: 0 100px;"></span></div>
                <div style="padding: 10px 0 50px;"><span style="margin: 10px 10px 50px 50px; font-weight: bold;">Bên giao</span><span style="margin: 10px 10px 10px 50px; font-weight: bold;">Bên nhận</span></div>
            </div>
        </div>
        <div class="BElement">
            <div class="EndLeft">
                <div style="clear: both; font-weight: bold;">Payment Confirmation / Xác nhận thanh toán</div>
                <div style="clear: both; font-weight: bold;">Total / Số tiền</div>
                <div style="clear: both; font-weight: bold;">Date / Ngày thanh toán</div>
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
    <%}
    %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
    </script>
</body>
</html>
