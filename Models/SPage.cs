using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebCrawlerWPF.Models
{
    public class SPage: SComponent
    {
        public SPage()
        {
        }

        public SPage(string pagelink):base(pagelink)
        {
            PageLink = pagelink;
        }

        public SPage(int id, string name, string pageLink):base(name)
        {
            Id = id;
            Title = name;
           PageLink = pageLink;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Title { get; set; }
        public string PageLink { get; set; }

        private List<SComponent> components = new List<SComponent>();
        public List<string> Links { get; set; } = new List<string>();
        //public List<string> Advertising { get; set; } = new List<string>();
        public override List<string> ShowLinks()
        {
            return Links;
        }
        public override void Add(List<SComponent> component)
        {
            components.AddRange(component);
        }
        

    }
}
