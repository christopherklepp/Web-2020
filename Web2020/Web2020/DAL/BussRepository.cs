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
                        reiserTil = s.reiserTil,
                        pris = s.pris
                    }); ;
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
                epost = sisteBestilling.epost
            };

            foreach (var bestilling in sisteBestilling.Bestilling)
            {
                buss.tidspunkt = bestilling.tidspunkt;
                buss.reiserFra = bestilling.reiser.reiserFra;
                buss.reiserTil = bestilling.reiser.reiserTil;
            }
            return buss;
        }

            [HttpPost]
        public async Task<bool> SettInnData(Buss buss)

        {
            try
            {
                Reise funnetReise = await _db.Reiser.FirstOrDefaultAsync(r => r.reiserFra == buss.reiserFra & r.reiserTil == buss.reiserTil);
                Kunde funnetKunde = await _db.Kunder.FirstOrDefaultAsync(k => k.epost == buss.epost);
                var bestilling = new Bestilling
                {
                    tidspunkt = buss.tidspunkt,
                    reiser = funnetReise
                };
                if (funnetKunde == null)
                {
                    var kunde = new Kunde
                    {
                        fornavn = buss.fornavn,
                        etternavn = buss.etternavn,
                        epost = buss.epost
                    };


                    kunde.Bestilling = new List<Bestilling>();
                    kunde.Bestilling.Add(bestilling);
                    _db.Kunder.Add(kunde);
                    await _db.SaveChangesAsync();
                    return true;
                }
                else
                {
                    funnetKunde.Bestilling.Add(bestilling);
                    await _db.SaveChangesAsync();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
