using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ContaController
    {
        private readonly Conta ContaModel = new();
        private readonly ClienteController ClienteController = new();

        public int CriarConta(string nome, string cpf, string telefone, string endereco, string tipoConta)
        {
            Cliente cliente = ClienteController.CadastrarCliente(nome, cpf, telefone, endereco);

            if(cliente == null)
                return -1;

            Conta conta;
            if (tipoConta == "CC")
            {
                if(cliente.ContaCorrente != null)
                    return 0;   

                conta = new ContaCorrente(cliente);

                if (!ContaModel.CadastrarConta(conta))
                    return 0;

                cliente.ContaCorrente = conta as ContaCorrente;
            }
            else
            {
                if (cliente.ContaPoupanca != null)
                    return 0;

                conta = new ContaPoupanca(cliente);

                if (!ContaModel.CadastrarConta(conta))
                    return 0;
                cliente.ContaPoupanca = conta as ContaPoupanca;
            }
            return 1;
        }

        public Conta? BuscarContaPeloNumero(string numeroConta)
        {
            return ContaModel.BuscarContaPeloNumero(numeroConta);
        }
    }
}
