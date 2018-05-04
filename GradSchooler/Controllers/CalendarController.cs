using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class CalendarController : Controller
    {
        public ActionResult Calendar()
        {
            ViewBag.User = User.Identity.Name;
            ViewBag.Date = Date();
            return View();
        }
        public ActionResult Deadline(){
            ViewBag.User = User.Identity.Name;
            ViewBag.Date = Date();
            return View();
        }

        [HttpPost]
        public ActionResult AddDeadline(){
            //db.submitDateRequest(deadline);
            string deadline = Request["deadline"];
            //Debug.WriteLine(" the deadline: " + deadline);
            return RedirectToAction(this.ControllerContext.RouteData.Values["controller"].ToString());
        }

        public string Date(){
            DateTime today = DateTime.Today;
            return today.ToString("yyyy-MM-dd");

        }
    }
}
