using ProCardsNew.Application.Common.Interfaces.Repositories;
using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Infrastructure.Repositories;

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
}