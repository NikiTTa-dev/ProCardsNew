namespace ProCardsNew.Contracts.Account.Authentication;

public record LoginRequest(
    string Login,
    string Password);