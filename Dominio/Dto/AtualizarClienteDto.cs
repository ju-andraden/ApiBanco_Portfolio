using Dominio.Validacao;

namespace Dominio.Dto
{
    public class AtualizarClienteDto
    {
        [ValidarNome]
        public string? Nome { get; set; }

        [ValidarDataNascimento]
        public string? DataNascimento { get; set; }

        [ValidarTelefone]
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}