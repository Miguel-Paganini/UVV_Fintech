using UVV_fintech.Db;

namespace UVV_fintech.Model
{
    public abstract class Transacao : ITransacao
    {
        public int TransacaoId { get; set; }
        public DateTime DataHora { get; set; } = DateTime.Now;
        public decimal Valor { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; } = null!;

        protected Transacao() { }

        protected Transacao(decimal valor, Conta conta)
        {
            DataHora = DateTime.Now;
            Valor = valor;
            Conta = conta;
            ContaId = conta.ContaId;
        }

        public abstract bool Executar();

        public static List<Transacao> GetListaTransacoes()
        {
            using var db = new BancoDbContext();

            List<Transacao> lista = [.. db.Transacoes];

            return lista;
        }
    }
}
