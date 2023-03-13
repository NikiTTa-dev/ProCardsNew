using FluentValidation;

namespace ProCardsNew.Application.Account.Authentication.Queries.Login;

public class LoginQueryValidator: AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(query => query.Login).NotEmpty();
        RuleFor(query => query.Password).NotEmpty();
    }
}