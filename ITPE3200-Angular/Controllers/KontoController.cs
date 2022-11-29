using Castle.Core.Logging;
using ITPE3200_Angular.DAL;
using ITPE3200_Angular.Module;
using Microsoft.AspNetCore.Http;
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
        private const string _inlog = "loggetInn";


        private ILogger<KontoController> _logger;
        public KontoController(IAksjeRepo db, ILogger<KontoController> log)
        {
            _db = db;
            _logger = log;
        }


        [HttpGet]
        public async Task<ActionResult> hentAlleKontoer()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Konto> kontoer = await _db.hentAlleKontoer();
            _logger.LogInformation("Hentet ut kontoInformasjon");
            return Ok(kontoer);
        }
        [HttpPut]
        public async Task<ActionResult> Endre(Konto konto)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(konto);
                if (!returOK)
                {
                    _logger.LogInformation("Endret ikke på konto");
                    return NotFound("Konto ble ikke endret - Feil");
                }
                _logger.LogInformation("Endret på konto ");
                return Ok();
            }
            _logger.LogInformation("Feil i inputvalidering");
            return BadRequest();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> hentKonto(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                Konto konto = await _db.hentKonto(id);
                if (konto == null)
                {
                    _logger.LogInformation("Konto ble ikke hentet");
                    return NotFound("Fant ikke konto");
                }
                _logger.LogInformation("Hentet ut konto med id: " + id);
                return Ok(konto);
            }
            _logger.LogInformation("Feil i inputvalidering");
            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> logInn(Konto konto)
        {
            if (ModelState.IsValid)
            {
                bool returnOk = await _db.logInn(konto);
                if (!returnOk)
                {
                    _logger.LogInformation("Konto ble ikke logget inn");
                    HttpContext.Session.SetString(_inlog, "");
                    return Ok(false);
                }
                else
                {
                    HttpContext.Session.SetString(_inlog, "loggetInn");
                    _logger.LogInformation("konto med id ble logget inn");
                    return Ok(true);
                }
            }
            _logger.LogInformation("Konto ble ikke logget inn");
            return BadRequest();
        }
        /*[HttpGet]
        public async Task<ActionResult> hentId(Konto konto)
        {
            Konto enKonto = await _db.hentId(konto);
            if(konto == null)
            {
                return NotFound();
            }
            return Ok();
        }

        */




    }
}
