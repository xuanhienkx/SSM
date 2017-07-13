<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.BillLandingModel>" %>
<%@ Import Namespace="SSM" %>
<%@ Import Namespace="SSM.Common" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System-Print BLDetail</title>
    <link id="Link1" href="~/Content/PrintPage/Site.css" rel="stylesheet" type="text/css" runat="server" />
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/PrintPage/global.css" media="all" runat="server" />
    <link id="Link12" type="text/css" rel="stylesheet" href="~/Content/PrintPage/top-nav-bar.css" media="all" runat="server" />
    <link id="Link13" type="text/css" rel="stylesheet" href="~/Content/PrintPage/main-nav-bar.css" media="all" runat="server" />
    <link id="Link14" type="text/css" rel="stylesheet" href="~/Content/PrintPage/homepage.css" media="all" runat="server" />
    <link id="Link15" type="text/css" rel="stylesheet" href="~/Content/PrintPage/section-block.css" media="all" runat="server" />
    <link id="Link16" type="text/css" rel="stylesheet" href="~/Content/PrintPage/footer-bar.css" media="all" runat="server" />
    <link id="Link17" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server" />
    <link id="Link18" type="text/css" rel="stylesheet" href="~/Content/PrintPage/Shipment.css" media="all" runat="server" />
      <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-2.1.4.js") %>"></script>
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
</head>
<body style="margin-left:40px; margin-top:15px">
<div style="font-family:Arial Baltic;font-size:14px;color: black">
    <table width="100%">
   <tr>
   <td>
   <div style="width:400px; text-align:right; padding:35px 50px 15px;float:left">
   <label style="font-family:Arial Baltic;font-size:20px;font-weight:bold;">SHIPPING INSTRUCTION</label>
   </div>
   </td>
   </tr>
   <tr>
   <td>
   <div style="height: auto; overflow: hidden; font-weight: bolder;padding: 15px;">
                   <div style="height: auto; overflow: hidden">
                       <div style="height: auto; overflow: hidden; float: left;">TO :    </div>
                       <%= Model.BillTo%>
                   </div>
    </div>
    </td>
   <td>
   <div style="height: auto; overflow: hidden; font-weight: bolder">
                   <div style="height: auto; overflow: hidden">
                       <div style="height: auto; overflow: hidden; float: left;">BKG # </div>
                       <%= Model.BKG%>
                    </div>
   </div>
   </td>   
   </tr>
   <tr>
   <td></td>
   <td>
   <div style="width:150px;float:left">
   <label style="font-family:Arial Baltic;font-size:14px;">Ref No:   #</label>
   <label style="font-family:Arial Baltic;font-size:14px;font-weight:bold;"> <%= Model.ShipmentId %></label>
   </div>
   </td>
   </tr>
    </table>
    <table class="BillDetailTable" cellpadding="3" width="750px">
       <tr>
            <td colspan="2">
            <label class="Label">Shipper:</label>
            </td>
       </tr>
       <tr>
           <td style="font-weight: bolder;" width="450px" colspan="2">
           <%= StringUtils.HtmlFilter(Model.BLComAddress)%>
           </td>
           <td width="300px">
           </td>
       </tr>
       <tr>
            <td colspan="2">
            <label class="Label">Consignee:</label>
            </td>
       </tr>
       <tr>
           <td colspan="2">
           <%= StringUtils.HtmlFilter(Model.ForRelease)%>
           </td>
           <td>
           </td>
       </tr>
       <tr>
           <td colspan="2">
            <label class="Label">Notify:</label>
            </td>           
       </tr>
       <tr>
           <td  colspan="2">
           <label class="Label">SAME AS CONSIGNEE</label>
           </td>
       </tr>
       <tr>
            <td>
            <label class="Label">Vessel</label>
            </td>
            <td>
            <label class="Label">Voy no</label>
            </td>
       </tr>
       <tr>
           <td><%= Model.OceanVessel %>
           </td>
            <td>
                <%= Model.VoyNo %>
           </td>
       </tr>
       <tr>
            <td>
            <label class="Label">Port of loading</label>
            </td>
            <td>
            <label class="Label">Port Discharge</label>
            </td>
            <td>
            <label class="Label">Final Destination</label>
            </td>
       </tr>
       <tr>
           <td>
           <%= Model.PortLoading %>
           </td>
           <td>
           <%= Model.PortDischarge %>
           </td>
            <td>
           <%= Model.PlaceDelivery %>
           </td>
       </tr>
       <tr>
          <td colspan="3">
                <table class="BillDetailTable" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <td>
                            <div style="height: auto; overflow: hidden">
                                <label class="BillLabel">
                                    Marks and Nos</label></div>
                        </td>
                        <td>
                            <div style="height: auto; overflow: hidden">
                                <label class="BillLabel">
                                    Quantity and description of goods</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Gross weight (Kgs)</label></div>
                        </td>
                        <td>
                        <div style="height:auto;overflow:hidden"><label class="BillLabel">Measurement (M3)</label></div>
                        </td>
                    </tr>
                    <tr>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.MarksNos)%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.QuanlityDescription)%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= Model.GrossWeight%></div>
                        </td>
                        <td valign=top>
                        
                        <div style="height:auto;overflow:hidden"><%= Model.Measurement%></div>
                        </td>
                    </tr>
                </table>
            </td>
       </tr>
       <tr></tr>
       <tr>
           <td>
               <label class="Label">Freight Term :</label>
           </td>
           <td colspan="3">
           <label class="Label" style="text-align:center;font-size:20px;font-weight:bold"> <%= Model.BillFrom%></label>
           </td>
       </tr>
       <tr></tr>
       <tr>
           <td colspan="2"><label class="Label">PLEASE EMAIL TO :  export-docs@sancofreight.com.vn</label>
           </td>
       </tr>
   </table>
</div>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        window.print();
    });
   </script>
</body>
</html>
