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
    public class EditDocViewModel: INotifyPropertyChanged
    {

        public event EventHandler Closing;
        SPage page;
        Document document;
        DocHistory docHistory;
        public EditDocViewModel(SPage page, Document document)
        {
            this.page = page;
            this.document = document;
            this.page = page;
            Text = document.items;
            RadioButton_html= true;
            docHistory = new DocHistory();
            docHistory.History.Push(document.SaveState());
        }

        private RelayCommand _save;
        public RelayCommand Save
        {
            get
            {

                return _save ??
                    (_save = new RelayCommand(obj => {

                        ////save in file
                        if (RadioButton_html == true)
                        {
                            MyFileSaveHtml file = new MyFileSaveHtml();
                            file.FileWrite(document);
                            MessageBox.Show("Save in html");
                        }
                        else
                        {
                            MyFileSaveDoc file = new MyFileSaveDoc();
                            file.FileWrite(document);
                            MessageBox.Show("Save in Doc ");
                        }

                    }));
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

        private bool _RadioButton_html;
        public bool RadioButton_html
        {
            get { return _RadioButton_html; }
            set
            {
                _RadioButton_html = value;
                OnPropertyChanged(nameof(RadioButton_html));
            }
        }

        private bool _RadioButton_doc;
        public bool RadioButton_doc
        {
            get { return _RadioButton_doc; }
            set
            {
                _RadioButton_doc = value;
                OnPropertyChanged(nameof(RadioButton_doc));
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
