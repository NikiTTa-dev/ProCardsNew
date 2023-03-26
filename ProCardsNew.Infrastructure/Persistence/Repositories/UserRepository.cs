using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public class UserRepository: IUserRepository
{
    private static readonly List<User> Users = new();
    
    public void Add(User user)
    {
        Users.Add(user);
    }
    
    public User? GetUserByLogin(string login)
    {
        return Users.SingleOrDefault(u => u.NormalizedLogin == login);
    }

    public User? GetUserById(Guid id)
    {
        return Users.SingleOrDefault(u => u.Id.Value == id);
    }
}