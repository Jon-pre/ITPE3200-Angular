using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


namespace ITPE3200_Angular.DAL
{
    public class DBinit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AksjeDB>();

                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var Askje1 = new Aksjer { navn = "TSLA", pris = 1000, prosent = 19 };
                var Askje2 = new Aksjer { navn = "GOOGL", pris = 100, prosent = -1 };
                var Askje3 = new Aksjer { navn = "AMAZN", pris = 231, prosent = -2 };
                var Askje4 = new Aksjer { navn = "NFLX", pris = 295, prosent = -3 };
                var Askje5 = new Aksjer { navn = "APPL", pris = 155, prosent = 13 };
                var Askje6 = new Aksjer { navn = "MSFT", pris = 145, prosent = 41 };
                var Askje7 = new Aksjer { navn = "XOM", pris = 99, prosent = 1 };
                var Askje8 = new Aksjer { navn = "JNJ", pris = 174, prosent = 17 };
                var Askje9 = new Aksjer { navn = "V", pris = 209, prosent = 15 };
                var Askje10 = new Aksjer { navn = "CVX", pris = 179, prosent = 11 };
                var Askje11 = new Aksjer { navn = "NVDA", pris = 138, prosent = 2 };
                var Askje12 = new Aksjer { navn = "LLY", pris = 359, prosent = 3 };

                var passord = "passord1";
                byte[] salt = AksjeRepository.lagSalt();
                byte[] hash = AksjeRepository.lagHash(passord, salt);
 
                var Konto1 = new Kontoer { kontonavn = "Petter", land = "Norge", kontobalanse = 100000, brukernavn="brukernavn", passord= hash, salt=salt };
                var passord2 = "Passord123";
                byte[] salt2 = AksjeRepository.lagSalt();
                byte[] hash2 = AksjeRepository.lagHash(passord2, salt2);

                var konto2 = new Kontoer { kontonavn = "Gunnar", land = "Norge", kontobalanse = 200000, brukernavn = "Gunnar", passord = hash2, salt = salt2 };
                context.Aksjer.Add(Askje1);
                context.Aksjer.Add(Askje2);
                context.Aksjer.Add(Askje3);
                context.Aksjer.Add(Askje4);
                context.Aksjer.Add(Askje5);
                context.Aksjer.Add(Askje6);
                context.Aksjer.Add(Askje7);
                context.Aksjer.Add(Askje8);
                context.Aksjer.Add(Askje9);
                context.Aksjer.Add(Askje10);
                context.Aksjer.Add(Askje11);
                context.Aksjer.Add(Askje12);

                context.Kontoer.Add(Konto1);
              //  context.Kontoer.Add(konto2);

                context.SaveChanges();

            }
        }
    }
}
