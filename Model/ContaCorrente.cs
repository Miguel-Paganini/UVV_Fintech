namespace UVV_fintech.Model
{
    public class ContaCorrente : Conta
    {
        public decimal TaxaManutencao { get; set; }
        public ContaCorrente() { }
        public ContaCorrente(Cliente cliente) : base(cliente)
        {
            TaxaManutencao = 100;
        }
    }
}
