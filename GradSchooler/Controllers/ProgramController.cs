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
            List<Program> progs = new List<Program>();
            progs = db.getPrograms();
            ViewData["programs"] = progs;


            return View();
        }
    }
}