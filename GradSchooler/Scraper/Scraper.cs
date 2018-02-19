using GradSchooler.Models;
using ScrapySharp.Network;
using ScrapySharp.Html;
using System;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using System.Linq;

namespace GradSchooler
{
    public class Scraper
    {
        public University[] scrape()
        {
            University[] un = null;
           
            var url = "https://university.graduateshotline.com/ubystate.html";
            var webGet = new HtmlWeb();
            if (webGet.Load(url) is HtmlDocument document)
            {
                var nodes = document.DocumentNode.CssSelect("#name b").ToList();
                for (var i = 0; i < 50; i++)
                {
                    un[i] = new University();
                    foreach (var node in nodes)
                    {
                        un[i].state = node.InnerText;

                    }
                }
            }
            return un;
        }
    }
}

