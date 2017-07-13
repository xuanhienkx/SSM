using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using SSM.Common;

using SSM.DomainProfiles;

namespace SSM
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);  
            AutoMapperDomainConfiguration.Config();                                               
            JobScheduler.Start();
             
        }
        /* protected void Application_End()
         {

         }*/
        protected void Application_Error(object sender, EventArgs e)
        {                                                                                        
            Exception ex = Server.GetLastError();
            Logger.LogError(ex);

            if (ex is HttpException)
            {
                HttpException httpEx = ex as HttpException;
                if (httpEx.Message == "You are not authorized to access this page")
                {
                    Logger.Log(httpEx.Message);
                    Response.Redirect(@"~/home/NoPermission");
                    Server.ClearError();
                }
            }
        }
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }
        protected void Application_EndRequest()
        {
            // Any AJAX request that ends in a redirect should get mapped to an unauthorized request
            // since it should only happen when the request is not authorized and gets automatically
            // redirected to the login page.
            var context = new HttpContextWrapper(Context);
            // If we're an ajax request, and doing a 302, then we actually need to do a 401
            if (Context.Response.StatusCode == 302 && context.Request.IsAjaxRequest())
            {
                Context.Response.Clear();
                Context.Response.StatusCode = 401;
            }
        }
        protected void Session_Start()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Shipment/Index/0");
        }
        protected void Session_End()
        {
            FormsAuthentication.SignOut();
            Response.Redirect("~/Shipment/Index/0");
        }

    }
}