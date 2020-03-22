using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.ViewModels
{
    public class File
    {
        public File() {
            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage1.html");
           // string text = "Привет мир!\nПока мир...";
          //  FileWrite(text);
           FileRead(writePath);

        }
        public File(string text)
        {
           // string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\HTMLPage1.html");
          //  string text = "Привет мир!\nПока мир...";
            FileWrite(text);
            //FileRead(writePath);

        }
        public void FileWrite(string text)
        {
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
                Console.WriteLine("Запись выполнена");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void FileRead(string path)
        {
            using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }


    }
}
