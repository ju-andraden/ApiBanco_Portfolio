using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    public class CriarClienteDto
    {
        [Required(ErrorMessage = "O campo Nome deve estar preenchido.")]
        [ValidarNome]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "O campo Data de Nascimento deve estar preenchido.")]
        [ValidarDataNascimento]
        public string? DataNascimento { get; set; }

        [Required(ErrorMessage = "O campo CPF deve estar preenchido.")]
        [ValidarCpf]
        public string? Cpf { get; set; }

        [Required(ErrorMessage = "O campo Telefone deve estar preenchido.")]
        [ValidarTelefone]
        public string? Telefone { get; set; }

        [Required(ErrorMessage = "O campo Endereço deve estar preenchido.")]
        public EnderecoDto Endereco { get; set; }
    }
}