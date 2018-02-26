using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal; //for GenericPrincipal method
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //for FormsAuthentication method
using GradSchooler.Database;
using System.Diagnostics;

namespace GradSchooler.Controllers
{
    public class LoginController : Controller
    {
        [HttpPost]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
                if (db.loginChecker(acc.email, acc.password))
                {
                    FormsAuthentication.SetAuthCookie(acc.email, true);
                    return RedirectToAction("Dashboard", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }//else
            }//if
            return View(acc);
        }//login

    }//main controller class end
}//first bracket end
