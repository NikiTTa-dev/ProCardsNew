using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Email
    {
        public static Error EmailSendingFailure => Error.Conflict(
            code: "Email.EmailSendingFailure",
            description: "An error occurred while sending the email.");
        
        public static Error EmailAlreadySent => Error.Conflict(
            code: "Email.EmailAlreadySent",
            description: "This user already sent recovery email.");
    }
}