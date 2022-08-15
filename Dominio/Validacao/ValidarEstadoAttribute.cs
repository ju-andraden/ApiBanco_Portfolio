using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarEstadoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var estado = value.ToString();

            Regex validarEstado = new Regex(@"^(?i)(\s*(AC|AL|AP|AM|BA|CE|DF|ES|GO|MA|MT|MS|MG|PA|PB|PR|PE|PI|RJ|RN|RS|RO|RR|SC|SP|SE|TO)?)$");

            if (!validarEstado.IsMatch(estado))
            {
                return new ValidationResult(Mensagens.EstadoInvalido);
            }

            return ValidationResult.Success;
        }
    }
}