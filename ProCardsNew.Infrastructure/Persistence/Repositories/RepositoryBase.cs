namespace ProCardsNew.Infrastructure.Persistence.Repositories;

public abstract class RepositoryBase
{
    protected readonly ProCardsDbContext DbContext;

    protected RepositoryBase(
        ProCardsDbContext dbContext)
    {
        DbContext = dbContext;
    }
}