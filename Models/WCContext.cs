namespace WebCrawlerWPF.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class WCContext : DbContext
    {
      
        public WCContext()
            : base("name=WCContext")
        {
        }

        public virtual DbSet<Site> Sites { get; set; }
        public virtual DbSet<SPage> SPages { get; set; }
    }

}