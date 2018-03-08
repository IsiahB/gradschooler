using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class ProfilerController : Controller
    {
        [Authorize] //if you are not authorized, it wont let you to the page
        public ActionResult Profiler()
        {
            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;

            String userEmail = User.Identity.Name;
            String userName = db.getAccFirstName(userEmail);
            List<University> favUnis = db.getFavUniversities(userEmail);
            var size = favUnis.Count;

            if (size == 0)
            {
                University u = new University
                {
                    name = "",
                    fundingtype = "",
                    city = "",
                    state = "",
                    environment = ""
                };
                favUnis.Add(u);
            }
            
            ViewData["favUnis"] = favUnis;

            return View();
        }

       [HttpPost]
       public ActionResult DeleteProfile()
        {
            string userEmail = User.Identity.Name;
            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            db.deleteAccount(userEmail);
            //can you log out of an account you've deleted?
            return RedirectToAction("Index", "Home");

        }
    }
}
