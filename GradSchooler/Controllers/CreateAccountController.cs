using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class CreateAccountController : Controller
    {
        // GET: Login
        public ActionResult CreateAccountPage()
        {
            return View();
        }
    }
}