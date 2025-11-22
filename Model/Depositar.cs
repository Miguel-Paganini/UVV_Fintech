namespace UVV_fintech.Model
{
    public class Depositar : Transacao
    {
        public Depositar() : base()
        {
        }

        public Depositar(decimal valor, Conta conta)
            : base(valor, conta)
        {
        }

        public override bool Executar()
        {
            Conta.Creditar(Valor);
            return true;
        }
    }
}
