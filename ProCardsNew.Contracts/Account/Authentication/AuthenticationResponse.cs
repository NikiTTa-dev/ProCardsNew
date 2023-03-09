namespace ProCardsNew.Contracts.Account.Authentication;

public record AuthenticationResponse(
    Guid Id,
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Token);