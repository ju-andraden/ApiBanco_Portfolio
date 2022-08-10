﻿using _4_Recursos;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Validacao
{
    public class PrimeiraLetraMaiusculaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value,
            ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return ValidationResult.Success;
            }

            var primeiraLetra = value.ToString()[0].ToString();

            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                return new ValidationResult(Mensagens.PrimeiraLetraMaiuscula);
            }
            return ValidationResult.Success;
        }
    }
}