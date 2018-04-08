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
using System.Net;

namespace GradSchooler
{
    public class Scraper
    {
        
        //Read the html file to get Universities by State
        public Dictionary<String, List<String>> UniversitiesByState()
        {
            Dictionary<String, List<String>> d = new Dictionary<String, List<String>>();

            string[] lines;
            lines = System.IO.File.ReadAllLines(@"/Users/JChase/Projects/Capstone/_git/website/GradSchooler/Scraper/ubystate.html");
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

        }//UniversitiesByState


        public void ProgramScrape()
        {
            //get the university state, name and city
            DBUtilities.DBUtilities db = DBUtilities.DBUtilities.Instance;
            List<University> unis = db.displayUniversities();

            string s = "";
            foreach (var u in unis)
            {
                s = u.name; //the university name 
                var sArray = s.Split(' '); //separate each word in the name into a string array
                string endURL = "";
                foreach(var ite in sArray)
                {
                    //Debug.Write(ite + " ");
                }

                //loop over the array to fix the url with appropriate dashes
                for (int i = 0; i < sArray.Length; i++)
                { 
                    if (sArray[i].Equals("of"))
                    {
                        sArray[i] = "";
                    }
                    if(sArray[i].Contains("'"))
                    {
                        string st = sArray[i];
                        string str = ""; //string that has no apostrophes
                        var arr = st.Split('\''); //handles apostrophes

                        for(var item = 0; item < arr.Length; item++)
                        {
                            Debug.WriteLine("CountterER:    " + item);
                            str += arr[item];
                        }

                        //Debug.WriteLine(str);

                        sArray[i] = str; //put the correct string back into the array
                        endURL += sArray[i] + "-";
                        Debug.WriteLine(sArray[i]);
                    }
                    else
                    {
                        endURL += sArray[i] + "-";
                    }
                }//end for

                endURL += u.city.Replace(' ', '-'); //concatinate the city name to the university name for scraping purposes

                string url = "https://www.gradschools.com/graduate-schools-in-united-states/";
                url += u.state.Replace(' ', '-') + "/" + endURL + "?page=";
                Debug.WriteLine("url before passing it in : " + url);
                ProgramScrape(url);

            }//foreach

            
        }//end ProgramScrape()

        //private helper method to scrape the programs
        private void ProgramScrape(string url)
        {
            //url = "https://www.gradschools.com/graduate-schools-in-united-states/washington/university-washington-seattle?page=";

            List<string> programs = new List<string>();
            bool empty = false;
            int pagenum = 0;
            //read the webpages and get the programs
            while (!empty)
            {
                int eq = url.IndexOf('=');
                url = url.Substring(0, eq + 1);
                url = url + "" + pagenum;
                Debug.WriteLine("theurl : " + url);
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = web.Load(url);
                HttpWebRequest r = (HttpWebRequest)WebRequest.Create(url);
                //check if valid url
                if (r.Address.OriginalString != "https://www.gradschools.com/")
                {

                    Debug.WriteLine("the url : " + r.Address.OriginalString);

                    try
                    {
                        foreach (HtmlNode li in doc.DocumentNode.SelectNodes("//*[@id='" + "eddy-listings" + "']/li/h3/a/span"))
                        {
                            //check if the page has programs

                            programs.Add(li.InnerText);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        Debug.WriteLine("The inner text was null" + e);
                        empty = true;
                    }
                    pagenum++;
                }//if

            }//end while

            foreach (var thing in programs)
            {
                //Debug.WriteLine("thing: " + thing);
            }
        }//Program Scrape Method



    }// class
}// namespace


      

