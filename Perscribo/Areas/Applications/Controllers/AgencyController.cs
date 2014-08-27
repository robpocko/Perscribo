using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Areas.Applications.Controllers
{
    public partial class AgencyController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index(string verb)
        {
            if ((verb ?? "").ToLower() == "new")
            {
                Agency newAgency = new Agency();

                return View("Edit", newAgency);
            }
            else
            {
                var agencies = db.Agencies
                                .OrderBy(a => a.Name);

                return View(agencies.ToList());
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ActionName("Index")]
        public virtual ActionResult Create([Bind(Include = "Name,PhoneNumber")] Agency newAgency)
        {
            db.Agencies.Add(newAgency);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit([Bind(Include = "ID,Name")] Agency agency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(agency).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your System Administrator.");
            }

            return View(agency);
        }
    }
}
