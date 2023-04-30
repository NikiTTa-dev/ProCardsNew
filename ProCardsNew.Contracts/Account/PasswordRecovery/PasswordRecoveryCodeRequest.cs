namespace ProCardsNew.Contracts.Account.PasswordRecovery;

public record PasswordRecoveryCodeRequest(
    string Login,
    string Code);