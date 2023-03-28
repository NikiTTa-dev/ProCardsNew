using Microsoft.AspNetCore.Identity;

namespace ProCardsNew.Application.Common.Interfaces.Authentication;

public interface IPasswordHasherService
{
    string GeneratePasswordHash(string password);
    PasswordVerificationResult VerifyPasswordHash(string hashedPassword, string providedPassword);
}