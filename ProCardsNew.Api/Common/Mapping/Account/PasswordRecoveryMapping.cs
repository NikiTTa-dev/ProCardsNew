using Mapster;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecovery;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryCode;
using ProCardsNew.Application.Account.PasswordRecovery.Commands.PasswordRecoveryNewPassword;
using ProCardsNew.Application.Account.PasswordRecovery.Common;
using ProCardsNew.Contracts.Account.PasswordRecovery;
using ProCardsNew.Contracts.Common;

namespace ProCardsNew.Api.Common.Mapping.Account;

public class PasswordRecoveryMapping: IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<PasswordRecoveryRequest, PasswordRecoveryCommand>();
        config.NewConfig<PasswordRecoveryCodeRequest, PasswordRecoveryCodeCommand>();
        config.NewConfig<PasswordRecoveryNewPasswordRequest, PasswordRecoveryNewPasswordCommand>();
        
        config.NewConfig<PasswordRecoveryResult, ResultResponse>();
    }
}