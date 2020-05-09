using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WebCrawlerWPF.FileSave;

namespace WebCrawlerWPF.FileSave
{
    public class Document
    {
        public int id;
        public List<string> items { get; set; } = new List<string>();
       
        public Document() { }

        public DocMemento SaveState()
        {
            return new DocMemento(items);
        }
        public void RestoreState(DocMemento memento)
        {
            items = new List<string>();
            items.AddRange(memento.Items);
        }
    }
}
