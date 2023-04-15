using ErrorOr;
using MediatR;

namespace ProCardsNew.Application.Service.Images.Commands.AddCardImage;

public record AddCardImageCommand(
    Guid UserId,
    Guid CardId,
    string Side,
    Stream Data,
    string Name,
    string FileExtension)
    : IRequest<ErrorOr<AddCardImageCommandResult>>;