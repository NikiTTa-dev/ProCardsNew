namespace ProCardsNew.Contracts.Account.PasswordRecovery;

public record PasswordRecoveryNewPasswordRequest(
    string Code,
    string Password,
    string Login);