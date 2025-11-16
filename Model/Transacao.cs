using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UVV_fintech.Model
{
    internal abstract class Transacao
    {
        private DateTime DataTransacao {  get; set; }
        private float Valor {  get; set; }

        public Transacao(DateTime dataTransacao, float valor)
        {
            DataTransacao = dataTransacao;
            Valor = valor;
        }
    }
}
