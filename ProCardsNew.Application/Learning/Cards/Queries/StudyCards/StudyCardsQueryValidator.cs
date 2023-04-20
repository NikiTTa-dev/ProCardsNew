using FluentValidation;

namespace ProCardsNew.Application.Learning.Cards.Queries.StudyCards;

public class StudyCardsQueryValidator
    : AbstractValidator<StudyCardsQuery>
{
    public StudyCardsQueryValidator()
    {
        RuleFor(q => q.DeckId)
            .NotEmpty();

        RuleFor(q => q.UserId)
            .NotEmpty();
    }
}