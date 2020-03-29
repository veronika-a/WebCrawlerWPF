using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.ViewModels
{
    public class SearchText
    {
        public SearchText(string newSearchText, string pagelink)
        {
            NewSearchText = newSearchText;
            PageLink = pagelink;
        }

        public string PageLink { get; set; }
        public string NewSearchText { get; set; }

        public List<string> newSearch()
        {
            List<string> nText = new List<string>();
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(PageLink);
            // HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[node()='{NewSearchText}']");
            // HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{NewSearchText}')]");
            //HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[text()[contains(normalize-space(),'{NewSearchText}')]]");
             HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[contains(text(),'{NewSearchText}')]");
            if (p != null) nText.AddRange(newSearch_foreach(p));
            return nText;
            //Text = nText;
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
}
