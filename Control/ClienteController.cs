using Microsoft.EntityFrameworkCore;
using UVV_fintech.Db;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ClienteController
    {
        private readonly TransacaoController _transacaoController = new();

        /// <summary>
        /// Cadastra um novo cliente.
        /// Retorna o cliente criado ou null se o CPF já existir.
        /// </summary>
        public Cliente? CadastrarCliente(string nome, string cpf, string telefone, string endereco)
        {
            using var db = new BancoDbContext();

            // Não permite CPF duplicado
            if (db.Clientes.Any(c => c.Cpf == cpf))
                return null;

            var cliente = new Cliente(nome, cpf, telefone, endereco);

            db.Clientes.Add(cliente);
            db.SaveChanges();

            return cliente;
        }

        public List<Cliente> ListarClientes()
        {
            using var db = new BancoDbContext();

            return db.Clientes
                     .Include(c => c.Conta)
                     .OrderBy(c => c.Nome)
                     .ToList();
        }
        /// <summary>
        /// Solicita uma transação a partir de uma conta já conhecida.
        /// tipoOperacao: 0 = Depósito, 1 = Saque, 2 = Transferência
        /// </summary>
        public bool SolicitarTransacao(int tipoOperacao, Conta contaOrigem, decimal valor, string? contaDestinoNumero = null)
        {
            var numeroOrigem = contaOrigem.NumeroConta;

            switch (tipoOperacao)
            {
                case 0: // Depósito
                    return _transacaoController.Depositar(numeroOrigem, valor);

                case 1: // Saque
                    return _transacaoController.Sacar(numeroOrigem, valor);

                case 2: // Transferência
                    if (string.IsNullOrWhiteSpace(contaDestinoNumero))
                        return false;

                    return _transacaoController.Transferir(numeroOrigem, contaDestinoNumero, valor);

                default:
                    return false;
            }
        }
    }
}
