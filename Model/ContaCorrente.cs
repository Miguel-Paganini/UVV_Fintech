namespace UVV_fintech.Model
{
    public class ContaCorrente : Conta
    {
        public decimal TaxaManutencao { get; set; } = 100m;

        public ContaCorrente() : base()
        {
        }

        public ContaCorrente(Cliente cliente) : base(cliente)
        {
        }

        public bool CobrarTaxaManutencao()
        {
            if (GetSaldo() >= TaxaManutencao)
            {
                Debitar(TaxaManutencao);
                return true;
            }

            return false;
        }
    }
}
