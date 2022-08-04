using Dominio.Validacao;

namespace Dominio.Dto
{
    public class CriarClienteDto
    {
        //[Required] //verifica se é nulo ou vazio
        //[PrimeiraLetraMaiuscula]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }

        [ValidarCpf]
        public string Cpf { get; set; }
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}