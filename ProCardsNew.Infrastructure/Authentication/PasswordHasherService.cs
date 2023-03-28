using Microsoft.AspNetCore.Identity;
using ProCardsNew.Application.Common.Interfaces.Authentication;
using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Infrastructure.Authentication;

public class PasswordHasherService: IPasswordHasherService
{
    private readonly IPasswordHasher<User> _passwordHasher;

    public PasswordHasherService(IPasswordHasher<User> passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }
    
    public string GeneratePasswordHash(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public PasswordVerificationResult VerifyPasswordHash(string hashedPassword, string providedPassword)
    {
        return _passwordHasher.VerifyHashedPassword(null!, hashedPassword, providedPassword);
    }
}