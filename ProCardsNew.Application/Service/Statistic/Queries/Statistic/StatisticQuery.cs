using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Service.Statistic.Queries.Statistic;

public record StatisticQuery : IRequest<ErrorOr<StatisticQueryResult>>;