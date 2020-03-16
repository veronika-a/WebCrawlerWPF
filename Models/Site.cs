using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.Models
{
    public class Site
    {
        public string Name { get; set; }
        public SPage MainPage { get; set; }
        public List<SPage> Pages { get; set; } = new List<SPage>();
        
    }
}
