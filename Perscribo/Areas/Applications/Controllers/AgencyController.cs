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
                LoadSelectLists(newAgency.Address);

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
        public virtual ActionResult Create([Bind(Include = "Name,PhoneNumber,AddressID, Address")] Agency newAgency)
        {
            db.Agencies.Add(newAgency);
            try
            {
                //  must check if address details were actually provided
                ClearValidationErrorsForNonExistentAddress(newAgency);
                //if (newAgency.Address != null && newAgency.Address.Suburb == null)
                //{
                //    db.Entry(newAgency.Address).State = EntityState.Unchanged;
                //    newAgency.Address = null;
                //    ModelState.Remove("Address.ID");
                //    ModelState.Remove("Address.Street1");
                //    ModelState.Remove("Address.Suburb");
                //    ModelState.Remove("Address.StateID");
                //    ModelState.Remove("Address.Postcode");
                //}
                
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit(string id)
        {
            if (id != null)
            {
                int agencyId = int.Parse(id);
                var agency = db.Agencies.Where(a => a.ID == agencyId).FirstOrDefault();

                LoadSelectLists(agency.Address);

                return View(agency);
            }
            else
            {
                return null;
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit([Bind(Include = "ID,Name,PhoneNumber,AddressID,Address")] Agency agency)
        {
            ClearValidationErrorsForNonExistentAddress(agency);

            if (agency.Address != null)
            {
                if (agency.AddressID == null)
                {
                    db.Entry(agency.Address).State = EntityState.Added;
                }
                else
                {
                    db.Entry(agency.Address).State = EntityState.Modified;
                }
            }

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

            LoadSelectLists(agency.Address);
            return View(agency);
        }

        private void LoadSelectLists(Address address = null)
        {
            var states = from StateName s in Enum.GetValues(typeof(StateName))
                         select new { ID = s, Name = s.ToString().Replace("_", " ") };
            if (address != null)
            {
                //  NOTE: The key (for ViewData) must match the Property name
                ViewData["StateID"] = new SelectList(states, "ID", "Name", address.StateID);
            }
            else
            {
                ViewData["StateID"] = new SelectList(states, "ID", "Name");
            }
        }

        private void ClearValidationErrorsForNonExistentAddress(Agency agency)
        {
            if (agency.Address != null && agency.Address.Suburb == null)
            {
                db.Entry(agency.Address).State = EntityState.Unchanged;
                agency.Address = null;
                ModelState.Remove("Address.ID"); 
                ModelState.Remove("Address.Street1");
                ModelState.Remove("Address.Suburb");
                ModelState.Remove("Address.StateID");
                ModelState.Remove("Address.Postcode");
            }
            else if (agency.AddressID == null)
            {
                ModelState.Remove("Address.ID");
            }
        }
    }
}
