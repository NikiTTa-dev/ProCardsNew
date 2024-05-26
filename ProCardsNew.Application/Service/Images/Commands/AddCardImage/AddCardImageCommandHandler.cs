using Amazon.S3;
using Amazon.S3.Model;
using ErrorOr;
using MediatR;
using ProCardsNew.Application.Common.Interfaces.Persistence;
using ProCardsNew.Domain.CardAggregate.Entities;
using ProCardsNew.Domain.CardAggregate.ValueObjects;
using ProCardsNew.Domain.Common.Errors;
using ProCardsNew.Domain.UserAggregate.ValueObjects;

namespace ProCardsNew.Application.Service.Images.Commands.AddCardImage;

public class AddCardImageCommandHandler
    : IRequestHandler<AddCardImageCommand, ErrorOr<AddCardImageCommandResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICardRepository _cardRepository;
    private readonly IAmazonS3 _amazonS3;

    public AddCardImageCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository, IAmazonS3 amazonS3)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _amazonS3 = amazonS3;
    }

    public async Task<ErrorOr<AddCardImageCommandResult>> Handle(
        AddCardImageCommand command,
        CancellationToken cancellationToken)
    {
        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        if (await _cardRepository.GetByIdAsync(CardId.Create(command.CardId)) is not { } card)
            return Errors.Deck.NotFound;

        if (card.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        switch (command.Side)
        {
            case "Front":
                if (await _cardRepository.HasFrontImageAsync(card.Id))
                    return Errors.Image.HasImage;
                break;
            case "Back":
                if (await _cardRepository.HasBackImageAsync(card.Id))
                    return Errors.Image.HasImage;
                break;
            default:
                return Errors.Side.NotFound;
        }

        var s3ImageId = Guid.NewGuid();
        await _amazonS3.PutObjectAsync(new PutObjectRequest
        {
            BucketName = "chatgpt-next-web",
            Key = s3ImageId.ToString(),
            InputStream = command.Data
        }, cancellationToken);

        var image = Image.Create(
            cardId: card.Id,
            name: command.Name,
            fileExtension: command.FileExtension,
            S3ImageId: s3ImageId);
        card.AddImage(image, command.Side);
        await _cardRepository.SaveChangesAsync();

        await command.Data.DisposeAsync();

        return new AddCardImageCommandResult();
    }
}