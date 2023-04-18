using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Learning.Decks.Queries.UserDecks;

public class UserDecksQueryValidator
    : AbstractValidator<UserDecksQuery>
{
    public UserDecksQueryValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        
        RuleFor(q => q.UserId)
            .NotEmpty();

        RuleFor(q => q.SearchQuery)
            .NotNull()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.DeckNameLength);
    }
}