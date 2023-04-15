namespace ProCardsNew.Contracts.Account.Profile;

public record UserProfilePreviewResponse(
    string Login,
    string Location,
    int CardsViewed,
    float Hours,
    int CardsCreated,
    int Score);