using LeitorXML.Models;
using Microsoft.EntityFrameworkCore;

namespace LeitorXML.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
 
        }

        // Models;
        public DbSet<Pais>? Paises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pais>().HasData(
               new Pais { PaisId = 1, Codigo = "1", Nome = "Estados Unidos da América do Norte" },
               new Pais { PaisId = 2, Codigo = "54", Nome = "Argentina" }
            );
        }
    }
}
