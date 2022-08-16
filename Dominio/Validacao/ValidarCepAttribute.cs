using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarCepAttribute : ValidationAttribute
    {
        private const string validandoCep = @"^\d{5}-\d{3}$";
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(Mensagens.CepNuloOuVazio);
            }

            if (!new Regex(validandoCep).IsMatch(value.ToString()))
            {
                return new ValidationResult(Mensagens.FormatoCep);
            }
            return ValidationResult.Success;
        }
    }
}