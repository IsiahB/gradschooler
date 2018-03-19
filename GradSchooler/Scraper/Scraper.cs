using GradSchooler.Models;
using ScrapySharp.Network;
using ScrapySharp.Html;
using System;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI.HtmlControls;

namespace GradSchooler
{
    public class Scraper
    {

        public Dictionary<String, List<String>> UniversitiesByState()
        {
            Dictionary<String, List<String>> d = new Dictionary<String, List<String>>();

            string[] lines;
            lines = System.IO.File.ReadAllLines(@"C:\Users\Isiah\Source\Repos\website\GradSchooler\Scraper\ubystate.html");
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
                            d.Add(statename, unis); // add the schools of each state to the dictionary
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


        public void ProgramScrape()
        {

            string url = "https://www.gradschools.com/institutions";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            

            //TODO
            //get university names from database
            string name = "Pacific Lutheran University";

            //getElementById(button name) for the button
            HtmlNode searchLink = doc.GetElementbyId("edit-submit-institution-campus-list");

            //put it in the search bar on the webpage and invoke a click on "apply" button
            //doc.  ("ok").InnerText = name;
            

            //get the first a href link and invoke the click (same way as previous invoke)
            //get the program titles and add them to the database

            
        }



    }// class
}// namespace


      

