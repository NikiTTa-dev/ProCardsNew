using ProCardsNew.Domain.UserAggregate;

namespace ProCardsNew.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    void Add(User user);
    User? GetUserByLogin(string login);
}