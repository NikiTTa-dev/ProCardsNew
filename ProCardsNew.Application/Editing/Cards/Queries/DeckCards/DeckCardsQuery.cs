using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Cards.Queries.DeckCards;

public record DeckCardsQuery(
    Guid UserId,
    Guid DeckId,
    string SearchQuery)
    : IRequest<ErrorOr<DeckCardsQueryResult>>;