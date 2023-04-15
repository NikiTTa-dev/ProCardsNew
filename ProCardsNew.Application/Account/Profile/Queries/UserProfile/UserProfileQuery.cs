using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Account.Profile.Queries.UserProfile;

public record UserProfileQuery(
    Guid UserId)
    : IRequest<ErrorOr<UserProfileQueryResult>>;