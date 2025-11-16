using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVV_fintech.Model
{
    internal class Transacao
    {
        public int TransacaoId { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataHora { get; set; }
        public int ContaId { get; set; }
        public Conta Conta { get; set; }
        
        protected Transacao() { }
    }
}
