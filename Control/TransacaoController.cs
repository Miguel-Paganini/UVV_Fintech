using Microsoft.EntityFrameworkCore;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    public class TransacaoController
    {
        private Transacao _modelTransacao = new();

        public bool ProcessarTransacaoControl(ITransacao entidade)
        {
            if(_modelTransacao.ProcessarTransacao(entidade))
            {
                return true;
            }
            return false;
        }

        public bool DepositarControl(string numeroConta, decimal valor)
        {
            if(_modelTransacao.Depositar(numeroConta, valor))
            {
                return true;
            }
            return false;
        }

        public bool SacarControl(string numeroConta, decimal valor)
        {
            if(_modelTransacao.Sacar(numeroConta, valor))
            {
                return true;
            }
            return false;
        }

        public bool TransferirControl(string numeroConta, string numeroContaDestino, decimal valor)
        {
            if(_modelTransacao.Transferir(numeroConta, numeroContaDestino, valor))
            {
                return true;
            }
            return false;
        }

        public List<Transacao> ObterTransacoesContaControl(string numeroConta)
        {
            return _modelTransacao.ObterTransacoesPorConta(numeroConta);
        }

        public List<Transacao> ObterListaTransacoes()
        {
            return _modelTransacao.GetListaTransacoes();
        }
    }
}
