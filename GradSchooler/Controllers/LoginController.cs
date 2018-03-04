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
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
                if (db.loginChecker(acc.email, acc.password))
                {
                    String name = db.getAccFirstName(acc.email);
                    //set the account attribute, firstName, to the user's first name by
                    //quering the database for the name
                    //acc.firstName = name; 
                    FormsAuthentication.SetAuthCookie(name, true);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Login data is incorrect!");
                }//else
            }//if
            return View(acc);

          //  FormsAuthentication.SignOut();
        }//login

        //logout method
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }//main controller class end
}//first bracket end
