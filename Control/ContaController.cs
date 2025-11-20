using System.Linq;
using Microsoft.EntityFrameworkCore;
using UVV_fintech.Db;
using UVV_fintech.Model;

namespace UVV_fintech.Control
{
    internal class ContaController
    {
        private readonly ClienteController _clienteController = new();

        /// <summary>
        /// Cria um cliente e uma conta (CC ou CP).
        /// Retornos:
        ///  1  = sucesso
        ///  0  = algum erro ao criar a conta
        /// -1  = CPF já cadastrado (cliente não criado)
        /// </summary>
        public int CriarConta(string nome, string cpf, string telefone, string endereco, string tipoConta)
        {
            // Primeiro tenta criar o cliente
            var cliente = _clienteController.CadastrarCliente(nome, cpf, telefone, endereco);

            if (cliente == null)
                return -1; // CPF já existe

            using var db = new BancoDbContext();

            // Garante que o cliente está sendo rastreado por este contexto
            db.Attach(cliente);

            // Regra: 1 cliente -> 1 conta (independente do tipo)
            // Se quiser reforçar:
            // db.Entry(cliente).Reference(c => c.Conta).Load();
            // if (cliente.Conta != null) return 0;

            Conta conta;

            if (tipoConta == "CC")
            {
                conta = new ContaCorrente(cliente);
            }
            else
            {
                conta = new ContaPoupanca(cliente);
            }

            // Amarra 1:1
            cliente.Conta = conta;

            db.Contas.Add(conta);
            db.SaveChanges();

            return 1;
        }

        /// <summary>
        /// Busca uma conta pelo número.
        /// </summary>
        public Conta? BuscarContaPeloNumero(string numeroConta)
        {
            using var db = new BancoDbContext();

            return db.Contas
                     .Include(c => c.Cliente)
                     .FirstOrDefault(c => c.NumeroConta == numeroConta);
        }

        /// <summary>
        /// Aplica rendimento se a conta for poupança.
        /// </summary>
        public bool AplicarRendimentoPoupanca(string numeroConta)
        {
            using var db = new BancoDbContext();

            var conta = db.Contas
                          .OfType<ContaPoupanca>()
                          .FirstOrDefault(c => c.NumeroConta == numeroConta);

            if (conta == null)
                return false;

            conta.AplicarRendimento();
            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// Cobra taxa de manutenção se a conta for corrente.
        /// </summary>
        public bool CobrarTaxaManutencaoCorrente(string numeroConta)
        {
            using var db = new BancoDbContext();

            var conta = db.Contas
                          .OfType<ContaCorrente>()
                          .FirstOrDefault(c => c.NumeroConta == numeroConta);

            if (conta == null)
                return false;

            var sucesso = conta.CobrarTaxaManutencao();

            if (!sucesso)
                return false;

            db.SaveChanges();
            return true;
        }

        /// <summary>
        /// Retorna o tipo textual da conta (Conta Corrente, Conta Poupança, etc.).
        /// </summary>
        public string GetTipoConta(string numeroConta)
        {
            using var db = new BancoDbContext();

            var conta = db.Contas.FirstOrDefault(c => c.NumeroConta == numeroConta);
            return conta != null ? conta.GetTipoConta() : "Conta";
        }
    }
}
