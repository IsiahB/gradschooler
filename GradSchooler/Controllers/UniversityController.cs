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
            ViewBag.Message = "Your University Page";

            //get info from the database
            //create model object
            //return array of model objects
            //pass model to view -- how contrllers send data to view

            
            

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            int size = db.tableSizes("University");
            University[] univs = new University[size];
            univs = db.displayUniversities(univs);
            ViewData["unis"] = univs;

            return View(); //automatically returns the University View
        }
    }
}