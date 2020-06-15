using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawlerWPF.Models;

namespace WebCrawlerWPF.ViewModels
{

    public abstract class SearchHandler
    {
        public SearchHandler Successor { get; set; }
        public abstract List<string> HandleRequest(Receiver receiver);
       // public abstract List<string> newSearch();
    }

    public class Receiver
    {
        public Receiver(bool pageSearch, bool siteSearch)
        {
            PageSearch = pageSearch;
            SiteSearch = siteSearch;
        }

        public bool PageSearch { get; set; }
        public bool SiteSearch { get; set; }
       
    }

    public class SearchInPage: SearchHandler
    {
        public SearchInPage(string newSearchText, SPage Page)
        {
            NewSearchText = newSearchText;
            thisPage = Page;
        }

        public SPage thisPage { get; set; }
        public string NewSearchText { get; set; }

        public override List<string> HandleRequest(Receiver receiver)
        {
            if (receiver.PageSearch == true)
            {
                return newSearch();
            }
            else if (Successor != null)
            {
                return Successor.HandleRequest(receiver);
            }
            else return null;
        }

        public List<string> newSearch()
        {
            List<string> nText = new List<string>();
            HtmlWeb webDoc = new HtmlWeb();
              HtmlDocument doc = webDoc.Load(thisPage.PageLink);
            // HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[node()='{NewSearchText}']");
            // HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{NewSearchText}')]");
            //HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[text()[contains(normalize-space(),'{NewSearchText}')]]");
               HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{NewSearchText}')]");
              if (p != null) nText.AddRange(newSearch_foreach(p));
            return nText;
        }


        public List<string> newSearch_foreach(HtmlNodeCollection str)
        {
            List<string> list = new List<string>();
            foreach (var t in str)
            {
                // Console.WriteLine(a.InnerText);
                Console.WriteLine(t.InnerHtml);
                list.Add(t.InnerHtml);
            }
            return list;

        }
    }

    public class SearchInSite : SearchHandler
    {
        public SearchInSite(string newSearchText, SPage Page)
        {
            NewSearchText = newSearchText;
            thisPage = Page;
        }

        public SPage thisPage { get; set; }
        public string NewSearchText { get; set; }


        public override List<string> HandleRequest(Receiver receiver)
        {
            if (receiver.SiteSearch == true)
            {
                return newSearch();
            }
            else if (Successor != null)
            {
                return Successor.HandleRequest(receiver);
            }
            else return null;
        }

        List<string> AllLinks = new List<string>();
        public List<string> newSearch()
        {
            List<string> nText = new List<string>();
              AllLinks.AddRange(AddUrlString(thisPage.PageLink));

            FindAllUrlString();
            foreach (var pl in AllLinks)
            {
                HtmlWeb webDoc = new HtmlWeb();
                HtmlDocument doc = webDoc.Load(pl);
                HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{NewSearchText}')]");
                if (p != null) nText.AddRange(newSearch_foreach(p));
            }
            return nText;
        }


        public List<string> newSearch_foreach(HtmlNodeCollection str)
        {
            List<string> list = new List<string>();
            foreach (var t in str)
            {
                list.Add(t.InnerHtml);
            }
            return list;

        }


        public void FindAllUrlString()
        {
            int i = 0;
            int c = 0;
            while (i < 100 & c != AllLinks.Count)
            {
                var ll = AddUrlString(AllLinks[c]);

                foreach (var l in ll)
                {
                    if (AllLinks.Find(u => u == (l)) == null)
                    {
                        AllLinks.Add(l);
                        i++;
                    }
                }
                c++;
            }
            MessageBox.Show("All links " + AllLinks.Count.ToString());
        }

        public List<string> AddUrlString(string plink)
        {
            string url = plink;
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(url);
            List<string> p = new List<string>();
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a"))
            {
                string link = node.GetAttributeValue("href", null);
                  if (link != null & link != thisPage.PageLink)
                if (p.Find(u => u == (link)) != link)
                        if (link.Contains("http") == false & link.Contains("javascript:") == false)
                        {        //  p.Add(link);
                            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                            if (GetAbsoluteUrlString(url, link).Contains("http"))
                                p.Add(GetAbsoluteUrlString(url, link));
                        }
            }
            return p;
        }

        static string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);
            return uri.ToString();
        }

    }
}
