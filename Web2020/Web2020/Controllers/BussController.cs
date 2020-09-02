using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2020.Models;

namespace Web2020.Controllers
{
    public class BussController
    {
        /*public class  : DbContext
        {
            public Context(DbContextOptions<> options) : base(options)
            {
                Database.EnsureCreated();
            }
            public DbSet<>  { get; set; }
            public DbSet<>  { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }*/

        /*public async Task<List<Buss>> hentAlle()
        {
            List<Kunde> alleKunder = await _db.Kunder.toListAsync();
            List<Buss> alleBestillinger = new List<Buss>();

            foreach(Kunde enKunde in alleKunder)
            {
                foreach(var best in enKunde.Bestillinger)
                {
                    var bestilling = new Buss
                    {
                        fornavn = enKunde.fornavn,
                        etternavn = enKunde.etternavn,
                        adresse = enKunde.adresse,
                        telefonnr = enKunde.telefonnr,
                        reiserFra = best.reiserFra,
                        reserTil = best.reiserTil,
                        tidspunkten = best.tidspunkt
                    };
                    alleBestillinger.Add(bestilling);
                }
            }
            return alleBestillinger;
        }*/

        /*public async Task<bool> settInnData(Buss buss)
        {
            try
            {
                var reise = new Reise
                {
                    reiserFra = buss.reserFra,
                    reiserTil = buss.reserTil,
                    tidspunkt = buss.tidspunkten
                };
                var kunde = new Kunde
                {
                    fornavn = buss.fornavn,
                    etternavn = buss.etternavn,
                    adresse = buss.adresse,
                    telefonnr = buss.telefonnr
                };
                kunde.Reiser = new List<Reiser>();
                kunde.Reiser.Add(reise);
                _db.Kunder.Add(kunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        } */
    }
}
