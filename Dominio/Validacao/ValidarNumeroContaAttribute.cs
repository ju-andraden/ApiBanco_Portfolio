using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarNumeroContaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var numeroConta = value.ToString();

            Regex validarNumeroConta = new Regex(@"^\d{5}-\d{1}$");

            if (!validarNumeroConta.IsMatch(numeroConta))
            {
                return new ValidationResult("Formato de número de conta inválido.");
            }
            return ValidationResult.Success;
        }
    }
}
