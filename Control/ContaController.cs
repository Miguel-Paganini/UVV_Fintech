using Microsoft.EntityFrameworkCore;
using System.Linq;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ContaController
    {
        private readonly ClienteController _clienteController = new();
        private Conta ContaModel = new();

        public int CriarConta(string nome, string cpf, string telefone, string endereco, string tipoConta)
        {
            var cliente = _clienteController.CadastrarCliente(nome, cpf, telefone, endereco);
            if (cliente == null)
                cliente = _clienteController.BuscarClientePorCpf(cpf)
                          ?? throw new System.Exception($"CPF '{cpf}' não encontrado.");

            // Carrega contas existentes
            cliente.Contas ??= new System.Collections.Generic.List<Conta>();

            if (tipoConta == "CC")
            {
                if (cliente.Contas.OfType<ContaCorrente>().Any()) return -1;
                var conta = new ContaCorrente(cliente);
                cliente.Contas.Add(conta);
                conta.SalvarConta();
            }
            else if (tipoConta == "CP")
            {
                if (cliente.Contas.OfType<ContaPoupanca>().Any()) return -1;
                var conta = new ContaPoupanca(cliente);
                cliente.Contas.Add(conta);
                conta.SalvarConta();
            }

            return 1;
        }

        public Conta? BuscarContaPeloNumero(string numeroConta)
        {
            return ContaModel.BuscarContaPeloNumero(numeroConta);
        }

        public bool AplicarRendimentoPoupanca(string numeroConta)
        {
            var conta = ContaModel.BuscarContaPeloNumero(numeroConta);
            if (conta is ContaPoupanca cp)
            {
                cp.AplicarRendimento();
                cp.SalvarConta();
                return true;
            }
            return false; // não é poupança
        }

        public bool CobrarTaxaManutencaoCorrente(string numeroConta)
        {
            var conta = ContaModel.BuscarContaPeloNumero(numeroConta);
            if (conta is ContaCorrente cc)
            {
                bool sucesso = cc.CobrarTaxaManutencao();
                if (sucesso) cc.SalvarConta();
                return sucesso;
            }
            return false; // não é corrente
        }

        public bool ExcluirContaControl(string numeroConta)
        {
            var conta = BuscarContaPeloNumero(numeroConta);

            if (conta == null)
                return false;

            var clienteId = conta.ClienteId;

            conta.ExcluirConta(numeroConta);

            // verifica se esse cliente ainda tem alguma conta
            var aindaTemConta = _clienteController.AindaTemContaControl(clienteId);

            if (!aindaTemConta)
            {
                var cliente = _clienteController.BuscarClientePorIdControl(clienteId);
                if (cliente != null)
                {
                    _clienteController.ExcluirClienteControl(clienteId);
                }
            }

            return true;
        }
    }
}