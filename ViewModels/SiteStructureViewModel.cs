using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        string oldLink;
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


        public SiteStructureViewModel( string link, string oldlink)
        {
            i = 0;
            oldLink = oldlink;
            Site = new Site();
            using (WCContext appContext = new WCContext())
            {
                SPageRepository sPageRepository = new SPageRepository(appContext);
                Page = new SPage(link);
                page.Links.AddRange(AddUrlString(Page.PageLink));
                sPageRepository.Insert(page);

                // AllLinks.AddRange(Links);
                // AddUrlString();
                Site.Pages.Add(Page);
                Links = Page.Links;
            }
               
           
        }
        public void AddAllUrlString()
        {
            string l = page.Links[i];
            SPage Page = new SPage(l);
            while (i< 100)
            {
                //if (Site.Pages.Find(u => u.Link == (l)) == null)
                //if(AllLinks.Find(u => u == (l)) == null)
                //{
                //    AllLinks.AddRange(AddUrlString(Page.Links[i+1]));
                //}
                //i++;
            }
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
                if (link != null & link!= page.PageLink)
                    if (p.Find(u => u == (link)) != link)
                        if (link.Contains("http") == false & link.Contains("javascript:") == false)
                        {        //  p.Add(link);
                            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
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
                        string oldLink = page.PageLink;
                        SiteStructure siteStructure = new SiteStructure(ref selectedLink, ref oldLink);
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
