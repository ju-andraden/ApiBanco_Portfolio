using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarDataNascimentoAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(Mensagens.DataNascimentoNulaOuVazia);
            }

            var data = value.ToString();

            if (data.Length != 10)
            {
                return new ValidationResult("Quantidade de caracteres diferente de 10.");
            }

            if (!ValidaPosicaoCaracter(data, 4, '-') || !ValidaPosicaoCaracter(data, 7, '-'))
            {
                return new ValidationResult("Hífen na posição inválida");
            }

            Regex validarData = new Regex(@"^\d{4}-\d{2}-\d{2}$");

            if (!validarData.IsMatch(data))
            {
                return new ValidationResult("Formato de data inválido.");
            }

            return ValidationResult.Success;
        }

        public bool ValidaPosicaoCaracter(string data, int posicao, char c)
        {
            return data[posicao] == c;
        }
    }
}
