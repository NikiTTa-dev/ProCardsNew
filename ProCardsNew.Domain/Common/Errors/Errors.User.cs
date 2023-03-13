using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateLogin => Error.Conflict(
            code: "User.DuplicateLogin",
            description: "Login must be unique value.");

        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User with given credentials not found.");
    }
}