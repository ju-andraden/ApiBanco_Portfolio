using Dominio.Dto;
using FluentValidation;

namespace Dominio.Validacao
{
    //<T> - tipo de classe para validar
    public class ClienteValidator : AbstractValidator<CriarClienteDto>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome).NotNull().WithMessage("Nome é obrigatório.");
            //RuleFor(x => x.DataNascimento).NotNull().WithMessage("Data de nascimento é obrigatória");
        }
    }
}
