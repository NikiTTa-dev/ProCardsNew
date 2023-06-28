using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Account.Profile.Commands.EditProfile;

public class EditProfileCommandHandler
    : IRequestHandler<EditProfileCommand, ErrorOr<EditProfileCommandResult>>
{
    private readonly IUserRepository _userRepository;

    public EditProfileCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<EditProfileCommandResult>> Handle(
        EditProfileCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;
        
        user.Edit(
            firstName: command.FirstName,
            lastName: command.LastName,
            email: command.Email,
            location: command.Location,
            avatarNumber: command.AvatarNumber);
        
        await _userRepository.SaveChangesAsync();
        
        return new EditProfileCommandResult();
    }
}