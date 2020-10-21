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
            Assert.Equal("Bestilling lagret", resultat.Value);
<<<<<<< HEAD
        }

        [Fact]
        public async Task SettInnDataLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(b => b.SettInnData(It.IsAny<Buss>())).ReturnsAsync(false);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.SettInnData(It.IsAny<Buss>()) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Bestilling ikke lagret", resultat.Value);
        }

        [Fact]
        public async Task SlettReiseLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(b => b.SlettReise(It.IsAny<int>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.SlettReise(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reise slettet", resultat.Value);
        }

        [Fact]
        public async Task SlettReiseLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(b => b.SlettReise(It.IsAny<int>())).ReturnsAsync(false);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.SlettReise(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting av Kunden ble ikke utført", resultat.Value);
        }

        /*
        [Fact]
        public async Task HentEnReiseLoggetInnOK()
        {
            // Arrange
            var buss1 = new Buss
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

            mockRep.Setup(k => k.HentEnReise(It.IsAny<int>())).ReturnsAsync(buss1);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.HentEnReise(It.IsAny<int>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Kunde>(kunde1, (Kunde)resultat.Value);
        }*/

        [Fact]
        public async Task EndreLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.Endre(It.IsAny<Reise>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.Endre(It.IsAny<Reise>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reise endret", resultat.Value);
        }


        [Fact]
        public async Task EndreLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.Endre(It.IsAny<Reise>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.Endre(It.IsAny<Reise>()) as NotFoundObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endring av reise kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task LogInOK()
        {
            mockRep.Setup(k => k.Login(It.IsAny<Admin>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.Login(It.IsAny<Admin>()) as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public void LoggUt()
        {
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            bussController.LoggUt();

            // Assert
            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);
=======
>>>>>>> baf998324d6b71f5ea701e41df4064f6bc9a8a82
        }
    }
}
