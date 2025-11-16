using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVV_fintech.Model;

namespace UVV_fintech.Db
{
    internal class UvvFintechDbContext : DbContext
    {
        DbSet<Cliente> Clientes { get; set; } = null;
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=DbUvvFintech;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);

            // Configurar relação de conta com cliente??? 1x1 1xn
            modelBuilder.Entity<Cliente>()
                .HasAlternateKey(c => c.CPF);

        }
    }
}
