using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVV_fintech.Db;

namespace UVV_fintech.Model
{
    internal class Cliente
    {

        public int ID { get; set; }
        public string CPF {  get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
        public string DataNascimento { get; set;}
       
        public Cliente(string cpf, string nome, string endereco, string dataNascimento)
        {
            this.Nome = nome;
            this.Endereco = endereco;
            this.CPF = cpf;
            this.DataNascimento = dataNascimento;
        }

        private void CriarCliente(Cliente cliente) {
            using var context = new UvvFintechDbContext();
            if(cliente != null)
            {
                context.Add(cliente);
            }
        }

    }
}
