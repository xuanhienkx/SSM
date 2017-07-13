<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM.Controllers" %>
<%@ Import Namespace="SSM.Services" %>
<% User User1 = (User)Session[SSM.Controllers.AccountController.USER_SESSION_ID];%>
<div id="mainNav">
    <div class="MainNavHolder">
        <div class="MainNavHolderL2">
            <ul id="mainNavBar">
                <li class="MainNavTab Active" id="FileTab"><a href="<%= Url.Action("Index", "Shipment", new {id=0}) %>" title="Administrator"><span>Home</span></a>
                    <ul>
                        <li class="MainNavGroup"><a href="#" title="Properties">Shipment Managerment</a>
                            <ul>
                                <%if (User1.IsFreight())
                                  {%>
                                  <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/shipmetCr.png);" href="<%= Url.Action("Create", "Shipment", new {id=0}) %>" title="New shipment">New shipment</a></li>
                                 <%} %>
                              
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-edit.png);" href="<%= Url.Action("Index", "Shipment", new {Id=0}) %>" title="Register">Shipment List</a></li>
                          
                                  </ul>
                        </li>
                        <li class="MainNavGroup"><a href="#" title="Properties">Shipment Managerment Consol</a>
                            <ul>
                                <%if (User1.IsFreight())
                                  {%>
                                 <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/shipmentcreatedCt.jpg);" href="<%= Url.Action("CreateControl", "Shipment", new {id=0}) %>" title="New shipment">New shipment Consol</a></li>
                                <%} %>
                              
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-edit.png);" href="<%= Url.Action("Index", "Shipment", new {Id=0}) %>" title="Register">Shipment List Consol</a></li>
                           </ul>
                        </li>
                    </ul>
                </li>

                <li class="MainNavTab" id="RatesTab"><a href="<%= Url.Action("ListFreight", "Freight", new {id = 0, FreightType="OceanFreight"}) %>" title="Contents"><span>Rates</span></a>
                    <ul>
                        <% if (User1.AllowUpdateSeaRate || User1.AllowUpdateAirRate)
                           {%>
                        <li class="MainNavGroup"><a href="#" title="Creation">Create Freight</a>
                            <ul>
                                <% if (User1.AllowUpdateSeaRate)
                                   { %>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-add.png);" href="<%= Url.Action("CreateFreight", "Freight", new { id = 0 }) %>" title="Create Freight">New Freight</a></li>
                                <%} %>
                            </ul>
                        </li>
                        <%} %>
                        <li class="MainNavGroup"><a href="#" title="Modification">List Freight</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-data-site-archives.png);" href="<%= Url.Action("ListFreight", "Freight", new {id = 0, FreightType="OceanFreight"}) %>" title="Editorial">Ocean Feight</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-data-site-archives.png);" href="<%= Url.Action("ListFreight", "Freight", new {id = 0, FreightType="AirFreight"}) %>" title="Form">Air Freight</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-data-site-archives.png);" href="<%= Url.Action("ListFreight", "Freight", new {id = 0, FreightType="InlandRates"}) %>" title="Survey">Inland Rates</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-data-site-archives.png);" href="<%= Url.Action("ListFreight", "Freight", new {id = 0, FreightType="CompanyTariff"}) %>" title="Editorial">Company Tariff</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li class="MainNavTab" id="databaseTab"><a href="<%= Url.Action("Index", "Data", new {id = 0}) %>" title="Database"><span>Database</span></a>
                    <ul>
                       <li class="MainNavGroup"><a href="#" title="Properties">Customers</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%= Url.Action("Index", "Data", new {id = 0}) %>" title="Editorial">Customers</a></li>
                            </ul>
                        </li>
                        <li class="MainNavGroup"><a href="#" title="Properties">Partners</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%= Url.Action("Carrier", "Data", new {id = 0}) %>" title="Shippers">Carriers</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%= Url.Action("Agent", "Data", new {id = 0}) %>" title="Shippers">Agent</a></li>
                            </ul>
                        </li>
                        <%if(User1.IsAdmin()){ %>
                        <li class="MainNavGroup"><a href="#" title="Properties">Categories</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("Country", "Data", new {id = 0}) %>" title="Air Line">Countries</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("Area", "Data", new {id = 0}) %>" title="Air Line">Area</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("Unit", "Data", new {id = 0}) %>" title="Air Line">Unit</a></li>
                            </ul>
                        </li>
                           <% }%>
                        <li class="MainNavGroup"><a href="#" title="Product">Trading</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("Index", "Supplier", new{id =(long)0}) %>" title="Air Line">Supplier</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("Index", "Warehouse", new{id =(long)0}) %>" title="Air Line">WareHoure</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("Index", "Product", new{id =(long)0}) %>" title="Air Line">Products</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <li class="MainNavTab" id="SalesTab"><a href="<%= Url.Action("Plan4SalesRedirect", "Users", new { id = 0 }) %>" title="Administrator"><span>Sales</span></a>
                    <ul>
                        <li class="MainNavGroup"><a href="#" title="Properties">Actions</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-other-importuser.png);" href="<%= Url.Action("Plan4SalesRedirect", "Users", new { id = 0 }) %>" title="Editorial">Plan for Sales/Dept/Office</a></li>

                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-other-importuser.png);" href="<%= Url.Action("SalePerformance", "Report", new {id = 0, ComId = 0, DeptId = 0}) %>" title="Form">Period Performance</a></li>

                            </ul>
                        </li>
                    </ul>
                </li>
                <%if (User1.IsAdmin())
                  {%>
                <li class="MainNavTab" id="SettingTab"><a href="<%= Url.Action("DisplaySetting", "Users", new {id=0}) %>" title="Administrator"><span>Setting</span></a>
                    <ul>
                        <li class="MainNavGroup"><a href="#" title="Properties">Update</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-item.png);" href="<%= Url.Action("DisplaySetting", "Users", new {id=0}) %>" title="Editorial">Sale Setting</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-item.png);" href="<%= Url.Action("Index", "Services", new {id=0}) %>" title="Editorial">Services</a></li>
                            </ul>
                        </li>
                        
                    </ul>
                </li>
                <%} %>
                <%if (User1.IsAdminAndAcct())
                  {%>
                <li class="MainNavTab" id="AccountingTab"><a href="<%= Url.Action("UnIssuedInvoice", "Shipment", new { id = 0 }) %>" title="Administrator"><span>Accounting</span></a>
                    <ul>
                        <li class="MainNavGroup"><a href="#" title="Properties">Properties</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-modify-forms.png);" href="<%= Url.Action("ViewSOA", "Shipment", new { id = 0 }) %>" title="Form">SOA</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-stat.png);" href="<%= Url.Action("FindInvoice", "Shipment", new { id = 0 }) %>" title="Form">Find Invoice</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
                <%} %>
                <li class="MainNavTab" id="ReportTab"><a href="<%= Url.Action("PersonalReport", "Report", new { id = 0 }) %>" title="Administrator"><span>Report</span></a>
                    <ul>
                        <li class="MainNavGroup"><a href="#" title="Properties">Report</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-stat.png);" href="<%= Url.Action("PersonalReport", "Report", new { id = 0 }) %>" title="Report1">Personal Report</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-stat.png);" href="<%= Url.Action("DepartmentReport", "Report", new { id = 0 }) %>" title="Report2">Department Report</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-stat.png);" href="<%= Url.Action("OfficeReport", "Report", new { id = 0 }) %>" title="Report3">Offices Report</a></li>
                            </ul>
                        </li>
                    </ul>
                </li>
               
              
                <%if(User1.IsTrading()==true)
                    {%>
                <li class="MainNavTab" id="TradingTab">
                    <a href="#" title="Trading"><span>Trading</span></a>
                    <ul>
                       <%-- <li class="MainNavGroup">
                            <a href="#" title="Properties">System</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%= Url.Action("Index", "StockReceiving" , new{id=(long)-1}) %>" title="Shippers">Config</a></li>
                            </ul>
                        </li>--%>
                        <li class="MainNavGroup">
                            <a href="#" title="Properties">Data Entry</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/order.png);" href="<%= Url.Action("Index", "StockReceiving" , new {id = 0} ) %>" title="Stock enter saving">Purchase Order<br/>(NK)</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/sales.png);" href="<%= Url.Action("Index", "Sales", new {id = 0}  ) %>" title="Stock issue ">Sales Order<br />(XK)</a></li>
                                
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/money.png);" href="<%= Url.Action("CalculateCost", "IssueVoucher" , new {id = 0} ) %>" title="Air Line">Calculate for cost of sales</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/moverStock.png);" href="<%= Url.Action("MoveStockToNextYear", "IssueVoucher" , new {id = 0} ) %>" title="Air Line">Move Stock To Next Year</a></li>
                            </ul>
                        </li>
                        <li class="MainNavGroup">
                            <a href="#" title="Product">Report</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/stock.png);" href="<%= Url.Action("Index", "IssueVoucher", new {id = 0}  ) %>" title="Stock saving report">Current Stock </a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/report.png);" href="<%= Url.Action("ReportStockInOut", "IssueVoucher" , new {id = 0} ) %>" title="Receipt summary report">Inventory<br />(History)</a></li>
                                
                         <% if (User1.IsAdminAndAcct())
                            {%>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-doc-edit.png);" href="<%= Url.Action("MonthYearReport", "IssueVoucher", new {id = 0}  ) %>" title="Air Line">Stock Summary</a></li>
                           <% }%>
                                       
                            </ul>
                        </li>
                    </ul>
                </li>

                <%  } %>
                 <li class="MainNavTab" id="AbountUsTab">
                    <a href="<%=Url.Action("Index", "OutNew", new {id = 0}) %>" title="Abount us"><span>Abount Us</span></a>
                    <ul>
                         <li class="MainNavGroup">
                             <a href="#" title="Properties">Abount Managerment</a>
                        <ul>
                            <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%=Url.Action("Index", "OutNew", new {id = 0}) %>" title="Shippers">Information</a></li>
                            <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%=Url.Action("Index", "Infomation", new {id =0}) %>" title="Shippers">Company Regulation</a></li>
                        </ul>
                    </li>
                    </ul>
                </li> 
                 
                  <%if(User1.IsAdmin()){%>
                  <li class="MainNavTab" id="StaffManagementTab">
                    <a href="<%= Url.Action("Index", "Users", new {id = 0}) %>" title="Administrator"><span>Staff Setting</span></a>
                    <ul>
                        <li class="MainNavGroup"><a href="#" title="Staff Management">Staff Management</a>
                            <ul>
                                <li class="MainNavLink" title="Create Users"><a style="background-image: url(../../Images/Menu/bg-users-creation-createuser.png);" href="<%= Url.Action("Create", "Users", new {id = 0}) %>">Create Staff</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifygroup.png);" href="<%= Url.Action("Index", "Users", new {id = 0}) %>">List Staff</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/bg-users-modification-modifyuser.png);" href="<%= Url.Action("Detail", "Users", new {id = User1.Id}) %>">My Pofile</a></li>
                            </ul>
                        </li>
                         <li class="MainNavGroup"><a href="#" title="Staff Management">Offices</a>
                            <ul>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-edit.png);" href="<%= Url.Action("Department", "Users", new{id =(long)-1}) %>">Departments</a></li>
                                <li class="MainNavLink"><a style="background-image: url(../../Images/Menu/nav-sys-edit.png);" href="<%= Url.Action("Company", "Users",new{id =(long)-1}) %>">Offices</a></li>
                            </ul>
                        </li>
                    </ul>
                </li> 
                       <%} %>
                 <li class="MainNavTab" id="Li1"><a href="#" title="Administrator"></a></li> 
            </ul>
            <a id="navMode" href="#" title="Mode">Mode</a>
            <div id="subNav"></div>
        </div>
    </div>
</div>
<div style="border-top: 1px solid #BFBFBF; border-bottom: 3px solid #F6F6F3"></div>
<%
    var ct = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
    var action = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
    var url = string.Format("/{0}/{1}", ct, action);
%>
<input type="hidden" id="urlAction" value="<%=url %>"/>
 