using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perscribo.EF.Library.DAL;
using Perscribo.EF.Library.Models;

namespace Perscribo.Areas.Applications.Controllers
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



        public virtual PartialViewResult AddressEdit(string id)
        {
            int addressId = id == null ? 0 : int.Parse(id);

            var address = db.Addresses.Where(a => a.ID == addressId).FirstOrDefault();

            return PartialView("_AddressEdit", address);
        }

    }
}
