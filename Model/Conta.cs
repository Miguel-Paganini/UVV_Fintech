using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Windows;
using UVV_fintech.Db;

namespace UVV_fintech.Model
{
    public class Conta
    {
        public int ContaId { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; } = 0;
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Conta() { }
        public Conta(Cliente cliente) { 
            NumeroConta= GerarNumeroConta();
            Saldo = 0;
            Cliente = cliente;
        }

        public string GerarNumeroConta()
        {
            var rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        public bool CadastrarConta(Conta conta)
        {
            using var db = new BancoDbContext();

            conta.ClienteId = conta.Cliente.ClienteId;

            db.Attach(conta.Cliente);
            db.Add(conta);
            db.SaveChanges();

            return true;
        }

        public Conta? BuscarContaPeloNumero(string numeroConta)
        {
            using var db = new BancoDbContext();
            var conta = db.Contas
                .Include(c => (c as ContaCorrente).Cliente)
                .Include(c => (c as ContaPoupanca).Cliente)
                .FirstOrDefault(c => c.NumeroConta == numeroConta);
            return conta;
        }
    }
}
