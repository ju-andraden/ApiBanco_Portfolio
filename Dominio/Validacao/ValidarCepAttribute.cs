using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarCepAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult(Mensagens.CepNuloOuVazio);
            }

            var cep = value.ToString();

            /*if (cep.Length != 9)
            {
                return new ValidationResult("Quantidade de caracteres diferente de 9.");
            }

            if (!ValidaPosicaoCaracter(cep, 5, '-'))
            {
                return new ValidationResult(Mensagens.PosicaoHifen);
            }*/

            Regex validarCep = new Regex(@"^\d{5}-\d{3}$");

            if (!validarCep.IsMatch(cep))
            {
                return new ValidationResult("Formato de CEP inválido.");
            }

            return ValidationResult.Success;
        }

        /*public bool ValidaPosicaoCaracter(string data, int posicao, char c)
        {
            return data[posicao] == c;
        }*/
    }
}