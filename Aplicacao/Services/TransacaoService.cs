using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Dominio.Entidade;
using Infraestrutura.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Aplicacao.Services
{
    public class TransacaoService : ITransacaoService
    {
        private readonly ApiDbContext _context;

        public TransacaoService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Transacao> CriarTransacao(CriarTransacaoDto
            criarTransacaoDto)
        {
            if (criarTransacaoDto.ContaId.Equals(Guid.Empty))
            {
                return null;
            }

            var existe = ValidarSeContaExiste(criarTransacaoDto.ContaId);

            if (!await existe)
            {
                return null;
            }

            Transacao transacao = new Transacao();
            transacao.ContaId = criarTransacaoDto.ContaId;
            transacao.TipoTransacao = criarTransacaoDto.TipoTransacao.ToString();
            transacao.Valor = decimal.Round((decimal)criarTransacaoDto.Valor, 2,
                MidpointRounding.AwayFromZero);

            transacao.DataHora = DateTime.Now;
            MensagemTipoTransacao(criarTransacaoDto, transacao);

            await _context.Transacoes.AddAsync(transacao);
            await _context.SaveChangesAsync();

            return transacao;
        }

        public async Task<List<Transacao>> LerTransacoes(Guid id, DateTime dataInicio,
            DateTime dataFim)
        {
            if (dataInicio == DateTime.MinValue)
            {
                dataInicio = new DateTime(2000, 1, 1);
            }

            if (dataFim == DateTime.MinValue)
            {
                dataFim = DateTime.Now;
            }

            var listaTransacoes = _context.Transacoes.Where(t
                => t.DataHora >= dataInicio && t.DataHora <= dataFim);

            if (id != Guid.Empty)
            {
                listaTransacoes = listaTransacoes.Where(t => t.ContaId == id);
            }
            return await listaTransacoes.ToListAsync();
        }

        public async Task<List<Transacao>> LerTransacoes(Guid id)
        {
            return await LerTransacoes(id, DateTime.MinValue, DateTime.MinValue);
        }

        public async Task<Transacao> LerTransacao(Guid id)
        {
            var transacao = await BuscarTransacaoPeloId(id);

            if (transacao is null)
            {
                return null;
            }
            return transacao;
        }

        private async Task<Transacao> BuscarTransacaoPeloId(Guid id)
        {
            var transacao = await _context.Transacoes.FirstOrDefaultAsync(transacao
                => transacao.Id.Equals(id));

            return transacao;
        }
        private async Task<bool> ValidarSeContaExiste(Guid contaId)
        {
            var conta = await _context.Contas.FirstOrDefaultAsync(conta
                => conta.Id.Equals(contaId));

            if (conta is null)
            {
                return false;
            }
            return true;
        }
        private void MensagemTipoTransacao(CriarTransacaoDto criarTransacaoDto,
            Transacao transacao)
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