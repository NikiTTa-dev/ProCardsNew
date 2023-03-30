using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Authentication
    {
        public static Error InvalidCredentials => Error.Conflict(
            code: "Auth.InvalidCredentials",
            description: "Invalid credentials.");

        public static Error InvalidRefreshToken => Error.Conflict(
            code: "Auth.InvalidRefreshToken",
            description: "Refresh token is not valid.");
        
        public static Error LockedOut => Error.Conflict(
            code: "Auth.LockedOut",
            description: "User locked out.");
    }
}