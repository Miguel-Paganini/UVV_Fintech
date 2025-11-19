using Microsoft.EntityFrameworkCore;
using UVV_fintech.Model;

namespace UVV_fintech.Db
{
    public class BancoDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Conta> Contas { get; set; } = null!;
        public DbSet<ContaCorrente> ContasCorrente { get; set; } = null!;
        public DbSet<ContaPoupanca> ContasPoupanca { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=UVV_FintechDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.ContaPoupanca)
                .WithOne(c => c.Cliente)
                .HasForeignKey<ContaPoupanca>(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.ContaCorrente)
                .WithOne(c => c.Cliente)
                .HasForeignKey<ContaCorrente>(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conta>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Conta>()
                .HasDiscriminator<string>("TipoConta")
                .HasValue<Conta>("Conta")
                .HasValue<ContaCorrente>("ContaCorrente")
                .HasValue<ContaPoupanca>("ContaPoupanca");

        }
    }
}
