using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Service.Images.Queries.CardImage;

public record CardImageQuery(
        Guid UserId, 
        Guid DeckId,
        Guid CardId,
        string Side)
    : IRequest<ErrorOr<CardImageQueryResult>>;