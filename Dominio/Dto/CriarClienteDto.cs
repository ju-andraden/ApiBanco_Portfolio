using Dominio.Validacao;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Dto
{
    //validacao com IValidatableObject - acessa as props do modelo
    //e realiza uma validacao mais complexa
    //não pode ser reutilizada em outros modelos
    public class CriarClienteDto //: IValidatableObject
    {
        [Required] //verifica se é nulo ou vazio
        [PrimeiraLetraMaiuscula]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string? Telefone { get; set; }
        public EnderecoDto Endereco { get; set; }

        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //ValidationResult = inicializa uma nova instancia com a msg de erro e uma lista de membros com erros de validacao
            //yield = retorna cada elemento individualmente 
            //verifica se o valor da prop é nulo ou vazio

            if (!string.IsNullOrEmpty(this.Nome))
            {
                var primeiraLetra = this.Nome[0].ToString();

                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    yield return new ValidationResult("A primeira letra do produto deve ser maiúscula.",
                        new[]
                        { nameof(this.Nome)}
                        );
                }
            }
        }*/
    }
}
