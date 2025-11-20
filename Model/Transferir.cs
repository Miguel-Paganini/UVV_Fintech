namespace UVV_fintech.Model
{
    public class Transferir : Transacao
    {
        // Conta de destino da transferência
        public int ContaDestinoId { get; set; }
        public Conta ContaDestino { get; set; } = null!;

        public Transferir() : base()
        {
        }

        public Transferir(decimal valor, Conta contaOrigem, Conta contaDestino)
            : base(valor, contaOrigem)
        {
            ContaDestino = contaDestino;
            ContaDestinoId = contaDestino.ContaId;
        }

        public override bool Executar()
        {
            if (!Conta.Debitar(Valor))
            {
                return false;
            }

            ContaDestino.Creditar(Valor);
            return true;
        }
    }
}
