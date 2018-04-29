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
        /// <summary>
        /// Default method that displays all universities when university page is accessed
        /// </summary>
        /// <returns>The university.</returns>
        [HttpGet]
        public ActionResult University()
        {
            ViewBag.Message = "University Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            List <University> univs = new List<University>();

            univs = db.getUniversities("", "");
            ViewData["unis"] = univs;

            return View(); //automatically returns the University View
        }//end of get method

        /// <summary>
        /// Called when a user is searching for a specific university
        /// by entering a search keyword
        /// </summary>
        [HttpPost]
        public ActionResult University(String keyword){
            string searchType = "";
            if(!String.IsNullOrEmpty(Request["name"])){
                keyword = Request["name"];
                searchType = "name";
            }
            else if (!String.IsNullOrEmpty(Request["city"]))
            {
                keyword = Request["city"];
                searchType = "city";
            }
            else if (!String.IsNullOrEmpty(Request["state"]))
            {
                keyword = Request["state"];
                searchType = "state";
            }
            else if (!String.IsNullOrEmpty(Request["url"]))
            {
                keyword = Request["url"];
                searchType = "uniUrl";
            }

            ViewBag.Message = "University Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            List<University> univs = new List<University>();

            univs = db.getUniversities(keyword, searchType);
            ViewData["unis"] = univs;

            return View();
        }

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
                            db.addFavUniversity(curEmail, item); //add favUni to database
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

            s = "NULL";
            String curEmail = User.Identity.Name;

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            db.addURequest(u, curEmail, s);


            return RedirectToAction("University", "University");
        }


    }//end of class
   
}//end of namespace