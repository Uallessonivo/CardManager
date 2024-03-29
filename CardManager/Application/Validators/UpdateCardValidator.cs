﻿using CardManager.Application.DTO;
using FluentValidation;

namespace CardManager.Application.Validators
{
    public class UpdateCardValidator : AbstractValidator<UpdateCardDto>
    {
        public UpdateCardValidator()
        {
            RuleFor(c => c.CardOwnerCpf)
                .NotEmpty().WithMessage("O CPF não pode estar vazio.");
            RuleFor(c => c.CardOwnerName)
                .NotEmpty().WithMessage("O nome não pode estar vazio.");
        }
    }
}
