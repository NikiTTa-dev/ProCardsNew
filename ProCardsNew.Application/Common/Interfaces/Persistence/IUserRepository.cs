using System.Linq.Expressions;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<User?> GetByLoginAsync(string normalizedLogin);
    Task<User?> GetByIdAsync(UserId id);
    Task<User?> GetByEmailAsync(string normalizedEmail);
    Task<User?> GetByIdIncludeAsync(
        UserId id,
        params Expression<Func<User, object?>>[] includeProperties);
    Task SaveChangesAsync();
}