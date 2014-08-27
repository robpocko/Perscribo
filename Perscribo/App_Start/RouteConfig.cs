using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Perscribo
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "ApplicationDetails",
                url: "Applications/{id}",
                defaults: new { controller = "Applications", action = "Details" });

            routes.MapRoute(
                name: "Consultants",
                url: "PartialViews/Consultants/{id}",
                namespaces: new string[] { "Perscribo.Areas.Applications.Controllers" },
                defaults: new { controller = "PartialViews", action = "Consultants", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "AddressEdit",
                url: "PartialViews/Address/{id}",
                namespaces: new string[] { "Perscribo.Areas.Applications.Controllers" },
                defaults: new
                {
                    controller = "PartialViews",
                    action = "AddressEdit",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                namespaces: new string[] { "Perscribo.Areas.Home.Controllers" },
                defaults: new { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional
                }
            );
        }
    }
}