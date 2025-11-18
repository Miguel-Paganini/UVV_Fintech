using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ContaControl
    {
        private readonly Conta ContaModel = new();

        public bool CriarConta(string nome, string cpf, string telefone, string endereco, string tipoConta)
        {
            var cliente = new Cliente
            {
                Nome = nome,
                Cpf = cpf,
                Telefone = telefone,
                Endereco = endereco
            };

            Conta conta;
            if (tipoConta == "CC")
            {
                conta = new ContaCorrente(cliente);

            }
            else
            {
                conta = new ContaPoupanca(cliente);
            }

            if (ContaModel.CadastrarConta(conta))
            {
                return true;
            }
            return false;
        }
    }
}
