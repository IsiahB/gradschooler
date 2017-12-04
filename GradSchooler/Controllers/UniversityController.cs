using GradSchooler.Models;
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
            ViewBag.Message = "University Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            int size = db.tableSizes("University");
            University[] univs = new University[size];
            univs = db.displayUniversities(univs);
            ViewData["unis"] = univs;

            return View(); //automatically returns the University View
        }
    }
}