<%@ Control Language="C#" Inherits="SSM.Views.Shared.WarningUser" CodeBehind="WarningUser.ascx.cs"%>
<script type="text/javascript" language="javascript">
         var deleteError = false;
</script>
<% if(ViewData["deleteError"] !=null) { %>
    <script type="text/javascript" language="javascript">
        deleteError = true;
    </script>
<%} %>
<div id="overlay" style="width: 1263px; height: 3113px; opacity: 0.8;display:none"></div>
    <div id="lightbox" style="top: 171px; left: 0px;display:none">
        <div id="lightboxContainer" style="width: 470px; height: 100px; font-size: 12px;">
            <div style="overflow:hidden;padding:7px 5px;vertical-align:top;text-align:right;font-weight:bold">
                <a href="#" title="Close" class="Close">X</a>
            </div>
            <div class="LightboxBlock">
                    <div class="DivHeader"><h2>Warning!!!!</h2></div>
                    <div>User has some relatived action, U can't delete it.</div>
             </div>
        </div>
        </div>
        <script type="text/javascript" language="javascript">
            jQuery(document).ready(function () {
                jQuery(".Close").click(function () {
                    jQuery('#overlay').hide();
                    jQuery('#lightbox').hide();
                });
                if (deleteError) {
                    jQuery('#overlay').show();
                    jQuery('#lightbox').show();
                }
            });
       </script>     
