namespace ProCardsNew.Infrastructure.Settings;

public static class DbContextEntitiesSettings
{
    public const int CardSideLength = 300;
    
    public const int DeckNameLength = 40;
    public const int DeckDescriptionLength = 300;
    public const int DeckPasswordHashLength = 128;

    public const int UserLoginLength = 30;
    public const int UserEmailLength = 100;
    public const int UserNormalizedLoginLength = UserLoginLength;
    public const int UserNormalizedEmailLength = UserEmailLength;
    public const int UserFirstNameLength = 50;
    public const int UserLastNameLength = 50;
    public const int UserLocationLength = 50;
    public const int UserRefreshTokenLength = 50;
    public const int UserRecoveryCodeLength = 50;
    public const int UserPasswordHashLength = 128;

    public const int ImageNameLength = 30;
    public const int ImageFileExtensionLength = 10;

    public const int SideNameLength = 10;
}