using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Editing.Decks.Commands.CreateDeck;

public class CreateDeckCommandHandler
    : IRequestHandler<CreateDeckCommand, ErrorOr<CreateDeckCommandResult>>
{
    private readonly IUserRepository _userRepository;

    public CreateDeckCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<CreateDeckCommandResult>> Handle(CreateDeckCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        
        
        return new CreateDeckCommandResult(Guid.NewGuid());
    }
}