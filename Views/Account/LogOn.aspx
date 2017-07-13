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

        .H2 {
            font-size: 1.5em;
            font-weight: bold;
            margin-left: 20px;
        }
    </style>
</head>
<body>
    <div class="container" style="height: 60%; margin-top: 200px; border: 1px solid black; padding: 10px 10px 20px 10px; max-width: 760px; border-radius: 3px">
        <div class="SectionBlock  BoxL1" style="width: auto; border-radius: 10px; width: 700px; margin-bottom: 7px">
            <div class="BoxL2">
                <div class="BoxL3">
                    <div class="BoxL4 form-group-sm  form-horizontal">
                        <% using (Html.BeginForm())
                            { %>
                        <div style="margin: 5px auto">
                            
                            <div class="col-md-1"></div>
                            <div class="col-md-10 ">
                                 <div class="" style="margin-bottom: 30px">
                                    <div class="H2">LOG IN <b class="companyname"><%= Helpers.SiteName %></b> System</div>
                                </div>
                                <div class="">
                                    <%: Html.ValidationSummary(true, "Login was unsuccessful.") %>
                                </div> 
                                <div class="row">
                                    <div class="form-group  row">
                                        <%: Html.LabelFor(m => m.UserName, new {@class=" control-label col-md-3"}) %>
                                       <div class="col-md-8">
                                            <%: Html.TextBoxFor(m => m.UserName, new{@class="form-control  input-sm", style="width:250px"}) %>
                                        <%: Html.ValidationMessageFor(m => m.UserName) %>
                                       </div>
                                    </div>
                                    <div class="form-group row">
                                        <%: Html.LabelFor(m => m.Password,new {@class=" control-label col-md-3"}) %>
                                       <div class="col-md-8">
                                            <%: Html.PasswordFor(m => m.Password, new{@class="form-control input-sm", style="width:250px"}) %>
                                        <%: Html.ValidationMessageFor(m => m.Password) %>
                                       </div>
                                    </div>
                                    <div class="row">
                                       <div class="col-md-3"></div>
                                       <div class="col-md-9">
                                            <p>
                                            <input type="submit" class="btn btn-primary" value="Log On" />
                                            <span class="show-version">VERSION 5</span>
                                        </p>
                                       </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1"></div>
                            <div class="clearfix"></div>
                        </div>
                        <% } %>
                    </div>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
