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

namespace WebCrawlerWPF.Patterns
{
    
    public class ProxyFile :IMyFile
    {
        private MyFile file;

        public ProxyFile()
        {
        }

        public void FileWrite(string text)
        {
            MessageBox.Show("Запись выполняеться...");
            if (file == null)
            {
                file = new MyFile();
            }
            file.FileWrite(text);
        }

        public void FileRead(string path)
        {
            MessageBox.Show("Подождите...");
            if (file == null)
            {
                file = new MyFile();
            }
            file.FileWrite(path);
        }

    }
    //class BookStoreProxy : IBook
    //{
    //    List<Page> pages;
    //    BookStore bookStore;
    //    public BookStoreProxy()
    //    {
    //        pages = new List<Page>();
    //    }
    //    public Page GetPage(int number)
    //    {
    //        Page page = pages.FirstOrDefault(p => p.Number == number);
    //        if (page == null)
    //        {
    //            if (bookStore == null)
    //                bookStore = new BookStore();
    //            page = bookStore.GetPage(number);
    //            pages.Add(page);
    //        }
    //        return page;
    //    }

    //    public void Dispose()
    //    {
    //        if (bookStore != null)
    //            bookStore.Dispose();
    //    }
    //}

}
