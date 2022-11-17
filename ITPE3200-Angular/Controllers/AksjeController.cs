using ITPE3200_Angular.DAL;
using ITPE3200_Angular.Module;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITPE3200_Angular.Controllers
{
    
        [Route("api/[controller]")] 
        public class AksjeController
        {
            private readonly IAksjeRepo _db;

            public AksjeController(IAksjeRepo db)
            {
                _db = db;
            }

            public async Task<List<Aksje>> hentAlle()
            {
                return await _db.hentAlle();
            }

            public async Task<List<Konto>> hentAlleKontoer()
            {
                return await _db.hentAlleKontoer();
            }

            public async Task<Aksje> hent(int id)
            {
                return await _db.hent(id);
            }

            public async Task<bool> kjop(Konto konto)
            {
                return await _db.kjop(konto);
            }

            public async Task<bool> Endre(Konto konto)
            {
                return await _db.Endre(konto);
            }

            public async Task<Konto> hentKonto(int id)
            {
                return await _db.hentKonto(id);
            }
            public async Task<bool> Slett(int id)
            {
                return await _db.Slett(id);
            }
        }
    }

