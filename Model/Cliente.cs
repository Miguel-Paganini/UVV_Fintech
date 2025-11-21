using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
        public ICollection<Conta> Contas { get; set; } = new List<Conta>();

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

        public Cliente? BuscarClientePeloCpf(string cpf)
        {
            using var db = new BancoDbContext();
            return db.Clientes
                .Include(c => c.Contas)
                .FirstOrDefault(c => c.Cpf == cpf);
        }
        public Cliente? BuscarClientePeloId(int clienteId)
        {
            using var db = new BancoDbContext();
            return db.Clientes
                .Include(c => c.Contas)
                .FirstOrDefault(c => c.ClienteId == clienteId);
        }

        public List<Cliente> ListarClientes()
        {
            using var db = new BancoDbContext();

            return db.Clientes
                     .Include(c => c.Contas)
                     .OrderBy(c => c.Nome)
                     .ToList();
        }

        public bool ExcluirCliente(int clienteId)
        {
            using var db = new BancoDbContext();

            Conta? conta = db.Contas.Find(clienteId);

            if (conta == null) return false;

            db.Contas.Remove(conta);
            return true;
        }


        public bool AindaTemConta(int clienteId)
        {
            using var db = new BancoDbContext();
            return db.Contas.Any(c => c.ClienteId == clienteId);
        }
    }
}