using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WebCrawlerWPF.FileSave
{
    public class MyFileSaveHtml: FileSave
    {

        private void Sleep()
        {
            Thread.Sleep(5000);
        }
        public MyFileSaveHtml() {
            
           
           // string text = "Привет мир!\nПока мир...";
          //  FileWrite(text);
          // FileRead(writePath);

        }

        //public MyFile(string text)
        //{
        //   // string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage1.html");

        //    FileWrite(text);
        //    //FileRead(writePath);

        //}
        public override void FileWrite(Document document)
        {
            List<string> text = document.items;
            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage2.html");
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    foreach(string st in text)
                    {
                        sw.Write("<p>");
                        sw.WriteLine(st);
                        sw.Write("</p>");
                    }
                    
                }

                MessageBox.Show("Записть выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public override void FileRead(string path)
        {
            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage1.html");
            
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    MessageBox.Show(line);
                }
            }
        }


    }
}
