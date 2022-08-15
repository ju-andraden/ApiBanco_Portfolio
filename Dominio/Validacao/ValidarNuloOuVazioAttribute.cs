using _4_Recursos;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    public class ValidarNuloOuVazioAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(Mensagens.CampoNuloOuVazio);
            }
            return ValidationResult.Success;
        }
    }
}
