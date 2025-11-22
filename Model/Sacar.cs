namespace UVV_fintech.Model
{
    public class Sacar : Transacao
    {
        public Sacar() : base()
        {
        }

        public Sacar(decimal valor, Conta conta)
            : base(valor, conta)
        {
        }

        public override bool Executar()
        {
            return Conta.Debitar(Valor);
        }
    }
}
