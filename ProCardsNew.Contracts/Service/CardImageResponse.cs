namespace ProCardsNew.Contracts.Service;

public record CardImageResponse(
    byte[] Data,
    string FileExtension);