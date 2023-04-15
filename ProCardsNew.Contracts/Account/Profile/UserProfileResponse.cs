namespace ProCardsNew.Contracts.Account.Profile;

public record UserProfileResponse(
    string FirstName,
    string LastName,
    string Email,
    string Location);