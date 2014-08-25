using System.Web.Mvc;

namespace Perscribo.Areas.Interviews
{
    public class InterviewsAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Interviews";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Interviews_default",
                "Interviews/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
