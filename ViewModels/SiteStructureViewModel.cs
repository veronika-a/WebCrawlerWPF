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
using WebCrawlerWPF.Views;

namespace WebCrawlerWPF.ViewModels
{
    public class SiteStructureViewModel: INotifyPropertyChanged
    {
        public event EventHandler Closing;
        private RelayCommand _start;
        private RelayCommand _back;

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


        public SiteStructureViewModel( string link)
        {
           // Site = new Site();
            Page = new SPage(link);
           
            AddUrlString(Page.Link);
           // Site.Pages.Add(Page);
            Links = Page.Links;
           
        }
        public void AddAllUrlString(string link)
        {
            int i = 0;
            while (i< 1000)
            {

                AddUrlString(Page.Links[i]);
            }
        }
        public void AddUrlString(string plink)
        {
            string url = plink;
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(url);
            List<string> p = new List<string>();
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a"))
            {
                string link = node.GetAttributeValue("href", null);
                if (link != null & link!= page.Link)
                    if (p.Find(u => u == (link)) != link)
                        if (link.Contains("http") == false & link.Contains("javascript:") == false)
                        {        //  p.Add(link);
                            var uri = new Uri(url, UriKind.RelativeOrAbsolute);
                            p.Add(GetAbsoluteUrlString(url, link));
                        }
            }

            page.Links.AddRange(p);
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
        public SiteStructureViewModel() { }
        

        public RelayCommand Start
        {
            get
            {

                return _start ??
                    (_start = new RelayCommand(obj => {

                        //var reader = new Reader()
                        //{
                        //    Email = this.email,
                        //    Password = this.password
                        //};
                        //service1.registrationViewModel_signUp(reader);

                        // CabinetReader cabinetReader = new CabinetReader(ref reader);
                        //cabinetReader.Show();
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

                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
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
