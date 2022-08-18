using Dominio.Validacao;

namespace Dominio.Dto
{
    public class AtualizarContaDto
    {
        [ValidarNumeroConta]
        public string? NumeroConta { get; set; }

        [ValidarAgencia]
        public string? Agencia { get; set; }
    }
}