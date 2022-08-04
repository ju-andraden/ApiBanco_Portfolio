using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    public class ValorMaiorQueZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var numero = Convert.ToDecimal(value);

            if (numero > 0)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("É permitido somente valores maior que zero.");
        }
    }
}
