<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SSM.Models.LogOnModel>" %>
<%@ Import Namespace="SSM.Common" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Helpers.SiteTitle %> SSM System- Sign In</title>
    <link href="../../Content/ssmlogo.png" rel="shortcut icon" type="image/x-icon" />
    <link href="../../Content/bootstrap.css" rel="stylesheet" type="text/css" />
    <link href="../../Content/Site.css" rel="stylesheet" type="text/css" />
    <link type="text/css" rel="stylesheet" href="../../Content/section-block.css" media="all" />
    <style type="text/css">
        .BoxL1 {
            background-image: url(../../Content/box-1.png);
            background-repeat: no-repeat;
            background-position: left top;
        }

        .BoxL2 {
            background-image: url(../../Content/box-2.png);
            background-repeat: no-repeat;
            background-position: right top;
        }

        .BoxL3 {
            background-image: url(../../Content/box-3.png);
            background-repeat: no-repeat;
            background-position: right bottom;
        }

        .BoxL4 {
            background-image: url(../../Content/box-4.png);
            background-repeat: no-repeat;
            background-position: left bottom;
        }

        .RowForm label {
            float: left;
            font-size: 1.1em;
            font-weight: bolder;
            line-height: 1em;
            margin: 0;
            color: #6E6D65;
            padding-right: 30px;
            padding-top: 5px;
            text-align: right;
            width: 145px;
        }

        .RowForm input {
            border: 1px solid #BFBFBF;
            color: #6E6D65;
            float: left;
            font-family: Arial,Helvetica,sans-serif;
            font-size: 1.2em;
            padding: 2px 7px;
            width: 200px;
        }

        .H2 {
            font-size: 1.4em;
            font-weight: bolder;
            line-height: 1em;
            color: #6D6D65;
        }

        .editor-label {
            font-size: 1.1em;
            line-height: 1em;
            margin: 0;
            color: #6E6D65;
            padding-left: 172px;
            text-align: left;
        }

        .show-version {
            color: red;
            font-size: 1.5em;
            font-weight: bold; 
            margin-left: 40px;
            position: absolute;
        }
        b.companyname {
            color: orange;
        }
    </style>
</head>
<body>
    <div class="container" style="height: 60%; padding-left: 25%; padding-top: 10%">
        <div class="col-md-8">
            <div style="text-align: center; vertical-align: middle; border: 1px solid #BFBFBF; padding: 5px 25px 20px 25px;">
                <div class="SectionBlock Expanded BoxL1" style="width: auto">
                    <div class="BoxL2">
                        <div class="BoxL3">
                            <div class="BoxL4">
                                <% using (Html.BeginForm())
                                   { %>
                                <table>
                                    <tr>
                                        <td>
                                            <%: Html.ValidationSummary(true, "Login was unsuccessful.") %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h2 class="H2">LOG IN <b class="companyname"><%= Helpers.SiteName %></b> SSM System</h2>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="RowForm">
                                                <%: Html.LabelFor(m => m.UserName) %>
                                                <%: Html.TextBoxFor(m => m.UserName, new{@class="form-control  input-sm"}) %>
                                                <%: Html.ValidationMessageFor(m => m.UserName) %>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="RowForm">
                                                <%: Html.LabelFor(m => m.Password) %>
                                                <%: Html.PasswordFor(m => m.Password, new{@class="form-control input-sm"}) %>
                                                <%: Html.ValidationMessageFor(m => m.Password) %>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="editor-label">
                                                <%: Html.CheckBoxFor(m => m.RememberMe) %>
                                                <%: Html.LabelFor(m => m.RememberMe) %>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <p>
                                                <input type="submit" class="btn btn-primary" value="Log On" />
                                                <span class="show-version">VERSION 5</span>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                                <% } %>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
