using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarEstadoAttribute : ValidationAttribute
    {
        private const string validandoEstado = @"^(?i)(\s*(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)?)$";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value != null)
            {
                if (!new Regex(validandoEstado).IsMatch(value.ToString()))
                {
                    return new ValidationResult(Mensagens.EstadoInvalido);
                }
            }
            return ValidationResult.Success;
        }
    }
}