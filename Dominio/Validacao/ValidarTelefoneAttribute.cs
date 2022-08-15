using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarTelefoneAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var telefone = value.ToString();

            Regex validarTelefone = new Regex(@"([0-9]{2}\))([0-9]{4,5})-([0-9]{4})$");

            if (!validarTelefone.IsMatch(telefone))
            {
                return new ValidationResult("Formato de telefone informado inválido.");
            }

            return ValidationResult.Success;
        }
    }
}
