using Dominio.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Interfaces
{
    public interface ITransacaoService
    {
        public Transacao CriarTransacao(CriarTransacaoDto criarTransacaoDto);
        public Transacao LerTransacao(Guid id);
        public List<Transacao> LerTransacoes(Guid id, DateTime dataInicio, DateTime dataFim);
        public List<Transacao> LerTransacoes(Guid id);
    }
}
