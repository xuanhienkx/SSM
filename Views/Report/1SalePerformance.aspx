<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.SalePerformamceModel>" %>
<%@ Import Namespace="SSM.Models" %>
<%@ Import Namespace="SSM.Controllers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	SCF HCM SSM System - Sale Performance
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%
    User User1 = (User)Session[AccountController.USER_SESSION_ID];
    List<Int32> MonthList = new List<Int32>(12); 
    for(int i=1; i<=12; i++){
        MonthList.Add(i);
    }
     String Action = (String) ViewData["Action"];
    %>

</asp:Content>
