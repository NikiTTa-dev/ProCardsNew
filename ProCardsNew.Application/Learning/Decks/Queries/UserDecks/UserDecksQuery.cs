using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Learning.Decks.Queries.UserDecks;

public record UserDecksQuery(
    Guid UserId,
    string SearchQuery)
    : IRequest<ErrorOr<UserDecksQueryResult>>;