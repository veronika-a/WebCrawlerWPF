using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WebCrawlerWPF.ViewModels
{
    public class MyFile:IMyFile
    {

        private void Sleep()
        {
            
                Thread.Sleep(5000);
            
        }
        public MyFile() {
            
           
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
        public void FileWrite(string text)
        {
            Sleep();
            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage2.html");
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Write(4.5);
                }
                MessageBox.Show("Записть выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FileRead(string path)
        {
            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage1.html");
            Sleep();
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
