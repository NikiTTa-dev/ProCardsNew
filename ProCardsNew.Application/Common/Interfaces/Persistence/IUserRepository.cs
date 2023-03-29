using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetUserByLoginAsync(string normalizedLogin);
    Task<User?> GetUserByIdAsync(UserId id);
    Task<User?> GetUserByEmailAsync(string normalizedEmail);
    Task SaveChangesAsync();
}