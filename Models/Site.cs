﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerWPF.Models
{
    public class Site
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string Name { get; set; }
        public SPage MainPage { get; set; }
        public virtual List<SPage> Pages { get; set; } = new List<SPage>();
        
    }
}
