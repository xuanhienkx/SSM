using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace SSM
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
           
            //routes.MapRoute(
            //    "Default", // Route name
            //    "{controller}/{action}/{id}", // URL with parameters
            //    new { controller = "Shipment", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            //);
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Shipment", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}