using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawlerWPF.Models;
using WebCrawlerWPF.Repository;
using WebCrawlerWPF.ViewModels;

namespace WebCrawlerWPF.FileSave
{
    
    public class ProxyFile : FileSave
    {
        private MyFileSaveHtml file;

        public ProxyFile()
        {
        }
         /// Тут могут быть условия 
        public void FileWrite(string text)
        {
            MessageBox.Show("Запись выполняеться...");
            if (file == null)
            {
                file = new MyFileSaveHtml();
            }
           // file.FileWrite(text);
        }

        public void FileRead(string path)
        { 
            MessageBox.Show("Подождите...");
            if (file == null)
            {
                file = new MyFileSaveHtml();
            }
           // file.FileWrite(path);
        }

    }
}
