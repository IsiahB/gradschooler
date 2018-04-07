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
                    Debug.Write(ite + " ");
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
                }

                endURL += u.city.Replace(' ', '-'); //concatinate the city name to the string url for scraping purposes

                string url = "https://www.gradschools.com/graduate-schools-in-united-states/"+u.state.Replace(' ', '-')+"/"+endURL;

                Debug.WriteLine(url);

                //ProgramScrape(url);

            }//foreach

            
            

            //TODO
            //get university names from database
            //string name = "Pacific Lutheran University";

            //getElementById(button name) for the button
            //HtmlNode searchLink = doc.GetElementbyId("edit-submit-institution-campus-list");

            //put it in the search bar on the webpage and invoke a click on "apply" button
            //doc.  ("ok").InnerText = name;
            

            //get the first a href link and invoke the click (same way as previous invoke)
            //get the program titles and add them to the database

            
        }

        private void ProgramScrape(string url)
        {
            //check if valid url
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
        }



    }// class
}// namespace


      

