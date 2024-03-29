﻿using ErrorOr;

namespace ProCardsNew.Domain.Common.Errors;

public static partial class Errors
{
    public static class Card
    {
        public static Error NotFound => Error.NotFound(
            code: "Card.NotFound",
            description: "Card not found.");
        public static Error AlreadyExists => Error.Conflict(
            code: "Card.AlreadyExists",
            description: "Card already exists.");
    }
}