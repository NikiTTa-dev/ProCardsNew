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
    
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }
    
    public User? GetUserByLogin(string login)
    {
        return _dbContext.Users.SingleOrDefault(u => u.NormalizedLogin == login);
    }

    public User? GetUserById(UserId id)
    {
        return _dbContext.Users.SingleOrDefault(u => u.Id == id);
    }

    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
}