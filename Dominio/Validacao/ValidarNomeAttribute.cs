using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarNomeAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) 
            {
                return new ValidationResult(Mensagens.NomeNuloOuVazio);
            }

            var primeiraLetra = value.ToString()[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult(Mensagens.PrimeiraLetraMaiuscula);
            }

            var nome = value.ToString();

            Regex validarCaracterEspecial = new Regex(@"^[a-zA-Z\s]+$");

            if (!validarCaracterEspecial.IsMatch(nome))
            {
                return new ValidationResult("Nome não pode conter caracter especial.");
            }
            
            return ValidationResult.Success;
        }
    }
}