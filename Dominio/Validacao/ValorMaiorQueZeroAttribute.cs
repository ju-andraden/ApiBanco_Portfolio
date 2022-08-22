using _4_Recursos;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    public class ValorMaiorQueZeroAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value,
            ValidationContext validationContext)
        {

            if (value != null)
            {
                var numero = Convert.ToDecimal(value);

                if (numero > 0)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult(Mensagens.ValorMaiorQueZero);
        }
    }
}