using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.FileSave
{
    class DocHistory
    {
        public Stack<DocMemento> History { get; private set; }

        public DocHistory()
        {
            History = new Stack < DocMemento >();
        }

    }
}
