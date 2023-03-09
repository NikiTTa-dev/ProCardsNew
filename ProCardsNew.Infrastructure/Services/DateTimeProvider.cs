using ProCardsNew.Application.Common.Interfaces.Services;

namespace ProCardsNew.Infrastructure.Services;

public class DateTimeProvider: IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}