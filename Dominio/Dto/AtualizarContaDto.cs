using Dominio.Validacao;

namespace Dominio.Dto
{
    public class AtualizarContaDto
    {
        [ValidarNumeroConta]
        public string? Numero { get; set; }

        [ValidarAgencia]
        public string? Agencia { get; set; }
    }
}