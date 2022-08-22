using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarNomeAttribute : ValidationAttribute
    {
        private const string validacaoCaractere = @"^[a-zA-Z\s]+$";
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {

            if (value != null)
            {

                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    var primeiraLetra = value.ToString()[0].ToString();

                    if (primeiraLetra != primeiraLetra.ToUpper())
                    {
                        return new ValidationResult(Mensagens.PrimeiraLetraMaiuscula);
                    }

                    if (!new Regex(validacaoCaractere).IsMatch(value.ToString()))
                    {
                        return new ValidationResult(Mensagens.CaractereEspecialNome);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}