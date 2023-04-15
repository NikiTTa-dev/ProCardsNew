using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Service.Images.Commands.DeleteCardImage;

public record DeleteCardImageCommand(
    Guid UserId,
    Guid CardId,
    string Side)
    : IRequest<ErrorOr<DeleteCardImageCommandResult>>;