<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.TransitmentAdvisedModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 TransitmentAdvised
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<% using (Html.BeginForm())
   { %>
    <% RevenueModel RevenueModel1 = new RevenueModel();
       RevenueModel1.Id = Model.ShipmentId;    
                                         %>
        <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>

        <div class="TransitmentAdvisedContrainer">
            <div class="CompanyIcon"><img src="../../Images/transitment.png"/></div>
            <div class="RefZone">Ref #: <span style="font-weight:bold;"><%= Model.ShipmentId %></span></div>
            <div class="TransitmentTo"><span>To</span><%: Html.TextAreaFor(m=>m.AdvisedTo) %></div>
            <div class="TransitmentATTN"><span>ATTN:</span><%: Html.TextBoxFor(m=>m.AdvisedATTN) %></div>
            <div class="TransitmentTitle">TRANSITMENT  ADVISED</div>
            <div class="WouldLike">We would like to inform that your cargo is loaded on the vessel as below</div>
            <div class="TransitmentBL"><span>* B\L :</span><%: Html.TextBoxFor(m=>m.AdvisedBL) %></div>
            <div class="GrossWeight">
            <div class="GrossWeightLeft">*</div>
            <div class="GrossWeightRight">
                <table>
                    <tr><td class="Title">Gross weight.kg</td><td>Measurement M3</td></tr>
                     <tr><td><%: Html.TextBoxFor(m=>m.Grossweight) %></td><td><%: Html.TextBoxFor(m=>m.Measurement) %></td></tr>
                </table>
              </div>
            </div>
            <div class="MainContent">
                <table border="1px solid Silver">
                    <tr>
                        <th>Vessel
                        </th>
                        <th>Port of loading
                        </th>
                        <th>ETD
                        </th>
                        <th>Port of discharge
                        </th>
                        <th>ETA
                        </th>
                    </tr>
                    <tr>
                        <td><%: Html.TextAreaFor(m => m.Vessel, new { Class = "Textarea" })%>
                        <%: Html.HiddenFor(m => m.Id)%>
                        </td>
                        <td><%: Html.TextAreaFor(m => m.Portofloading, new { Class = "Textarea" })%>
                        </td>
                        <td style="vertical-align:top;"><%: Html.TextBoxFor(m=>m.ETD) %>
                        </td>
                        <td><%: Html.TextAreaFor(m => m.Portofdischarge, new { Class = "Textarea" })%>
                        </td>
                        <td style="vertical-align:top;"><%: Html.TextBoxFor(m=>m.ETA) %>
                        </td>
                    </tr>
                    <tr><td></td><td></td><td><br /></td><td></td><td></td>
                    </tr>
                </table>
            </div>
            <div class="Note"><span>Note : the above information is provided by carrier.</span></div>
            <div class="Note"><span>Any change will be up-date to customer soon.</span></div>
            <div class="Thanhks"><span>Please contact us for detailed information</span></div>
            <div class="Thanhks"><span>Thanhks and best regards,</span></div>
            <div class="CustomerServices"><span>Customer Services</span></div>
            <div class="Tell"><span>Tell : </span> <%: Html.TextBoxFor(m=>m.CustomerServices) %></div>
            <div class="PayCharge">
                <table>
                    <tr>
                        <td style="width:500px;">
                            <div class="PayChargeLeft">
                                <table>
                                    <tr><td colspan="2" style="font-weight:bold;">FAX OUT</td><td colspan="3"></td>
                                    </tr>
                                    <tr><td colspan="2">Times \ Date:</td><td colspan="3"><%: Html.TextBoxFor(m=>m.AdvisedTime) %></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Status
                                        </td>
                                        <td>
                                            <%:Html.CheckBoxFor(m=>m.StatusOk) %>
                                        </td>
                                        <td>Ok
                                        </td>
                                        <td><%:Html.CheckBoxFor(m=>m.StatusNoReport) %>
                                        </td>
                                        <td>No Report 
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td><%:Html.CheckBoxFor(m=>m.StatusReport) %> 
                                        </td>
                                        <td>Report 
                                        </td>
                                        <td><%:Html.CheckBoxFor(m=>m.MachineNoReport) %>
                                        </td>
                                        <td>Machine no report
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                        <td>
                            <div class="PayChargeRight">
                            <div class="PayTitle">PLS PAY CHARGE AS BELOW</div>
                            <div class="PayContent">
                                <table>
                                    <tr><td>* Freight:</td><td><%:Html.TextBoxFor(m=>m.ChargeFreight) %></td>
                                    </tr>
                                    <tr><td>* Bill: </td><td><%:Html.TextBoxFor(m=>m.ChargeBill) %></td>
                                    </tr>
                                    <tr><td>* THC:</td><td><%:Html.TextBoxFor(m=>m.ChargeTHC) %></td>
                                    </tr>
                                    <tr><td>* AMS:</td><td><%:Html.TextBoxFor(m=>m.ChargeAMS) %></td>
                                    </tr>
                                    <tr><td>* + VAT for above</td><td><%:Html.TextBoxFor(m=>m.ChargeVAT) %></td>
                                    </tr>
                                </table>
                            </div>
                            </div>
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
        <%: Html.ActionLink("Print", "PrintTransitmentAdvised", "Shipment", new { ShipmentId = Model.ShipmentId }, new { Class = "LinkForm", target = "_blank" })%>
        </div>
 </div>
    <%}
         %>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#FileTab").addClass("Active");
        jQuery('#FileTab').activeThisNav();
        new DateTimePicker('DebitDate', 'dd/MM/yyyy');
        jQuery("#DocumentMenuContainer .LinkClose").hide();
        jQuery("#TransitmentAdvised").addClass("LinkActive");
    });
    </script>
</asp:Content>
