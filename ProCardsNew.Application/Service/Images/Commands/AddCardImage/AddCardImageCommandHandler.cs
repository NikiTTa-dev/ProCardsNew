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
    private readonly IImageRepository _imageRepository;

    public AddCardImageCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository,
        IImageRepository imageRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
        _imageRepository = imageRepository;
    }

    public async Task<ErrorOr<AddCardImageCommandResult>> Handle(
        AddCardImageCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _imageRepository.SideExists("Front") || !await _imageRepository.SideExists("Back"))
        {
            await _imageRepository.InsertSide(Side.Create("Front"));
            await _imageRepository.InsertSide(Side.Create("Back"));
            await _imageRepository.SaveChangesAsync();
        }

        if (await _cardRepository.GetSideByNameAsync(command.Side) is not { } side)
            return Errors.Side.NotFound;

        if (await _userRepository.GetByIdAsync(UserId.Create(command.UserId)) is not { } user)
            return Errors.User.NotFound;

        var cardId = CardId.Create(command.CardId);
        if (await _cardRepository.GetByIdAsync(cardId) is not { } card)
            return Errors.Deck.NotFound;

        if (card.OwnerId != user.Id)
            return Errors.User.AccessDenied;

        if (await _cardRepository.HasImageAsync(cardId, command.Side))
            return Errors.Image.HasImage;

        using (var stream = new MemoryStream())
        {
            await command.Data.CopyToAsync(stream, cancellationToken);
            var image = Image.Create(
                sideId: side.Id,
                cardId: card.Id,
                name: command.Name,
                fileExtension: command.FileExtension,
                data: stream.ToArray());

            card.AddImage(image);
            await _cardRepository.SaveChangesAsync();
        }

        await command.Data.DisposeAsync();

        return new AddCardImageCommandResult();
    }
}