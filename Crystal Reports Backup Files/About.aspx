<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SSM.Models.CompanyInfo>" %>

<asp:Content ID="aboutTitle" ContentPlaceHolderID="TitleContent" runat="server">
    About Us
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="CompanyInfor">
<% using (Html.BeginForm("About", "Home", FormMethod.Post, new { enctype = "multipart/form-data" }))
   {%>
    <table>
        <tr>
            <td style="width: 200PX">
                Company Logo
            </td>
            <td>
                <div style="height: auto; overflow: hidden">
                    <img src="<%= Page.ResolveClientUrl("~/" + Model.CompanyLogo) %>"/>
                </div>
                <div style="height: auto; overflow: hidden">
                    <div class="fakeupload">
		                    <input type="text" name="fakeupload" /> <!-- browse button is here as background -->
                        </div>
                        <div class="DivRealUpload">
		                    <input type="file" name="upload" id="File2" class="realupload" onchange="this.form.fakeupload.value = this.value;" />
	                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                Company Information Header
            </td>
            <td>
            <%: Html.TextAreaFor(m => m.CompanyHeader)%>
            </td>
        </tr>
        <tr>
            <td>
                Company Information Footer
            </td>
            <td>
            <%: Html.TextAreaFor(m => m.CompanyFooter)%>
            </td>
        </tr>
        <tr>
            <td>
                Account Information
            </td>
            <td>
            <%: Html.TextAreaFor(m => m.AccountInfor)%>
            </td>
        </tr>
    </table>
    <div class="ButtonZone">
       <input id="submitButton" type="submit" value="Update" title="Update Info"/>
  </div>
    <%} %>
    </div>
    <script language="javascript" type="text/javascript">
        jQuery(document).ready(function () {
                    CKEDITOR.replace('AccountInfor',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
					CKEDITOR.replace('CompanyFooter',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
					CKEDITOR.replace('CompanyHeader',
					{
					    fullPage: true,
					    extraPlugins: 'autogrow',
					    autoGrow_maxHeight: 800

					});
            jQuery("#StaffManagementTab").addClass("Active");
            jQuery('#StaffManagementTab').activeThisNav();
        });
</script>
</asp:Content>
