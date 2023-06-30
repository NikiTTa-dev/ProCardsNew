using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Deck
    {
        public static Error DuplicateName => Error.Conflict(
            code: "Deck.DuplicateName",
            description: "User already have deck with given name.");
        
        public static Error DuplicateCard => Error.Conflict(
            code: "Deck.DuplicateCard",
            description: "Deck already have this card.");
        
        public static Error Duplicate => Error.Conflict(
            code: "Deck.Duplicate",
            description: "User already have this deck.");
        
        public static Error YouAreOwner => Error.Conflict(
            code: "Deck.YouAreOwner",
            description: "Owner can not delete his deck.");
        
        public static Error AlreadyRemoved => Error.Conflict(
            code: "Deck.AlreadyRemoved",
            description: "Source already been removed.");
        
        public static Error NotFound => Error.NotFound(
            code: "Deck.NotFound",
            description: "Deck not found.");
        
        public static Error InvalidCredentials => Error.Conflict(
            code: "Deck.InvalidCredentials",
            description: "Invalid credentials.");
    }
}