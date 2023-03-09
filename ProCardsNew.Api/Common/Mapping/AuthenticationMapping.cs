using Mapster;
using ProCardsNew.Application.Account.Authentication.Commands.Register;
using ProCardsNew.Application.Account.Authentication.Common;
using ProCardsNew.Application.Account.Authentication.Queries.Login;
using ProCardsNew.Contracts.Account.Authentication;

namespace ProCardsNew.Api.Common.Mapping;

public class AuthenticationMappingConfig: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>();
        config.NewConfig<LoginRequest, LoginQuery>();
        
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest.Id, src=>src.User.Id.Value)
            .Map(dest => dest.Login, src => src.User.NormalizedLogin)
            .Map(dest => dest.Email, src => src.User.NormalizedEmail)
            .Map(dest => dest, src => src.User);
    }
}