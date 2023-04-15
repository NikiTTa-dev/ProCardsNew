namespace ProCardsNew.Application.Service.Images.Queries.CardImage;

public record CardImageQueryResult(
    byte[] Data,
    string FileExtension);