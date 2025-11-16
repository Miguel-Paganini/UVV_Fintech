using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVV_fintech.Model
{
    internal class ContaPoupanca : Conta
    {
        private float TaxaJuros {  get; set; }
            
        public ContaPoupanca(int numero, float saldo, Cliente donoConta, float taxaJuros) : base(numero, saldo, donoConta)
        {
            TaxaJuros = taxaJuros;
        }
    }
}
