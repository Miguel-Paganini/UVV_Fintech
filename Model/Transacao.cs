namespace UVV_fintech.Model
{
    public class Transacao
    {
        public int TransacaoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }

        protected Transacao() { }
    }
}
