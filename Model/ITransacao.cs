using System;

namespace UVV_fintech.Model
{
    public interface ITransacao
    {
        DateTime DataHora { get; set; }
        decimal Valor { get; set; }

        bool Executar();
    }
}
