using Mapster;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Application.Account.PasswordRecovery.Queries.PasswordRecoveryCode;
using ProCardsNew.Contracts.Account.PasswordRecovery;

namespace ProCardsNew.Api.Common.Mapping;

public class PasswordRecoveryMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PasswordRecoveryRequest, PasswordRecoveryCommand>();
        config.NewConfig<PasswordRecoveryCodeRequest, PasswordRecoveryCodeQuery>();
        config.NewConfig<PasswordRecoveryNewPasswordRequest, PasswordRecoveryNewPasswordCommand>();
        
        config.NewConfig<PasswordRecoveryResult, PasswordRecoveryResponse>();
    }
}