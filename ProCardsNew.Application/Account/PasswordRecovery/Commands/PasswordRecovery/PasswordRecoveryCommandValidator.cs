using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;

public class PasswordRecoveryCommandValidator: AbstractValidator<PasswordRecoveryCommand>
{
    public PasswordRecoveryCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        
        RuleFor(c => c.Login)
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserLoginLength);
    }
}