using Dominio.Validacao;

namespace Dominio.Dto
{
    public class EnderecoDto
    {
        [ValidarNuloOuVazio]
        public string? Logradouro { get; set; }

        [ValidarNuloOuVazio]
        public string? Numero { get; set; }

        [ValidarCep]
        public string? Cep { get; set; }

        [ValidarNuloOuVazio]
        public string? Cidade { get; set; }

        [ValidarNuloOuVazio]
        [ValidarEstado]
        public string? Estado { get; set; }
    }
}