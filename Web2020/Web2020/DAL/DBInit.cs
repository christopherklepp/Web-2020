using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Web2020.DAL;

namespace Web2020.Models
{
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
                reise1.reiserFra = "Oslo";
                reise1.reiserTil = "Bergen";
                reise1.pris = 299;
                reise1.avganger = "Mandag: kl 11:00";
                db.Reiser.Add(reise1);
              

                var reise2 = new Reise();
                reise2.reiserFra = "Bergen";
                reise2.reiserTil = "Oslo";
                reise2.pris = 499;
                reise2.avganger = "Tirsdag: kl 14:00 ";
                db.Reiser.Add(reise2);

                var reise3 = new Reise();
                reise3.reiserFra = "Oslo";
                reise3.reiserTil = "Trondheim";
                reise3.pris = 599;
                reise3.avganger = "Torsdag: kl 13:00";
                db.Reiser.Add(reise3);

                var admin = new Adminer();
                admin.Brukernavn = "Admin";
                var passord = "Passord1";
                byte[] salt = BussRepository.LagSalt();
                byte[] hash = BussRepository.LagHash(passord, salt);
                admin.Passord = hash;
                admin.Salt = salt;
                db.Adminer.Add(admin);
                db.SaveChanges();


            }
        }
    }
}



