using UVV_fintech.Db;

namespace UVV_fintech.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }

        public string Nome { get; set; } = null!;
        public string Cpf { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Endereco { get; set; } = null!;

        // Relacionamento 1:1 – um cliente tem (no máximo) uma conta
        public Conta? Conta { get; set; }

        public Cliente() { }

        public Cliente(string nome, string cpf, string telefone, string endereco)
        {
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Endereco = endereco;
        }

        public bool ExcluirCliente(int clienteId)
        {
            using var db = new BancoDbContext();

            Conta? conta = db.Contas.Find(clienteId);

            if (conta == null) return false;

            db.Contas.Remove(conta);
            return true;
        }
    }
}
