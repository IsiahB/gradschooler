using GradSchooler.Models;
using ScrapySharp.Network;
using ScrapySharp.Html;
using System;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Linq;
using System.Collections.Generic;

namespace GradSchooler
{
    public class Scraper
    {
        /*public University[] scrape()
       {
           System.Diagnostics.Debug.WriteLine("HERE MTHRFKR!!");
           University[] un = new University[10000000];
           String[] states = new String[55];

           var url = "https://university.graduateshotline.com/ubystate.html#WA";
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
       }//end scrape*/


        public List<University> scrape()
        {
            var uniList = new List<University>();
            const string url = "https://university.graduateshotline.com/ubystate.html#WA";
            var web = new HtmlWeb();
            var htmlDoc = web.Load(url);

            if (htmlDoc.DocumentNode != null && htmlDoc.ParseErrors != null && !htmlDoc.ParseErrors.Any())
                return null;

            var names = htmlDoc.DocumentNode.SelectNodes("/html/body/div/div/ol/li/a");
            var states = htmlDoc.DocumentNode.SelectNodes("/html/body/div/div/p/a/big/b");

            foreach (var node in names.Zip(states, (n, s) => new University {name = n.InnerText, state = s.InnerText}))
            {
                uniList.Add(node);
            }

            return uniList;
        }//end method
    }// class
}// namespace


      

