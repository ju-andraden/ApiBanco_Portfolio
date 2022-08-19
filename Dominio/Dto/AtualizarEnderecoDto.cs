using Dominio.Validacao;

namespace Dominio.Dto
{
    public class AtualizarEnderecoDto
    {
        public string? Logradouro { get; set; }

        public string? Numero { get; set; }

        [ValidarCep]
        public string? Cep { get; set; }

        public string? Cidade { get; set; }

        [ValidarEstado]
        public string? Estado { get; set; }
    }
}