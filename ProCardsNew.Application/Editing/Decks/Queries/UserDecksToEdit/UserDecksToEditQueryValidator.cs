using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;

public class UserDecksToEditQueryValidator : AbstractValidator<UserDecksToEditQuery>
{
    public UserDecksToEditQueryValidator(IOptions<ValidationSettings> settings)
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