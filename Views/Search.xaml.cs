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
    /// Логика взаимодействия для Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        SearchViewModel viewModel;
        public Search(ref SPage page)
        {
            viewModel = new SearchViewModel(page);
            DataContext = viewModel;
            InitializeComponent();

            viewModel.Closing += (s, e) => this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
