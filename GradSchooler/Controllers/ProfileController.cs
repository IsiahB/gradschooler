using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Calendar.v3;
using Google.Apis.Services;
using GradSchooler.CalendarUtil;
using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics.Contracts;

namespace GradSchooler.Controllers
{
    public class ProfileController : Controller
    {
        [Authorize] //if you are not authorized, it wont let you to the page
        public ActionResult Profile()
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

        public async Task IndexAsync(CancellationToken cancellationToken)
        {
            var result = await new AuthorizationCodeMvcApp(this, new GCalFlowMetaData()).AuthorizeAsync(cancellationToken);

            if (result.Credential != null)
            {
                var service = new CalendarService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "gradschooler"
                });

                // YOUR CODE SHOULD BE HERE..
                // SAMPLE CODE:
                //var list = await service.CalendarList.ToString();
                //ViewBag.Message = "FILE COUNT IS: " + list.Items.Count();
                //return View();
            }
            else
            {
                //return new RedirectResult(result.RedirectUri);
            }
        }


    }
}
