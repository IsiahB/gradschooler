using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GradSchooler.DBUtilities;
using GradSchooler.Models;

namespace GradSchooler.Controllers
{
    public class CreateAccountController : Controller
    {
        /// <summary>
        /// Creates the account form.
        /// </summary>
        /// <returns>The account form.</returns>
        public ActionResult CreateAccountForm()
        {
            ViewBag.Title = "Enter Account Info"; 
            return View();
        }//end CreateAccountForm
        
        /// <summary>
        /// Creates the account page.
        /// </summary>
        /// <returns>The account page.</returns>
        public ActionResult CreateAccountPage()
        {
           // CreateAccountForm(); // load empty form
            ViewBag.Title = "Account Creation Page";
            //create Account model
            Account a = new Account();
            Profile p = new Profile();
            //get user response
            Console.Write(Request.HttpMethod);
            if (Request.HttpMethod == "POST") //print out to make sure if uses all caps
            {
                a.email = Request.Form["email"];
                a.password = Request.Form["password"];
                a.firstName = Request.Form["firstName"];
                a.lastName = Request.Form["lastName"];
                a.birthday = Request.Form["birthday"];

                //empty profile created with account creation
                p.pEmail = Request.Form["email"];
                p.favUnis = null;
                p.deadlines = null;

                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;

                //pass Account model object to the database to create the account
                db.createAccount(a);
                return View("/Views/Login/Login.cshtml");
            }
            return View();
        }//end CreateAccountPage()

    }//end CreateAccountController
}//namespace