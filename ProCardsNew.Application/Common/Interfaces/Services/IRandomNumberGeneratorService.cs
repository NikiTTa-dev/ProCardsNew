namespace ProCardsNew.Application.Common.Interfaces.Services;

public interface IRandomNumberGeneratorService
{
    public int GenerateRandom(int minInclusive, int maxExclusive);
    public int GeneratePasswordRecoveryCode();
}