using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    public class CriarClienteDto
    {
        [ValidarNome]
        public string? Nome { get; set; }

        [ValidarDataNascimento]
        public string? DataNascimento { get; set; }

        [ValidarCpf]
        public string? Cpf { get; set; }

        [ValidarTelefone]
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}