using Microsoft.EntityFrameworkCore;
using UVV_fintech.Db;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class TransacaoController
    {
        //private Transacao _modelTransacao = new();

        public bool ProcessarTransacao(ITransacao transacao)
        {
            if (transacao is not Transacao entidade)
            {
                // Não é uma entidade do EF, só executa a lógica.
                return transacao.Executar();
            }

            using var db = new BancoDbContext();

            // Garante que a Conta usada na transação está ligada ao contexto
            if (entidade.Conta != null)
            {
                db.Attach(entidade.Conta);
            }
            else
            {
                var conta = db.Contas.FirstOrDefault(c => c.ContaId == entidade.ContaId);
                if (conta == null)
                    return false;

                entidade.Conta = conta;
            }

            return ProcessarTransacaoInternal(entidade, db);
        }

        public bool Depositar(string numeroConta, decimal valor)
        {
            using var db = new BancoDbContext();

            var conta = db.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta);
            if (conta == null)
                return false;

            var deposito = new Depositar(valor, conta);

            return ProcessarTransacaoInternal(deposito, db);
        }

        public bool Sacar(string numeroConta, decimal valor)
        {
            using var db = new BancoDbContext();

            var conta = db.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta);
            if (conta == null)
                return false;

            var saque = new Sacar(valor, conta);

            return ProcessarTransacaoInternal(saque, db);
        }

        public bool Transferir(string numeroContaOrigem, string numeroContaDestino, decimal valor)
        {
            using var db = new BancoDbContext();

            var contaOrigem = db.Contas.FirstOrDefault(c => c.NumeroConta == numeroContaOrigem);
            var contaDestino = db.Contas.FirstOrDefault(c => c.NumeroConta == numeroContaDestino);

            if (contaOrigem == null || contaDestino == null)
                return false;

            var transferencia = new Transferir(valor, contaOrigem, contaDestino);

            return ProcessarTransacaoInternal(transferencia, db);
        }

        public List<Transacao> ObterTransacoesPorConta(string numeroConta)
        {
            using var db = new BancoDbContext();

            return db.Transacoes
                     .Include(t => t.Conta)
                     .Where(t => t.Conta.NumeroConta == numeroConta)
                     .OrderByDescending(t => t.DataHora)
                     .ToList();
        }

        private bool ProcessarTransacaoInternal(Transacao transacao, BancoDbContext db)
        {
            var sucesso = transacao.Executar();
            if (!sucesso)
                return false;

            db.Transacoes.Add(transacao);
            db.SaveChanges();

            return true;
        }

        public List<Transacao> ObterListaTransacoes()
        {
            return Model.Transacao.GetListaTransacoes();
        }
    }
}
