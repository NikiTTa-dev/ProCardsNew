using FluentValidation;

namespace ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;

public class UserProfilePreviewQueryValidator
    : AbstractValidator<UserProfilePreviewQuery>
{
    public UserProfilePreviewQueryValidator()
    {
        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}