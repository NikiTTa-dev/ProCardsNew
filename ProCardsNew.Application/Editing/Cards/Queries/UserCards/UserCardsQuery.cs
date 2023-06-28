using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Cards.Queries.UserCards;

public record UserCardsQuery(
    Guid UserId,
    string SearchQuery)
    : IRequest<ErrorOr<UserCardsQueryResult>>;