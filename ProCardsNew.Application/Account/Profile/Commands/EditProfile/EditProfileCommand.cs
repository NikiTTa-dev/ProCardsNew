using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Account.Profile.Commands.EditProfile;

public record EditProfileCommand(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Location,
    int AvatarNumber)
    : IRequest<ErrorOr<EditProfileCommandResult>>;