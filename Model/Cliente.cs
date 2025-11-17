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
        public Conta Conta { get; set; }

        public Cliente() { }

        public bool CadastrarCliente(Cliente cliente)
        {
            using var db = new BancoDbContext();

            // Verificar CPF existente
            if (db.Clientes.Any(c => c.Cpf == cliente.Cpf))
                return false;

            db.Add(cliente);
            db.SaveChanges();
            return true;
        }
    }
}
