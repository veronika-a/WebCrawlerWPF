using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.Models
{
     public class Statistics
    {
        public Statistics(Site site)
        {
            Site = site;
        }

        public Statistics(int id, User user)
        {
            Id = id;
            User = user;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public User User { get; set; } = new User("user","1111");
        public Site Site { get; set; }
        public int NumberOfVisits { get; set; } = 0;

    }
}
