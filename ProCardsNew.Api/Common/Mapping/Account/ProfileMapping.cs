using Mapster;
using ProCardsNew.Application.Account.Profile.Commands.EditPassword;
using ProCardsNew.Application.Account.Profile.Commands.EditProfile;
using ProCardsNew.Application.Account.Profile.Queries.UserProfile;
using ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;
using ProCardsNew.Contracts.Account.Profile;
using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Api.Common.Mapping.Account;

public class ProfileMapping : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserProfilePreviewRequest, UserProfilePreviewQuery>();
        config.NewConfig<UserProfilePreviewQueryResult, UserProfilePreviewResponse>();

        config.NewConfig<UserProfileRequest, UserProfileQuery>();
        config.NewConfig<UserProfileQueryResult, UserProfileResponse>();

        config.NewConfig<EditProfileRequest, EditProfileCommand>();
        config.NewConfig<EditProfileCommandResult, ResultResponse>();

        config.NewConfig<EditPasswordRequest, EditPasswordCommand>();
        config.NewConfig<EditPasswordCommandResult, ResultResponse>();
    }
}