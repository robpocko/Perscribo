using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perscribo.Areas.Applications.Controllers
{
    public class PartialViewsController : Controller
    {
        //
        // GET: /Applications/PartialViews/

        public PartialViewResult AddressEdit()
        {
            return PartialView();
        }

    }
}
