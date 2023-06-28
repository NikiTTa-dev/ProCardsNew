using FluentValidation;

namespace ProCardsNew.Application.Editing.Cards.Queries.UserCards;

public class UserCardsQueryValidator : AbstractValidator<UserCardsQuery>
{
    public UserCardsQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}