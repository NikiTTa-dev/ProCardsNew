using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.DeckAggregate.ValueObjects;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Commands.EditDeckPassword;

public class EditDeckPasswordCommandHandler
    : IRequestHandler<EditDeckPasswordCommand, ErrorOr<EditDeckPasswordCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IDeckRepository _deckRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public EditDeckPasswordCommandHandler(
        IUserRepository userRepository,
        IDeckRepository deckRepository,
        IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _deckRepository = deckRepository;
        _passwordHasherService = passwordHasherService;
    }

    public async Task<ErrorOr<EditDeckPasswordCommandResult>> Handle(EditDeckPasswordCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not {} user)
            return Errors.User.NotFound;
        
        if (await _deckRepository.GetByIdAsync(DeckId.Create(command.DeckId)) is not { } deck)
            return Errors.Deck.NotFound;

        if (deck.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        string? passwordHash = null;
        if (!command.IsPrivate)
            passwordHash = _passwordHasherService.GeneratePasswordHash(command.Password);

        deck.EditPassword(
            isPublic: !command.IsPrivate,
            passwordHash: passwordHash);
        await _deckRepository.SaveChangesAsync();

        return new EditDeckPasswordCommandResult();
    }
}