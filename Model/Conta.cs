using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVV_fintech.View;

namespace UVV_fintech.Model
{
    internal class Conta
    {
        public int ContaId { get; set; }
        public string NumeroConta { get; set; }
        public decimal Saldo { get; set; } = 0;
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public List<Transacao> Transacoes { get; set; } = new List<Transacao>();

        public Conta() { }
    }
}
