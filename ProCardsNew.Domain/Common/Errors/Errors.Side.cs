using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Side
    {
        public static Error NotFound => Error.NotFound(
            code: "Side.NotFound",
            description: "Side not found.");
    }
}