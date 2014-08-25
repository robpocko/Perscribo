using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Perscribo.Areas.Applications.Controllers
{
    public class ApplicationController : Controller
    {
        //
        // GET: /Applications/Application/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Applications/Application/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Applications/Application/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Applications/Application/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Applications/Application/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Applications/Application/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Applications/Application/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Applications/Application/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
