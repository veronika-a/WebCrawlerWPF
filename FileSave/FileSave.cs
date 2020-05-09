using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.FileSave
{
    abstract public class FileSave
    {
        public virtual void FileWrite(Document document)
        {

        }
        public virtual void FileRead(string path)
        {

        }
        
    }
}
