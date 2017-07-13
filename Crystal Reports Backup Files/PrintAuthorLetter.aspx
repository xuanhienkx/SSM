<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.AuthorLetterModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System-Print Author Letter</title>
    <link id="Link1" href="~/Content/Site.css" rel="stylesheet" type="text/css" runat="server"/>
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/global.css" media="all" runat="server"/>
    <link id="Link3" type="text/css" rel="stylesheet" href="~/Content/top-nav-bar.css" media="all" runat="server"/>
    <link id="Link4" type="text/css" rel="stylesheet" href="~/Content/main-nav-bar.css" media="all" runat="server"/>
    <link id="Link5" type="text/css" rel="stylesheet" href="~/Content/homepage.css" media="all" runat="server"/>
     <link id="Link6" type="text/css" rel="stylesheet" href="~/Content/section-block.css" media="all" runat="server"/>
      <link id="Link7" type="text/css" rel="stylesheet" href="~/Content/footer-bar.css" media="all" runat="server"/>
      <link id="Link8" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server"/>
      <link id="Link9" type="text/css" rel="stylesheet" href="~/Content/Shipment.css" media="all" runat="server" />
      <link id="Link10" type="text/css" rel="stylesheet" href="~/Content/Freights.css" media="all" runat="server" />
        <link id="Link11" type="text/css" rel="stylesheet" href="~/Content/BookingConfirm.css" media="all" runat="server" />
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
<body style="margin-left:20px; margin-top:15px;color: black;">
   <div class="AuthorLetterContent AuthorLetterContentPrint">
        <table>
           <tr>
               <td align="center" colspan="2" style="font-size:16px">
               CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM
               <br />
               Độc Lập - Tự Do - Hạnh Phúc
               </td>
           </tr>
           <tr>
               <td align="center" colspan="2">
                   --------------------------------------------------------------
               </td>               
           </tr>
           <tr>
               <td style="font-size:12px;">
                   <br />
                   <br />
                   <% = StringUtils.HtmlFilter(Model.CompanyAddress) %> 
               </td>
               <td style="font-size:16px; width:400px;text-align:right;vertical-align:top">
                   <br />
                   <br />
                   Tp. Hồ Chí Minh , Ngày ...... tháng ...... năm <%= DateTime.Now.Year %>
               </td>
           </tr>
           <tr>
               <td style="font-size:16px;">
               <br />
                   Số : <span style="font-weight:bold"> <%= Model.ShipmentId %></span>
               </td>
           </tr>
           <tr>
               <table>
                   <tr>
                       <td style="font-size:16px; vertical-align:top">
                       <br />
                           Kính gửi :
                       </td>
                       <td style="font-size:16px; vertical-align:top">
                       <br />
                           <%= Model.DearTo%>
                       </td>
                   </tr>
               </table>
           </tr>
           <tr>
               <div class="SideA">
               <br />
                   <span style="font-size: 16px">A. BÊN ỦY QUYỀN (BÊN A):</span></div>
           </tr>
           <tr>
               <div class="SideAInput" style="font-size: 16px">
                   <%= Model.BenA%></div>
           </tr>
           <tr>
               <div class="SideA">
               <br />
                   <span style="font-size: 16px">B. BÊN ĐƯỢC ỦY QUYỀN (BÊN B):</span></div>
           </tr>
           <tr>
               <div class="SideBInput" style="font-size: 16px">
                   <%= Model.BenB%></div>
           </tr>
           <tr>
               <div class="SideA">
               <br />
                   <span style="font-size: 16px">C. NỘI DUNG ỦY QUYỀN</span></div>
           </tr>
           <tr>
           <div class="DetailAuthor1" style="font-size:16px">1- Bên A ủy quyền cho bên B thay mặt bên A thực hiện các giao dịch tại Công ty TCS/SCSC như sau:</div>
           </tr>
           <tr>
           <div class="DetailAuthor1" style="font-size:16px">- Bên B được quyền làm các thủ tục và nhận hàng tại Công Ty TCS/SCSC chi tiết lô hàng như sau:</div>
           </tr>
       </table>
        
        
        <div class="SoMAWB" style="font-size:16px"><label class="ContentTitle">* Số MAWB (MAWB No)</label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%= Model.MAWBNo%></label></div>
        <div class="SoMAWB" style="font-size:16px"><label class="ContentTitle">* Số HAWB (HAWB No)</label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%= Model.HAWNNo%></label></div>
        <div class="SoMAWB" style="font-size:16px"><label class="ContentTitle">* Chuyến bay (Flight)</label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%= Model.Flight%></label><label class="NormalLabel" style="padding:0 20px;font-size:16px">Ngày chuyến bay : </label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%=Model.FlightDate%></label></div>
        <div class="SoMAWB" style="font-size:16px"><label class="ContentTitle">* Trọng lượng (Weight)</label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%= Model.Weight%></label></div>
        <div class="SoMAWB" style="font-size:16px"><label class="ContentTitle">* Số kiện (No of packages)</label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%= Model.NoOfPackage%></label></div>
        <div class="SoMAWB" style="font-size:16px"><label class="ContentTitle">* Loại hàng (Description of goods)</label><label class="NormalLabel" style="padding:0 20px;font-size:16px"><%= Model.DescriptionOfGoods%></label></div>
        <div class="DetailAuthor1" style="font-size:16px; width: 780px">- Bên B chịu trách nhiệm thanh toán mọi chi phí liên quan đến việc nhận hàng tại Công Ty TCS/SCSC theo MST (nếu có) của bên B</div>
        <div class="DetailAuthor1" style="font-size:16px">2- Bên A cam kết chịu mọi trách nhiệm trước pháp luật về việc ủy quyền này.</div>
        <div class="DetailAuthor1" style="font-size:16px">Kính mong cơ quan hữu quan tạo điều kiện thuận lợi cho cơ quan trên trong việc làm thủ tục nhận hàng.</div>
        <div class="DetailAuthor1" style="font-size:16px">Trân trọng kính chào!</div>
        <div class="BehalfAuthor" style="font-size:16px">ĐẠI DIỆN BÊN ỦY QUYỀN</div>
        <div class="BehalfAuthorName" style="font-size:16px">Giám Đốc (Director)</div>
        <div class="Title5" style="font-size:14px">Người nhận chứng từ (Receiver's D.O Signature):</div>
        <div class="Title6"><label class="NormalLabel" style="font-size:14px">Ngày (Date)</label></div>
        <div class="Title6"><label class="NormalLabel" style="font-size:14px">Ngày (Signature)</label></div>
        <div class="Title6"><label class="NormalLabel" style="font-size:14px">Họ và tên (Full Name)</label></div>
        <div class="Title6"><label class="NormalLabel" style="font-size:14px">1- Vận đơn(AWB, B/L) <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 2- Invoice / Packing list: <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 3- C/O <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 4- Chứng thư bảo hiểm <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> 5- Khác <img style="padding:0 10px;" src="../../Images/context-menu.gif"/> </label></div>
    </div>
  
   <script language="javascript" type="text/javascript">
       jQuery(document).ready(function () {
           window.print();
       });
   </script>
</body>
</html>


