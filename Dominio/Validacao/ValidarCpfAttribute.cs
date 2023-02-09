﻿using _4_Recursos;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Dominio.Validacao
{
    public class ValidarCpfAttribute : ValidationAttribute
    {
        private const string validacaoCpf = @"\d{3}\.\d{3}\.\d{3}-\d{2}";
        protected override ValidationResult? IsValid(object? value,
            ValidationContext validationContext)
        {
            if (value != null)
            {
                if (!new Regex(validacaoCpf).IsMatch(value.ToString()))
                {
                    return new ValidationResult(Mensagens.FormatoCpf);
                }

                if (IsCpf(value.ToString()))
                {
                    return new ValidationResult("Teste");
                }
            }
            return ValidationResult.Success;
        }

        public static bool IsCpf(string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;

            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");

            if (cpf.Length != 11)
            {
                return false;
            }

            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            }

            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
            {
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            }
            resto = soma % 11;

            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = digito + resto.ToString();

            return cpf.EndsWith(digito);
        }
    }
}