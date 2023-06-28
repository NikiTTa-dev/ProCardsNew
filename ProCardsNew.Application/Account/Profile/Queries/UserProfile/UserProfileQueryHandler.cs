using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Account.Profile.Queries.UserProfile;

public class UserProfileQueryHandler
    : IRequestHandler<UserProfileQuery, ErrorOr<UserProfileQueryResult>>
{
    private readonly IUserRepository _userRepository;

    public UserProfileQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<UserProfileQueryResult>> Handle(
        UserProfileQuery query,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(query.UserId)) is not { } user)
            return Errors.User.NotFound;

        return new UserProfileQueryResult(
            FirstName: user.FirstName,
            LastName: user.LastName,
            Email: user.Email,
            Location: user.Location,
            AvatarNumber: user.AvatarNumber);
    }
}