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
    public partial class ApplicationController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index(string verb)
        {
            if ((verb ?? "").ToLower() == "new")
            {
                LoadSelectLists();

                Role newRole = new Role();
                newRole.AppliedForOn = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    DateTime.Now.Day,
                    DateTime.Now.Hour,
                    30 * (DateTime.Now.Minute / 30),
                    0);

                return View("Create", newRole);
            }
            else
            {
                var roles = db.Roles
                            .Include("Agency")
                            .Where(r => r.Status != RoleStatus.Closed)
                            .OrderByDescending(r => r.Status)
                            .ThenByDescending(r => r.AppliedForOn);

                return View(roles.ToList());
            }

        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ActionName("Index")]
        public virtual ActionResult Create([Bind(Include = "PositionTitle,AppliedForOn,ReferenceNumber,PositionType,LowSalaryRange,HighSalaryRange,SalaryType,Status,AgentInterview,AgencyID,ConsultantID,CompanyID")] Role newRole)
        {
            db.Roles.Add(newRole);
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

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit(string id)
        {
            if (id == null)
            {
                id = "0";
            }

            int applicationId = int.Parse(id);
            var application = db.Roles.Where(r => r.ID == applicationId).FirstOrDefault();

            LoadSelectLists(application);
            LoadAgencyConsultants(application);

            return View(application);
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit([Bind(Include = "ID,PositionTitle,AppliedForOn,ReferenceNumber,PositionType,LowSalaryRange,HighSalaryRange,SalaryType,Status,AgentInterview,AgencyID,ConsultantID,CompanyID")] Role application)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your System Administrator.");
            }

            return View(application);
        }

        public virtual ActionResult QuickCompanySave(string newCompanyName)
        {
            if (newCompanyName != null && newCompanyName.Trim().Length > 0)
            {
                db.Companies.Add(new Company { Name = newCompanyName.Trim() });
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                }
            }

            return RedirectToAction(MVC.PartialViews.ActionNames.Companies, MVC.PartialViews.Name);
        }

        public virtual ActionResult QuickAgencySave(string newAgencyName)
        {
            if (newAgencyName != null && newAgencyName.Trim().Length > 0)
            {
                db.Agencies.Add(new Agency { Name = newAgencyName.Trim() });
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                }
            }
            return RedirectToAction(MVC.PartialViews.ActionNames.Agencies, MVC.PartialViews.Name);
        }

        public virtual ActionResult QuickConsultantSave(string agencyId, string newConsultantFirstName)
        {
            if (agencyId != null && agencyId.Trim().Length > 0 && 
                newConsultantFirstName != null && newConsultantFirstName.Trim().Length > 0)
            {
                int agencyID = int.Parse(agencyId);

                db.Consultants.Add(new Consultant { FirstName = newConsultantFirstName.Trim(), AgencyID = agencyID });

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    string temp = ex.Message;
                }
            }

            return RedirectToAction(MVC.PartialViews.ActionNames.Consultants, MVC.PartialViews.Name, new { id = agencyId });
        }

        private void LoadSelectLists(Role role = null)
        {
            var positionTypes = from PositionType p in Enum.GetValues(typeof(PositionType))
                                select new { ID = p, Name = p.ToString().Replace("_", " ") };
            var salaryTypes = from SalaryType s in Enum.GetValues(typeof(SalaryType))
                              select new { ID = s, Name = s.ToString().Replace("_", " ") };
            var statusTypes = from RoleStatus r in Enum.GetValues(typeof(RoleStatus))
                              select new { ID = r, Name = r.ToString().Replace("_", " ") };
            var agencies = from a in db.Agencies
                           orderby a.Name
                           select new { AgencyID = a.ID, Name = a.Name };
            var companies = from c in db.Companies
                            orderby c.Name
                            select new { CompanyID = c.ID, Name = c.Name };

            if (role != null)
            {
                //  NOTE: The key (for ViewData) must match the Property name
                ViewData["PositionType"] = new SelectList(positionTypes, "ID", "Name", role.PositionType);
                ViewData["SalaryType"] = new SelectList(salaryTypes, "ID", "Name", role.SalaryType);
                ViewData["Status"] = new SelectList(statusTypes, "ID", "Name", role.Status);
                ViewData["AgencyID"] = new SelectList(agencies, "AgencyID", "Name", role.AgencyID);
                ViewData["CompanyID"] = new SelectList(companies, "CompanyID", "Name", role.CompanyID);
            }
            else
            {
                ViewData["PositionType"] = new SelectList(positionTypes, "ID", "Name");
                ViewData["SalaryType"] = new SelectList(salaryTypes, "ID", "Name");
                ViewData["Status"] = new SelectList(statusTypes, "ID", "Name");
                ViewData["AgencyID"] = new SelectList(agencies, "AgencyID", "Name");
                ViewData["CompanyID"] = new SelectList(companies, "CompanyID", "Name");
            }
        }

        private void LoadAgencyConsultants(Role application)
        {
            var consultants = from c in db.Consultants
                              where c.Agency.ID == application.AgencyID
                              orderby c.LastName, c.FirstName
                              select new { ConsultantID = c.ID, Name = c.FirstName + " " + c.LastName };
            if (application.Consultant != null)
            {
                ViewData["ConsultantID"] = new SelectList(consultants, "ConsultantID", "Name", application.ConsultantID);
            }
            else
            {
                ViewData["ConsultantID"] = new SelectList(consultants, "ConsultantID", "Name");
            }
        }
    }
}
