using Mapster;
using ProCardsNew.Application.Account.Authentication.Commands.Login;
using ProCardsNew.Application.Account.Authentication.Commands.Refresh;
using ProCardsNew.Application.Account.Authentication.Commands.Register;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Contracts.Account.Authentication;

namespace ProCardsNew.Api.Common.Mapping;

public class AuthenticationMappingConfig: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginCommand>();
        config.NewConfig<RefreshTokenRequest, RefreshTokenCommand>();

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Id, src=>src.User.Id.Value)
            .Map(dest => dest, src => src.User);
    }
}