using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Learning.Decks.Commands.RemoveDeckFromLatest;

public class RemoveDeckFromLatestCommandHandler
    : IRequestHandler<RemoveDeckFromLatestCommand, ErrorOr<RemoveDeckFromLatestCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;

    public RemoveDeckFromLatestCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<RemoveDeckFromLatestCommandResult>> Handle(
        RemoveDeckFromLatestCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (!await _deckRepository.HasAccess(deck.Id, user.Id))
            return Errors.User.AccessDenied;

        if (await _deckRepository.GetUserDeck(deck.Id, user.Id) is not { } userDeck)
            return Errors.Deck.AlreadyRemoved;
        
        _deckRepository.DeleteUserDeck(userDeck);
        await _deckRepository.SaveChangesAsync();
        
        return new RemoveDeckFromLatestCommandResult();
    }
}