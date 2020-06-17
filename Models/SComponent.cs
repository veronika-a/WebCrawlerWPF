using System.Collections.Generic;

namespace WebCrawlerWPF.Models
{
    public abstract class SComponent
    {
        protected string Link;
        public SComponent()
        {
        }

        public SComponent(string link)
        {
            this.Link = link;
        }

        public virtual void Add(SComponent component) { }
        public virtual void Add(List<SComponent> component) { }

        public virtual void Remove(SComponent component) { }

        public virtual List<string> ShowLinks() {
            return new List<string>();
        }
    }
}
