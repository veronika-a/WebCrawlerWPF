using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawlerWPF.Models;

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
            HtmlDocument doc = webDoc.Load(page.Link);
            HtmlNodeCollection p = doc.DocumentNode.SelectNodes($".//*[p='{stext}']");
            if (p != null)
            {
                foreach (var a in p)
                {
                    // Console.WriteLine(a.InnerText);
                    Console.WriteLine(a.InnerHtml);
                    nText.Add(a.InnerHtml);
                }
                Text = nText;
            }
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

                        File file = new File(SelectedText);

                        MessageBox.Show("Записть выполнена");
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
