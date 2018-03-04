using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GradSchooler.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize] //if you are not authorized, it wont let you to the page
        public ActionResult Profile()
        {


            return View();
        }
    }
}
