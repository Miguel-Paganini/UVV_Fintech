namespace UVV_fintech.Model
{
    public class ContaPoupanca : Conta
    {
        public decimal TaxaRendimento { get; set; }

        public ContaPoupanca(Cliente cliente) : base(cliente)
        {
            TaxaRendimento = 0.005m;
        }
    }
}
