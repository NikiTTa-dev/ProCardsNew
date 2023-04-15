using Mapster;
using ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;
using ProCardsNew.Contracts.Account.Profile;

namespace ProCardsNew.Api.Common.Mapping.Account;

public class ProfileMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserProfilePreviewRequest, UserProfilePreviewQuery>();
        config.NewConfig<UserProfilePreviewQueryResult, UserProfilePreviewResponse>();
    }
}