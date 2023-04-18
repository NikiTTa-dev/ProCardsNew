using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Queries.UserDecksToEdit;

public class UserDecksToEditQueryHandler :
    IRequestHandler<UserDecksToEditQuery, ErrorOr<UserDecksToEditQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;

    public UserDecksToEditQueryHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<UserDecksToEditQueryResult>> Handle(UserDecksToEditQuery query,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(query.UserId)) is not { } user)
            return Errors.User.NotFound;

        var decks = await _deckRepository.GetByOwnerIdWhereAsync(
            userId: user.Id,
            filter: d => d.Name
                .ToUpper()
                .Contains(query.SearchQuery.ToUpper()),
            orderByDesc: d => d.UpdatedAtDateTime);

        return new UserDecksToEditQueryResult(
            decks.ConvertAll(d => new DeckPreview(
                DeckId: d.Id.Value,
                Name: d.Name,
                Description: d.Description,
                IsPrivate: !d.IsPublic)));
    }
}