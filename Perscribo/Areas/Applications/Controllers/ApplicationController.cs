using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Areas.Applications.Controllers
{
    public class ApplicationController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Index(string verb)
        {
            if ((verb ?? "").ToLower() == "new")
            {
                LoadSelectLists();

                Role newRole = new Role();

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
                //return View(db.Roles.Include("Agency").ToList());
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        [ActionName("Index")]
        public ActionResult Create([Bind(Include = "PositionTitle,AppliedForOn,ReferenceNumber,PositionType,LowSalaryRange,HighSalaryRange,SalaryType,Status,AgentInterview,AgencyID,ConsultantID,CompanyID")] Role newRole)
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
        public ActionResult Edit(string id)
        {
            if (id != null)
            {
                int applicationId = int.Parse(id);
                var application = db.Roles.Where(r => r.ID == applicationId).FirstOrDefault();
                
                LoadSelectLists(application);
                
                return View(application);
            }
            else
            {
                return null;
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public ActionResult Edit(Role application)
        {
            return RedirectToAction("Index");
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
                ViewData["AgencyID"] = new SelectList(agencies, "AgencyID", "Name", role.Agency);
                ViewData["CompanyID"] = new SelectList(companies, "CompanyID", "Name", role.Company);
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
    }
}
