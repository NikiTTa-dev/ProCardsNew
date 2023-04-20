using FluentValidation;

namespace ProCardsNew.Application.Learning.Cards.Commands.GradeCard;

public class GradeCardCommandValidator
    : AbstractValidator<GradeCardCommand>
{
    public GradeCardCommandValidator()
    {
        RuleFor(c => c.CardId)
            .NotEmpty();

        RuleFor(c => c.DeckId)
            .NotEmpty();

        RuleFor(c => c.UserId)
            .NotEmpty();

        RuleFor(c => c.Grade)
            .NotEmpty()
            .InclusiveBetween(1, 5);

        RuleFor(c => c.TimeInSeconds)
            .NotEmpty()
            .InclusiveBetween(float.MinValue, float.MaxValue);
    }
}