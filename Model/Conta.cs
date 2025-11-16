using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVV_fintech.Model
{
    internal abstract class Conta
    {

        private int ID { get; set; }
        private int Numero { get; set; }
        private float Saldo { get; set; }
        private Cliente DonoConta { get; set; }

        public Conta(int numero, float saldo, Cliente donoConta)
        {
            Numero = numero;
            Saldo = saldo;
            DonoConta = donoConta;
        }
    }
}
