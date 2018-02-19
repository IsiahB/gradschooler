using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            Scraper s = new Scraper();
            University[] u = s.scrape();
            ViewData["uninames"] = u;
            return View();
        }
    }
}