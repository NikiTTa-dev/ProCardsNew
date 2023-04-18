using FluentValidation;

namespace ProCardsNew.Application.Learning.Decks.Commands.RemoveDeckFromLatest;

public class RemoveDeckFromLatestCommandValidator
    : AbstractValidator<RemoveDeckFromLatestCommand>
{
    public RemoveDeckFromLatestCommandValidator()
    {
        RuleFor(c => c.DeckId)
            .NotEmpty();

        RuleFor(c => c.UserId)
            .NotEmpty();
    }
}