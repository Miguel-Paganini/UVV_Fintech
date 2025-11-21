using Microsoft.EntityFrameworkCore;
using UVV_fintech.Db;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ClienteController
    {
        private readonly TransacaoController _transacaoController = new();

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

        public bool SolicitarTransacao(int tipoOperacao, Conta contaOrigem, decimal valor, string? contaDestinoNumero = null)
        {
            var numeroOrigem = contaOrigem.NumeroConta;

            switch (tipoOperacao)
            {
                case 0:
                    return _transacaoController.DepositarControl(numeroOrigem, valor);

                case 1:
                    return _transacaoController.SacarControl(numeroOrigem, valor);

                case 2:
                    if (string.IsNullOrWhiteSpace(contaDestinoNumero))
                        return false;

                    return _transacaoController.TransferirControl(numeroOrigem, contaDestinoNumero, valor);

                default:
                    return false;
            }
        }
    }
}
