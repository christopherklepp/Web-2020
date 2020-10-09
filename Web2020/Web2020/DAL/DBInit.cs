using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Web2020.DAL;

namespace Web2020.Models
{
    public static class DBInit
    {

        public static void Initialize(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.CreateScope();

            var db = serviceScope.ServiceProvider.GetService<BussContext>();

            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();


            var admin = new Adminer();
            admin.Brukernavn = "Admin";
            var passord = "Test11";
            byte[] salt = BussRepository.LagSalt();
            byte[] hash = BussRepository.LagHash(passord, salt);
            admin.Passord = hash;
            admin.Salt = salt;
            db.Adminer.Add(admin);

            db.SaveChanges();
        }
    }
}


