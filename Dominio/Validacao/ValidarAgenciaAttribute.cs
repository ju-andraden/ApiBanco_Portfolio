using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarAgenciaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var agencia = value.ToString();

            Regex validarAgencia = new Regex(@"^\d{4}$");

            if (!validarAgencia.IsMatch(agencia))
            {
                return new ValidationResult(Mensagens.FormatoAgencia);
            }
            return ValidationResult.Success;
        }
    }
}