using Microsoft.EntityFrameworkCore;
using UVV_fintech.Db;
using Microsoft.Data.SqlClient;

namespace UVV_fintech.Model
{
    public class Conta
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

        public Conta() { }

        public Conta(Cliente cliente)
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
        public string TipoContaDescricao => GetTipoConta();

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

        /*public bool ExcluirConta(string numeroConta)
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
        }*/

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

            var cliente = new Cliente
            {
                ClienteId = leitor.GetInt32(leitor.GetOrdinal("ClienteId")),
                Nome = leitor.GetString(leitor.GetOrdinal("Nome")),
                Cpf = leitor.GetString(leitor.GetOrdinal("Cpf"))
            };

            string tipoConta = leitor.GetString(leitor.GetOrdinal("TipoConta"));

            Conta conta = tipoConta switch
            {
                "ContaCorrente" => new ContaCorrente(cliente),
                "ContaPoupanca" => new ContaPoupanca(cliente),
                _ => throw new Exception($"TipoConta inválido: {tipoConta}")
            };

            conta.ContaId = leitor.GetInt32(leitor.GetOrdinal("ContaId"));
            conta.NumeroConta = leitor.GetString(leitor.GetOrdinal("NumeroConta"));
            conta.DataAbertura = leitor.GetDateTime(leitor.GetOrdinal("DataAbertura"));
            conta.Ativa = leitor.GetBoolean(leitor.GetOrdinal("Ativa"));

            typeof(Conta).GetProperty("Saldo")!
                .SetValue(conta, leitor.GetDecimal(leitor.GetOrdinal("Saldo")));

            return conta;
        }
    }
}
