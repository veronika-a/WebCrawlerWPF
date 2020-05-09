using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCrawlerWPF.Models;

namespace WebCrawlerWPF.FileSave
{
    public class DocMemento
    {
        public List<string> Items { get; private set; }
        public DocMemento(List<string> items)
        {
            Items= new List<string>();
            Items.AddRange(items);
        }

    }
}
