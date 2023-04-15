using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;

public class CreateDeckCommandHandler
    : IRequestHandler<CreateDeckCommand, ErrorOr<CreateDeckCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;

    public CreateDeckCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<CreateDeckCommandResult>> Handle(CreateDeckCommand command,
        CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        if (await _userRepository.GetByIdAsync(userId) is not {} user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByNameAsync(userId, command.Name) is not null)
            return Errors.Deck.DuplicateName;
        
        var deck = Deck.Create(
            name: command.Name,
            description: command.Description,
            ownerId: userId);
        
        user.AddDeck(deck);
        await _userRepository.SaveChangesAsync();

        return new CreateDeckCommandResult(deck.Id.Value);
    }
}