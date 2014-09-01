using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Controllers
{
    public partial class AgencyController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index()
        {
            var agencies = db.Agencies
                                .OrderBy(a => a.Name).ToList();

            return View(agencies);
        }

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit(string agencyId)
        {
            if (agencyId == null)
            {
                var newAgency = new Agency();

                return View(newAgency);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        public ActionResult Address()
        {
            Agency agency = null;

            return PartialView("EditorTemplates/Address", agency);
        }
    }
}
