﻿using System;
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
        public virtual ActionResult Edit(string id)
        {

            if (id == null)
            {
                var newAgency = new Agency();

                return View(newAgency);
            }
            else
            {
                if (id == "Address")
                {
                    return RedirectToAction(MVC.Agency.ActionNames.Address);
                }
                else
                {
                    int agencyId = int.Parse(id);
                    var agency = db.Agencies.Where(a => a.ID == agencyId).FirstOrDefault();
                    LoadSelectLists(agency.Address);

                    return View(agency);
                }
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit([Bind(Include = "ID,Name,PhoneNumber,AddressID, Address")] Agency agency)
        {
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
            db.Agencies.Add(agency);
            try
            {
                //if (ModelState.IsValid)
                //{
                    if (agency.ID != 0)
                    {
                        db.Entry(agency).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                //}
            }
            catch (Exception ex)
            {
                string temp = ex.Message;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Address()
        {
            Agency agency = null;
            LoadSelectLists();

            return PartialView("EditorTemplates/Address", agency);
        }

        [HttpGet]
        public virtual ActionResult Consultants(string id)
        {
            int agencyId = int.Parse(id);
            var agency = db.Agencies.Where(a => a.ID == agencyId).FirstOrDefault();

            return View(agency.Consultants);
        }

        [HttpGet]
        public virtual ActionResult ConsultantEdit(string id)
        {
            if (id == null)
            {
                var newConsultant = new Consultant();
                return View(newConsultant);
            }
            else
            {
                int consultantID = int.Parse(id);
                var consultant = db.Consultants.Where(c => c.ID == consultantID).FirstOrDefault();
                return View(consultant);
            }
        }

        [HttpPost]
        public virtual ActionResult ConsultantEdit([Bind(Include = "ID,FirstName,LastName,PhoneNumber,Email,AgencyID")] Consultant consultant)
        {
            if (consultant.ID != 0)
            {
                db.Entry(consultant).State = EntityState.Modified;
            }
            else
            {
                db.Entry(consultant).State = EntityState.Added;
            }
            return null;
        }

        private void LoadSelectLists(Address address = null)
        {
            var states = from StateName s in Enum.GetValues(typeof(StateName))
                         select new { ID = s, Name = s.ToString().Replace("_", " ") };
            if (address != null)
            {
                //  NOTE: The key (for ViewData) must match the Property name
                ViewData["Address.StateID"] = new SelectList(states, "ID", "Name", address.StateID);
            }
            else
            {
                ViewData["Address.StateID"] = new SelectList(states, "ID", "Name");
            }
        }
    }
}
