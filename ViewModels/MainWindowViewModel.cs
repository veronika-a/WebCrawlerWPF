using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlerWPF.Models;
using WebCrawlerWPF.Views;

namespace WebCrawlerWPF.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event EventHandler Closing;

        private string link;
        public string Link
        {
            get { return link; }
            set
            {
                link = value;
                OnPropertyChanged(nameof(Link));
            }
        }
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
        public RelayCommand Start
        {
            get
            {

                return _start ??
                    (_start = new RelayCommand(obj => {
                        if (link != "")
                        {

                            
                            SiteStructure siteStructure = new SiteStructure(ref link);
                            //MWindow mWindow = new MWindow();
                            //mWindow.WindowFrame.Content = siteStructure;
                            siteStructure.Show();
                            Closing?.Invoke(this, EventArgs.Empty);
                        }
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
