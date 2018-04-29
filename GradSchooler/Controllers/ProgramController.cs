using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class ProgramController : Controller
    {
        private List<Program> progs = null;

        // GET: Program
        public ActionResult Program()
        {
            ViewBag.Message = "Program Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            progs = new List<Program>();
            progs = db.getPrograms("", "");
            ViewData["programs"] = progs;
            return View();
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
    }
}