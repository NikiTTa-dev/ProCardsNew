using ProCardsNew.Domain.Common.Models;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Domain.UserAggregate;

public class User: AggregateRoot<UserId>
{
    public string Login { get; }
    public string Email { get; }
    public string NormalizedLogin { get; } 
    public string NormalizedEmail { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public string Location { get; }
    public string? PasswordRecoveryCode { get; } = null;
    public DateTime? PasswordRecoveryEndDateTime { get; } = null;
    public string PasswordHash { get; }
    public int AccessFailedCount { get; }
    public DateTime? LockoutEndDateTime { get; } = null;
    public DateTime CreatedAtDateTime { get; }
    public DateTime UpdatedAtDateTime { get; }
    
    
    private User(
        UserId id,
        string login,
        string email,
        string normalizedLogin,
        string normalizedEmail,
        string firstName,
        string lastName,
        string location,
        string passwordHash,
        int accessFailedCount,
        DateTime createdAtDateTime,
        DateTime updatedAtDateTime)
        : base(id)
    {
        Login = login;
        Email = email;
        NormalizedLogin = normalizedLogin;
        NormalizedEmail = normalizedEmail;
        FirstName = firstName;
        LastName = lastName;
        Location = location;
        PasswordHash = passwordHash;
        CreatedAtDateTime = createdAtDateTime;
        UpdatedAtDateTime = updatedAtDateTime;
        AccessFailedCount = accessFailedCount;
    }

    public static User Create(
        string login,
        string email,
        string firstName,
        string lastName,
        string location,
        string passwordHash)
    {
        return new(
            UserId.CreateUnique(), 
            login,
            email,
            login.ToUpper(),
            email.ToUpper(),
            firstName,
            lastName,
            location,
            passwordHash,
            0,
            DateTime.UtcNow, 
            DateTime.UtcNow);
    }
}