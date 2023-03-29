namespace ProCardsNew.Contracts.Account.PasswordRecovery;

public record PasswordRecoveryCodeRequest(
    string Email,
    string Code);