namespace ProCardsNew.Contracts.Common;

public record CardResponse(
    Guid Id,
    string FrontSide,
    string BackSide,
    bool HasFrontImage,
    bool HasBackImage);