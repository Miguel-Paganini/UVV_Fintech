using Microsoft.EntityFrameworkCore;
using UVV_fintech.Model;

namespace UVV_fintech.Db
{
    public class BancoDbContext : DbContext
    {
        DbSet<Cliente> Clientes { get; set; } = null!;
        DbSet<Conta> Contas { get; set; } = null!;
        DbSet<Transacao> Transacoes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UVV_FintechDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Conta)
                .WithOne(c => c.Cliente)
                .HasForeignKey<Conta>(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conta>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conta>()
                .Property(c => c.Saldo)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Transacao>()
                .Property(t => t.Valor)
                .HasColumnType("decimal(18,2)");

        }
    }
}
