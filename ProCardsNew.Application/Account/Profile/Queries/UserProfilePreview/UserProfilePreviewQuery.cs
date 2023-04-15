using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;

public record UserProfilePreviewQuery(
    Guid UserId)
    : IRequest<ErrorOr<UserProfilePreviewQueryResult>>;