using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.Models
{
    public class SPage
    {
        public SPage()
        {
        }

        public SPage(string pagelink)
        {
            PageLink = pagelink;
        }

        public SPage(int id, string title, string pageLink)
        {
            Id = id;
            Title = title;
           PageLink = pageLink;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PageLink { get; set; }
        
        public  List<string> Links { get; set; } = new List<string>();
        //public List<string> Advertising { get; set; } = new List<string>();
    }
}
