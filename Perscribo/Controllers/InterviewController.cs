using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.UI;
using System.Web.Mvc;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Controllers
{
    public partial class InterviewController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index()
        {
            var roles = db.Roles
                        .Include("Company").Include("CompanyInterviews")
                        .Where(r => r.Status != RoleStatus.Closed && r.Status != RoleStatus.Offer_Received)
                        .OrderByDescending(r => r.AppliedForOn);

            return View(roles.ToList());
        }
    }
}
