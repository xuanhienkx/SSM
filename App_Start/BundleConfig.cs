using System.Web;
using System.Web.Optimization;

namespace SSM
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-confirm.js",
                        "~/Scripts/jquery-cookie.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/bootstrap.js"

                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js",
                          "~/Scripts/jquery.resizableColumns.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(

                        "~/Scripts/validation.js",
                        "~/Scripts/jquery.form.js",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/SCFListPagingJs").Include( 
                "~/Scripts/listScript.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/CRMJS").Include(
                           "~/Scripts/CRMScript.js"
                           ));


            bundles.Add(new ScriptBundle("~/bundles/SCFJs").Include(
                // "~/Scripts/jquery.unobtrusive-ajax.min.js",
                    "~/Content/ckeditor/ckeditor.js",
                    "~/Scripts/jquery-mousewheel.js",
                    "~/Scripts/AjaxGlobalHandler.js",
                    "~/Scripts/jquery.number.js",
                    "~/Scripts/autoNumeric.js",
                    "~/Scripts/mbqScript.js",
                    "~/Scripts/j-select.js",
                    "~/Scripts/-select.external.js",
                    "~/Scripts/main.js",
                    "~/Scripts/utils.js",
                    "~/Scripts/global.js",
                    "~/Scripts/top-nav-bar.js",
                    "~/Scripts/main-nav-bar.js",
                    "~/Scripts/homepage.js",
                    "~/Scripts/prototype.js",
                    "~/Scripts/ui.datetimepicker.js",
                    "~/Scripts/jquery.timepicker.js", 
                    "~/Scripts/calendar-time-custom.js",
                    "~/Scripts/date-validator.js"));

            bundles.Add(new ScriptBundle("~/bundles/SCFJsInfo").Include(
                "~/Scripts/uploadeRendFile.js"
                ));
            bundles.Add(new StyleBundle("~/Content/info").Include(
                      "~/Content/style.css",
                      "~/Content/style_info.css"
                ));
            bundles.Add(new StyleBundle("~/Content/CRM").Include(
                    "~/Content/CrmStyleSheet.css" 
              ));
            bundles.Add(new StyleBundle("~/Content/SCFCss").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-theme.cs",
                      "~/Content/font-awesome.css",
                      "~/Content/themes/base/all.css",
                      "~/Content/global.css",
                      "~/Content/jquery-confirm.css",
                      "~/Content/top-nav-bar.css",
                      "~/Content/main-nav-bar.css",
                      "~/Content/homepage.css",
                      "~/Content/section-block.css",
                      "~/Content/footer-bar.css",
                      "~/Content/Shipment.css",
                      "~/Content/Freights.css",
                      "~/Content/jquery.timepicker.css",
                      "~/Content/BookingConfirm.css",
                      "~/Content/CustomStyle.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/SCFCPrintss").Include(
                     "~/Content/global.css",
                     "~/Content/top-nav-bar.css",
                     "~/Content/main-nav-bar.css",
                     "~/Content/homepage.css",
                     "~/Content/section-block.css",
                     "~/Content/footer-bar.css",
                     "~/Content/themes/base/jquery-ui.css",
                     "~/Content/Shipment.css", 
                     "~/Content/site.css"));

        } 

    }
}