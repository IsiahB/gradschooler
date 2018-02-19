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

