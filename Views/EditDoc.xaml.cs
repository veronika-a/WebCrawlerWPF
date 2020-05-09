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
using WebCrawlerWPF.FileSave;
using WebCrawlerWPF.Models;
using WebCrawlerWPF.ViewModels;

namespace WebCrawlerWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для EditDocWindow.xaml
    /// </summary>
    public partial class EditDoc : Window
    {
        EditDocViewModel viewModel;

        public EditDoc(ref SPage page,ref Document document)
        {
            viewModel = new EditDocViewModel(page, document);
            DataContext = viewModel;
            InitializeComponent();

            viewModel.Closing += (s, e) => this.Close();
        }
    }
}
