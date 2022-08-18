using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    public class CriarContaDto
    {
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O campo Número Conta deve estar preenchido.")]
        [ValidarNumeroConta]
        public string? NumeroConta { get; set; }

        [Required(ErrorMessage = "O campo Agência deve estar preenchido.")]
        [ValidarAgencia]
        public string? Agencia { get; set; }
    }
}