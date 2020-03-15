using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WebCrawlerWPF.Models;
using WebCrawlerWPF.ViewModels;

namespace WebCrawlerWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для SiteStructure.xaml
    /// </summary>
    public partial class SiteStructure : Window
    {
        SiteStructureViewModel viewModel;

        //public SiteStructure()
        //{
        //    InitializeComponent();
        //    viewModel = new SiteStructureViewModel();
        //    DataContext = viewModel;
        //    viewModel.Closing += (s, e) => this.Close();
        //}

        public SiteStructure(ref string link)
        {
            viewModel = new SiteStructureViewModel(link);
            DataContext = viewModel;
            InitializeComponent();
           
            viewModel.Closing += (s, e) => this.Close();
        }
       
    }
}
