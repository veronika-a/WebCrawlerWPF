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

namespace WebCrawlerWPF.ViewModels
{
    public class SiteStructureViewModel: INotifyPropertyChanged
    {
        public event EventHandler Closing;

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

        private RelayCommand _start;

        public SiteStructureViewModel( string link)
        {
            Page = new SPage(link);
            AddUrlString();
            Links = Page.Links;
           
        }

        public void AddUrlString()
        {
            string url = Page.Link;
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(url);
            List<string> p = new List<string>();
            foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//a"))
            {
                var li = node.GetAttributeValue("href", null);
                if (p.Find(u => u == (li))!=li) p.Add(li);
                //var uri = new Uri(url, UriKind.RelativeOrAbsolute);

                // p.Add(GetAbsoluteUrlString(url, node.GetAttributeValue("href", null)));
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
