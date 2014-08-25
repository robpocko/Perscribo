using System.Web.Mvc;

namespace PerscriboMVC.Areas.PartialViews
{
    public class PartialViewsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "PartialViews";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "PartialViews_default",
                "PartialViews/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
