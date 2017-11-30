using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class UniversityController : Controller
    {
        // GET: University
        public ActionResult University()
        {
            ViewBag.Message = "Your University Page";

            return View();
        }
    }
}