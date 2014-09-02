using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Controllers
{
    public partial class ApplicationController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index()
        {
            var roles = db.Roles
                            .Include("Agency")
                            .Where(r => r.Status != RoleStatus.Closed)
                            .OrderByDescending(r => r.Status)
                            .ThenByDescending(r => r.AppliedForOn);

            return View(roles.ToList());
        }

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit(string id)
        {
            if (id == null)
            {
                id = "0";
            }

            return View();
        }

    }
}
