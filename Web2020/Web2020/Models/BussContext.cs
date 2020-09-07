using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;


namespace Web2020.Models
{
    public class Kunde
    {
        public String fornavn { get; set; }
        public String etternavn { get; set; }
        public String adresse { get; set; }
        public String telefonnr { get; set; }
        public virtual List<Reise> Reiser { get; set; }
    }
     public class Reise
    {
        public int Id { get; set; }
        public String reiserFra { get; set; }
        public String reiserTil { get; set; }
        public String tidspunkt { get; set; }
    }
   
    public class BussContext : DbContext
    {
        public BussContext(DbContextOptions<BussContext> options)
         : base(options)
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
