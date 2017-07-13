<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.FreightModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace ="SSM.Controllers"%>
<%@ Import Namespace="System.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
     Create OcreanFreight
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                <h4 class="Subject">Create Freight</h4>
    <% using (Html.BeginForm("CreateFreight", "Freight", FormMethod.Post, new { enctype = "multipart/form-data" }))
       {%>
    <table width="850px" cellspacing="0" cellpadding="5">
        <tr>
            <td width="120px">
                <label class="RateLabel">
                    From</label>
            </td>
            <td width="300px">
                <%= Html.DropDownList("CountryNameDep", (SelectList)ViewData["CountryList"], new { Class = "RateSelect" })%>
            </td>
            <td width="120px">
                <label class="RateLabel">
                    To</label>
            </td>
            <td width="310px">
                <%= Html.DropDownList("CountryNameDes", (SelectList)ViewData["CountryList"], new { Class = "RateSelect" })%>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <%= Html.DropDownList("DepartureId", (SelectList)ViewData["AreaListDep"], "--Please select--", new { Class = "RateSelect" })%>
                <%: Html.ValidationMessageFor(m => m.DepartureId)%>
            </td>
            <td>
            </td>
            <td>
                <%= Html.DropDownList("DestinationId", (SelectList)ViewData["AreaListDes"], "--Please select--", new { Class = "RateSelect" })%>
                <%: Html.ValidationMessageFor(m => m.DestinationId)%>
            </td>
        </tr>
        <tr>
            <td>
                <label class="RateLabel">
                    Carrier</label>
            </td>
            <td>
                <select id="AirlineId" name="AirlineId" class="RateSelect">
                <option value="">--Select--</option>
                                            <%IEnumerable<CarrierAirLine> Carriers =  null;%>
                                            <% Carriers = (IEnumerable<CarrierAirLine>)ViewData["AirlineList"];%>
                                            <% if (Carriers != null && Carriers.Count() > 0)
                                               {
                                                   foreach (CarrierAirLine Carrier1 in Carriers)
                                                   {%>
                                                       <option <% if(Model != null && Carrier1.Id == Model.AirlineId) { %> selected="selected"<%} %> class="CustomizeTooltip" value="<%= Carrier1.Id %>" title="<%= Carrier1.Description %>"> <%=Carrier1.CarrierAirLineName%> </option>
                                            <%}
                                               }%>
                                        </select>
                                        <div style="float:left;padding:5px 0 0 5px;overflow:hidden">
                                        <%: Html.ActionLink("Add New", "Carrier", "Shipment", new { id = 0 }, new { Class = "AddNewAgent", title = "Add New Carrier" })%>
                                        </div>
                                        <%: Html.ValidationMessageFor(m => m.AirlineId)%>
            </td>
            <td>
                <label class="RateLabel">
                    Freight Type</label>
            </td>
            <td>
                <%: Html.DropDownList("Type", (SelectList)ViewData["FreightTypes"], new { Class = "RateSelect" })%>
            </td>
            <td></td>
        </tr>
         <tr>
            <td>
                <label class="RateLabel">
                    Agent</label>
            </td>
            <td>
                <select id="AgentId" name="AgentId" class="RateSelect">
                <option value="">--Select--</option>
                                            <%IEnumerable<Agent> Cnees =  null;%>
                                            <% Cnees = (IEnumerable<Agent>)ViewData["AgentList"];%>
                                            <% if (Cnees != null && Cnees.Count() > 0)
                                               {
                                                   foreach (Agent cnee in Cnees)
                                                   {%>
                                                       <option <% if(Model != null && cnee.Id == Model.AgentId) { %> selected="selected"<%} %> class="CustomizeTooltip" value="<%= cnee.Id %>" title="<%= cnee.Description %>"> <%=cnee.AbbName %> </option>
                                            <%}
                                               }%>
                                        </select>
                                        <div style="float:left;padding:5px 0 0 5px;overflow:hidden">
                                        <%: Html.ActionLink("Add New", "Agent", "Shipment", new { id =0 }, new { Class = "AddNewAgent", title="Add New Agent" })%>
                                        </div>
                                        <%: Html.ValidationMessageFor(m => m.AgentId)%>
                                        
            </td>
            <td>
                <label class="RateLabel">
                    Valid Date</label>
            </td>
            <td>
                <%: Html.TextBoxFor(m => m.ValidDate, new { Class = "RateText" })%>
                <label for="ValidDate" class="RevenueInputDate"></label>
                <%: Html.ValidationMessageFor(m => m.ValidDate)%>
            </td>
            <td>
            </td>
        </tr>
         <tr>
           <td>
                <label class="RateLabel">
                    Contents</label>
            </td>
            <td colspan="4">
                <%: Html.TextAreaFor(m => m.Remark, new { Class = "RateTextArea" })%>
            </td>
        </tr><tr>
                <td>
                    <label class="RateLabel">Attachment a File</label>
                </td>
                <td colspan="4">
                        <div class="fakeupload">
		                    <input type="text" name="fakeupload" /> <!-- browse button is here as background -->
                        </div>
                        <div class="DivRealUpload">
		                    <input type="file" name="upload" id="File2" class="realupload" onchange="this.form.fakeupload.value = this.value;" />
	                    </div>
	                    
                </td>
            </tr>
            <tr>
            <td>
            </td>
            <td>
                <input type="submit" value="Create"/>
                <input type="button" value="Cancel" onclick="cancel(); return false;" style="width:75px;background-color:#ED1B2E"/>
            </td>
            <td>
            </td>
            <td colspan="2">
            </td>
        </tr>
    </table>
    <%} %>
    </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
            //<![CDATA[

            CKEDITOR.replace('Remark',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});

            //]]>
            jQuery("#RatesTab").addClass("Active");
            jQuery('#RatesTab').activeThisNav();
            jQuery("#CountryNameDep").change(function () {
                getWeather(jQuery(this).val(), 'DepartureId');
            });
            new DateTimePicker('ValidDate', 'dd/MM/yyyy');
            jQuery("#CountryNameDes").change(function () {
                getWeather(jQuery(this).val(), 'DestinationId');
            });
            jQuery("#Type").change(function () {
                getCarrierSelect(jQuery(this).val());
            });
            function getCarrierSelect(_FreightType) {
                var URL = "/Freight/GetJsonByFreightType/";
                jQuery.getJSON(URL, { FreightType: _FreightType }, function (data) {
                    var result = '';
                    jQuery.each(data, function (index, d) {
                        if (d.Id != '') {
                            result += '<option class="CustomizeTooltip" value="' + d.Id + '" title="' + d.Description + '">' + d.CarrierAirlineName + '</option> ';
                        }
                    });
                    jQuery("#AirlineId").html(result);
                });
            }
            function getWeather(_CountryId, destination) {
                var URL = "/Freight/GetJsonByCountry/";
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

            jQuery("input[type='text']").each(function () {
                if (jQuery(this).hasClass("Freights")) {
                    var number = jQuery(this).val();
                    if (jQuery.trim(number) != "" && !isNaN(number)) {
                        jQuery(this).val(parseInt(number).toFixed(2));
                    }
                    else {
                        jQuery(this).val("0.00");
                    }
                }
            });
            
            function initToolTip() {
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
                        jQuery('#tooltip').css('top', 80);
                        jQuery('#tooltip').css('left', 0);

                        //Show the tooltip with faceIn effect
                        jQuery('#tooltip').fadeIn('500');
                        jQuery('#tooltip').fadeTo('10', 0.8);

                    }).mousemove(function (e) {

                        //Keep changing the X and Y axis for the tooltip, thus, the tooltip move along with the mouse
                        jQuery('#tooltip').css('top', 80);
                        jQuery('#tooltip').css('left', 0);

                    }).mouseout(function () {

                        //Put back the title attribute's value
                        jQuery(this).attr('title', jQuery('.tipBody').html());

                        //Remove the appended tooltip template
                        jQuery(this).parent().parent().children('div#tooltip').remove();

                    });
                }
            }
        });
    </script>
</asp:Content>

