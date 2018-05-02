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

        public string Date(){
            DateTime today = DateTime.Today;
            //Debug.WriteLine(today.ToString("dd-MM-yyyy"));
            return today.ToString("dd-MM-yyyy");

        }
    }
}
