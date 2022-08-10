using _4_Recursos;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    public class ValidarCpfAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, 
            ValidationContext validationContext)
        {
            if (value is null)
            {
                return new ValidationResult(Mensagens.CpfNaoNulo);
            }

            var cpf = value.ToString();

            if (cpf.Length != 14)
            {
                return new ValidationResult(Mensagens.QtdeCaracteresDif);
            }
            if (!ValidaPosicaoCaracter(cpf, 3, '.') || !ValidaPosicaoCaracter(cpf, 7, '.'))
            {
                return new ValidationResult(Mensagens.PosicaoPonto);
            }
            if (!ValidaPosicaoCaracter(cpf, 11, '-'))
            {
                return new ValidationResult(Mensagens.PosicaoHifen);
            }
            if (cpf.Where(c => char.IsNumber(c)).Count() != 11)
            {
                return new ValidationResult(Mensagens.QtdeNumerosCpf);
            }
            return ValidationResult.Success;
        }

        private bool ValidaPosicaoCaracter(string cpf, int posicao, char c)
        {
            return cpf[posicao] == c;
        }
    }
}