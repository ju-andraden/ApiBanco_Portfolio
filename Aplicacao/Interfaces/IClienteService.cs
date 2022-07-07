using Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IClienteService
    {
        public Cliente CriarCliente(CriarClienteDto criarClienteDto);
        public Cliente LerCliente(string cpf);
        public List<Cliente> LerClientes();
        public Cliente AtualizarCliente(string cpf, AtualizarClienteDto atualizarClienteDto);
        public string DeletarCliente(string cpf);
    }
}