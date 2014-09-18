using System;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.UI;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Controllers
{
    public partial class CompanyController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Index()
        {
            var companies = db.Companies
                                .OrderBy(a => a.Name).ToList();

            return View(companies);
        }

        [HttpGet, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit(string id)
        {
            if (id == null)
            {
                var newCompany = new Company();

                return View(newCompany);
            }
            else
            {
                if (id == "Address")
                {
                    return RedirectToAction(MVC.Agency.ActionNames.Address);
                }
                else
                {
                    int companyId = int.Parse(id);
                    var company = db.Companies.Where(c => c.ID == companyId).FirstOrDefault();
                    LoadSelectLists(company.Address);

                    return View(company);
                }
            }
        }

        [HttpPost, OutputCache(NoStore = true, Location = OutputCacheLocation.None)]
        public virtual ActionResult Edit([Bind(Include = "ID,Name,AddressID, Address")] Company company)
        {
            if (company.Address != null)
            {
                if (company.AddressID == null)
                {
                    db.Entry(company.Address).State = EntityState.Added;
                }
                else
                {
                    db.Entry(company.Address).State = EntityState.Modified;
                }
            }
            db.Companies.Add(company);
            try
            {
                if (ModelState.IsValid)
                {
                    if (company.ID != 0)
                    {
                        db.Entry(company).State = EntityState.Modified;
                    }

                    db.SaveChanges();
                }
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
