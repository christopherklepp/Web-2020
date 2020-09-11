
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using Web2020.Models;


namespace Web2020.Controllers
{
    [Route("[controller]/[action]")]

    public class BussController :ControllerBase
    {
        private readonly BussContext _db;
        public BussController(BussContext db)
        {
            _db = db;
        }

        public async Task<List<Buss>> HentAlle()
        {
            List<Kunde> alleKunder = await _db.Kunder.ToListAsync();
            List<Buss> alleBestillinger = new List<Buss>();

            foreach(Kunde enKunde in alleKunder)
            {
                foreach(var best in enKunde.Reiser)
                {
                    var bestilling = new Buss
                    {
                        fornavn = enKunde.fornavn,
                        etternavn = enKunde.etternavn,
                        adresse = enKunde.adresse,
                        telefonnr = enKunde.telefonnr,
                        reiserFra = best.reiserFra,
                        reiserTil = best.reiserTil,
                        tidspunkt = best.tidspunkt
                    };
                    alleBestillinger.Add(bestilling);
                }
            }
            return alleBestillinger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<bool> SettInnData(Buss buss)

        {
            try
            {
                DateTime dt = new DateTime(2010, 08, 07);
                var reise = new Reise
                {
                    reiserFra = buss.reiserFra,
                    reiserTil = buss.reiserTil,
                    tidspunkt = dt
                };
                var kunde = new Kunde
                {
                    fornavn = buss.fornavn,
                    etternavn = buss.etternavn,
                    adresse = buss.adresse,
                    telefonnr = buss.telefonnr
                };
                kunde.Reiser = new List<Reise>();
                kunde.Reiser.Add(reise);
                _db.Kunder.Add(kunde);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
