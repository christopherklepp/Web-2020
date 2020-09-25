using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2020.Models;

namespace Web2020.DAL
{
    public class BussRepository : IBussRepository
    {
        private readonly BussContext _db;
        public BussRepository (BussContext db)
        {
            _db = db;
        }


        //Returnerer en liste med alle steder fra databasen
        public async Task<List<Reise>> HentReiser()
        {
            try
            {
                var steder = await _db.Reiser.ToListAsync();
                var returnList = new List<Reise>();

                foreach (Reise s in steder)
                {
                    returnList.Add(new Reise
                    {
                        Rid = s.Rid,
                        reiserFra = s.reiserFra,
                        reiserTil = s.reiserTil
                    });
                }

                return returnList;

            }
            catch
            {
                return null;
            }
        }
            public async Task<Buss> SisteBestilling()
            {
           
            List<Kunde> alleKunder = await _db.Kunder.ToListAsync();
            Kunde sisteBestilling = alleKunder.Last();
            Buss buss = new Buss
            {
                fornavn = sisteBestilling.fornavn,
                etternavn = sisteBestilling.etternavn,
                adresse = sisteBestilling.adresse,
                telefonnr = sisteBestilling.telefonnr
            };

            DateTime tidspunkt;
            string reiserFra;
            string reiserTil;
            foreach (var bestilling in sisteBestilling.Bestilling)
            {
                buss.tidspunkt = bestilling.tidspunkt;
                buss.reiserFra = bestilling.reiser.reiserFra;
                buss.reiserTil = bestilling.reiser.reiserTil;
            }
            return buss;

            /*foreach (Kunde enKunde in alleKunder)
            {
                foreach (var best in enKunde.Bestilling)
                {  
                    var bestilling = new Buss
                    {   
                        fornavn = enKunde.fornavn,
                        etternavn = enKunde.etternavn,
                        adresse = enKunde.adresse,
                        telefonnr = enKunde.telefonnr,
                        reiserFra = best.reiser.reiserFra,
                        reiserTil = best.reiser.reiserTil,
                        tidspunkt = best.tidspunkt
                    };
                    alleBestillinger.Add(bestilling);
                }
            }
            return alleBestillinger;*/
        }

        [HttpPost]
        public async Task<bool> SettInnData(Buss buss)

        {
            try
            {
                Reise funnetReise = await _db.Reiser.FirstOrDefaultAsync(r => r.reiserFra == buss.reiserFra & r.reiserTil == buss.reiserTil);
                var kunde = new Kunde
                {
                    fornavn = buss.fornavn,
                    etternavn = buss.etternavn,
                    adresse = buss.adresse,
                    telefonnr = buss.telefonnr
                };
                var bestilling = new Bestilling
                {
                    tidspunkt = buss.tidspunkt,
                    reiser = funnetReise
                };

                kunde.Bestilling = new List<Bestilling>();
                kunde.Bestilling.Add(bestilling);
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
