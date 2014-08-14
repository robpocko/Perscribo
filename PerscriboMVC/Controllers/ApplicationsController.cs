using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace PerscriboMVC.Controllers
{
    public class ApplicationsController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Index(string verb)
        {
            if ((verb ?? "").ToLower() == "new")
            {
                return View("Create", new Role());
            }
            else
            {
                return View(db.Roles.Include("Agency").ToList());
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ActionName("Index")]
        public ActionResult Create([Bind(Include="PositionTitle,AppliedForOn,ReferenceNumber,PositionType,LowSalaryRange,HighSalaryRange,SalaryType,Status,AgentInterview,Agency,Consultant,Company")] Role newRole)
        {
            return RedirectToAction("Index");
        }

    }
}
