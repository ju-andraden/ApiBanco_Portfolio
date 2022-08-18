using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarCpfAttribute : ValidationAttribute
    {
        private const string validacaoCpf = @"\d{3}\.\d{3}\.\d{3}-\d{2}";
        protected override ValidationResult? IsValid(object? value, 
            ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!new Regex(validacaoCpf).IsMatch(value.ToString()))
                {
                    return new ValidationResult(Mensagens.FormatoCpf);
                }
            }
            return ValidationResult.Success;
        }
    }
}