<%@ Control Language="C#" Inherits="SSM.Views.Shared.TopNarBarUC" CodeBehind="TopNarBarUC.ascx.cs" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM.Controllers" %>
<% User User1 = (User)Session[SSM.Controllers.AccountController.USER_SESSION_ID];%>
<div id="topNav">
    <ul id="topNavBar">
        <li class="logotop">
            <img src="<%= Url.Content("/Images/ssmlogo.png") %>" alt="logo"  />
        </li>
        <li class="SiteList">
            <label class="Label">SCF HCM SSM System -  <%if (User1 != null)
                                                         {%><%=User1.FullName %><%} %></label>
        </li>
        <%if (UsersModel.isAdminOrDirctor(User1))
          { %>
        <li><a style="background-image: url(../../Images/Menu/bg-home.gif);" href="<%= Url.Action("About", "Home", new {id = 0}) %>"
            title="Home">Home</a></li>
        <%} %>
        <li><a style="background-image: url(../../Images/Menu/bg-logout.gif);" href="<%= Url.Action("LogOff", "Account") %>" title="Disconnect">Disconnect</a></li>
        <li><a href="http://sanco.smartlog.vn/" title="Smartlog" target="_blank">Quan ly xe</a></li>
    </ul>
    <asp:Table ID="Table1" runat="server">
        <asp:TableRow ID="TableRow1" runat="server">
            <asp:TableCell ID="TableCell1" runat="server">
            This is Cell 1
            </asp:TableCell>
            <asp:TableCell ID="TableCell2" runat="server">
            This is Cell 2
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>



