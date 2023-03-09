namespace ProCardsNew.Contracts.Account.Authentication;

public record RegisterRequest(
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Location,
    string Password);