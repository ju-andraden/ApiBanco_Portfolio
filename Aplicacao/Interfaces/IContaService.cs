using Dominio.Dto;
using Dominio.Entidade;

namespace Aplicacao.Interfaces
{
    public interface IContaService
    {
        public Task<Conta> Criar(CriarContaDto criarContaDto);
        public Task<Conta> Ler(string numero);
        public Task<List<Conta>> Ler();
        public Task<List<Conta>> Ler(Guid id);
        public Task<Conta> AtualizarConta(string numeroConta, AtualizarContaDto atualizarContaDto);
        public Task<string> Deletar(string numero);
    }
}
