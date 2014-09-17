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
                name: "QuickAgency",
                url: "Application/QuickAgencySave/{newAgencyName}",
                defaults: new { controller = "Application", action = "QuickAgencySave", newAgencyName = UrlParameter.Optional });

            routes.MapRoute(
                name: "QuickCompany",
                url: "Application/QuickCompanySave/{newCompanyName}",
                defaults: new { controller = "Application", action = "QuickCompanySave", newCompanyName = UrlParameter.Optional });

            //routes.MapRoute(
            //    name: "Consultants",
            //    url: "PartialViews/Consultants/{id}",
            //    namespaces: new string[] { "Perscribo.Areas.Applications.Controllers" },
            //    defaults: new { controller = "PartialViews", action = "Consultants", id = UrlParameter.Optional }
            //);

            //routes.MapRoute(
            //    name: "AddressEdit",
            //    url: "PartialViews/Address/{id}",
            //    namespaces: new string[] { "Perscribo.Areas.Applications.Controllers" },
            //    defaults: new
            //    {
            //        controller = "PartialViews",
            //        action = "AddressEdit",
            //        id = UrlParameter.Optional
            //    }
            //);

            routes.MapRoute(
                name: "ConsultantsEdit",
                url: "Agency/ConsultantEdit/{agencyId}/{consultantId}",
                namespaces: new string[] {"Perscribo.Controllers"},
                defaults: new 
                {
                    controller = "Agency",
                    action = "ConsultantEdit",
                    agencyId = UrlParameter.Optional,
                    consultantId = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                namespaces: new string[] { "Perscribo.Controllers" },
                defaults: new { 
                    controller = "Home", 
                    action = "Index", 
                    id = UrlParameter.Optional
                }
            );
        }
    }
}