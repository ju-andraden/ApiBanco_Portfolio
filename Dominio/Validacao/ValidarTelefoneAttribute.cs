using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarTelefoneAttribute : ValidationAttribute
    {
        private const string validandoTelefone = @"([0-9]{2}\))([0-9]{4,5})-([0-9]{4})$";
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    if (!new Regex(validandoTelefone).IsMatch(value.ToString()))
                    {
                        return new ValidationResult(Mensagens.FormatoTelefone);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}