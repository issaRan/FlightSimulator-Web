using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ex3
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("display", "display/{ip}/{port}",
                defaults: new { controller = "First", action = "display" });
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FirstController" + "", action = "Index", id = UrlParameter.Optional }
             );
            /*
            routes.MapRoute("displayPath", "display/{ip}/{port}/{rate}",
                new {controller = "First", action = "display"});
            routes.MapRoute("displayPath", "/save/{ip}/{port}/4/10/flight1",
                new { controller = "First", action = "display" });
            routes.MapRoute("displayPath", "/display/flight1/4",
                new { controller = "First", action = "display" });
            */
        }
    }
}
