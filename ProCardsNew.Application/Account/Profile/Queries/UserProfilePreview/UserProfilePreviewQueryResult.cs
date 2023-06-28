namespace ProCardsNew.Application.Account.Profile.Queries.UserProfilePreview;

public record UserProfilePreviewQueryResult(
    string Login,
    string Location,
    int CardsViewed,
    float Hours,
    int CardsCreated,
    int Score,
    int AvatarNumber);