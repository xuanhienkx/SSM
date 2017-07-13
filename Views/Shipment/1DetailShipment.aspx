<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.ShipmentModel>" %>
<%@ Import Namespace="SSM.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
   SANCO SSM System - Create
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   <% using (Html.BeginForm())
       { %>
   <div class="SectionBlock Expanded BoxL1">
   <style type="text/css">
       .field-validation-error {
        color:#FF0000;
        float:left;
        font-size:1.2em;
        margin:auto 15px;
        padding:0 0 0 135px;
        text-align:center;
    }
   </style>
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                  <table width="100%">
                            <tr><td colspan="2">
                                    <div class="ButtonZone1">
                                        <input id="DetailShipmentButton" type="button" value=" SHIPMENT " title="Create New Shipment" style="background-color:#ED1B2E;"/>
                                        <%: Html.ActionLink("REVENUE", "Revenue", "Shipment", new { id = Model.Id }, new { Class = "RevenueLink" })%>
                                        <input id="DocumentsButton" type="button" value="DOCUMENTS" title="Close this Shipment"/>
                                    </div>
                                        <% RevenueModel RevenueModel1 = new RevenueModel();
                                            RevenueModel1.Id = Model.Id;                         
                                         %>
                                    <% Html.RenderPartial("_DocumentMenu", RevenueModel1); %>
                            </td></tr>
                            <tr>
                                <td style="width:400px">
                                    <div class="ShipmentRow">
                                        <label>Ref</label>
                                        <%: Html.TextBoxFor(m => m.Id, new { disabled = "disabled" })%>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.ShipperId)%>
                                        <select id="ShipperId" name="ShipperId" disabled = "disabled">
                                            <%IEnumerable<Customer> Shippers = (IEnumerable<Customer>)ViewData["ShippersFull"];%>
                                            <% if (Shippers != null && Shippers.Count() > 0)
                                               {
                                                   foreach (Customer Shipper1 in Shippers)
                                                   {%>
                                                       <option class="CustomizeTooltip"  <% if(Shipper1.Id == Model.ShipperId) { %> selected="selected" <%} %> value="<%= Shipper1.Id %>" title="<%= Shipper1.Description %>"> <%=Shipper1.CompanyName%> </option>
                                            <%}
                                               }%>
                                        </select>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:400px">
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.Dateshp)%>
                                        <%: Html.TextBoxFor(m => m.Dateshp, new { disabled = "disabled" })%>
                                    </div>
                                    <div style="height:25px;width:auto;float:left;">
                                    <label for="Dateshp" class="DateInput"></label>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.CneeId)%>
                                         <select id="CneeId" name="CneeId" disabled = "disabled">
                                            <%IEnumerable<Customer> Cnees =  (IEnumerable<Customer>)ViewData["Cnees"];%>
                                            <% if (Cnees != null && Cnees.Count() > 0)
                                               {
                                                   foreach (Customer cnee in Cnees)
                                                   {%>
                                                       <option class="CustomizeTooltip" <% if(cnee.Id == Model.CneeId) { %> selected="selected" <%} %> value="<%= cnee.Id %>" title="<%= cnee.Description %>"> <%=cnee.CompanyName %> </option>
                                            <%}
                                               }%>
                                        </select>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.QtyNumber)%>
                                        <%: Html.TextBoxFor(m => m.QtyNumber, new { disabled = "disabled" })%>
                                        <%: Html.ValidationMessageFor(m => m.QtyNumber)%>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.AgentId)%>
                                        <select id="AgentId" name="AgentId" disabled = "disabled">
                                            <% IEnumerable<Agent> Agents = (IEnumerable<Agent>)ViewData["Agents"];%>
                                            <% if (Agents != null && Agents.Count() > 0)
                                               {
                                                   foreach (Agent Agent1 in Agents)
                                                   {%>
                                                       <option class="CustomizeTooltip" <% if(Agent1.Id == Model.AgentId) { %> selected="selected"<%} %> value="<%= Agent1.Id %>" title="<%= Agent1.Description %>"> <%=Agent1.AbbName%> </option>
                                            <%}
                                               }%>
                                        </select>
                                    </div>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.QtyUnit)%>
                                        <%= Html.DropDownList("QtyUnit", (SelectList)ViewData["Units"], new { disabled = "disabled" })%>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.ServiceName)%>
                                        <%= Html.DropDownList("ServiceName", (SelectList)ViewData["Services"], new { disabled = "disabled" })%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.CarrierAirId)%>
                                        <select id="CarrierAirId" name="CarrierAirId" disabled="disabled">
                                            <% IEnumerable<CarrierAirLine> carriers = (IEnumerable<CarrierAirLine>)ViewData["Carriers"];%>
                                            <% if (Agents != null && Agents.Count() > 0)
                                               {
                                                   foreach (CarrierAirLine carrier in carriers)
                                                   {%>
                                                       <option <% if(carrier.Id == Model.CarrierAirId) { %> selected="selected"<%} %> value="<%= carrier.Id %>" title="<%= carrier.Description %>"> <%=carrier.AbbName%> </option>
                                            <%}
                                               }%>
                                        </select>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.HouseNum)%>
                                        <%: Html.TextBoxFor(m => m.HouseNum, new { disabled = "disabled" })%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ShipmentRow">
                                       <%: Html.LabelFor(m => m.DepartureId)%>
                                       <%= Html.DropDownList("CountryDeparture", (SelectList)ViewData["CountryList"], new { disabled = "disabled" })%>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.MasterNum)%>
                                        <%: Html.TextBoxFor(m => m.MasterNum, new { disabled = "disabled" })%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ShipmentRow">
                                    <label></label>
                                        <%= Html.DropDownList("DepartureId", (SelectList)ViewData["AreaListDep"], "--Please select--", new { disabled = "disabled" })%>
                                        <%: Html.ValidationMessageFor(m=>m.DepartureId) %>
                                    </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                       <%: Html.LabelFor(m => m.SaleType)%>
                                        <%= Html.DropDownList("SaleType", (SelectList)ViewData["SaleTypes"], new { disabled = "disabled" })%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ShipmentRow">
                                       <%: Html.LabelFor(m => m.DestinationId)%>
                                       <%= Html.DropDownList("CountryDestination", (SelectList)ViewData["CountryList"], new { disabled = "disabled" })%>
                                    </div>
                                </td>
                                <td>
                                     <div class="ShipmentRow">
                                        <%: Html.LabelFor(m => m.SFreights)%>
                                       <%: Html.TextBoxFor(m => m.SFreights, new { disabled = "disabled" })%>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ShipmentRow">
                                       <label></label>
                                        <%= Html.DropDownList("DestinationId", (SelectList)ViewData["AreaListDes"], "--Please select--", new { disabled = "disabled" })%>
                                        <%: Html.ValidationMessageFor(m=>m.DestinationId) %>
                                    </div>
                                </td>
                                <td>
                                    
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                <div class="ShipmentRow" style="padding-top:10px;width:145px;">
                                    <label></label>
                                </div>
                                <%: Html.CheckBoxFor(m => m.LockShipment, new { disabled = "disabled" })%>
                                    <span style="font-size:1.2em;">lock shipment. Your shipment automatic to lock at</span>  <span style="font-size:1.2em;color:#ED1B2E;font-weight:bold;">  <%= Model.LockDate%></span>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                <div class="ShipmentRow" style="padding-top:10px">
                                    <label></label>
                                    <span class="Label" style="font-size:1.4em;float:right">Last Update <%= Model.UpdateDate%> by <%= ViewData["UserName"] %></span>
                                    </div>
                                </td>

                                <td>
                                <div class="ShipmentRow" style="padding-top:10px">
                                    <label></label>
                                    <% if (Model.isDelivered)
                                       {%>
                                    <span class="Label" style="font-size:1.4em;">shipment delivered on <%= Model.DeliveredDate %></span>
                                    <%} %>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="ButtonZone"> 
                                    <%:Html.ActionLink("Close", "Index", "Shipment", new { id = 0 }, new { Class = "ShipmentLink", style = "background-color:#ED1B2E;" }) %>
                                   </div>
                                </td>
                                <td>
                                    <div class="ShipmentRow">
                                    <div class="DOZone">
                                       
                                    </div>
                                    <input type="hidden" name="submitType" value="submit" id="submibType" />
                                    </div>
                                </td>
                            </tr>
                        </table>
                </div>
            </div>
        </div>
    </div>
    <%} %>
    <script type="text/javascript" language="javascript">
        jQuery(document).ready(function () {
            jQuery("#FileTab").addClass("Active");
            jQuery('#FileTab').activeThisNav();

            jQuery('#DocumentMenuContainer').hide();

            jQuery("#DocumentMenuContainer .Close").click(function () {
                jQuery('#DocumentMenuContainer').hide();
            });
            jQuery("#DocumentsButton").click(function () {
                jQuery('#DocumentMenuContainer').show();
            });

            jQuery('#submitButton').bind('click', function (event) {
                jQuery('#submibType').val('submit');
            });
            jQuery('#deleteButton').bind('click', function (event) {
                jQuery('#submibType').val('delete');
                jQuery(this).parents('form').submit();
            });
            jQuery('#closeButton').bind('click', function (event) {
                jQuery('#submibType').val('close');
                jQuery(this).parents('form').submit();
            });
            new DateTimePicker('Dateshp', 'dd/MM/yyyy');
            jQuery("#CountryDeparture").change(function () {
                getWeather(jQuery(this).val(), 'DepartureId');
            });
            jQuery("#CountryDestination").change(function () {
                getWeather(jQuery(this).val(), 'DestinationId');
            });
            function getWeather(_CountryId, destination) {
                var URL = "../../Shipment/GetJsonByCountry/0";
                jQuery.getJSON(URL, { CountryId: _CountryId }, function (data) {
                    var result = '<option value="">--Please select--</option>';
                    jQuery.each(data, function (index, d) {
                        if (d.Id != '') {
                            result += '<option value="' + d.Id + '">' + d.AreaAddress + '</option> ';
                        }
                    });
                    jQuery("#" + destination).html(result);
                });
            }
            jQuery("#ServiceName").change(function () {
                getCarrier(jQuery(this).val(), 'CarrierAirId');
                getUnit(jQuery(this).val(), 'QtyUnit');

            });
            function getUnit(_ServiceName, destination) {
                var URL = "../../Shipment/GetUnitJsonByService/0";
                jQuery.getJSON(URL, { ServiceName: _ServiceName }, function (data) {
                    var result = '';
                    jQuery.each(data, function (index, d) {
                        if (d.Id != '') {
                            result += '<option value="' + d.Id + '">' + d.Unit1 + '</option> ';
                        }
                    });
                    jQuery("#" + destination).html(result);
                });
            }
            function getCarrier(_ServiceName, destination) {
                var URL = "../../Shipment/GetCarrierJsonByService/0";
                jQuery.getJSON(URL, { ServiceName: _ServiceName }, function (data) {
                    var result = '';
                    jQuery.each(data, function (index, d) {
                        if (d.Id != '') {
                            result += '<option value="' + d.Id + '">' + d.Address + '</option> ';
                        }
                    });
                    jQuery("#" + destination).html(result);
                });
            }
            /**
            var $optionsCustomer = jQuery(".CustomizeTooltip");
            var i = 0;
            for (i = 0; i < $optionsCustomer.length; i++) {
            var $eachOption = $optionsCustomer[i];
            jQuery($eachOption).mouseover(function (e) {

            //Grab the title attribute's value and assign it to a variable
            var tip = jQuery(this).attr('title');

            //Remove the title attribute's to avoid the native tooltip from the browser
            jQuery(this).attr('title', '');

            //Append the tooltip template and its value
            jQuery(this).parent().parent().append('<div id="tooltip"><div class="tipHeader"></div><div class="tipBody">' + tip + '</div><div class="tipFooter"></div></div>');

            //Set the X and Y axis of the tooltip
            jQuery('#tooltip').css('top', 50);
            jQuery('#tooltip').css('left', 380);

            //Show the tooltip with faceIn effect
            jQuery('#tooltip').fadeIn('500');
            jQuery('#tooltip').fadeTo('10', 0.8);

            }).mousemove(function (e) {

            //Keep changing the X and Y axis for the tooltip, thus, the tooltip move along with the mouse
            jQuery('#tooltip').css('top', 50);
            jQuery('#tooltip').css('left', 380);

            }).mouseout(function () {

            //Put back the title attribute's value
            jQuery(this).attr('title', jQuery('.tipBody').html());

            //Remove the appended tooltip template
            jQuery(this).parent().parent().children('div#tooltip').remove();

            });
            }
            **/
        });
    </script>
</asp:Content>
