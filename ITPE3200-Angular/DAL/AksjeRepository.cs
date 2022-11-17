using ITPE3200_Angular.Module;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ITPE3200_Angular.DAL
{
    public class AksjeRepository : IAksjeRepo
    {
        private readonly AksjeDB _db;

        public AksjeRepository(AksjeDB db)
        {
            _db = db;
        }


        public async Task<List<Aksje>> hentAlle()
        {
            try
            {
                List<Aksje> alleAksjer = await _db.Aksjer.Select(k => new Aksje
                {
                    id = k.id,
                    navn = k.navn,
                    pris = k.pris,
                    prosent = k.prosent
                }).ToListAsync();
                return alleAksjer;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<Konto>> hentAlleKontoer()
        {
            try
            {
                List<Konto> alleKontoer = await _db.Kontoer.Select(k => new Konto
                {
                    id = k.id,
                    navn = k.navn,
                    land = k.land,
                    kontobalanse = k.kontobalanse
                }).ToListAsync();
                return alleKontoer;
            }
            catch
            {
                return null;
            }
        }

        public async Task<Aksje> hent(int id)
        {
            Aksjer enAksje = await _db.Aksjer.FindAsync(id);
            var hentetAksje = new Aksje()
            {
                id = enAksje.id,
                navn = enAksje.navn,
                pris = enAksje.pris,
                prosent = enAksje.prosent
            };
            return hentetAksje;
        }

        public async Task<bool> kjop(Konto konto)
        {
            try
            {
                var endreKonto = await _db.Kontoer.FindAsync(konto.id);
                endreKonto.navn = konto.navn;
                endreKonto.land = konto.land;
                endreKonto.kontobalanse = konto.kontobalanse;
                await _db.SaveChangesAsync();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
        public async Task<Konto> hentKonto(int id)
        {
            Kontoer enKonto = await _db.Kontoer.FindAsync(id);
            var hentetKonto = new Konto()
            {
                id = enKonto.id,
                navn = enKonto.navn,
                land = enKonto.land,
            };
            return hentetKonto;
        }
        public async Task<bool> Slett(int id)
        {
            try
            {
                Aksjer enAksje = await _db.Aksjer.FindAsync(id);
                _db.Aksjer.Remove(enAksje);
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }



        public async Task<bool> Endre(Konto konto)
        {
            try
            {
                var konto1 = await _db.Kontoer.FindAsync(konto.id);
                konto1.navn = konto.navn;
                konto1.land = konto.land;
                await _db.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
