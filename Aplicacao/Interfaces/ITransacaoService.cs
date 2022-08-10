using Dominio.Dto;
using Dominio.Entidade;

namespace Aplicacao.Interfaces
{
    public interface ITransacaoService
    {
        public Task<Transacao> CriarTransacao(CriarTransacaoDto criarTransacaoDto);
        public Transacao LerTransacao(Guid id);
        public List<Transacao> LerTransacoes(Guid id, DateTime dataInicio, DateTime dataFim);
        public List<Transacao> LerTransacoes(Guid id);
    }
}
