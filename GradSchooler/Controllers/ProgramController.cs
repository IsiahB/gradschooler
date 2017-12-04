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
        // GET: Program
        public ActionResult Program()
        {
            ViewBag.Message = "Program Page";

            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            int size = db.tableSizes("Program");
            Program[] progs = new Program[size];
            progs = db.displayPrograms(progs);
            ViewData["programs"] = progs;

            return View();
        }
    }
}