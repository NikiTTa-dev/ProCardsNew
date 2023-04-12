using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;

public record UserDecksToEditQuery(
    Guid UserId,
    string SearchQuery)
    : IRequest<ErrorOr<UserDecksToEditQueryResult>>;