using Dominio.Dto;
using Dominio.Entidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface IClienteService
    {
        public Task<Cliente> CriarCliente(CriarClienteDto criarClienteDto);
        public Task<Cliente> LerCliente(string cpf);
        public Task<List<Cliente>> LerClientes();
        public Task<Cliente> AtualizarCliente(string cpf, AtualizarClienteDto atualizarClienteDto);
        public string DeletarCliente(string cpf);
    }
}