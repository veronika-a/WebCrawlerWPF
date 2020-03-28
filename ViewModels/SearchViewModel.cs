using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawlerWPF.Models;
using WebCrawlerWPF.Patterns;

namespace WebCrawlerWPF.ViewModels
{
   public class SearchViewModel : INotifyPropertyChanged
    {
        public event EventHandler Closing;
        SPage page;

        public SearchViewModel(SPage page)
        {
            this.page = page;
            Text = new List<string>();
           
        }



        public void newSearch()
        {
            List<string> nText = new List<string>();
            string stext = NewSearchText;
            HtmlWeb webDoc = new HtmlWeb();
            HtmlDocument doc = webDoc.Load(page.PageLink);
            HtmlNodeCollection str;
            HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[node()='{stext}']");
            if (p != null) nText.AddRange( newSearch_foreach(p));
            Text = nText;
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

        private string _newSearchText;
        public string NewSearchText
        {
            get { return _newSearchText; }
            set
            {
                _newSearchText = value;
                OnPropertyChanged(nameof(NewSearchText));
            }
        }


        List<string> _text;

        public List<string> Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        private string _selectedText;
        public string SelectedText
        {
            get { return _selectedText; }
            set
            {
                _selectedText = value;
                OnPropertyChanged(nameof(SelectedText));
            }
        }

        private RelayCommand _searchText;
        public RelayCommand SearchText
        {
            get
            {

                return _searchText ??
                    (_searchText = new RelayCommand(obj => {

                        newSearch();
                        
                       // Closing?.Invoke(this, EventArgs.Empty);
                    }));
            }
        }
        private RelayCommand _save;
        public RelayCommand Save
        {
            get
            {

                return _save ??
                    (_save = new RelayCommand(obj => {

                        IMyFile file = new ProxyFile();
                        file.FileWrite(SelectedText);

                    }));
            }
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
