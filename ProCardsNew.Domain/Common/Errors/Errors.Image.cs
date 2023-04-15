using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Image
    {
        public static Error HasImage => Error.Conflict(
            code: "Image.HasImage",
            description: "Card already has image.");
        
        public static Error NotFound => Error.NotFound(
            code: "Image.NotFound",
            description: "Image not found.");
    }
}