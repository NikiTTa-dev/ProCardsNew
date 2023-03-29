using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Email
    {
        public static Error EmailSendingFailure => Error.Failure(
            code: "Email.EmailSendingFailure",
            description: "An error occurred while sending the email.");
    }
}