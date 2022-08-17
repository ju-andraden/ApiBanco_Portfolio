using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarAgenciaAttribute : ValidationAttribute
    {
        private const string validandoAgencia = @"^\d{4}$";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(Mensagens.CampoNuloOuVazio);
            }

            if (!new Regex(validandoAgencia).IsMatch(value.ToString()))
            {
                return new ValidationResult(Mensagens.FormatoAgencia);
            }
            return ValidationResult.Success;
        }
    }
}