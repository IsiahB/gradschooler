using GradSchooler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ScrapySharp.Network;
using ScrapySharp.Html;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace GradSchooler.Controllers
{
    public class AboutController : Controller
    {
        // GET: About
        public ActionResult About()
        {
            //Scraper s = new Scraper();
            var i = test();
            var k = 1;
            //University[] u = scrape();
            //ViewData["uninames"] = u;
            String[] kl = { "1", "2" };
            ViewBag.info = "Hello I am here"; 

            return View();
        }
        public University[] scrape()
        {
            System.Diagnostics.Debug.WriteLine("HERE MTHRFKR!!");
            University[] un = new University[10000000];

            var url = "https://tipidpc.com/catalog.php?cat=0&sec=s";
            var webGet = new HtmlWeb();
            if (webGet.Load(url) is HtmlDocument document)
            {
                System.Diagnostics.Debug.WriteLine("Here 1" + '\n');
                var nodes = document.DocumentNode.CssSelect("#item-search-results li").ToList();
                for (var i = 0; i < 50; i++)
                {
                    System.Diagnostics.Debug.WriteLine("Here 2" + '\n');
                    un[i] = new University();
                    foreach (var node in nodes)
                    {
                        System.Diagnostics.Debug.WriteLine("Here 3" + '\n');
                        un[i].state = node.CssSelect("h2 a").Single().InnerText;

                    }//end foreach
                }//end outer for
            }//end if
            return un;
        }//end scrape

        public int test()
        {
            return 1;
        }
    }
}