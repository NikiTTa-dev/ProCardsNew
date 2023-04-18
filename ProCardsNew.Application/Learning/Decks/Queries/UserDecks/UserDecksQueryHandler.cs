using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Learning.Decks.Queries.UserDecks;

public class UserDecksQueryHandler
    : IRequestHandler<UserDecksQuery, ErrorOr<UserDecksQueryResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;

    public UserDecksQueryHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
    }
    
    public async Task<ErrorOr<UserDecksQueryResult>> Handle(UserDecksQuery query, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(query.UserId)) is not { } user)
            return Errors.User.NotFound;

        var decks = await _deckRepository.GetUserDecks(user.Id, query.SearchQuery);

        return new UserDecksQueryResult(
            decks.ConvertAll(d => new UserDeckPreview(
                DeckId: d.Id.Value,
                Name: d.Name,
                IsOwner: d.OwnerId == user.Id)));
    }
}