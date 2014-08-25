using System.Web.Mvc;

namespace Perscribo.Areas.Engagements
{
    public class EngagementsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Engagements";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Engagements_default",
                "Engagements/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
