using System;
using System.Threading.Tasks;
using Moq;
using Web2020.DAL;
using Web2020.Models;
using Xunit;

namespace Web2020Test
{
    public class BussController
    {
        [Fact]
        public async Task SettInnData()
        {
            //Arrange
            var innBuss = new Buss()
            {
                Id = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                avganger = "Mandag: kl 11:00",
                fornavn = "Ola",
                etternavn = "Kristiansen",
                epost = "ola1999@gmail.com",
                pris = 299
            };

            var mock = new Mock<IBussRepository>();
            mock.Setup(k => k.SettInnData(innBuss)).ReturnsAsync(true);
            var bussController = new BussController(mock.Object);

            //act
            bool resultat = await bussController.SettInnData(innBuss)
        }
    }
}
