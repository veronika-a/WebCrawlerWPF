using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WebCrawlerWPF.Models
{
    public class Site : SComponent
    {
        private List<SComponent> components = new List<SComponent>();

        public Site()
        {
        }

        public Site(string name, string mainPage) : base(mainPage)
        {
            Name = name;
            MainPage = mainPage;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public string MainPage { get; set; }
        public virtual List<SPage> Pages { get; set; } = new List<SPage>();

        public override void Add(SComponent component)
        {
            if (components.Find(u => u.Link == (component.Link)) == null)
            components.Add(component);
        }
        public override void Add(List<SComponent> component)
        {
            components.AddRange(component);
        }

        public override void Remove(SComponent component)
        {
            components.Remove(component);
        }
       
        public virtual List<string> ShowLinks()
        {
            List<string> links = new List<string>();

            for (int i = 0; i < components.Count; i++)
            {
                //components.Add(components[i]);
               links.AddRange( components[i].ShowLinks());
            }
            return links;
        }
    }
}
