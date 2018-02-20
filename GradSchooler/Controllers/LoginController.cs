using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal; //for GenericPrincipal method
using System.Web;
using System.Web.Mvc;
using System.Web.Security; //for FormsAuthentication method
using GradSchooler.Database;

namespace GradSchooler.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Login(string returnURL)
        {
            var userinfo = new Login();

            try
            {
                // We do not want to use any existing identity information
                EnsureLoggedOut();

                // Store the originating URL so we can attach it to a form field
                userinfo.returnURL = returnURL;

                return View(userinfo);
            }
            catch
            {
                throw;
            }
        }


        private void EnsureLoggedOut()
        {
            // If the request is (still) marked as authenticated we send the user to the logout action
            if (Request.IsAuthenticated)
                Logout();
        }

        //POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken] //this feature prevents data forgery or tampering
        //use @Html.AntiForgeryToken() in the forms posting to the method to invoke
        public ActionResult Logout()
        {
            try
            {
                // First we clean the authentication ticket like always
                //required NameSpace: using System.Web.Security;
                FormsAuthentication.SignOut();

                // Second we clear the principal to ensure the user does not retain any authentication
                //required NameSpace: using System.Security.Principal;
                HttpContext.User = new GenericPrincipal(new GenericIdentity(string.Empty), null);

                Session.Clear();
                System.Web.HttpContext.Current.Session.RemoveAll();

                // Last we redirect to a controller/action that requires authentication to ensure a redirect takes place
                // this clears the Request.IsAuthenticated flag since this triggers a new request
                return RedirectToLocal();
            }
            catch
            {
                throw;
            }
        }//end of method


        //GET: SignInAsync
        private void SignInRemember(string userName, bool isPersistent = false)
        {
            // Clear any lingering authencation data
            FormsAuthentication.SignOut();

            // Write the authentication cookie
            FormsAuthentication.SetAuthCookie(userName, isPersistent);
        }//end of method


        //GET: RedirectToLocal
        private ActionResult RedirectToLocal(string returnURL = "")
        {
            try
            {
                // If the return url starts with a slash "/" we assume it belongs to our site
                // so we will redirect to this "action"
                if (!string.IsNullOrWhiteSpace(returnURL) && Url.IsLocalUrl(returnURL))
                    return Redirect(returnURL);

                // If we cannot verify if the url is local to our host we redirect to a default location
                return RedirectToAction("Index", "Dashboard");
            }
            catch
            {
                throw;
            }
        }//end of method

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login entity)
        {
            string OldHASHValue = string.Empty;
            byte[] SALT = new byte[CryptoClass.saltLengthLimit];

            try
            {
                DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
                {
                    // Ensure we have a valid viewModel to work with
                    if (!ModelState.IsValid)
                        return View(entity);

                    //Retrieve Stored HASH Value From Database According To Username (one unique field)
                    //rewrite this to get the hashed password for a specific account
                    //var userInfo = db.UserMaster.Where(s => s.username == entity.username.Trim()).FirstOrDefault();

                    var userInfo = db.GetHashPass(); //fix this

                    //Assign HASH Value
                    if (userInfo != null)
                    {
                        //OldHASHValue = userInfo.HASH;
                        //SALT = userInfo.SALT;
                    }

                    bool isLogin = CompareHashValue(entity.password, entity.username, OldHASHValue, SALT);

                    if (isLogin)
                    {
                        //Login Success
                        //For Set Authentication in Cookie (Remeber ME Option)
                        SignInRemember(entity.username, entity.isRemember);

                        //Set A Unique ID in session
                        //Session["UserID"] = userInfo.UserID; ???????????????MAYBE WE CAN ADD THIS TO OUR DATABASE????

                        // If we got this far, something failed, redisplay form
                        // return RedirectToAction("Index", "Dashboard");
                        return RedirectToLocal(entity.returnURL);
                    }
                    else
                    {
                        //Login Fail
                        TempData["ErrorMSG"] = "Access Denied! Wrong Credential";
                        return View(entity);
                    }
                }
            }
            catch
            {
                throw;
            }

        }//end of method


        public static bool CompareHashValue(string password, string username, string OldHASHValue, byte[] SALT)
        {
            try
            {
                string expectedHashString = CryptoClass.Get_HASH_SHA512(password, username, SALT);

                return (OldHASHValue == expectedHashString);
            }
            catch
            {
                return false;
            }
        }//end of method


    }//main controller class end
}//first bracket end
