using Microsoft.EntityFrameworkCore;
using UVV_fintech.Model;

namespace UVV_fintech.Db
{
    public class BancoDbContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Conta> Contas { get; set; } = null!;
        public DbSet<Transacao> Transacoes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\mssqllocaldb;Database=UVV_FintechDB;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Indice único para CPF
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Cpf)
                .IsUnique();

            // Cliente 1 -> Muitas Contas (uma de cada na logica)
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Contas)
                .WithOne(c => c.Cliente)
                .HasForeignKey(c => c.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            // Conta 1 -> Muitas Transacoes
            modelBuilder.Entity<Conta>()
                .HasMany(c => c.Transacoes)
                .WithOne(t => t.Conta)
                .HasForeignKey(t => t.ContaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Herança TPH para Conta
            modelBuilder.Entity<Conta>()
                .HasDiscriminator<string>("TipoConta")
                .HasValue<ContaCorrente>("ContaCorrente")
                .HasValue<ContaPoupanca>("ContaPoupanca");

            // Herança TPH para Transacao
            modelBuilder.Entity<Transacao>()
                .HasDiscriminator<string>("TipoTransacao")
                .HasValue<Depositar>("Deposito")
                .HasValue<Sacar>("Saque")
                .HasValue<Transferir>("Transferencia");

            modelBuilder.Entity<Transferir>()
                .HasOne(t => t.ContaDestino)
                .WithMany()
                .HasForeignKey(t => t.ContaDestinoId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}