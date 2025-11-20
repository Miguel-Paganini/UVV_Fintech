using Microsoft.EntityFrameworkCore;
using UVV_fintech.Model;

namespace UVV_fintech.Db
{
    public class BancoDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Conta> Contas { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;

        // opcionais, se você quiser consultar por tipo direto:
        public DbSet<ContaCorrente> ContasCorrente { get; set; } = null!;
        public DbSet<ContaPoupanca> ContasPoupanca { get; set; } = null!;
        public DbSet<Depositar> Depositos { get; set; } = null!;
        public DbSet<Sacar> Saques { get; set; } = null!;
        public DbSet<Transferir> Transferencias { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=UVV_FintechDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CPF único (boa prática pra banco)
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Cpf)
                .IsUnique();

            // Cliente 1 <-> 1 Conta (independente de ser CC ou CP)
            modelBuilder.Entity<Cliente>()
                .HasOne(c => c.Conta)
                .WithOne(c => c.Cliente)
                .HasForeignKey<Conta>(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Conta 1 -> N Transacoes
            modelBuilder.Entity<Conta>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Herança de Conta (TPH)
            modelBuilder.Entity<Conta>()
                .HasDiscriminator<string>("TipoConta")
                .HasValue<ContaCorrente>("ContaCorrente")
                .HasValue<ContaPoupanca>("ContaPoupanca");

            // Herança de Transacao (TPH)
            modelBuilder.Entity<Transacao>()
                .HasDiscriminator<string>("TipoTransacao")
                .HasValue<Depositar>("Deposito")
                .HasValue<Sacar>("Saque")
                .HasValue<Transferir>("Transferencia");

            modelBuilder.Entity<Conta>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transferir>()
                .HasOne(t => t.ContaDestino)
                .WithMany()
                .HasForeignKey(t => t.ContaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
