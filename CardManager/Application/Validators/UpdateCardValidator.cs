using CardManager.Application.DTO;
using CardManager.Domain.Entities;
using FluentValidation;

namespace CardManager.Application.Validators
{
    public class UpdateCardValidator : AbstractValidator<UpdateCardDto>
    {
        public UpdateCardValidator()
        {
            RuleFor(c => c.CardOwnerCpf)
                .NotEmpty().WithMessage("O CPF não pode estar vazio.")
                .Length(11).WithMessage("O CPF deve conter 11 dígitos.");
            RuleFor(c => c.CardOwnerName)
                .NotEmpty().WithMessage("O nome não pode estar vazio.");
        }
    }
}
