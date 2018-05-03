using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class ProgramController : Controller
    {
        private List<Program> progs = null;

        // GET: Program
        public ActionResult Program(int page = 0)
        {
            const int pageSize = 15; //number of elements per page

            ViewBag.Message = "Program Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            progs = new List<Program>();

            progs = db.getPrograms("", "");
            var count = progs.Count;
            var data = progs.Skip(page * pageSize).Take(pageSize).ToList();
            ViewBag.MaxPage = (count / pageSize) - (count % pageSize == 0 ? 1 : 0);
            ViewBag.Page = page;

            ViewData["programs"] = data;
            return View(data);
        }
        /// <summary>
        /// Called when a user is searching for a specific program
        /// by entering a search keyword
        /// </summary>
        [HttpPost]
        public ActionResult Program(String keyword)
        {
            string searchType = "";
            if (!String.IsNullOrEmpty(Request["name"]))
            {
                keyword = Request["name"];
                searchType = "programname";
            }
            else if (!String.IsNullOrEmpty(Request["institution"]))
            {
                keyword = Request["institution"];
                searchType = "schoolname";
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

            ViewBag.Message = "Program Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            progs = new List<Program>();

            progs = db.getPrograms(keyword, searchType);
            ViewData["programs"] = progs;

            return View();
        }

        [HttpPost]
        public ActionResult FavoriteProgram()
        {
            if (ModelState.IsValid)
            {
                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;

                String curEmail = User.Identity.Name;//get the email of the person logged in

                //get post data from request object
                List<String> favProgsData = new List<String>();

                foreach(var key in Request.Form.AllKeys)
                {
                    if (key.StartsWith("favProgs"))
                    {
                        String s = Convert.ToString(Request.Form[key]);
                        String[] values = s.Split(',');
                        foreach(var item in values)
                        {
                            db.addFavProgram(curEmail, item); //add favUni to database
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
    }
}