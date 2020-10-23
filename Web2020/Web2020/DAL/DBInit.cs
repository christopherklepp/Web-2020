using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Web2020.DAL;

namespace Web2020.Models
{
    //Sletter og oppretter databasen, initialiserer informasjon som skal være i databasen ved opprettelse
    public static class DBInit
    {

        public static void Initialize(IApplicationBuilder app)
        {

            using var serviceScope = app.ApplicationServices.CreateScope();
            {

                var db = serviceScope.ServiceProvider.GetService<BussContext>();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                var reise1 = new Reise();
                reise1.reiserFra = "Kristiansand";
                reise1.reiserTil = "Bergen";
                reise1.pris = 299;
                reise1.dag = "Mandag";
                reise1.tidspunkt = "11:00";
                db.Reiser.Add(reise1);


                var reise2 = new Reise();
                reise2.reiserFra = "Bergen";
                reise2.reiserTil = "Oslo";
                reise2.pris = 499;
                reise2.dag = "Tirsdag";
                reise2.tidspunkt = "14:00";
                db.Reiser.Add(reise2);

                var reise3 = new Reise();
                reise3.reiserFra = "Stavanger";
                reise3.reiserTil = "Trondheim";
                reise3.pris = 599;
                reise3.dag = "Torsdag";
                reise3.tidspunkt = "13:00";
                db.Reiser.Add(reise3);

                var reise4 = new Reise();
                reise4.reiserFra = "Oslo";
                reise4.reiserTil = "Bergen";
                reise4.pris = 599;
                reise4.dag = "Torsdag";
                reise4.tidspunkt = "11:50";
                db.Reiser.Add(reise4);

                var admin = new Adminer();
                admin.Brukernavn = "Admin";
                var passord = "Passord1";
                byte[] salt = BussRepository.LagSalt();
                byte[] hash = BussRepository.LagHash(passord, salt);
                admin.Passord = hash;
                admin.Salt = salt;
                db.Adminer.Add(admin);


                var innBuss = new Buss()
                {
                    Id = 1,
                    reiserFra = "Oslo",
                    reiserTil = "Bergen",
                    dag = "Mandag",
                    tidspunkt = "11:00",
                    fornavn = "Ola",
                    etternavn = "Kristiansen",
                    epost = "ola1999@gmail.com",
                    pris = 299
                };
                var Bestilling = new Bestilling()
                {
                    Bid = 1,
                    reiser = reise4
                };

                var Kunde = new Kunde()
                {
                    Kid = 1,
                    fornavn = innBuss.fornavn,
                    etternavn = innBuss.etternavn,
                    epost = innBuss.epost,
                };
                Kunde.Bestilling = new List<Bestilling>();
                Kunde.Bestilling.Add(Bestilling);
                db.Kunder.Add(Kunde);

                db.SaveChanges();

                

                
            }
        }
    }
}



