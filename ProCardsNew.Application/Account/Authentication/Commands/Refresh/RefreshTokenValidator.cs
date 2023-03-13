using FluentValidation;

namespace ProCardsNew.Application.Account.Authentication.Commands.Refresh;

public class RefreshTokenValidator: AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenValidator()
    {
        RuleFor(command => command.UserId).NotEmpty();
        RuleFor(command => command.RefreshToken).NotEmpty();
    }
}