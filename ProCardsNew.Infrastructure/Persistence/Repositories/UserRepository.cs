using System.Linq.Expressions;
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
    
    public async Task<User?> GetByLoginAsync(string normalizedLogin)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Login.ToUpper() == normalizedLogin);
    }

    public async Task<User?> GetByIdAsync(
        UserId id,
        params Expression<Func<User, object?>>[] includeProperties)
    {
        var users = _dbContext.Users.AsQueryable();

        foreach (var property in includeProperties)
        {
            users = users.Include(property);
        }
        
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string normalizedEmail)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToUpper() == normalizedEmail);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}