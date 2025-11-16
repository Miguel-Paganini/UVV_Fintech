using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVV_fintech.Model
{
    internal class Deposito : Transacao
    {
        public Deposito(DateTime dataTransacao, float valor) : base(dataTransacao, valor)
        {
        }
    }
}
