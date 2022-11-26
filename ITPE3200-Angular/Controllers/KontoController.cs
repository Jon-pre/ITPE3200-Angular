using Castle.Core.Logging;
using ITPE3200_Angular.DAL;
using ITPE3200_Angular.Module;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITPE3200_Angular.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class KontoController : ControllerBase
    {
        private readonly IAksjeRepo _db;
        private ILogger<KontoController> _logger;

        public KontoController(IAksjeRepo db, ILogger<KontoController> log)
        {
            _db = db;
            _logger = log;
        }


        [HttpGet]
        public async Task<ActionResult> hentAlleKontoer()
        {
            _logger.LogInformation("Hentet ut kontoInformasjon");
            List<Konto> kontoer = await _db.hentAlleKontoer();
            return Ok(kontoer);
        }
        [HttpPut]
        public async Task<ActionResult> Endre(Konto konto)
        {
            bool returOK = await _db.Endre(konto);
            if (!returOK)
            {
                _logger.LogInformation("Endret ikke på konto: " + konto.navn + " " + konto.land);
                return NotFound("Konto ble ikke endret - Feil");
            }
            _logger.LogInformation("Endret på konto " + konto.navn + " " + konto.land);
            return Ok(returOK);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> hentKonto(int id)
        {
            Konto konto = await _db.hentKonto(id);
            if(konto == null)
            {
                _logger.LogInformation("Konto med id: " + id + " Ble ikke hentet");
            }
            _logger.LogInformation("Hentet ut konto med id: " + id);
            return Ok(konto);
        }

    }
}
