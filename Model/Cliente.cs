using UVV_fintech.Db;

namespace UVV_fintech.Model
{
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public ContaPoupanca ContaPoupanca { get; set; }
        public ContaCorrente ContaCorrente { get; set; }

        public Cliente() { }
        public Cliente(string nome, string cpf, string telefone, string endereco)
        {
            Nome = nome;
            Cpf = cpf;
            Telefone = telefone;
            Endereco = endereco;
        }

        public bool CadastrarCliente(Cliente cliente)
        {
            using var db = new BancoDbContext();

            // Verificar CPF existente
            if (cliente.VerificarClienteExistente(cliente.Cpf))
                return false;

            db.Add(cliente);
            db.SaveChanges();
            return true;
        }

        public bool VerificarClienteExistente(string cpf)
        {
            using var db = new BancoDbContext();
            return db.Clientes.Any(c => c.Cpf == cpf);
        }
    }
}
