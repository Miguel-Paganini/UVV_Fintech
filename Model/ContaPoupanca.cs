namespace UVV_fintech.Model
{
    public class ContaPoupanca : Conta
    {
        // Defina a taxa oficial (ex.: 0.5% = 0.005m)
        public decimal TaxaRendimento { get; set; } = 0.005m;

        public ContaPoupanca() : base()
        {
        }

        public ContaPoupanca(Cliente cliente) : base(cliente)
        {
        }

        public void AplicarRendimento()
        {
            if (!Ativa) return;

            var rendimento = GetSaldo() * TaxaRendimento;
            Creditar(rendimento);
        }
    }
}
