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
        public Task<Conta> Atualizar(string numeroConta, AtualizarContaDto novosDados);
        public Task<string> Deletar(string numero);
    }
}
