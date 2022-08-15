using Dominio.Validacao;

namespace Dominio.Dto
{
    public class CriarClienteDto
    {
        [ValidarNome]
        public string? Nome { get; set; }

        [ValidarDataNascimento]
        public string? DataNascimento { get; set; }

        [ValidarCpf]
        [ValidarNuloOuVazio]
        public string? Cpf { get; set; }

        [ValidarNuloOuVazio]
        [ValidarTelefone]
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}