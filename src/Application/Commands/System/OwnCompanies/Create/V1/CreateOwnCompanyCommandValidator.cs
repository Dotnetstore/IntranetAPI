﻿using FluentValidation;

namespace Application.Commands.System.OwnCompanies.Create.V1;

public sealed class CreateOwnCompanyCommandValidator : AbstractValidator<CreateOwnCompanyCommand>
{
    public CreateOwnCompanyCommandValidator()
    {
        RuleFor(q => q.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(1)
            .MaximumLength(100);
    }
}