using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UVV_fintech.Db;

namespace UVV_fintech.Model
{
    public class Conta
    {
        public int ContaId { get; set; }
        public string NumeroConta { get; set; } = null!;

        public decimal Saldo { get; protected set; } = 0m;

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public bool Ativa { get; set; } = true;

        public List<Transacao> Transacoes { get; set; } = new();

        public Conta() { }

        protected Conta(Cliente cliente)
        {
            Cliente = cliente;
            ClienteId = cliente.ClienteId;
            NumeroConta = GerarNumeroConta();
            Saldo = 0m;
        }

        protected string GerarNumeroConta()
        {
            var rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        public bool SalvarConta()
        {
            using var db = new BancoDbContext();

            db.Attach(Cliente);
            db.Add(this);
            db.SaveChanges();

            return true;
        }

        public decimal GetSaldo() => Saldo;
        public virtual void Creditar(decimal valor)
        {
            if (valor <= 0) return;
            Saldo += valor;
        }

        public virtual bool Debitar(decimal valor)
        {
            if (valor <= 0) return false;
            if (Saldo < valor) return false;

            Saldo -= valor;
            return true;
        }

        public Conta? BuscarContaPeloNumero(string numeroConta)
        {
            using var db = new BancoDbContext();
            var conta = db.Contas
                .Include(c => c.Cliente)
                .FirstOrDefault(c => c.NumeroConta == numeroConta);
            return conta;
        }

        public virtual string GetTipoConta() => "Conta Genérica";

        public bool ExcluirConta(string numeroConta)
        {
            using var db = new BancoDbContext();

            var contaDeletar = BuscarContaPeloNumero(numeroConta);

            if (contaDeletar != null)
            {
                if (contaDeletar.Cliente.ExcluirCliente(contaDeletar.ClienteId)) {
                    db.Contas.Remove(contaDeletar);
                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}