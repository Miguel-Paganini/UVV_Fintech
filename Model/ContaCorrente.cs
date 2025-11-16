using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVV_fintech.Model
{
    internal class ContaCorrente : Conta
    {
        private float LimiteChequeEspecial { get; set; }

        public ContaCorrente(int numero, float saldo, Cliente donoConta, float limiteChequeEspecial) : base(numero, saldo, donoConta)
        {
            LimiteChequeEspecial = limiteChequeEspecial;
        }
    }
}
