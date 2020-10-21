using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";

        private readonly Mock<IBussRepository> mockRep = new Mock<IBussRepository>();
        private readonly Mock<ILogger<BussController>> mockLog = new Mock<ILogger<BussController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession();

        [Fact]
        public async Task SettInnDataLoggetInnOK()
        {
            /*
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
            };*/

            //Arrange
            
            mockRep.Setup(b => b.SettInnData(It.IsAny<Buss>())).ReturnsAsync(true);
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;


            //act
            var resultat = await bussController.SettInnData(It.IsAny<Buss>()) as OkObjectResult;

            //assert

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestilling lagretttt", resultat.Value);
        }
    }
}
