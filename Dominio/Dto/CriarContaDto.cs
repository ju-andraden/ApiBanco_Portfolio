using Dominio.Validacao;

namespace Dominio.Dto
{
    public class CriarContaDto
    {
        public Guid ClienteId { get; set; }

        [ValidarNuloOuVazio]
        [ValidarNumeroConta]
        public string? Numero { get; set; }

        [ValidarNuloOuVazio]
        [ValidarAgencia]
        public string? Agencia { get; set; }
    }
}