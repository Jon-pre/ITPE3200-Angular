using ITPE3200_Angular.Controllers;
using ITPE3200_Angular.DAL;
using ITPE3200_Angular.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace AksjeAppUnitTest
{
    public class AksjeTest
    {
        private const string _loggetInn = "loggetInn";
        private const string _ikkeLoggetInn = "";
        private readonly Mock<IAksjeRepo> mockRepo = new Mock<IAksjeRepo>();
        private readonly Mock<ILogger<AksjeController>> mockLogAksje = new Mock<ILogger<AksjeController>>();
        private readonly Mock<ILogger<KontoController>> mockLogKonto = new Mock<ILogger<KontoController>>();

        private readonly Mock<HttpContext> mockHttpContext = new Mock<HttpContext>();
        private readonly MockHttpSession mockSession = new MockHttpSession(); 

        [Fact]
        public async Task hentAlle()
        {
            //arrange
            var aksje1 = new Aksje
            {
                id = 1,
                navn = "Googl",
                prosent = 2,
                pris = 100
            };
            var aksje2 = new Aksje
            {
                id = 2,
                navn = "STSLA",
                prosent = 100,
                pris = 200
             };
            var aksje3 = new Aksje
            {
                id = 3,
                navn = "AMAZN",
                prosent = 13,
                pris = 131
            };

            var aksjeListe = new List<Aksje>();
            aksjeListe.Add(aksje1);
            aksjeListe.Add(aksje2);
            aksjeListe.Add(aksje3);

            mockRepo.Setup(k => k.hentAlle()).ReturnsAsync(aksjeListe);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            // act
            var resultat = await aksjeController.hentAlle() as OkObjectResult;

            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Aksje>>((List<Aksje>)resultat.Value, aksjeListe);  
        }

        [Fact]
        public async Task hentAlleIkkeLoggetInn()
        {
            //arrange

            
            mockRepo.Setup(k => k.hentAlle()).ReturnsAsync(It.IsAny<List<Aksje>>());

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //act
            var resultat = await aksjeController.hentAlle() as UnauthorizedObjectResult;

            //assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task slettAksjeLoggetInn()
        {
            //arrange
            mockRepo.Setup(a => a.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;

            //act
            var resultat = await aksjeController.Slett(It.IsAny<int>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Kunde slettet", resultat.Value);
        }
        [Fact]
        public async Task slettAksjeError()
        {
            //arrange
            mockRepo.Setup(a => a.Slett(It.IsAny<int>())).ReturnsAsync(false);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.Slett(It.IsAny<int>()) as NotFoundObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Sletting ble ikke utført", resultat.Value);
        }

        [Fact]
        public async Task SlettIkkeInnlogget()
        {
            //arrange
            mockRepo.Setup(a => a.Slett(It.IsAny<int>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.Slett(It.IsAny<int>()) as UnauthorizedObjectResult;
            // assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }


        [Fact]
        public async Task hentLoggetInn()
        {
            //arrange
            var aksje = new Aksje
            {
                id = 1,
                navn = "AMAZN",
                prosent = 10,
                pris = 111
            };
            mockRepo.Setup(a => a.hent(It.IsAny<int>())).ReturnsAsync(aksje);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.hent(It.IsAny<int>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<Aksje>(aksje,(Aksje)resultat.Value);
        }

        [Fact]
        public async Task hentError()
        {
            //arrange
            mockRepo.Setup(a => a.hent(It.IsAny<int>())).ReturnsAsync(() => null);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.hent(It.IsAny<int>()) as NotFoundObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Aksje ble ikke funnet", resultat.Value);
        }
        [Fact]
        public async Task hentIkkeloggetInn()
        {
            //arrange
            mockRepo.Setup(a => a.hent(It.IsAny<int>())).ReturnsAsync(() => null);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.hent(It.IsAny<int>()) as UnauthorizedObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        [Fact]
        public async Task KjopLoggetInn()
        {
            //arrange
            mockRepo.Setup(a => a.kjop(It.IsAny<Konto>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.kjop(It.IsAny<Konto>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal("Aksjen ble kjøp", resultat.Value);
        }
        [Fact]
        public async Task kjopLoggetInnError()
        {
            //arrange
            mockRepo.Setup(a => a.kjop(It.IsAny<Konto>())).ReturnsAsync(false);
            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.kjop(It.IsAny<Konto>()) as NotFoundObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Aksjen ble ikke kjøp", resultat.Value);
        }

        [Fact]
        public async Task kjopIkkeLoggetInn()
        {
            //arrange
            mockRepo.Setup(a => a.kjop(It.IsAny<Konto>())).ReturnsAsync(true);

            var aksjeController = new AksjeController(mockRepo.Object, mockLogAksje.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            aksjeController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await aksjeController.kjop(It.IsAny<Konto>()) as UnauthorizedObjectResult;


            //assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task LoggInnOk()
        {
            //arrange
            mockRepo.Setup(k => k.logInn(It.IsAny<Konto>())).ReturnsAsync(true);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.logInn(It.IsAny<Konto>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.True((bool)resultat.Value);
        }

        [Fact]
        public async Task LoggInnFeilPassordEllerBruker()
        {
            //arrange
            mockRepo.Setup(k => k.logInn(It.IsAny<Konto>())).ReturnsAsync(false);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.logInn(It.IsAny<Konto>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.False((bool)resultat.Value);
        }
        
        [Fact]
        public async Task hentAlleKontoerOK()
        {
            //arrange
            var konto1 = new Konto { id=  1, kontonavn="Petter",land = "Norge",kontobalanse= 100};
            var konto2 = new Konto { id = 1, kontonavn = "Gunnar", land = "Sverige", kontobalanse = 1001222};
            var konto3 = new Konto { id = 1, kontonavn = "Pedro", land = "Guatemala", kontobalanse = 1000002};

            var kontoListe = new List<Konto>();
            kontoListe.Add(konto1);
            kontoListe.Add(konto2);
            kontoListe.Add(konto3);

            mockRepo.Setup(k => k.hentAlleKontoer()).ReturnsAsync(kontoListe);
            //act
            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;

            var resultat = await kontoController.hentAlleKontoer() as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
            Assert.Equal<List<Konto>>((List<Konto>)resultat.Value, kontoListe);
        }

        [Fact]
        public async Task hentAlleKontoerIkkeLoggetInn()
        {
            //arrange
            mockRepo.Setup(k => k.hentAlleKontoer()).ReturnsAsync(It.IsAny<List<Konto>>());

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.hentAlleKontoer() as UnauthorizedObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }
        [Fact]
        public async Task EndreKontoLoginOK()
        {
            //arrange
            mockRepo.Setup(k => k.Endre(It.IsAny<Konto>())).ReturnsAsync(true);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.Endre(It.IsAny<Konto>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
        }

        [Fact]
        public async Task EndreLoggetInnError()
        {
            //arrange
            mockRepo.Setup(k => k.Endre(It.IsAny<Konto>())).ReturnsAsync(false);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.Endre(It.IsAny<Konto>()) as NotFoundObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Konto ble ikke endret - Feil", resultat.Value);
        }

        [Fact]
        public async Task EndreIkkeLoggetInn()
        {
            //arrange
            mockRepo.Setup(k => k.Endre(It.IsAny<Konto>())).ReturnsAsync(true);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.Endre(It.IsAny<Konto>()) as UnauthorizedObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);
        }

        [Fact]
        public async Task HentEnKontoLoggetInnOk()
        {
            //arrange
            var konto = new Konto
            {
                id = 1,
                kontonavn = "Petter",
                land = "Estonia",
                kontobalanse = 100000
            };

            mockRepo.Setup(k => k.hentKonto(It.IsAny<int>())).ReturnsAsync(konto);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.hentKonto(It.IsAny<int>()) as OkObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.OK, resultat.StatusCode);
        }
        [Fact]
        public async Task HentEnKontoError()
        {
            //arrange
            mockRepo.Setup(k => k.hentKonto(It.IsAny<int>())).ReturnsAsync(() => null);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _loggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.hentKonto(It.IsAny<int>()) as NotFoundObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.NotFound, resultat.StatusCode);
            Assert.Equal("Fant ikke konto", resultat.Value);
        }

        [Fact]
        public async Task HentKontoIkkeLoggetInn()
        {
            //arrange
            mockRepo.Setup(k => k.hentKonto(It.IsAny<int>())).ReturnsAsync(() => null);

            var kontoController = new KontoController(mockRepo.Object, mockLogKonto.Object);

            mockSession[_loggetInn] = _ikkeLoggetInn;
            mockHttpContext.Setup(s => s.Session).Returns(mockSession);
            kontoController.ControllerContext.HttpContext = mockHttpContext.Object;
            //act
            var resultat = await kontoController.hentKonto(It.IsAny<int>()) as UnauthorizedObjectResult;
            //assert
            Assert.Equal((int)HttpStatusCode.Unauthorized, resultat.StatusCode);
            Assert.Equal("Ikke logget inn", resultat.Value);

        }
    }
  
}
