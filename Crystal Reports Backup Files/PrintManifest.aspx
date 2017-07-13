<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ManifestModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title> <%=Helpers.SiteTitle %> SSM System-Print Manifest</title>
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
<body style="margin-left:40px; margin-top:15px">
<% using (Html.BeginForm())
   { %>
   <div class="ManifestContainer">
        <div class="CompanyName">SANCO FREIGHT</div>
        <div class="Address">23B Ton Duc Thang St, Ben Nghe Ward, Dist. 01 </div>
        <div class="Address">Hochiminh City, Viet Nam</div>
        <div class="Address">Tel: 84.8.9104532 / 9104534 / 9104535 / 9104537 / 9104538     –  Fax: 84.8.9104533</div>
        <div class="ManifestName">CARGO  MANIFEST</div>
        
       <table style="width:100%">
           <tr>
               <td><label>MAWB No.:</label>
               </td>
               <td><%: Model.MAWBNo%>
               </td>
               <td><label> FLT No.:</label>
               </td>
               <td><%: Model.FLTNo%>
               </td>
           </tr>
           <tr>
               <td><label>CONSIGNED TO:</label>
               </td>
               <td><%: Model.CONSIGNEDTO%>
               </td>
               <td><label> DATE:</label>
               </td>
               <td><%: Model.DATE%>
               </td>
           </tr>
           <tr>
               <td><label>DEPARTURE :</label>
               </td>
               <td><%: Model.DEPARTURE%>
               </td>
               <td><label>DESTINATION :</label>
               </td>
               <td><%: Model.ESTINATION%>
               </td>
           </tr>
       </table>
       <div class="MainContent">
       <table  border="1px solid Silver" style="width:100%">
           <tr>
               <th>
                   ORD No.
               </th>
               <th>
                   HAWB No.
               </th>
               <th>
                   No. of Pcs
               </th>
               <th>
                   G.WEIGHT (KGS)
               </th>
               <th>
                   DESCRIPTION OF GOODS
               </th>
               <th>
                   SHIPPER
               </th>
               <th>
                   CONSIGNEE
               </th>
               <th>
                   CHARG CODE
               </th>
           </tr>
           <tr>
               <td>
                   <%: Model.ORDNo%>
               </td>
               <td>
                   <%: Model.HAWBNo%>
               </td>
               <td>
                   <%: Model.NoOfPcs%>
               </td>
               <td>
                   <%: Model.GWEIGHT%>
               </td>
               <td>
                   <%= StringUtils.HtmlFilter(Model.DESCRIPTIONOFGOODS)%>
               </td>
               <td>
                   <%= StringUtils.HtmlFilter(Model.SHIPPER)%>
               </td>
               <td>
                   <%= StringUtils.HtmlFilter(Model.CONSIGNEE)%>
               </td>
               <td>
                   <%: Model.CHARGECODE%>
               </td>
           </tr>
       </table>
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
