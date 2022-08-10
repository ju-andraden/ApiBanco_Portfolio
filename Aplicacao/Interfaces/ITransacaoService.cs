using Dominio.Dto;
using Dominio.Entidade;

namespace Aplicacao.Interfaces
{
    public interface ITransacaoService
    {
        public Task<Transacao> CriarTransacao(CriarTransacaoDto criarTransacaoDto);
        public Task<Transacao> LerTransacao(Guid id);
        public Task<List<Transacao>> LerTransacoes(Guid id, DateTime dataInicio, DateTime dataFim);
        public Task<List<Transacao>> LerTransacoes(Guid id);
    }
}
