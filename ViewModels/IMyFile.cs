using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.ViewModels
{
    public interface IMyFile
    {
        void FileWrite(string text);
        void FileRead(string path);
    }
}
