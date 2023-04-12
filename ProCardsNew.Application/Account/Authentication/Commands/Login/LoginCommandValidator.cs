using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.Authentication.Commands.Login;

public class LoginCommandValidator: AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        
        RuleFor(query => query.Login)
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserLoginLength);

        RuleFor(query => query.Password)
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.UserPasswordMaxLength);
    }
}