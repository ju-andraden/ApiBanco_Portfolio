using Dominio.Enum;
using Dominio.Validacao;

namespace Dominio.Dto
{
    public class CriarTransacaoDto
    {
        public Guid ContaId { get; set; }

        [ValorMaiorQueZero]
        public decimal Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
    }
}