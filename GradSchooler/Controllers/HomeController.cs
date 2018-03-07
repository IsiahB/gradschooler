using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using GradSchooler.Models;

namespace GradSchooler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var client = new HttpClient();
            //var response = client.GetAsync("http://localhost:52473/api/team").Result;
            //var products = response.Content.ReadAsStringAsync();
            //products

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}