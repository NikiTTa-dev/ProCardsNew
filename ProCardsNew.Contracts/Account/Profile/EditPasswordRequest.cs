namespace ProCardsNew.Contracts.Account.Profile;

public record EditPasswordRequest(
    Guid UserId,
    string OldPassword,
    string NewPassword);