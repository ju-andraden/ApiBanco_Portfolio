using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarNumeroContaAttribute : ValidationAttribute
    {
        private const string validandoNumeroConta = @"^(?i)(\d*\d{5}-\d{1})?$";
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!new Regex(validandoNumeroConta).IsMatch(value.ToString()))
                {
                    return new ValidationResult(Mensagens.FormatoNumeroConta);
                }
            }
            return ValidationResult.Success;
        }
    }
}