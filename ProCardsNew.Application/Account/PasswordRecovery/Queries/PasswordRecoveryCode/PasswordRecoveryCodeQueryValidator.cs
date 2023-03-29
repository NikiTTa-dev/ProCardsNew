using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Account.PasswordRecovery.Queries.PasswordRecoveryCode;

public class PasswordRecoveryCodeQueryValidator : AbstractValidator<PasswordRecoveryCodeQuery>
{
    public PasswordRecoveryCodeQueryValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(q => q.Code)
            .NotEmpty()
            .MaximumLength(validationSettings.UserPasswordRecoveryCodeLength);

        RuleFor(q => q.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(validationSettings.UserEmailLength);
    }
}