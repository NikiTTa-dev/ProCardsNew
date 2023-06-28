using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Service.Images.Commands.DeleteCardImage;

public class DeleteCardImageCommandHandler
    : IRequestHandler<DeleteCardImageCommand, ErrorOr<DeleteCardImageCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IImageRepository _imageRepository;

    public DeleteCardImageCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IImageRepository imageRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _imageRepository = imageRepository;
    }
    
    public async Task<ErrorOr<DeleteCardImageCommandResult>> Handle(
        DeleteCardImageCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _cardRepository.GetByIdAsync(CardId.Create(command.CardId)) is not { } card)
            return Errors.Card.NotFound;

        if (card.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        Image? image;
        switch (command.Side)
        {
            case "Front":
                image = await _imageRepository.GetFrontImageByCardId(card.Id);
                if (image is null)
                    return Errors.Image.NotFound;
                break;
            case "Back":
                image = await _imageRepository.GetBackImageByCardId(card.Id);
                if (image is null)
                    return Errors.Image.NotFound;
                break;
            default:
                return Errors.Side.NotFound;
        }
        
        _imageRepository.DeleteImage(image);
        await _imageRepository.SaveChangesAsync();

        return new DeleteCardImageCommandResult();
    }
}