<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="SSM.Views.Freight.ListOceanFreight" CodeBehind="ListFreight.aspx.cs"%>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace ="SSM.Controllers"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	 Freight List
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="SectionBlock Expanded BoxL1">
        <div class="BoxL2">
            <div class="BoxL3">
                <div class="BoxL4">
                    <h4 class="Subject">
                        List of <%= ViewData["FreightType"] %></h4>
                        <% User User1 = (User)Session[AccountController.USER_SESSION_ID];
                            using (Html.BeginForm())
                            { %>
                            <table id="quickSearch">
                                <tr><td><label class="RateListLabel">QUICK TRACING CARRIER CODE</label></td><td><%: Html.TextBoxFor(m => m.AirlineName, new {Class="RateListText" })%></td><td></td></tr>
                            </table>
                            <br />
                            <br />
                    <table id="FormSearch">
                        <tr>
                            <td>
                                <label class="RateListLabel">
                                    Select for rate</label>
                            </td>
                            <td>
                            </td>
                            <td>
                            <%= Html.HiddenFor(m=>m.SortType) %>
                            <%= Html.HiddenFor(m=>m.SortOder) %>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="RateListLabel">
                                    Original</label>
                            </td>
                            <td>
                                <%= Html.DropDownList("CountryNameDep", (SelectList)ViewData["CountryList"], "--Please select--", new { Class = "RateListSelect" })%>
                            </td>
                            <td>
                                <%= Html.DropDownList("DepartureId", (SelectList)ViewData["AreaListDep"], "--Please select--", new { Class = "RateListSelect" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <label class="RateListLabel">
                                    Destination</label>
                            </td>
                            <td>
                                <%= Html.DropDownList("CountryNameDes", (SelectList)ViewData["CountryList"], "--Please select--", new { Class = "RateListSelect" })%>
                            </td>
                            <td>
                            <%= Html.DropDownList("DestinationId", (SelectList)ViewData["AreaListDes"], "--Please select--", new { Class = "RateListSelect" })%>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <input type="submit" value="Search" />
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <br />
                            <table id="FreightBody" class="grid" width="100%">
                                <tr>
                                    <th>STT
                                    </th>
                                    <th>
                                    From
                                    </th>
                                    <th>
                                    To
                                    </th>
                                    <th>
                                    <a href="#" onclick="sortAction('Carrier');"><div style="float:inherit">Carrier</div> <div id="Carrier_Title" class="SortHeader"></div></a>
                                    </th>
                                    <th>
                                    <a href="#" onclick="sortAction('Agent');"><div style="float:inherit">Agent</div> <div id="Agent_Title" class="SortHeader"></div></a>
                                    </th>
                                    <th>
                                    <a href="#" onclick="sortAction('ValidDate');"><div style="float:inherit">Valid Until</div> <div id="ValidDate_Title" class="SortHeader"></div></a>
                                    
                                    </th>
                                    <th>
                                    Update By
                                    </th>
                                    <th>Documents</th>
                                    <th>
                                    Edit
                                    </th>
                                    <th>
                                    Delete
                                    </th>
                                </tr>
                                <%
                                    IEnumerable<Freight> FreightList1 = (IEnumerable<Freight>)ViewData["FreightList1"];
                                    if (FreightList1 != null && FreightList1.Count() > 0)
                                    {
                                        int count = 0;
                                        foreach (Freight Freight1 in FreightList1)
                                        {
                                            count++;
                                            bool ood = count % 2 == 0;
                                 %>
                                 <tr <%if(ood) {%>class="GridLight" <% }%>>
                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%= count %></a></td>
                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%=Freight1.Area.AreaAddress %></a></td>
                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%=Freight1.Area1.AreaAddress%></a></td>

                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%=Freight1.CarrierAirLine.CarrierAirLineName %></a></td>
                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%=Freight1.Agent.AgentName %></a></td>
                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%=Freight1.ValidDate.Value.ToString("dd/MM/yyyy")%></a></td>
                                 <td><a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><%=Freight1.User.FullName %></a></td>
                                 <td><%= getDocumentsBy(Freight1.Id)%></td>
                                 <td>
                                <a href="<%= Url.Action("EditFreight","Freight",new { id=Freight1.Id}) %>"><img alt="Edit" src="../../Images/btn-edit.png"/></a></td>
                      <td class="t-last">
                      <% if (User1.Id == Freight1.UserId || UsersModel.isAdminOrDirctor(User1))
                         {%>
                                            <a onclick="return confirm('Are you sure you want to delete?')" href="<%= Url.Action("Delete","Freight",new { id=Freight1.Id}) %>"><img alt="Delete" src="../../Images/btn-delete.png"/></a>
                                            <%} %>
                                            </td>
                                 </tr>
                                 <%}
                                    }%>
                            </table>
                            
                        <%} %>
                        <table></table>
                </div>
            </div>
        </div>
    </div>
    <script language="javascript" type="text/javascript">
        function sortAction(fieldSort) {
            jQuery("#SortType").val(fieldSort);
            var sord = jQuery("#SortOder").val();
            if (sord == 'asc') {
                jQuery("#SortOder").val('desc');
            } else {
                jQuery("#SortOder").val('asc');
            }
            submitForm();
        }
        function submitForm() {
            var form = jQuery("#quickSearch").parents("form:first");
            form.submit();
        }
        jQuery(document).ready(function () {
            jQuery("#RatesTab").addClass("Active");
            jQuery('#RatesTab').activeThisNav();

            var sord = jQuery("#SortOder").val();
            var sidx = jQuery("#SortType").val();
            jQuery(".SortHeader").each(function (index) {
                jQuery(this).html('');
            });
            if (sord == 'asc') {
                jQuery("#" + sidx + "_Title").html('<img src="../../Images/sort_asc.gif"/>');
            } else {
                jQuery("#" + sidx + "_Title").html('<img src="../../Images/sort_desc.gif"/>');
            }

            jQuery("#CountryNameDep").change(function () {
                getWeather(jQuery(this).val(), 'DepartureId');
            });

            jQuery("#CountryNameDes").change(function () {
                getWeather(jQuery(this).val(), 'DestinationId');
            });
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
        });
    </script>            
</asp:Content>

