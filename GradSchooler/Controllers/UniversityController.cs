using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GradSchooler.Controllers
{
    public class UniversityController : Controller
    {
        [HttpGet]
        public ActionResult University()
        {
            ViewBag.Message = "University Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            int size = db.tableSizes("University");
            University[] univs = new University[size];
            univs = db.displayUniversities(univs);
            ViewData["unis"] = univs;

            return View(); //automatically returns the University View
        }//end of get method

        [HttpPost]
        public ActionResult University(Profile p)
        {
            if (ModelState.IsValid)
            {
                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;

                String curEmail = User.Identity.Name;//get the email of the person logged in
                Debug.WriteLine("CURRENT LOGGED IN USER: " + curEmail); //test

                //get post data from request object
                var favUnisData = this.Request.Form; //get the data from the form
                for (int i = 0; i < favUnisData.Count; i++)
                {
                    p.favUnis[i] = favUnisData[i];
                    db.addFavUniversity(curEmail, p.favUnis[i]); //add favUni to database
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("","This action cannot be performed");
            }

            return View(p);
        }//end of post method

    }//end of class
}//end of namespace