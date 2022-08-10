namespace Dominio.Dto
{
    public class AtualizarClienteDto
    {
        public string? Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }
    }
}