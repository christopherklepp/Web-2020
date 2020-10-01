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

    //Klasser for kunde, reise og bestilling
    public class Kunde
    {
        [Key]
        public int Kid { get; set; }
        public string fornavn { get; set; }
        public string etternavn { get; set; }
        public string epost { get; set; }
        public virtual List<Bestilling> Bestilling { get; set; }
    }
     public class Reise
    {
        [Key]
        public int Rid { get; set; }
        public string reiserFra { get; set; }
        public string reiserTil { get; set; }
        public double pris { get; set; }
    }

    public class Bestilling
    {
        [Key]
        public int Bid { get; set; }
        public DateTime tidspunkt { get; set; }
        [ForeignKey("ReiseRid")]
        public virtual Reise reiser { get; set; }
    }

    //Oppretter databasen
    public class BussContext : DbContext
    {
        public BussContext(DbContextOptions<BussContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Kunde> Kunder { get; set; }
        public DbSet<Reise> Reiser { get; set; }
        public DbSet<Bestilling> Bestillinger { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
