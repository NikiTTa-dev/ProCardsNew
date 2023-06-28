namespace ProCardsNew.Contracts.Account.Profile;

public record EditProfileRequest(
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string Location,
    int AvatarNumber);