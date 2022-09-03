using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarNomeAttribute : ValidationAttribute
    {
        private const string validacaoNumero = @"\d";

        private const string validacaoCaractere = @"[0-9&_.\-@]";
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty((string)value))
            {
                var primeiraLetra = value.ToString()[0].ToString();

                if (primeiraLetra != primeiraLetra.ToUpper())
                {
                    return new ValidationResult(Mensagens.PrimeiraLetraMaiuscula);
                }

                if (new Regex(validacaoNumero).IsMatch(value.ToString()))
                {
                    return new ValidationResult(Mensagens.NumeroNoNome);
                }

                if (new Regex(validacaoCaractere).IsMatch(value.ToString()))
                {
                    return new ValidationResult(Mensagens.CaractereEspecialNome);
                }
            }
            return ValidationResult.Success;
        }
    }
}