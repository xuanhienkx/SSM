<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.FindInvoice>" %>

<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    SCF HCM SSM System - Find Invoice
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <h2>Find Invoice</h2>
                    <% using (Html.BeginForm())
                       {%>
                    <div class="NormalZone" style="margin-bottom: 20px">
                        <table cellpadding="1">
                            <tr>
                                <td style="width: 120px;">
                                    <label class="Label">Find invoice No</label>
                                </td>
                                <td>
                                    <%: Html.TextBoxFor(m => m.InvoiceNo, new { Class = "ShipmentInput", MAXLENGTH = "15"})%>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td colspan="2">
                                   <%-- <label class="NoteLabel">(type 3 last numer)</label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="Label">Issue Priod </label>
                                </td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <%= Html.RadioButtonFor(m => m.ShipmentPriod, 1, new { Class = "ShipmentCheckBox" })%>
                                    <label class="NoteLabel">Shipment Priod</label>
                                </td>
                                <td>
                                    <%= Html.RadioButtonFor(m => m.ShipmentPriod, 0, new { Class = "ShipmentCheckBox" })%>
                                    <label class="NoteLabel">Invoice Issue Priod</label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <%: Html.TextBoxFor(m => m.DateFrom, new { Class = "ShipmentInput" })%>
                                    <div style="height: 25px; width: auto; float: left; padding-right: 20px;">
                                        <label for="DateFrom" class="DateInput"></label>
                                    </div>
                                </td>
                                <td>
                                    <%: Html.TextBoxFor(m => m.DateTo, new { Class = "ShipmentInput"})%>
                                    <div style="height: 25px; width: auto; float: left;">
                                        <label for="DateTo" class="DateInput"></label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="Label">Ref No# </label>
                                </td>
                                <td>
                                    <%: Html.TextBoxFor(m => m.ShipmentId, new { Class = "ShipmentInput"})%>
                                </td>
                                <td></td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <label class="Label" style="padding-right: 15px;">Search the shipment approved but not issue invoice yet</label>
                                    <%: Html.CheckBoxFor(m => m.UnIssueInvoice, new { Class = "ShipmentCheckBox" })%>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>
                                    <input type="submit" value="Find Issue" />
                                </td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <div class="NormalZone">
                        <% IEnumerable<InvoideIssued> FindInvoices = (IEnumerable<InvoideIssued>)ViewData["FindInvoices"];
                           if (FindInvoices == null || FindInvoices.Count() <= 0)
                           {
                        %>
                        <label>Data is not Found!</label>
                        <%}
       else
       { %>
                        <table class="grid" width="850px">
                            <tr>
                                <th>NO.
                                </th>
                                <th>Ref #
                                </th>
                                <th>ETD / ETA
                                </th>
                                <th>Shipper
                                </th>
                                <th>CNEE
                                </th>
                                <th>Invoice NO.
                                </th>
                                <th>Date Issue
                                </th>
                                <th>See</th>
                            </tr>
                            <% int no = 0;
                               foreach (InvoideIssued Invoice1 in FindInvoices)
                               {
                                   no++;
                                   bool ood = no % 2 == 0;
                            %>
                            <tr <%if (!ood)
                                  {%>class="GridLight" <% }%>>
                                <td><%= no %>
                                </td>
                                <td>
                                    <%= Invoice1.ShipmentId%>
                                </td>
                                <td>
                                    <%= Invoice1.Shipment.DateShp.Value.ToString("dd/MM/yyyy")%>
                                </td>
                                <td>
                                    <%= Invoice1.Shipment.Customer.CompanyName%>
                                </td>
                                <td>
                                    <%= Invoice1.Shipment.Customer1.CompanyName%>
                                </td>
                                <td>
                                    <%= Invoice1.InvoiceNo%>
                                </td>
                                <td>
                                    <%= Invoice1.Date != null ? Invoice1.Date.Value.ToString("dd/MM/yyyy"):""%>
                                </td>
                                <td class=" ">
                                    <span title="View Detail" style="cursor: pointer" onclick="GetDetailInvoice(<%=Invoice1.RevenueId %>)">
                                        <img alt="View" src="../../Images/btn-view.png" />
                                    </span>
                                </td>

                            </tr>
                            <%} %>
                        </table>
                        <%} %>
                    </div>
                    <%} %>
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#AccountingTab").addClass("Active");
            jQuery('#AccountingTab').activeThisNav();
            new DateTimePicker('DateFrom', 'dd/MM/yyyy');
            new DateTimePicker('DateTo', 'dd/MM/yyyy');
        });
        function GetDetailInvoice(ids) {
            var url = '<%= Url.Action("RevenueDetail","Shipment") %>';
            jQuery.mbqAjax({
                url: url,
                method: "GET",
                dataType: 'html',
                data: { id: ids },
                success: function (result) {
                    if (result != null || result !== undefined) {
                        jQuery.mbqDialog({
                            title: "Reveneu " + ids,
                            content: result,
                            columnClass: 'col-md-9 col-md-offset-2',
                            theme: 'hololight',
                        });
                    }
                }
            });
        }
    </script>
</asp:Content>
