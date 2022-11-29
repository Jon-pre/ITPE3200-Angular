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
        public class AksjeController : ControllerBase
        {
            private readonly IAksjeRepo _db;

            private ILogger<AksjeController> _logger;
            private const string _inlog = "loggetInn";


        public AksjeController(IAksjeRepo db, ILogger<AksjeController> log)
            {
                _db = db;
                _logger = log;
            }

            [HttpGet]
            public async Task<ActionResult> hentAlle()
            {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
            List<Aksje> alleAksjer = await _db.hentAlle();
            _logger.LogInformation("Aksjer blir listet ut");
            return Ok(alleAksjer);
                
            }
            [HttpGet("{id}")]
            public async Task<ActionResult> hent(int id)
            {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
            Aksje aksje = await _db.hent(id);
            if(aksje == null)
            {
                return NotFound("Aksje ble ikke funnet");
            }
            _logger.LogInformation("Henter ut aksje med id" + id);
                return Ok(aksje);
            }
            
            public async Task<ActionResult> kjop(Konto konto)
            {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
                bool returOK = await _db.kjop(konto);
            if (!returOK)
            {
                _logger.LogInformation("Aksjen ble ikke kjøpt");
                return NotFound("Aksjen ble ikke kjøp");
            }
            //_logger.LogInformation("Kjøp av aksje av konto: " + konto.id + " " + konto.navn);
            _logger.LogInformation("Aksjen ble ikke kjøp");
            return Ok("Aksjen ble kjøp");
            }

            [HttpDelete("{id}")]
            public async Task<ActionResult> Slett(int id)
            {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString(_inlog)))
            {
                return Unauthorized("Ikke logget inn");
            }
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _logger.LogInformation("Aksje med id "+id+" ble ikke slettet");
                return NotFound("Sletting ble ikke utført");
            }

            _logger.LogInformation("Sletter aksje " + id);
            return Ok("Kunde slettet");
            }
        }
}





