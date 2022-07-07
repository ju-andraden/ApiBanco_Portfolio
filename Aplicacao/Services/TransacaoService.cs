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
            //var style = NumberStyles.AllowDecimalPoint;
            //var provider = new CultureInfo("pt-BR");

            Transacao transacao = new Transacao();
            transacao.TipoTransacao = criarTransacaoDto.TipoTransacao.ToString();
            transacao.Descricao = criarTransacaoDto.Descricao;            
            transacao.Valor = decimal.Round(criarTransacaoDto.Valor, 2, MidpointRounding.AwayFromZero);

            //transacao.Valor = decimal.Parse(transacao.Valor.ToString().Replace(".", ","), style, provider);

            transacao.DataHora = DateTime.Now;

            _context.Transacoes.Add(transacao);
            _context.SaveChanges();

            //Convert.ToDecimal(transacao.Valor.ToString("#.##0,00"));

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

        private Transacao BuscarTransacaoPeloId(Guid id)
        {
            var transacao = _context.Transacoes.FirstOrDefault(transacao => transacao.Id.Equals(id));
            return transacao;
        }
    }
}
