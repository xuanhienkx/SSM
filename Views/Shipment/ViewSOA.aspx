<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.SOAModel>" %>

<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     SOA
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <h2>SOA</h2>
                    <% using (Html.BeginForm())
                       {%>
                    <div class="NormalZone" style="margin-bottom: 20px">
                        <table>
                            <tr>
                                <td colspan="2" style="width: 200px;">
                                    <label class="Label">SOA with agent / partner</label></td>
                                <td><%: Html.DropDownListFor(m => m.AgentId, (SelectList)ViewData["AgentsList"], "-- Please select --", new { Class = "ShipmentSelect" })%></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="Label">Within period</label></td>
                                <td>
                                    <label class="Label">From</label></td>
                                <td><%: Html.TextBoxFor(m => m.DateFrom, new { Class = "SOAInputShort" })%>
                                    <label for="DateFrom" class="DateInput"></label>
                                </td>
                                <td>
                                    <label class="Label" style="padding-right: 10px;">To</label></td>
                                <td><%: Html.TextBoxFor(m => m.DateTo, new { Class = "SOAInputShort" })%>
                                    <label for="DateTo" class="DateInput"></label>
                                </td>
                                <td><%: Html.CheckBoxFor(m=>m.IsPayment) %><label for="IsPaymemt" title="Mặc định tìm kết quả full, khi chech vào pending SOA chỉ tìm những Invoiced chưa thanh toán!">Pending SOA</label></td>
                            </tr>
                            <tr>
                                <td colspan="6">
                                    <br />
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td>
                                    <input type="submit" value="View SOA" id="bnt-searchSoa" /></td>
                                <td></td>
                                <td></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                    <%} %>
                    <%
                        IEnumerable<SOAInvoice> ListSOA = (IEnumerable<SOAInvoice>)ViewData["ListSOA"];
                        if (ListSOA == null || ListSOA.Count() <= 0)
                        { %>
                    <label>Data is not Found !.</label>
                    <%}
                        else
                        { %>
                    <div style="height: auto; overflow: hidden; padding: 20px 0;">
                        <table>
                            <tr>
                                <td rowspan="2">
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        TOTAL SOA</label>
                                </td>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        USD</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["USDAmount"] %></label>
                                </td>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        GBP</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["GBPAmount"] %></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        EUR</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["EURAmount"] %>
                                    </label>
                                </td>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        AUD</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["AUDAmount"] %></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="NormalZone" style="padding: 15px 50px;">
                        <label class="Label">Detail SOA</label>
                    </div>
                    <div class="NormalZone">
                        <table class="tableNormal" style="width: 900px" border="1px solid #BFBFBF">
                            <tr>
                                <th>No.</th>
                                <th>Ref</th>
                                <th>ETD/ETA</th>
                                <th>House Bill</th>
                                <th>Master Bill</th>
                                <th>Service</th>
                                <th>Amount1</th>
                                <th>Currency</th>
                                <th>Type Of Note</th>
                                <th>Amount2</th>
                                <th>Currency</th>
                                <th>Type Of Note</th>
                                <th>View</th>
                                <th>SOA
                                    <%if (int.Parse(ViewData["StatusSOA"].ToString()) == 0)
                                      { %>
                                    <img src="../../Content/OSX-Checkbox-OFF@2x.png" alt="uncheck" data-satus="<%=ViewData["StatusSOA"] %>" class="changeIsPayments" />
                                    <%} %>
                                    <% else if (int.Parse(ViewData["StatusSOA"].ToString()) == 1)
                                      { %>
                                    <img src="../../Content/OSX-Checkbox-ON@2x.png" alt="check" class="changeIsPayments" data-satus="<%=ViewData["StatusSOA"] %>" />
                                    <%} %>
                                    <%else
                                      {%>
                                    <img src="../../Content/OSX-Checkbox-INDETERMINATE@2x.png" alt="indeterminate-check" class="changeIsPayments" data-satus="<%=ViewData["StatusSOA"] %>" />
                                    <% } %>
                                      	
                                </th>
                            </tr>
                            <%int no = 0;
                              foreach (SOAInvoice Invoice1 in ListSOA)
                              {
                                  no++;
                                  bool ood = no % 2 != 0;
                            %>
                            <tr <%if (ood)
                                  {%>class="GridLight"
                                <% }%>>
                                <td><%= no%>
                                </td>
                                <td class="idSOA"><%= Invoice1.RevenueId%></td>
                                <td><%= Invoice1.Shipment.DateShp.Value.ToString("dd MMM yyyy")%></td>
                                <td><%= Invoice1.Shipment.HouseNum%></td>
                                <td><%= Invoice1.Shipment.MasterNum%></td>
                                <td><%= Invoice1.Shipment.ServiceName%></td>
                                <td><%= (Invoice1.Amount1 != null? Invoice1.Amount1.Value.ToString("0.00"):"0.00")%></td>
                                <td><%= Invoice1.Currency1%></td>
                                <td><%= Invoice1.TypeNote1%></td>
                                <td><%= (Invoice1.Amount2 != null ? Invoice1.Amount2.Value.ToString("0.00") : "0.00")%></td>
                                <td><%= Invoice1.Currency2%></td>
                                <td><%= Invoice1.TypeNote2%></td>
                                <td >
                                    <span title="View Detail"  style="cursor: pointer" onclick="GetDetailInvoice(<%=Invoice1.RevenueId %>);">
                                    <img alt="View" src="../../Images/btn-view.png" /></span>
                                </td>
                                <td>
                                    <% if (Invoice1.IsPayment)
                                       {%>
                                    <input type="checkbox" id="IsPayment" invoiceid="<%= Invoice1.Id %>" name="IsPayment" checked="checked" class="changeIsPayment" />
                                    <%}
                                       else
                                       {%>
                                    <input type="checkbox" id="IsPayment" invoiceid="<%= Invoice1.Id %>" name="IsPayment" class="changeIsPayment" />
                                    <%} %>
                                    
                                </td>
                            </tr>
                            <%} %>
                        </table>
                    </div>
                    <div class="NormalZone"></div>

                    <div style="height: auto; overflow: hidden; padding: 20px 0;">
                        <table>
                            <tr>
                                <td rowspan="2">
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        TOTAL SOA</label>
                                </td>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        USD</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["USDAmount"] %></label>
                                </td>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        GBP</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["GBPAmount"] %></label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        EUR</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["EURAmount"] %>
                                    </label>
                                </td>
                                <td>
                                    <label class="Label" style="padding: 0 20px 0 15px;">
                                        AUD</label>
                                </td>
                                <td>
                                    <label style="font-size: 12px; color: Red; font-family: Arial;">
                                        <%= ViewData["AUDAmount"] %></label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <%
                        }
                    %>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        jQuery(document).ready(function () {
            jQuery("#AccountingTab").addClass("Active");
            jQuery('#AccountingTab').activeThisNav();
            new DateTimePicker('DateFrom', 'dd/MM/yyyy');
            new DateTimePicker('DateTo', 'dd/MM/yyyy');

            jQuery("#DateFrom").change(function () {
                getAgents(jQuery(this).val(), jQuery('#DateTo').val(), 'AgentId');
            });
            jQuery("#DateTo").change(function () {
                getAgents(jQuery('#DateFrom').val(), jQuery(this).val(), 'AgentId');
            });
            //   jQuery(".changeIsPayment").each(function () {
            jQuery(".changeIsPayment").on("click", function () {
                var $t = jQuery(this);
                var id = $t.attr("invoiceId");
                var ids = id + ";";
                var check = $t.is(":checked");
                var url = "../../Shipment/ChangePaymentSoa";
                var data = JSON.stringify({ "ids": ids, "isPament": check });
                jQuery.ajax({
                    url: url,
                    type: 'post',
                    dataType: 'json',
                    contentType: "application/json; charset=utf-8",
                    data: data,
                    success: function (result) {
                        var checked = 0;
                        var unckecked = 0;
                        jQuery(".changeIsPayment").each(function () {
                            var $it = jQuery(this);
                            if ($it.is(":checked")) {
                                checked  ++;
                            } else {
                                unckecked++;
                            } 
                        });
                        if (checked > 0 && unckecked == 0) {
                            jQuery(".changeIsPayments").attr("data-satus", "1");
                            jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-ON@2x.png");
                        } else if (checked == 0 && unckecked > 0) {
                            jQuery(".changeIsPayments").attr("data-satus", "0");
                            jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-OFF@2x.png");
                        } else {
                            jQuery(".changeIsPayments").attr("data-satus", "2");
                            jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-INDETERMINATE@2x.png");
                        }
                    }
                });
            });
        });
        jQuery(".changeIsPayments").on("click", function () {
            var $t = jQuery(this);
            var status = $t.attr("data-satus");
            var ids = "";
            jQuery(".changeIsPayment").each(function () {
                var $it = jQuery(this);
                var id = $it.attr("invoiceId");
                ids += id + ";";
            });
            var check = 0;
            if (status == 1) {
                check = false;
                jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-OFF@2x.png");
            } else {
                check = true;
                jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-ON@2x.png");
            }
            var url = "../../Shipment/ChangePaymentSoa";
            var data = JSON.stringify({ "ids": ids, "isPament": check });
            jQuery.ajax({
                url: url,
                type: 'post',
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: data,
                success: function (result) {
                    if (status == 1) {
                        $t.attr("data-satus", "0");
                        jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-OFF@2x.png");
                        jQuery(".changeIsPayment").each(function () {
                            var $it = jQuery(this);
                            $it.prop("checked", false);
                        });
                    } else {
                        $t.attr("data-satus", "1");
                        jQuery(".changeIsPayment").each(function () {
                            var $it = jQuery(this);
                            $it.prop("checked", true);
                        });
                        jQuery(".changeIsPayments").attr("src", "../../Content/OSX-Checkbox-ON@2x.png");
                    }

                    //jQuery("#bnt-searchSoa").click();
                }
            });

        });
        function getAgents(dateFrom, dateTo, destination) {
            var URL = "../../Shipment/GetAgentsJsonByService";
            jQuery.getJSON(URL, { DateFrom: dateFrom, DateTo: dateTo }, function (data) {
                var result = '';
                jQuery.each(data, function (index, d) {
                    if (d.Id != '') {
                        result += '<option value="' + d.Id + '" title="' + d.FullName + '">' + d.AbbName + '</option> ';
                    }
                });
                jQuery("#" + destination).html(result);
            });
        }

        function GetDetailInvoice(ids) {
            var url = '<%= Url.Action("RevenueDetail","Shipment") %>';
            jQuery.mbqAjax({
                url: url,
                method: "GET",
                dataType: 'html',
                data:{id:ids}, 
                success: function (result) { 
                    if (result != null || result !== undefined) { 
                        jQuery.mbqDialog({
                            title: "Reveneu " + ids,
                            content: result,
                            columnClass: 'col-md-9 col-md-offset-2',
                            theme: 'bootstrap',
                        });
                    }
                }
            });
        }
    </script> 
</asp:Content>
