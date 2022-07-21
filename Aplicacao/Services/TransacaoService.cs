using _4_Recursos;
using Aplicacao.Interfaces;
using Dominio.Dto;
using Dominio.Enum;
using Infraestrutura.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacao.Services
{

    public class TransacaoService : ITransacaoService
    {
        private readonly ApiDbContext _context;

        public TransacaoService(ApiDbContext context)
        {
            _context = context;
        }

        public Transacao CriarTransacao(CriarTransacaoDto criarTransacaoDto)
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

            _context.Transacoes.Add(transacao);
            _context.SaveChanges();

            return transacao;
        }

        public List<Transacao> LerTransacoes()
        {
            return _context.Transacoes.ToList();
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

        public List<Transacao> LerTransacoes(Guid id)
        {
            var listaTransacoes = _context.Transacoes.Where(transacao => transacao.ContaId == id).ToList();

            return listaTransacoes;
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
                    transacao.Descricao = MensagensTransacao.TedRealizado;
                    break;
                case 1:
                    transacao.Descricao = MensagensTransacao.DocRealizado;
                    break;
                case 2:
                    transacao.Descricao = MensagensTransacao.PixRealizado;
                    break;
                case 3:
                    transacao.Descricao = MensagensTransacao.DepositoRealizado;
                    break;
                case 4:
                    transacao.Descricao = MensagensTransacao.SaqueRealizado;
                    break;
                default:
                    throw new Exception(MensagensTransacao.TransacaoInvalida);
            }
        }
    }
}
