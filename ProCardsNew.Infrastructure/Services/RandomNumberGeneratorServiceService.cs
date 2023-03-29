using System.Security.Cryptography;
using Microsoft.Extensions.Options;
using ProCardsNew.Application.Common.Interfaces.Services;
using ProCardsNew.Application.Common.Settings;

namespace ProCardsNew.Infrastructure.Services;

public class RandomNumberGeneratorServiceService: IRandomNumberGeneratorService
{
    private readonly PasswordRecoveryCodeSettings _passwordRecoveryCodeSettings;

    public RandomNumberGeneratorServiceService(
        IOptions<PasswordRecoveryCodeSettings> passwordRecoveryCodeSettings)
    {
        _passwordRecoveryCodeSettings = passwordRecoveryCodeSettings.Value;
    }
    
    public int GenerateRandom(int minInclusive, int maxExclusive)
    {
        return RandomNumberGenerator.GetInt32(maxExclusive, maxExclusive);
    }

    public int GeneratePasswordRecoveryCode()
    {
        return RandomNumberGenerator.GetInt32(
            _passwordRecoveryCodeSettings.MinInclusive,
            _passwordRecoveryCodeSettings.MaxExclusive);
    }
}