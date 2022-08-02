using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Dominio.Entidade
{
    public class Cliente
    {
        [Key]
        public Guid Id { get; set; }
        public Guid EnderecoId { get; set; }

        [Required] //verifica se é nulo ou vazio
        [PrimeiraLetraMaiuscula]
        public string Nome { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string? Telefone { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Endereco Endereco { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Conta> Contas { get; set; }
    }
}
