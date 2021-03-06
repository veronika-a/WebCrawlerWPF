﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebCrawlerWPF.ViewModels;

namespace WebCrawlerWPF.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel viewModel = new MainWindowViewModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Closing += (s, e) => this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            P2P_MainWindow p2p = new P2P_MainWindow();
            p2p.Show();
        }
    }
}
