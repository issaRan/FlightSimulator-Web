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
            // Display the route
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("pathOnMap", "display/{ip}/{port}/{rate}",
               defaults: new { controller = "First", action = "pathOnMap" });
                
            // Display a point
            routes.MapRoute("display", "display/{ip}/{port}",
                defaults: new { controller = "First", action = "display" });

            // Saving
            routes.MapRoute(
                name: "save",
                url: "save/{ip}/{port}/{rate}/{duration}/{name}",
                defaults: new { controller = "First", action = "save" });
            
            // Loading
            routes.MapRoute(
                name: "load",
                url: "display/{fileName}/{rate}",
                defaults: new { controller = "First", action = "display" });

            // Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "FirstController" + "", action = "Index", id = UrlParameter.Optional }
             );
              
            /*
            routes.MapRoute("displayPath", "/display/flight1/4",
                new { controller = "First", action = "display" });
            */
        }
    }
}
