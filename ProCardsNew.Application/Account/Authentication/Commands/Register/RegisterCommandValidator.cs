using FluentValidation;

namespace ProCardsNew.Application.Account.Authentication.Commands.Register;

public class RegisterCommandValidator: AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(rc => rc.FirstName).NotEmpty();
        RuleFor(rc => rc.LastName).NotEmpty();
        RuleFor(rc => rc.Email).NotEmpty();
        RuleFor(rc => rc.Location).NotEmpty();
        RuleFor(rc => rc.Login).NotEmpty();
        RuleFor(rc => rc.Password).NotEmpty();
    }
}