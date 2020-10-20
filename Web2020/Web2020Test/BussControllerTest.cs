using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Web2020.Controllers;
using Web2020.DAL;
using Web2020.Models;
using Xunit;

namespace Web2020Test
{
    public class BussControllerTest
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
            var loggerMock = new Mock<ILogger<BussController>>();
            mock.Setup(k => k.SettInnData(innBuss)).ReturnsAsync(true);
            var bussController = new BussController(mock.Object, loggerMock.Object);


            //act
            ActionResult returOK = await bussController.SettInnData(innBuss);

            //assert
            //Assert.True(returOK);
        }
    }
}
