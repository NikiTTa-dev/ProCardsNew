using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Account.Authentication.Commands.Login;

public class LoginCommandValidator: AbstractValidator<LoginCommand>
{
    public LoginCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        
        RuleFor(query => query.Login)
            .NotEmpty()
            .MaximumLength(validationSettings.UserLoginLength);
        
        RuleFor(query => query.Password)
            .NotEmpty()
            .MaximumLength(validationSettings.UserPasswordMaxLength);
    }
}