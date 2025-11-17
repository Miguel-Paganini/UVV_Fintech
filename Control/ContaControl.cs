using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ContaControl
    {
        private Conta ContaModel = new();

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
                conta = new ContaCorrente
                {
                    TaxaManutencao = 100
                };

            }
            else
            {
                conta = new ContaPoupanca
                {
                    TaxaRendimento = 0.005m
                };
            }

            if (ContaModel.CriarConta(conta, cliente))
            {
                return true;
            }
            return false;
        }
    }
}
