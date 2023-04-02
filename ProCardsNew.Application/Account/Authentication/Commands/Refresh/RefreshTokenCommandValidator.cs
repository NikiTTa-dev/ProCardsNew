using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Account.Authentication.Commands.Refresh;

public class RefreshTokenCommandValidator: AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        
        RuleFor(command => command.UserId)
            .NotEmpty();
        
        RuleFor(command => command.RefreshToken ?? "")
            .NotEmpty()
            .ContainsNoSpaces()
            .MaximumLength(validationSettings.GuidLength);
    }
}