namespace ProCardsNew.Application.Common.Settings;

public class LockoutSettings
{
    public const string SectionName = "LockoutSettings";
    
    public int AccessFailedMaxCountInclusive { get; init; } 
    public int LockoutMinutes { get; init; }
    
    public int PasswordRecoveryFailMaxCountInclusive { get; init; }
}