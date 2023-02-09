using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarDataNascimentoAttribute : ValidationAttribute
    {
        private const int menorIdade = -18;
        private const string validandoData = @"^\d{4}-\d{2}-\d{2}$";
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {

            if (value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    if (!new Regex(validandoData).IsMatch(value.ToString()))
                    {
                        return new ValidationResult(Mensagens.FormatoDataNascimento);
                    }

                    try
                    {
                        if (DateTime.Parse(value.ToString()) > DateTime.Now)
                        {
                            return new ValidationResult(Mensagens.DataMaiorQueAtual);
                        }
                    }
                    catch (Exception)
                    {
                        return new ValidationResult(Mensagens.DataInvalida);
                    }

                    DateTime dataMenos18Anos = DateTime.Parse(value.ToString());

                    if (dataMenos18Anos > DateTime.Now.AddYears(menorIdade))
                    {
                        return new ValidationResult(Mensagens.MenorDeIdade);
                    }
                }
            }
            return ValidationResult.Success;
        }
    }
}