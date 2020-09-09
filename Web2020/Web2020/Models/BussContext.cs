using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Web2020.Models
{
    public class Kunde
    {
        [Key]
        public int Kid { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string adresse { get; set; }
        public string telefonnr { get; set; }
        public virtual List<Reise> Reiser { get; set; }
    }
     public class Reise
    {
        [Key]
        public int Rid { get; set; }
        public string reiserFra { get; set; }
        public string reiserTil { get; set; }
        public string tidspunkt { get; set; }
    }
   
    public class BussContext : DbContext
    {
        /*
        public BussContext(DbContextOptions<BussContext> options)
         : base(options)
        {
            Database.EnsureCreated();
        }*/
        public BussContext(DbContextOptions<BussContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Reise> Reiser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
