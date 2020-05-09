using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawlerWPF.FileSave;
using WebCrawlerWPF.Models;

namespace WebCrawlerWPF.ViewModels
{
   public class SearchViewModel : INotifyPropertyChanged
    {
        public event EventHandler Closing;
        SPage page;
        Document document;
        DocHistory docHistory;
        public SearchViewModel(SPage page)
        {
            this.page = page;
            Text = new List<string>();
            RadioButton_page_Checked = true;
             document = new Document();
             docHistory = new DocHistory();
            // RadioButton_site_Checked = false;
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

        private bool _RadioButton_page_Checked;
        public bool RadioButton_page_Checked
        {
            get { return _RadioButton_page_Checked; }
            set
            {
                _RadioButton_page_Checked = value;
                OnPropertyChanged(nameof(RadioButton_page_Checked));
            }
        }

        private bool _RadioButton_site_Checked;
        public bool RadioButton_site_Checked
        {
            get { return _RadioButton_site_Checked; }
            set
            {
                _RadioButton_site_Checked = value;
                OnPropertyChanged(nameof(RadioButton_site_Checked));
            }
        }

        private RelayCommand _searchText;
        public RelayCommand SearchText
        {
            get
            {

                return _searchText ??
                    (_searchText = new RelayCommand(obj => {

                        Receiver receiver = new Receiver( RadioButton_page_Checked, RadioButton_site_Checked);

                        SearchInSite searchInSite = new SearchInSite(NewSearchText, page);
                        SearchInPage searchInPage = new SearchInPage(NewSearchText, page);
                        searchInPage.Successor = searchInSite;

                        Text = searchInPage.HandleRequest(receiver);
                        
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

                        ////save in file
                        //IMyFile file = new ProxyFile();
                        // file.FileWrite(SelectedText);


                        ////сохранение в Document and DocMomento
                        docHistory.History.Push(document.SaveState());
                        document.items.Add(SelectedText);
                        
                        MessageBox.Show(document.items.Count.ToString());

                    }));
            }
        }
        private RelayCommand _cancel;
        public RelayCommand Cancel
        {
            get
            {

                return _cancel ??
                    (_cancel = new RelayCommand(obj => {

                        DocMemento docMemento = docHistory.History.Pop();
                        document.RestoreState(docMemento);
                        // document.RestoreState(docHistory.History.Pop());
                        MessageBox.Show(document.items.Count.ToString());

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
