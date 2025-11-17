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

        public string GerarNumeroConta()
        {
            var rand = new Random();
            return rand.Next(100000, 999999).ToString();
        }

        public bool CriarConta(Conta conta, Cliente cliente)
        {
            using var db = new BancoDbContext();

            // Gerar numero de conta até eles ser único
            string numeroConta;
            do
            {
                numeroConta = GerarNumeroConta();
            }
            while (db.Contas.Any(c => c.NumeroConta == numeroConta));

            cliente.Conta = conta;
            if (!Cliente.CadastrarCliente(cliente))
                return false;

            db.Add(conta);
            db.SaveChanges();

            return true;
        }
    }
}
