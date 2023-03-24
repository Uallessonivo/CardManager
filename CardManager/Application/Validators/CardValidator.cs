using CardManager.Domain.Entities;
using CardManager.Presentation.DTO;
using FluentValidation;

namespace CardManager.Application.Validators
{
    public class CardValidator : AbstractValidator<CardDto>
    {
        public CardValidator()
        {
            RuleFor(c => c.CardOwnerCpf)
                .NotEmpty().WithMessage("O CPF não pode estar vazio.")
                .Length(11).WithMessage("O CPF deve conter 11 dígitos.");
            RuleFor(c => c.CardSerial)
                .NotEmpty().WithMessage("O Serial não pode estar vazio.")
                .Length(15).WithMessage("O Serial deve conter 15 dígitos.");
            RuleFor(c => c.CardOwnerName)
                .NotEmpty().WithMessage("O nome não pode estar vazio.");
            RuleFor(c => c.CardType)
                .NotEmpty().WithMessage("O tipo não pode estar vazio.");
        }
    }
}
