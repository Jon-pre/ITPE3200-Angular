using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ITPE3200_Angular.DAL
{

    public class Aksjer
    {
        public int id { get; set; }
        public string navn { get; set; }
        public int pris { get; set; }
        public int prosent { get; set; }
    }

    public class Kontoer
    {
        public int id { get; set; }
        public string kontonavn { get; set; }
        public string land { get; set; }
        public int kontobalanse { get; set; }
        public string brukernavn { get; set; }
        public byte[] passord { get; set; }
        public byte[] salt { get; set; }
    }




    public class AksjeDB : DbContext
    {
        public AksjeDB(DbContextOptions<AksjeDB> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Aksjer> Aksjer { get; set; }
        public DbSet<Kontoer> Kontoer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
