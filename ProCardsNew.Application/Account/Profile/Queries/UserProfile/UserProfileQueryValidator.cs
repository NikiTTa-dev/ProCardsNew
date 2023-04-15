using FluentValidation;

namespace ProCardsNew.Application.Account.Profile.Queries.UserProfile;

public class UserProfileQueryValidator
    : AbstractValidator<UserProfileQuery>
{
    public UserProfileQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}