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

            //Scraper s = new Scraper();
            //s.ProgramScrape();

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            List <University> univs = new List<University>();

            univs = db.displayUniversities();
            ViewData["unis"] = univs;

            return View(); //automatically returns the University View
        }//end of get method

        [HttpPost]
        public ActionResult FavoriteUniversity()
        {
            if (ModelState.IsValid)
            {
                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;

                String curEmail = User.Identity.Name;//get the email of the person logged in

                //get post data from request object
                List<String> favUnisData = new List<String>();

                foreach(var key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("favUnis"))
                    {
                        String s = Convert.ToString(Request.Form[key]);
                        String[] values = s.Split(',');
                        foreach(var item in values)
                        {
                            db.addFavUniversity(curEmail, item); //add favUni to database;
                        }//end foreach
                    }//end if
                }//end foreach


                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("","This action cannot be performed");
            }

            return View();
        }//end of post method

        public ActionResult SingleUniversity(University u)
        {
            ViewData["uni"] = u;
            return View();
        }

        [HttpPost]
        public ActionResult SingleUniversity(University u, string s){
            //Debug.Write("University wanted to name: " + u.name);
            //Debug.WriteLine("The funding type: " + u.fundingtype);

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;


            return RedirectToAction("University", "University");
        }


    }//end of class
   
}//end of namespace