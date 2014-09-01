using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Perscribo.EF.Library.Models;

namespace Perscribo.Controllers
{
    public class SharedController : Controller
    {
        [HttpGet]
        public ActionResult Address()
        {
            Agency agency = null;

            return PartialView("EditorTemplates/Address", agency);
        }

    }
}
