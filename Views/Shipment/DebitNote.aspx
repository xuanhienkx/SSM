<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.DebitNoteModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM" %>
<%@ Import Namespace="SSM.Controllers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Debit Note
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% User User1 = (User)Session[SSM.Controllers.AccountController.USER_SESSION_ID];%>
<% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.ShipmentId;    
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
    <div style="height: auto; overflow: hidden; padding: 0 10px; text-align: left;clear:both;padding-top:10px;
                        font-size: 20px; font-weight: bold;">
                        <%: Html.CheckBoxFor(m=>m.Logo) %><span>Logo</span>
                        <%: Html.CheckBoxFor(m=>m.Header) %>
                        <span>Header</span>
                        <%: Html.CheckBoxFor(m=>m.Footer) %><span>Footer</span>
                    </div>
    <div class="DebitNoteContent">
        <table>
        <tr style="height:20px;"><td></td></tr>
            <tr>
                <td>
                <div class="DebitNoteTo">
                    <div class="Title">TO : </div>
                    <div class="Content"><%: Html.TextAreaFor(m => m.CompanyTo, new { Class = "ShipmentTextArea" })%></div>
                </div>
                <div class="DebitNoteRef">
                    <table>
                        <tr>
                            <td>REF. NO.</td>
                             
                            <td style="font-size:17px"><%="S "+ Html.TextBoxFor(m => m.ShipmentId, new { Class = "ShipmentInput1", ReadOnly = "true" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>DEBIT. NO.
                            </td>
                            <td><%: Html.TextBoxFor(m => m.DebitNo, new { Class = "ShipmentInput" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>DATE
                            </td>
                            <td><%: Html.TextBoxFor(m => m.DebitDate, new { Class = "ShipmentInput" })%><label for="DebitDate" class="DateInput"></label>
                            </td>
                        </tr>
                        <tr>
                            <td>TERMS
                            </td>
                            <td><%: Html.TextBoxFor(m => m.DebitTerms, new { Class = "ShipmentInput" })%>
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
                            <td style="font-size:20px;font-weight:bold;">DEBIT NOTE
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
                        <th valign=top>
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
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.FlightNo, new { Class = "FlightNoInput" })%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.HAWB_HBL, new { Class = "ShipmentTextArea" })%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.Origin, new { Class = "ShipmentTextArea" })%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.Destination, new { Class = "ShipmentTextArea" })%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.Pieces, new { Class = "ShipmentInputShort" })%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.Weight, new { Class = "ShipmentInputShort" })%></div>
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
                        
                        <div class="DescriptionZone"><%: Html.TextAreaFor(m => m.Description, new { Class = "DescriptionTextArea" })%></div>
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextAreaFor(m => m.DebitAmount, new { Class = "ShipmentTextArea", style="height:200px", onkeypress = "return imposeMaxLength(this, 100);" })%></div>
                        </td>
                        
                    </tr>
                     <tr>
                        <td valign="top" align="right" style="font-weight:bold">USD
                        </td>
                        <td valign="top">
                        
                        <div style="height:auto;overflow:hidden"><%: Html.TextBoxFor(m => m.DebitUSD, new { Class = "ShipmentInput" })%></div>
                        </td>
                        
                    </tr>
                </table>
                </td>
            </tr>
             <tr style="height:20px;"><td></td></tr>
            <tr>
                <td><div>
                <%: Html.TextAreaFor(m => m.CompanyFrom)%></div>
                </td>
            </tr>
            <tr>
                <td>
                <div style="overflow:hidden;height:auto;padding: 20px 0">
                <div class="InWords"> IN WORDS: </div>
                <div class="InWordsInput"><%: Html.TextBoxFor(m => m.InWords)%></div>
                </div>
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
                        <td><div style="height:auto;overflow:hidden;text-align:center;padding-left:25px"><%: Html.TextBoxFor(m => m.AuthName, new { Class = "ShipmentInput" })%></div>
                        <div style="height:auto;overflow:hidden"><hr class="DebitHr"/></div>
                        <div style="height:auto;overflow:hidden;text-align:center;font-size:11pxr">AUTHORISED SIGNATURE</div>
                        </td>
                    </tr>
                </table>
                </td>
            </tr>
        </table>

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
        <%: Html.ActionLink("Print", "PrintDebitNote", "Shipment", new { id = 0, ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
    <%} %>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            CKEDITOR.replace('CompanyFrom',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();
            new DateTimePicker('DebitDate', 'dd/MM/yyyy');
            jQuery("#DocumentMenuContainer .LinkClose").hide();
            jQuery("#DebitNote").addClass("LinkActive");
        });
    </script>
</asp:Content>
