using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByLogin(string login);

    User? GetUserById(Guid id);
}