using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Commands.EditDeck;

public class EditDeckCommandHandler
    : IRequestHandler<EditDeckCommand, ErrorOr<EditDeckResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;

    public EditDeckCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
    }

    public async Task<ErrorOr<EditDeckResult>> Handle(EditDeckCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _deckRepository.GetByIdAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        if (await _deckRepository.GetByNameAsync(user.Id, command.Name) is { } deckWithGivenName 
            && deck.Id != deckWithGivenName.Id)
            return Errors.Deck.DuplicateName;

        deck.Edit(
            name: command.Name,
            description: command.Description);

        await _deckRepository.SaveChangesAsync();
        
        return new EditDeckResult();
    }
}