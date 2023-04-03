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
        
        public static Error RecoveryCodeExpired => Error.Conflict(
            code: "User.RecoveryCodeExpired",
            description: "This recovery code already expired.");
        
        public static Error WrongRecoveryCode => Error.Conflict(
            code: "User.WrongRecoveryCode",
            description: "This recovery code is wrong.");
        
        public static Error AccessDenied => Error.Failure(
            code: "User.AccessDenied",
            description: "User doesn't have access to this source.");
    }
}