using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using UVV_fintech.Db;
using Microsoft.Data.SqlClient;

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
            string connectionString =
                "Server=(localdb)\\mssqllocaldb;Database=UVV_FintechDB;Trusted_Connection=True;";

            using var conexao = new SqlConnection(connectionString);
            conexao.Open();

            var comando = conexao.CreateCommand();

            comando.CommandText = @"
                SELECT 
                    c.ContaId,
                    c.NumeroConta,
                    c.Saldo,
                    c.DataAbertura,
                    c.Ativa,
                    c.ClienteId,
                    c.TipoConta,
                    cli.Nome,
                    cli.Cpf
                FROM Contas c
                INNER JOIN Clientes cli ON cli.ClienteId = c.ClienteId
                WHERE c.NumeroConta = @numeroConta";

            comando.Parameters.Add(new SqlParameter("@numeroConta", numeroConta));

            using var leitor = comando.ExecuteReader();

            if (!leitor.Read())
                return null;

            // Cliente
            var cliente = new Cliente
            {
                ClienteId = leitor.GetInt32(leitor.GetOrdinal("ClienteId")),
                Nome = leitor.GetString(leitor.GetOrdinal("Nome")),
                Cpf = leitor.GetString(leitor.GetOrdinal("Cpf"))
            };

            // Descobre o tipo da conta
            string tipoConta = leitor.GetString(leitor.GetOrdinal("TipoConta"));

            Conta conta = tipoConta switch
            {
                "ContaCorrente" => new ContaCorrente(cliente),
                "ContaPoupanca" => new ContaPoupanca(cliente),
                _ => throw new Exception($"TipoConta inválido: {tipoConta}")
            };

            // Preenche dados comuns
            conta.ContaId = leitor.GetInt32(leitor.GetOrdinal("ContaId"));
            conta.NumeroConta = leitor.GetString(leitor.GetOrdinal("NumeroConta"));
            conta.DataAbertura = leitor.GetDateTime(leitor.GetOrdinal("DataAbertura"));
            conta.Ativa = leitor.GetBoolean(leitor.GetOrdinal("Ativa"));

            // Saldo precisa de setter interno se for protected
            typeof(Conta).GetProperty("Saldo")!
                .SetValue(conta, leitor.GetDecimal(leitor.GetOrdinal("Saldo")));

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