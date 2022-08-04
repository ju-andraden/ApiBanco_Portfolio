using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Validacao
{
    public class ValidarCpfAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult("Cpf não pode ser nulo.");
            }

            var cpf = value.ToString();

            if (cpf.Length != 14)
            {
                return new ValidationResult("Quantidade de caracteres diferente de 14.");
            }
            if (!ValidaPosicaoCaracter(cpf, 3, '.') || !ValidaPosicaoCaracter(cpf, 7, '.'))
            {
                return new ValidationResult("Ponto na posição inválida.");
            }
            if (!ValidaPosicaoCaracter(cpf, 11, '-'))
            {
                return new ValidationResult("Hífen na posição inválida.");
            }
            if (cpf.Where(c => char.IsNumber(c)).Count() != 11)
            {
                return new ValidationResult("O CPF deve ter 11 números.");
            }
            return ValidationResult.Success;
        }

        private bool ValidaPosicaoCaracter(string cpf, int posicao, char c)
        {
            return cpf[posicao] == c;
        }
    }
}
