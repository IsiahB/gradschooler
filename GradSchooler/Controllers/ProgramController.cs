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
    }
}