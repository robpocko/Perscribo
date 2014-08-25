using System.Web.Mvc;

namespace PerscriboMVC.Areas.Applications
{
    public class ApplicationsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Applications";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Applications_default",
                "Applications/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
