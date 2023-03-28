namespace ProCardsNew.Infrastructure.Settings;

public class EmailSettings
{
    public const string SectionName = "EmailSettings";
    
    public string From { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string EmailServiceUrl { get; init; } = null!;
}