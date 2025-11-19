using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UVV_fintech.Model;
using UVV_fintech.Db;

namespace UVV_fintech.Control
{
    internal class ClienteController
    {
        private readonly Cliente ClienteModel = new();
        public Cliente CadastrarCliente(string nome, string cpf, string telefone, string endereco)
        {
            Cliente cliente = new Cliente(nome, cpf, telefone, endereco);

            if(ClienteModel.CadastrarCliente(cliente))
                return cliente;
            return null;
        }
    }
}
