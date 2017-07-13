<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>
<%@ Import Namespace="SSM.Models"%>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SCF HCM SSM System - UnIssued Invoice
</asp:Content> 

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%
        IEnumerable<Shipment> ListUnIssuedInvoice = (IEnumerable<Shipment>)ViewData["ListUnIssuedInvoice"];
        %>
        <div class="SectionBlock Expanded BoxL1">
            <div class="BoxL2">
                <div class="BoxL3">
                    <div class="BoxL4">
                    <div class="DivHeader"><h2>Un Issued Invoice List</h2></div>
                    <%if (ListUnIssuedInvoice != null && ListUnIssuedInvoice.Count() > 0)
                      { %>
                    <table class="grid" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th>
                                No
                            </th>
                            <th>
                                Ref #
                            </th>
                            <th>
                                ETD/ETA
                            </th>
                            <th>
                                Shipper
                            </th>
                            <th>
                                CNEE
                            </th>
                             <th>
                                Instruction Issue Invoice
                            </th>
                            <th class="t-last">
                                See Detail
                            </th>
                        </tr>
        <%
            
                        int no = 0;
                        foreach (Shipment AUnIssued in ListUnIssuedInvoice)
                        {
                            no++;
                            bool ood = no % 2 == 0;
                            // DateTime DuringDate = DateTime.FromBinary(DateTime.Now.ToBinary() - AUnIssued.DateShp.Value.ToBinary());
                            long LDays = DateTime.Now.DayOfYear - AUnIssued.DateShp.Value.Day;//365 * (DuringDate.Year - 1) + DuringDate.DayOfYear;
                            if (LDays >= 0)
                            {
                  %>
                      <tr <%if(!ood) {%>class="GridLight" <% }%>>
                      <td><%= no%></td>
                      <td><%= AUnIssued.Id%></td>
                      <td><%= AUnIssued.DateShp.Value.ToString("dd/MM/yyyy")%></td>
                      <td><%= AUnIssued.Customer.CompanyName%></td>
                      <td><%= AUnIssued.Customer1.CompanyName%></td>
                      <td><%= AUnIssued.Revenue != null ? AUnIssued.Revenue.Description4Invoice : ""%></td>
                      <td class=" ">
                          <span title="View Detail"  style="cursor: pointer"  onclick="GetDetailInvoice(<%=AUnIssued.Id %>)">
                              <img alt="View" src="../../Images/btn-view.png"/>
                          </span>
                      </td>
                      </tr>
                  <%}
                        }
                 %>
                    </table>
                    <%}
                      else
                      { %>
                      Data is not found!!!
                    <%} %>
                    
                    </div>
                </div>
            </div>
        </div>
<script language="javascript" type="text/javascript">
    jQuery(document).ready(function () {
        jQuery("#AccountingTab").addClass("Active");
        jQuery('#AccountingTab').activeThisNav();
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
