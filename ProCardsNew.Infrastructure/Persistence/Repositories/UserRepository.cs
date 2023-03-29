using Microsoft.EntityFrameworkCore;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.UserAggregate;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private readonly ProCardsDbContext _dbContext;

    public UserRepository(ProCardsDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }
    
    public async Task<User?> GetUserByLoginAsync(string normalizedLogin)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.NormalizedLogin == normalizedLogin);
    }

    public async Task<User?> GetUserByIdAsync(UserId id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string normalizedEmail)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(u => u.NormalizedEmail == normalizedEmail);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}