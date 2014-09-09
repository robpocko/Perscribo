using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Controllers
{
    public partial class PartialViewsController : Controller
    {
        private PerscriboContext db = new PerscriboContext();

        public virtual PartialViewResult Consultants(string id)
        {
            List<Consultant> consultants = new List<Consultant>();
            if (id != null)
            {
                int agencyID = int.Parse(id);
                consultants = db.Agencies.Where(a => a.ID == agencyID).FirstOrDefault().Consultants.ToList();
            }

            return PartialView("_ConsultantsDropDown", consultants);
        }
    }
}
