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

    public AddCardImageCommandHandler(
        IUserRepository userRepository,
        ICardRepository cardRepository)
    {
        _userRepository = userRepository;
        _cardRepository = cardRepository;
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

        using (var stream = new MemoryStream())
        {
            await command.Data.CopyToAsync(stream, cancellationToken);
            var image = Image.Create(
                cardId: card.Id,
                name: command.Name,
                fileExtension: command.FileExtension,
                data: ImageData.Create(stream.ToArray()));

            card.AddImage(image, command.Side);
            await _cardRepository.SaveChangesAsync();
        }

        await command.Data.DisposeAsync();

        return new AddCardImageCommandResult();
    }
}