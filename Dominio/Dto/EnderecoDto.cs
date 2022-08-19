using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    public class EnderecoDto
    {
        [Required(ErrorMessage = "O campo Logradouro deve estar preenchido.")]
        public string? Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Número deve estar preenchido.")]
        public string? Numero { get; set; }

        [Required(ErrorMessage = "O campo CEP deve estar preenchido.")]
        [ValidarCep]
        public string? Cep { get; set; }

        [Required(ErrorMessage = "O campo Cidade deve estar preenchido.")]
        public string? Cidade { get; set; }

        [Required(ErrorMessage = "O campo Estado deve estar preenchido.")]
        [ValidarEstado]
        public string? Estado { get; set; }
    }
}