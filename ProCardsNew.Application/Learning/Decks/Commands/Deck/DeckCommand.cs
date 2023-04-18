using ErrorOr;
using MediatR;
using ProCardsNew.Application.Learning.Decks.Common;

namespace ProCardsNew.Application.Learning.Decks.Commands.Deck;

public record DeckCommand(
    Guid UserId,
    Guid DeckId)
    : IRequest<ErrorOr<DeckResult>>;