using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebCrawlerWPF.FileSave
{
    class MyFileSaveDoc: FileSave
    {
        public override void FileWrite(Document document)
        {
            List<string> text = document.items;
            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\DocPage2.txt");
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    foreach (string st in text)
                    {
                        sw.WriteLine();
                        sw.WriteLine(st);
                        sw.WriteLine();
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

            string writePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Files\\DocPage1.doc");

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
