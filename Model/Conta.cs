using System;
using System.Collections.Generic;

namespace UVV_fintech.Model
{
    public abstract class Conta
    {
        public int ContaId { get; set; }
        public string NumeroConta { get; set; } = null!;

        // EF precisa enxergar o saldo → public com set protegido/privado
        public decimal Saldo { get; protected set; } = 0m;

        // Relacionamento 1:1 com Cliente
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; } = null!;

        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public bool Ativa { get; set; } = true;

        // 1 Conta -> N Transações
        public List<Transacao> Transacoes { get; set; } = new();

        protected Conta() { }

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

        public decimal GetSaldo() => Saldo;

        public string GetTipoConta()
        {
            if (this is ContaCorrente) return "Conta Corrente";
            if (this is ContaPoupanca) return "Conta Poupança";
            return "Conta";
        }

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
    }
}
