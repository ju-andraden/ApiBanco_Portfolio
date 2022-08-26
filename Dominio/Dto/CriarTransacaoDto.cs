using Dominio.Enum;
using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    public class CriarTransacaoDto
    {
        public Guid ContaId { get; set; }

        [Required(ErrorMessage = "O campo Valor deve estar preenchido.")]
        [ValorMaiorQueZero]
        public decimal? Valor { get; set; }
        public TipoTransacao TipoTransacao { get; set; }
    }
}