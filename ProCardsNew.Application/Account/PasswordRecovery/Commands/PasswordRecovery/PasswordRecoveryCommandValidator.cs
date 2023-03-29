using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;

public class PasswordRecoveryCommandValidator: AbstractValidator<PasswordRecoveryCommand>
{
    public PasswordRecoveryCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        
        RuleFor(c => c.Email)
            .NotNull()
            .EmailAddress()
            .MaximumLength(validationSettings.UserEmailLength);
    }
}