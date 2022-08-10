using Dominio.Dto;
using Dominio.Entidade;

namespace Aplicacao.Interfaces
{
    public interface IClienteService
    {
        public Task<Cliente> CriarCliente(CriarClienteDto criarClienteDto);
        public Task<Cliente> LerCliente(string cpf);
        public Task<List<Cliente>> LerClientes();
        public Task<Cliente> AtualizarCliente(string cpf, AtualizarClienteDto atualizarClienteDto);
        public Task<string> DeletarCliente(string cpf);
    }
}