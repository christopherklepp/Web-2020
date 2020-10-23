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

            //Arrange
            
            mockRep.Setup(b => b.SettInnData(It.IsAny<Buss>())).ReturnsAsync(true);
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            //Act
            var resultat = await bussController.SettInnData(It.IsAny<Buss>()) as OkObjectResult;

            //Assert

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Bestilling lagret", resultat.Value);

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
        public async Task SettInnDataLoggetInnFeilModel()
        {
            // Arrange

            var innBuss = new Buss()
            {
                Id = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                dag = "Mandag",
                tidspunkt="13:00",
                fornavn = "Ola",
                etternavn = "",
                epost = "ola1999@gmail.com",
                pris = 299
            };

            mockRep.Setup(k => k.SettInnData(innBuss)).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            bussController.ModelState.AddModelError("etternavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var resultat = await bussController.SettInnData(innBuss) as BadRequestObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }


        [Fact]
        public async Task HentReiserLoggetInnOK()
        {
            // Arrange

            var reise1 = new Reise
            {
                Rid = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                pris = 299,
                dag = "Mandag",
                tidspunkt = "13:00"
            };
            var reise2 = new Reise
            {
                Rid = 2,
                reiserFra = "Bergen",
                reiserTil = "Oslo",
                pris = 399,
                dag = "Tirsdag",
                tidspunkt = "12:00"
            };
            var reise3 = new Reise
            {
                Rid = 3,
                reiserFra = "Trondheim",
                reiserTil = "Oslo",
                pris = 599,
                dag = "Onsdag",
                tidspunkt = "14:00"
            };

            var reiseListe = new List<Reise>();
            reiseListe.Add(reise1);
            reiseListe.Add(reise2);
            reiseListe.Add(reise3);

            mockRep.Setup(k => k.HentReiser()).ReturnsAsync(reiseListe);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.HentReiser() as OkObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Reise>>((List<Reise>)resultat.Value, reiseListe);
        }






        [Fact]
        public async Task HentReiserAdminIkkeLoggetInn()
        {
            // Arrange
  
            mockRep.Setup(k => k.HentReiserAdmin()).ReturnsAsync(It.IsAny<List<Reise>>());
            var bussController = new BussController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act

            var resultat = await bussController.HentReiserAdmin() as UnauthorizedObjectResult;
            // Assert 

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task HentReiserOK()
        {
            // Arrange

            var reise1 = new Reise
            {
                Rid = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                pris = 299,
                dag = "Mandag",
                tidspunkt = "13:00"
            };
            var reise2 = new Reise
            {
                Rid = 2,
                reiserFra = "Bergen",
                reiserTil = "Oslo",
                pris = 399,
                dag = "Tirsdag",
                tidspunkt = "12:00"
            };
            var reise3 = new Reise
            {
                Rid = 3,
                reiserFra = "Trondheim",
                reiserTil = "Oslo",
                pris = 599,
                dag = "Onsdag",
                tidspunkt = "14:00"
            };
            var reiseListe = new List<Reise>();
            reiseListe.Add(reise1);
            reiseListe.Add(reise2);
            reiseListe.Add(reise3);
            mockRep.Setup(k => k.HentReiser()).ReturnsAsync(reiseListe);
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            // Act

            var resultat = await bussController.HentReiser() as OkObjectResult;

            // Assert 

            Assert.Equal((List<Reise>)resultat.Value, reiseListe);
        }

        [Fact]
        public async Task HentReiserAdminLoggetInnOK()
        {
            // Arrange

            var reise1 = new Reise
            {
                Rid = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                pris = 299,
                dag = "Mandag",
                tidspunkt = "13:00"
            };
            var reise2 = new Reise
            {
                Rid = 2,
                reiserFra = "Bergen",
                reiserTil = "Oslo",
                pris = 399,
                dag = "Tirsdag",
                tidspunkt = "12:00"
            };
            var reise3 = new Reise
            {
                Rid = 3,
                reiserFra = "Trondheim",
                reiserTil = "Oslo",
                pris = 599,
                dag = "Onsdag",
                tidspunkt = "14:00"
            };
            var reiseListe = new List<Reise>();
            reiseListe.Add(reise1);
            reiseListe.Add(reise2);
            reiseListe.Add(reise3);
            mockRep.Setup(k => k.HentReiserAdmin()).ReturnsAsync(reiseListe);
            var bussController = new BussController(mockRep.Object, mockLog.Object);
            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.HentReiserAdmin() as OkObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal((List<Reise>)resultat.Value, reiseListe);
        }

        [Fact]
        public async Task LagreReiseLoggetInnFeilModel()
        {
            // Arrange
 
            var reise1 = new Reise
            {
                Rid = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                pris = 299,
                dag = "Mandag",
                tidspunkt = "13:00"
            };

            mockRep.Setup(k => k.LagreReise(reise1)).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            bussController.ModelState.AddModelError("dag", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.Endre(reise1) as BadRequestObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task LagreLoggetInnOK()
        {
            // Arrange

            mockRep.Setup(k => k.LagreReise(It.IsAny<Reise>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.LagreReise(It.IsAny<Reise>()) as OkObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Reise lagret", resultat.Value);
        }

        [Fact]
        public async Task LagreReiseLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.LagreReise(It.IsAny<Reise>())).ReturnsAsync(false);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.LagreReise(It.IsAny<Reise>()) as NotFoundObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Reise kunne ikke lagres", resultat.Value);
        }

        [Fact]
        public async Task LagreReiseIkkeLoggetInn()
        {
            mockRep.Setup(k => k.LagreReise(It.IsAny<Reise>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.LagreReise(It.IsAny<Reise>()) as UnauthorizedObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task EndreLoggetInnFeilModel()
        {
            // Arrange
 
            var reise1 = new Reise
            {
                Rid = 1,
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                pris = 299,
                dag = "Mandag",
                tidspunkt = "13:00"
            };

            mockRep.Setup(k => k.Endre(reise1)).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            bussController.ModelState.AddModelError("Fornavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.Endre(reise1) as BadRequestObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }


        [Fact]
        public async Task SlettReiseIkkeLoggetInn()
        {

            // Arrange

            mockRep.Setup(k => k.SlettReise(It.IsAny<int>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.SlettReise(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
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

        [Fact]
        public async Task HentEnReiseLoggetInnIkkeOK()
        {
            // Arrange

            mockRep.Setup(k => k.HentEnReise(It.IsAny<int>())).ReturnsAsync(() => null);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.HentEnReise(It.IsAny<int>()) as NotFoundObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke reisen", resultat.Value);
        }

        [Fact]
        public async Task HentEnReiseLoggetInnOK()
        {
            // Arrange

            var reise1 = new Reise
            {
                Rid = 3,
                reiserFra = "Trondheim",
                reiserTil = "Oslo",
                pris = 599,
                dag = "Onsdag",
                tidspunkt = "14:00"
            };

            mockRep.Setup(k => k.HentEnReise(It.IsAny<int>())).ReturnsAsync(reise1);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.HentEnReise(It.IsAny<int>()) as OkObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Reise>(reise1, (Reise)resultat.Value);
        }

        [Fact]
        public async Task HentEnReiseIkkeLoggetInn()
        {
            // Arrange

            mockRep.Setup(k => k.HentEnReise(It.IsAny<int>())).ReturnsAsync(() => null);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.HentEnReise(It.IsAny<int>()) as UnauthorizedObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

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

            mockRep.Setup(k => k.LagreReise(It.IsAny<Reise>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.Endre(It.IsAny<Reise>()) as NotFoundObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Endring kunne ikke utføres", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            // Arrange
            
            mockRep.Setup(k => k.Endre(It.IsAny<Reise>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.Endre(It.IsAny<Reise>()) as UnauthorizedObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }



        [Fact]
        public async Task LogInOK()
        {
            // Arrange
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
            // Arrange

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            mockSession[_loggetInn] = _loggetInn;
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            bussController.LoggUt();

            // Assert

            Assert.Equal(_ikkeLoggetInn, mockSession[_loggetInn]);

        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            // Arrange

            mockRep.Setup(k => k.Login(It.IsAny<Admin>())).ReturnsAsync(false);
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.Login(It.IsAny<Admin>()) as OkObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnInputFeil()
        {

            // Arrange

            mockRep.Setup(k => k.Login(It.IsAny<Admin>())).ReturnsAsync(true);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            bussController.ModelState.AddModelError("Brukernavn", "Feil i inputvalidering på server");

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.Login(It.IsAny<Admin>()) as BadRequestObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.BadRequest, resultat.StatusCode);
            Assert.Equal("Feil i inputvalidering på server", resultat.Value);
        }

        [Fact]
        public async Task ErIkkeLoggetInn()
        {

            // Arrange
       
            mockRep.Setup(k => k.Login(It.IsAny<Admin>())).ReturnsAsync(false);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act

            var resultat = await bussController.ErLoggetInn() as UnauthorizedObjectResult;

            // Assert 

            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task ErLoggetInn()
        {

            // Arrange

            mockRep.Setup(k => k.Login(It.IsAny<Admin>())).ReturnsAsync(true);
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            bussController.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
         
            var resultat = await bussController.ErLoggetInn() as OkObjectResult;

            // Assert 
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Logget inn", resultat.Value);
          
        }

        [Fact]
        public async Task SisteBestillingOK()
        {

            // Arrange

            var innBuss = new Buss()
            {
                reiserFra = "Oslo",
                reiserTil = "Bergen",
                pris = 599,
                dag = "Torsdag",
                tidspunkt = "11:50",
                fornavn = "Ola",
                etternavn = "Kristiansen",
                epost = "ola1999@gmail.com",
            };

            mockRep.Setup(k => k.SisteBestilling()).ReturnsAsync(innBuss);

            var bussController = new BussController(mockRep.Object, mockLog.Object);

            // Act

            var resultat = await bussController.SisteBestilling() as OkObjectResult;

            // Assert 

            Assert.Equal(innBuss, resultat.Value);
        }
        [Fact]
        public async Task SisteBestillingIkkeOK()
        {

            // Arrange

            mockRep.Setup(k => k.SisteBestilling()).ReturnsAsync(It.IsAny<Buss>);
            var bussController = new BussController(mockRep.Object, mockLog.Object);

            // Act
            var resultat = await bussController.SisteBestilling() as BadRequestObjectResult;

            // Assert 
            Assert.Equal("Fant ikke siste bestilling", resultat.Value);
        }
    }
}
