using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace test
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{countryCode}",
                defaults: new { controller = "Home", action = "Index", countryCode = UrlParameter.Optional }
            );
            //routes.MapRoute(
            //    name: "countriesPagination", 
            //    url: )
        }
    }
}
