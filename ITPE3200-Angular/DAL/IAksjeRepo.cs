using ITPE3200_Angular.Module;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITPE3200_Angular.DAL
{
    public interface IAksjeRepo
    {
        Task<List<Aksje>> hentAlle();
        Task<Aksje> hent(int id);
        Task<List<Konto>> hentAlleKontoer();
        Task<bool> kjop(Konto konto);
        Task<bool> Endre(Konto konto);
        Task<Konto> hentKonto(int id);
        Task<bool> Slett(int id);

    }
}
