using FluentValidation;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Application.Service.Images.Queries.CardImage;

public class CardImageQueryValidator
    : AbstractValidator<CardImageQuery>
{
    public CardImageQueryValidator(IOptions<ValidationSettings> settings)
    {
        var validationSettings = settings.Value;

        RuleFor(q => q.CardId)
            .NotEmpty();

        RuleFor(q => q.UserId)
            .NotEmpty();

        RuleFor(q => q.DeckId)
            .NotEmpty();

        RuleFor(q => q.Side)
            .NotEmpty()
            .MaximumLength(validationSettings.SideNameLength);
    }
}