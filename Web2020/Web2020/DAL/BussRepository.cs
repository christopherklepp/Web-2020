using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web2020.Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Logging;


namespace Web2020.DAL
{
    public class BussRepository : IBussRepository
    {
        private readonly BussContext _db;
        private ILogger<BussRepository> _log;
        public BussRepository (BussContext db, ILogger<BussRepository> log)
        {
            _db = db;
            _log = log;
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
                    });
                }

                return returnList;

            }
            catch
            {
                return null;
            }
        }


        //Lagrer bestilling til databasen, sjekker at bruker ikke alt er lagret i databasen
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

        //Henter informasjon om bestilling fra databasen
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

        public async Task<Reise> HentEnReise(int id)
        {
            try
            {
                Reise hentetReise = await _db.Reiser.FindAsync(id);
                return hentetReise;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Reise>> HentAlleReiser()
        {
            List<Reise> iDatabasenReiser = await _db.Reiser.ToListAsync();
            List<Reise> alleReiser = new List<Reise>();
            foreach (Reise enReise in iDatabasenReiser)
            {
                alleReiser.Add(enReise);
            }
            return alleReiser;
        }

        public async Task<bool> Endre(Reise endretReise)
        {
            try
            {
                var nyReise = await _db.Reiser.FindAsync(endretReise.Rid);

                nyReise.reiserFra = endretReise.reiserFra;
                nyReise.reiserTil = endretReise.reiserTil;
                nyReise.pris = endretReise.pris;
                await _db.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }


        public async Task<bool> SlettSted(int id)
        {
            try
            {
                    Reise slettSted = await _db.Reiser.FindAsync(id);
                    _db.Reiser.Remove(slettSted);
                    await _db.SaveChangesAsync();

                    return true;
                }
            
            catch
            {  
                return false;
            }
        }

        public static byte[] LagHash(string passord, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                                password: passord,
                                salt: salt,
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 1000,
                                numBytesRequested: 32);
        }

        public static byte[] LagSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csp.GetBytes(salt);
            return salt;
        }

        public async Task<bool> Login(Admin admin)
        {
            try
            {
                Adminer finnAdmin = await _db.Adminer.FirstOrDefaultAsync(b => b.Brukernavn == admin.Brukernavn);
              
                byte[] hash = LagHash(admin.Passord, finnAdmin.Salt);
                bool ok = hash.SequenceEqual(finnAdmin.Passord);
                if (ok)
                {
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                _log.LogInformation(e.Message);
                return false;
            }
        }
    }
}
