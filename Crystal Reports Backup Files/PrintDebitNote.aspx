<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.DebitNoteModel>" %>
<%@ Import Namespace="SSM.Common" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<%@ Import Namespace="SSM.Controllers" %>
<% User User1 = (User)Session[SSM.Controllers.AccountController.USER_SESSION_ID];%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=Helpers.SiteTitle %> SSM System-Print Debit Note</title>
    <link id="Link1" href="~/Content/Site.css" rel="stylesheet" type="text/css" runat="server"/>
    <link id="Link2" type="text/css" rel="stylesheet" href="~/Content/global.css" media="all" runat="server"/>
    <link id="Link3" type="text/css" rel="stylesheet" href="~/Content/top-nav-bar.css" media="all" runat="server"/>
    <link id="Link4" type="text/css" rel="stylesheet" href="~/Content/main-nav-bar.css" media="all" runat="server"/>
    <link id="Link5" type="text/css" rel="stylesheet" href="~/Content/homepage.css" media="all" runat="server"/>
     <link id="Link6" type="text/css" rel="stylesheet" href="~/Content/section-block.css" media="all" runat="server"/>
      <link id="Link7" type="text/css" rel="stylesheet" href="~/Content/footer-bar.css" media="all" runat="server"/>
      <link id="Link8" type="text/css" rel="stylesheet" href="~/Content/themes/base/jquery-ui.css" media="all" runat="server"/>
      <link id="Link9" type="text/css" rel="stylesheet" href="~/Content/Shipment.css" media="all" runat="server" />
    <script type="text/javascript" src="<%=Page.ResolveClientUrl("~/Scripts/jquery-2.1.4.min.js") %>"/>
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
<div class="DebitNoteContent DebitNoteContentPrint">
    <table width="850px">
            <tr>
                <td>
                    <div style="overflow: hidden; line-height: 1px; height: auto; float: left; width: auto;">
                    <% if (Model.Logo)
                       { %>
                        <img src="<%= Page.ResolveClientUrl("~/" + ViewData["CompanyLogo"]) %>" width="auto" />
                        <%} %>
                    </div>
                </td>
                <td>
                <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: auto">
                <% if (Model.Header)
                   { %>
                    <%= ViewData["CompanyHeader"]%>
                    <%} %>
                </div>
                </td>
                <td>
              
                </td>
            </tr>
        </table>
       
        <table>
            <tr>
                <td>
                <div class="DebitNoteTo">
                    <div class="Title">TO : </div>
                    <div class="Content"><%= Model.CompanyTo%></div>
                </div>
                <div class="DebitNoteRef">
                    <table>
                        <tr>
                            <td>REF. NO.
                            </td>
                            <td align="center" class="Borderdotted"><%="S "+ Model.ShipmentId%>
                            </td>
                        </tr>
                        <tr>
                            <td>DEBIT. NO.
                            </td>
                            <td align="center" class="Borderdotted"><%= Model.DebitNo%>
                            </td>
                        </tr>
                        <tr>
                            <td>DATE
                            </td>
                            <td align="center" class="Borderdotted"><%= Model.DebitDate%>
                            </td>
                        </tr>
                        <tr>
                            <td>TERMS
                            </td>
                            <td align="center" class="Borderdotted"><%= Model.DebitTerms%>
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="font-size:16px;font-weight:bold;">DEBIT NOTE
                            </td>
                        </tr>
                    </table>
                </div>
                </td>
            </tr>
            <tr style="height:20px;"><td></td></tr>
            <tr>
                <td>
                <table class="TableNormal" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <th valign="top">
                            <div style="height: auto; overflow: hidden">
                                <label>FLIGHT NO/VESSEL</label></div>
                        </th>
                        <th>
                            <div style="height: auto; overflow: hidden">
                                <label>HAWB/HBL</label></div>
                        </th>
                        <th>
                        <div style="height:auto;overflow:hidden"><label>ORIGIN</label></div>
                        </th>
                        <th>
                        <div style="height:auto;overflow:hidden"><label>DEST</label></div>
                        </th>
                        <th>
                        <div style="height:auto;overflow:hidden"><label>PIECES</label></div>
                        </th>
                        <th>
                        <div style="height:auto;overflow:hidden"><label>WEIGHT/CONT</label></div>
                        </th>
                    </tr>
                    <tr>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= Model.FlightNo%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.HAWB_HBL)%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.Origin)%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= StringUtils.HtmlFilter(Model.Destination)%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= Model.Pieces%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= Model.Weight%></div>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
             <tr style="height:20px;"><td></td></tr>
            <tr>
                <td>
                <table border="2px solid #a0a0a0" class="TableNormal" style="width:100%;min-height:100px;vertical-align:top;">
                    <tr>
                        <th valign="top" align="center" style="width:550px">
                        DESCRIPTIOIN
                        </th>
                        <th>AMOUNT
                        </th>
                        
                    </tr>
                    <tr style="min-height:100px">
                        <td valign="top">
                        
                        <div class="DescriptionZone"><%= StringUtils.HtmlFilter(Model.Description)%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= Model.DebitAmount%></div>
                        </td>
                        
                    </tr>
                     <tr>
                        <td valign="top" align="right" style="font-weight:bold">USD
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%= Model.DebitUSD%></div>
                        </td>
                        
                    </tr>
                </table>
                </td>
            </tr>
             <tr style="height:20px;"><td></td></tr>
           
            <tr>
                <td>
                <div style="overflow:hidden;height:auto;padding: 20px 0">
                <div class="InWords"> IN WORDS: </div>
                <div class="InWordsInput"><%= Model.InWords%></div>
                </div>
                </td>
            </tr>
             <tr>
                <td><div style="padding-bottom:20px;">
                <%= Model.CompanyFrom%></div>
                </td>
            </tr>
            <tr>
                <td>
                <table style="font-size:12px">
                <tr><td colspan="5"><div class="EnCloseSures">ENCLOSESURES</div></td></tr>
                    <tr>
                        <td style="width: 50px; text-align: right">
                        <input type="checkbox" />
                        </td>
                        <td style="width: 150px">AWB / BILL OF LADING
                        </td>
                        <td style="width: 50px; text-align: right">
                        <input type="checkbox" />
                        </td>
                        <td style="width: 250px">RECEIPT
                        </td>
                        <td style="font-weight:bold">FOR     <%= User1.Company.CompanyName %>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                        <input type="checkbox" />
                        </td>
                        <td>SUPPIER'S INVOICE
                        </td>
                        <td style="text-align: right">
                        <input type="checkbox" />
                        </td>
                        <td>AS PER LISTS / ATTACHED
                        </td>
                        <td>
                        </td>
                    </tr>
                <tr>
                        <td style="text-align: right">
                        <input type="checkbox" />
                        </td>
                        <td colspan="4">MASTER AWB / BL
                        </td>
                        
                    </tr>
                <tr>
                        <td style="text-align: right">
                        <input type="checkbox" />
                        </td>
                        <td colspan="4">DELIVERY NOTE
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" style="text-align: left">
                        <div style="height:auto;overflow:hidden;font-size:11px">ALL CHEQUES SHOULD BE CROSSED MADE PAYABLE TO SANCO FREIGHT LTD,.</div>
                        <div style="height:auto;overflow:hidden;font-size:11px">INTEREST WILL BE CHARGER ON OVERDUE ACOUNT AT 1.5% PER MOUNT OR PART THEREOF</div>
                        </td>
                        <td><div style="height:auto;overflow:hidden;text-align:center;padding-left:25px"><%= Model.AuthName%></div>
                        <div style="height:auto;overflow:hidden"><hr class="DebitHr"/></div>
                        <div style="height:auto;overflow:hidden;text-align:center;font-size:11pxr">This is a computer genarated copy</div>
                          <div style="height:auto;overflow:hidden;text-align:center;font-size:11pxr">no signature is required</div>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>
        <div style="height:auto"><p></p><p></p><br /><br /></div>
        <div style="overflow: hidden; height: auto; float: left; padding: 0 15px; width: auto">
                   <% if (Model.Footer)
                      { %>
                    <%= ViewData["CompanyFooter"]%>
                    <%} %>
                </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            window.print();
        });
   </script>
</body>
</html>
