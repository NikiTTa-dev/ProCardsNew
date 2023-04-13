using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;
using ProCardsNew.Application.Common.Validators;

namespace ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

public class DeckCardsQueryValidator
    : AbstractValidator<DeckCardsQuery>
{
    public DeckCardsQueryValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;
        RuleFor(q => q.DeckId)
            .NotEmpty();

        RuleFor(q => q.UserId)
            .NotEmpty();
        
        RuleFor(q => q.SearchQuery)
            .NotNull()
            .ContainsNoMultipleSpaces()
            .MaximumLength(validationSettings.CardSideLength);
    }
}