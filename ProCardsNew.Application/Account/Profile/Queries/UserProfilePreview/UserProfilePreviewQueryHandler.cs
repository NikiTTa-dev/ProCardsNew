using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;

public class UserProfilePreviewQueryHandler
    : IRequestHandler<UserProfilePreviewQuery, ErrorOr<UserProfilePreviewQueryResult>>
{
    private readonly IUserRepository _userRepository;

    public UserProfilePreviewQueryHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserProfilePreviewQueryResult>> Handle(
        UserProfilePreviewQuery query,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByIdIncludeAsync(
                UserId.Create(query.UserId),
                u => u.Statistic);

        if (user is null 
            || user.Statistic is null)
            return Errors.User.NotFound;

        return new UserProfilePreviewQueryResult(
            Login: user.Login,
            Location: user.Location,
            CardsViewed: user.Statistic.CardsViewed,
            Hours: user.Statistic.Hours,
            CardsCreated: user.Statistic.CardsCreated,
            Score: user.Statistic.Score);
    }
}