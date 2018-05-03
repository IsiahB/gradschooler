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
        public ActionResult University(int page = 0)
        {
            const int pageSize = 15; //number of elements per page

            ViewBag.Message = "University Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            List <University> univs = new List<University>();

            univs = db.getUniversities("", "");
            var count = univs.Count;
            var data = univs.Skip(page * pageSize).Take(pageSize).ToList();
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;

            ViewData["unis"] = data;

            return View(data); //automatically returns the University View
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the view of the SingleUniversity page in order
        /// for a user to view the data of one university
        /// </summary>
        /// <param name="u"></param>
        /// <returns></returns>
        public ActionResult SingleUniversity(University u)
        {
            ViewData["uni"] = u;
            return View();
        }

        /// <summary>
        /// Returns the view of the SingleUniversity page with a form 
        /// in order for a user to change the data of one university
        /// </summary>
        /// <param name="u"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SingleUniversity(University u, string s){

            s = "NULL";
            String curEmail = User.Identity.Name; //the user that is logged in

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            db.addURequest(u, curEmail, s);

            return RedirectToAction("University", "University");
        }


    }//end of class
   
}//end of namespace