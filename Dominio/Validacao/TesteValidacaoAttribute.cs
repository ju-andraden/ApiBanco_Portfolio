using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    /*[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TesteValidacaoAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return true;
            }

            var primeiraLetra = value.ToString()[0].ToString();
            //verifica se é dif de caixa alta
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return false;
                //return new ValidationResult("A primeira letra do nome deve ser maiúscula.");
            }

            return true;
        }
    }*/
}
