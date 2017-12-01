using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradSchooler.DBUtilities;

namespace GradSchooler.Controllers
{
    public class CreateAccountController : Controller
    {
        // GET: Login
        public ActionResult CreateAccountPage()
        {
            ViewBag.Title = "Account Creation Page";

            var email = "";
            var password = "";
            var firstName = "";
            var lastName = "";
            var birthday = "";

            if (Request.HttpMethod == "POST") //print out to make sure if uses all caps
            {
                email = Request.Form["email"];
                password = Request.Form["password"];
                firstName = Request.Form["firstName"];
                lastName = Request.Form["lastName"];
                birthday = Request.Form["birthday"];

                //create a Model object -- Account then pass it to database

                GradSchooler.DBUtilities.DBUtilities db = GradSchooler.DBUtilities.DBUtilities.Instance;
                var insertCommand = "INSERT INTO Account (email, password_clr, password, firstName, lastName, birthday) VALUES(@0, @1, @2, @3, @4, @5)";
                //db.Execute(insertCommand, email, password, password, firstName, lastName, birthday);
            }
            
            return View();
        }
    }
}