using GradSchooler.Models;
using ScrapySharp.Network;
using ScrapySharp.Html;
using System;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

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

/*
        public List<University> scrape()
        {
            var uniList = new List<University>();
            //string file = @"C:\Users\Administrator\source\repos\GradSchooler\GradSchooler\Scraper\ubystate.html";
            string url = "https://www.gradschools.com/graduate-schools-in-united-states/washington";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            //var doc = new HtmlDocument();
            //doc.Load(file);
            var node = doc.DocumentNode.SelectSingleNode("
            /*Debug.WriteLine(node.OuterHtml);

            if (doc.DocumentNode != null && doc.ParseErrors != null && !doc.ParseErrors.Any())
                return null;

            var names = doc.DocumentNode.SelectNodes("/html/body/div//div/ol/li//a");
            var states = doc.DocumentNode.SelectNodes("/html/body/div//div/ol/li//p//a/big/b");

            foreach (var el in names.Zip(states, (n, s) => new University {name = n.InnerText, state = s.InnerText}))
            {
                uniList.Add(el);
            }

            return uniList;
        }//end method
   **/
        public Dictionary<String, List<String>> UniversitiesByState()
        {
            Dictionary<String, List<String>> d = new Dictionary<String, List<String>>();

            string[] lines;
            lines = System.IO.File.ReadAllLines(@"C:\Users\Administrator\source\repos\GradSchooler\GradSchooler\Scraper\ubystate.html");
            string statename = "";
            List<String> unis = new List<String>();
            var counter = 0;
            foreach( string line in lines)
            {
                // make sure we can index into the given string
                if (line.Length > 30)
                {
                    if (line.Substring(0, 11) == "<P><A NAME=")
                    {
                        if(statename != "")
                        {
                            d.Add(statename, unis);
                        }
                        string s = line.Substring(24); //returns a string that contains all chars past the 24th char
                        statename = s.Substring(0, s.IndexOf('<'));
                        unis = new List<String>();
                    }
                    else if(line.Substring(0, 12) == "<LI><A HREF="){
                        string university = lines[counter + 1].Trim();
                        university = university.Substring(0, university.IndexOf('<'));
                        unis.Add(university); //add university to list
                    }
                }
                counter++;
            }


            return d;


        }
    }// class
}// namespace


      

