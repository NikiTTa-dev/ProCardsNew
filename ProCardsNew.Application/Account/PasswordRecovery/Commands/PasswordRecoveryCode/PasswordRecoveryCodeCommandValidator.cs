using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryCode;

public class PasswordRecoveryCodeCommandValidator : AbstractValidator<PasswordRecoveryCodeCommand>
{
    public PasswordRecoveryCodeCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(q => q.Code)
            .NotEmpty()
            .ContainsNoSpaces()
            .ContainsNumbersOnly()
            .MaximumLength(validationSettings.UserPasswordRecoveryCodeLength);

        RuleFor(q => q.Login)
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserLoginLength);
    }
}