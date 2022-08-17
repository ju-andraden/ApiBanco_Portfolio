using Dominio.Validacao;

namespace Dominio.Dto
{
    public class CriarContaDto
    {
        public Guid ClienteId { get; set; }

        [ValidarNumeroConta]
        public string? Numero { get; set; }

        [ValidarAgencia]
        public string? Agencia { get; set; }
    }
}