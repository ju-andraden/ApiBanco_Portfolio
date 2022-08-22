using Dominio.Enum;
using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    public class CriarTransacaoDto
    {
        public Guid ContaId { get; set; }

        [Required(ErrorMessage = "Teste")]
        [ValorMaiorQueZero]
        public Decimal? Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
    }
}