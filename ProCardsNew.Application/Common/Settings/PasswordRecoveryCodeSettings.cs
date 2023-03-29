namespace ProCardsNew.Application.Common.Settings;

public class PasswordRecoveryCodeSettings
{
    public const string SectionName = "PasswordRecoveryCodeSettings";
    
    public int MinInclusive { get; init; } 
    public int MaxExclusive { get; init; }
    public int ExpirationMinutes { get; init; }
    
}