using CardManager.Application.DTO;
using FluentValidation;

namespace CardManager.Application.Validators
{
    public class CardValidator : AbstractValidator<CardDto>
    {
        public CardValidator()
        {
            RuleFor(c => c.CardOwnerCpf)
                .NotEmpty().WithMessage("O CPF não pode estar vazio.");
            RuleFor(c => c.CardSerial)
                .NotEmpty().WithMessage("O Serial não pode estar vazio.");
            RuleFor(c => c.CardOwnerName)
                .NotEmpty().WithMessage("O nome não pode estar vazio.");
            RuleFor(c => c.CardStatus)
                .NotEmpty().WithMessage("O status não pode estar vazio");
            RuleFor(c => c.CardType)
                .NotEmpty().WithMessage("O tipo não pode estar vazio.");   
        }
    }
}
