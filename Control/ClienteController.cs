using System.Linq;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ClienteController
    {
        private readonly TransacaoController _transacaoController = new();
        private readonly Cliente ClienteModel = new();

        public Cliente? CadastrarCliente(string nome, string cpf, string telefone, string endereco)
        {
            Cliente cliente = new Cliente(nome, cpf, telefone, endereco);

            if (ClienteModel.CadastrarCliente(cliente))
                return cliente;
            return null;
        }

        public bool SolicitarTransacao(int tipoOperacao, Conta contaOrigem, decimal valor, string? contaDestinoNumero = null)
        {
            var numeroOrigem = contaOrigem.NumeroConta;

            switch (tipoOperacao)
            {
                case 0: // Depósito
                    return _transacaoController.DepositarControl(numeroOrigem, valor);

                case 1: // Saque
                    return _transacaoController.SacarControl(numeroOrigem, valor);

                case 2: // Transferência
                    if (string.IsNullOrWhiteSpace(contaDestinoNumero))
                        return false;

                    return _transacaoController.TransferirControl(numeroOrigem, contaDestinoNumero, valor);

                default:
                    return false;
            }
        }

        public Cliente? BuscarClientePorCpf(string cpf)
        {
            return ClienteModel.BuscarClientePeloCpf(cpf);
        }

        public Cliente? BuscarClientePorIdControl(int clienteId)
        {
            return ClienteModel.BuscarClientePeloId(clienteId);
        }

        public List<Cliente> ListarClientesControl()
        {
            return ClienteModel.ListarClientes();
        }

        public bool ExcluirClienteControl(int clienteId)
        {
            return ClienteModel.ExcluirCliente(clienteId);
        }

        public bool AindaTemContaControl(int clienteId)
        {
            return ClienteModel.AindaTemConta(clienteId);
        }
    }
}