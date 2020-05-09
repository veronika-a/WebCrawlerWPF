using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WebCrawlerWPF.Models;
using WebCrawlerWPF.Repository;
using WebCrawlerWPF.Views;

namespace WebCrawlerWPF.ViewModels
{
    public class SiteStructureViewModel: INotifyPropertyChanged
    {
        int i;
        public event EventHandler Closing;
        private RelayCommand _start;
        private RelayCommand _back;
        private RelayCommand _newLink;
        private RelayCommand _search;

        private SPage page;
        public SPage Page
        {
            get { return page; }
            set
            {
                page = value;
                OnPropertyChanged(nameof(Page));
            }
        }
        private Site site;
        public Site Site
        {
            get { return site; }
            set
            {
                site = value;
                OnPropertyChanged(nameof(Site));
            }
        }


        public SiteStructureViewModel(string link)
        {
            string buf = "";
            i = 0;
            string sdf;
            if (link.Contains("https://") == true)
                i += 8;
            while (i < link.Length)
            {
                buf += link.Substring(i, 1);
                i++;
                if (link.Substring(i-1, 1) == "/") i = link.Length;
            }

            string url = "https://" + buf;
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(url);
            string siteName = doc.DocumentNode.SelectNodes("//title").FirstOrDefault().InnerText;

            MessageBox.Show(buf + " " + siteName);
            Site = new Site(siteName, buf);

            using (WCContext appContext = new WCContext())
            {
                // SPageRepository sPageRepository = new SPageRepository(appContext);
                SPageRepositoryProxy sPageRepositoryProxy = new SPageRepositoryProxy(appContext);
                Page = new SPage(link);
                
                Page = sPageRepositoryProxy.Insert(page);
                Site.Pages.Add(Page);
                page.Links.AddRange(AddUrlString(Page.PageLink));
                Links = Page.Links;
                //AllLinks=Links;
                //FindAllUrlString();
            }
        }
        public void FindAllUrlString()
        {
           int i = 0;
            int c = 0;
           // string l = page.Links[i];
           // SPage Page = new SPage(l);
            while (i < 100 & c!= AllLinks.Count)
            {
                var ll = AddUrlString(AllLinks[c]);
               
               foreach(var l in ll)
                {
                    if (AllLinks.Find(u => u == (l)) == null)
                    {
                        AllLinks.Add(l);
                        i++;
                    }
                }
                c++;
                // MessageBox.Show(AllLinks.Count.ToString());
            }
            MessageBox.Show("All links "+ AllLinks.Count.ToString());
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
                if (link != null & link != page.PageLink)
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
        List<string> _links;

        public List<string> Links
        {
            get { return _links; }
            set
            {
                _links = value;
                OnPropertyChanged(nameof(Links));
            }
        }

        List<string> _allLinks;

        public List<string> AllLinks
        {
            get { return _allLinks; }
            set
            {
                _allLinks = value;
                OnPropertyChanged(nameof(AllLinks));
            }
        }

        private string selectedLink;
        public string SelectedLink
        {
            get { return selectedLink; }
            set
            {
                selectedLink = value;
                OnPropertyChanged(nameof(SelectedLink));
            }
        }
        private string _searchLink;
        public string SearchLink
        {
            get { return _searchLink; }
            set
            {
                _searchLink = value;
                OnPropertyChanged(nameof(Search));
                Links = search_thispagelinks(Page, SearchLink);
            }
        }
        public List<string> search_thispagelinks(SPage thispage, string Search)
        {
            List<string> SearchLinks = new List<string>();
            foreach (var link in thispage.Links)
            {
                if(link.Contains(Search)) SearchLinks.Add(link);
            }
            return SearchLinks;
        }
        public RelayCommand Start
        {
            get
            {

                return _start ??
                    (_start = new RelayCommand(obj => {
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
        public RelayCommand Back
        {
            get
            {

                return _back ??
                    (_back = new RelayCommand(obj => {

                       // if (oldLink == "main")
                        {
                            MainWindow mainWindow = new MainWindow();
                            mainWindow.Show();
                        }
                        //else
                        //{
                        //    string oldLink = page.Link;
                        //    SiteStructure siteStructure = new SiteStructure(ref oldLink,ref oldLink);
                        //    siteStructure.Show();
                        //}
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        public RelayCommand NewLink
        {
            get
            {

                return _newLink ??
                    (_newLink = new RelayCommand(obj => {
                        SiteStructure siteStructure = new SiteStructure(ref selectedLink);
                        siteStructure.Show();
                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
        public RelayCommand Search
        {
            get
            {

                return _search ??
                    (_search = new RelayCommand(obj => {

                        Search search = new Search(ref page);
                        search.Show();

                        Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }

        static string GetAbsoluteUrlString(string baseUrl, string url)
        {
            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
            if (!uri.IsAbsoluteUri)
                uri = new Uri(new Uri(baseUrl), uri);
            return uri.ToString();
        }

        #region PropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #endregion
    }
   
}
