using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Microsoft.EntityFrameworkCore;


namespace Web2020.Models
{
    class Busser
    {
        public int Id { get; set; }
        public String reiserFra { get; set; }
        public String reiserTil { get; set; }
        public String tidspunkten { get; set; }
        public String fornavn { get; set; }
        public String etternavn { get; set; }
        public String adresse { get; set; }
        public String telefonnr { get; set; }
    }


    public class BussContext : DbContext
    {
        public BussContext(DbContextOptions<BussContext> options)
         : base(options)
        {
            Database.EnsureCreated();
        }
    
        public DbSet<Buss> Busser { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
