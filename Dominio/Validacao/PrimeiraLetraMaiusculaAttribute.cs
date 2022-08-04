using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    //validacao de modelo com atributos personalizados - validar uma prop
    //reutilizada em outros modelos e props
    /*public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        //1-parametro: valor da prop
        //2-parametro: informacoes do contexto, onde a validacao esta sendo executada (DTO CriarCliente)
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();

            //verifica se é dif de caixa alta
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult("A primeira letra do nome deve ser maiúscula.");
            }

            return ValidationResult.Success;
        }
    }*/
}