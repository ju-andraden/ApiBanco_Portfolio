using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Dominio.Entidade;
using Infraestrutura.DataBase;

namespace Aplicacao.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ApiDbContext _context;

        public TransacaoService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> CriarTransacao(CriarTransacaoDto criarTransacaoDto)
        {
            if (criarTransacaoDto.ContaId.Equals(Guid.Empty))
            {
                return null;
            }

            var existe = ValidarSeContaExiste(criarTransacaoDto.ContaId);

            if (!existe)
            {
                return null;
            }

            Transacao transacao = new Transacao();
            transacao.ContaId = criarTransacaoDto.ContaId;
            transacao.TipoTransacao = criarTransacaoDto.TipoTransacao.ToString();
            transacao.Valor = decimal.Round(criarTransacaoDto.Valor, 2, MidpointRounding.AwayFromZero);

            transacao.DataHora = DateTime.Now;
            MensagemTipoTransacao(criarTransacaoDto, transacao);

            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();

            return transacao;
        }

        public List<Transacao> LerTransacoes(Guid id, DateTime dataInicio, DateTime dataFim)
        {
            if (dataInicio == DateTime.MinValue)
            {
                dataInicio = new DateTime(2000, 1, 1);
            }

            if (dataFim == DateTime.MinValue)
            {
                dataFim = DateTime.Now;
            }

            var listaTransacoes = _context.Transacoes.Where(t => t.DataHora >= dataInicio && t.DataHora <= dataFim);

            if (id != Guid.Empty)
            {
                listaTransacoes = listaTransacoes.Where(t => t.ContaId == id);
            }

            return listaTransacoes.ToList();
        }

        public List<Transacao> LerTransacoes(Guid id)
        {
            return LerTransacoes(id, DateTime.MinValue, DateTime.MinValue);
        }

        public Transacao LerTransacao(Guid id)
        {
            var transacao = BuscarTransacaoPeloId(id);

            if (transacao is null)
            {
                return null;
            }
            return transacao;
        }

        private Transacao BuscarTransacaoPeloId(Guid id)
        {
            var transacao = _context.Transacoes.FirstOrDefault(transacao => transacao.Id.Equals(id));

            return transacao;
        }
        private bool ValidarSeContaExiste(Guid contaId)
        {
            var conta = _context.Contas.FirstOrDefault(conta => conta.Id.Equals(contaId));

            if (conta is null)
            {
                return false;
            }

            return true;
        }
        private void MensagemTipoTransacao(CriarTransacaoDto criarTransacaoDto, Transacao transacao)
        {
            var tipoTransacao = (int)criarTransacaoDto.TipoTransacao;

            switch (tipoTransacao)
            {
                case 0:
                    transacao.Descricao = Mensagens.TedRealizado;
                    break;
                case 1:
                    transacao.Descricao = Mensagens.DocRealizado;
                    break;
                case 2:
                    transacao.Descricao = Mensagens.PixRealizado;
                    break;
                case 3:
                    transacao.Descricao = Mensagens.DepositoRealizado;
                    break;
                case 4:
                    transacao.Descricao = Mensagens.SaqueRealizado;
                    break;
                default:
                    throw new Exception(Mensagens.TransacaoInvalida);
            }
        }
    }
}
