using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Learning.Decks.Commands.RemoveDeckFromLatest;

public record RemoveDeckFromLatestCommand(
    Guid UserId,
    Guid DeckId)
    : IRequest<ErrorOr<RemoveDeckFromLatestCommandResult>>;