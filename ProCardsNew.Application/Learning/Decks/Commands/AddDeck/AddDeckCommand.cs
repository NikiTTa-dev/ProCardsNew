using ErrorOr;
using MediatR;
using ProCardsNew.Application.Learning.Decks.Common;

namespace ProCardsNew.Application.Learning.Decks.Commands.AddDeck;

public record AddDeckCommand(
        Guid UserId,
        Guid DeckId,
        string Password)
    : IRequest<ErrorOr<DeckResult>>;