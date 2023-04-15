namespace ProCardsNew.Application.Account.Profile.Queries.UserProfile;

public record UserProfileQueryResult(
    string FirstName,
    string LastName,
    string Email,
    string Location);