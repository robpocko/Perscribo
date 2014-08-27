using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Areas.Engagements.Controllers
{
    public partial class EngagementController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index()
        {
            var currentEngagement = db.Engagements
                .Where(e => e.Commencement <= DateTime.Now && (e.Completion == null || e.Completion > DateTime.Now))
                .First();

            return View(currentEngagement);
        }

    }
}
