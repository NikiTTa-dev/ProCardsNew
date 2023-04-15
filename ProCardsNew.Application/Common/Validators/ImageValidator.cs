﻿using FluentValidation;

namespace ProCardsNew.Application.Common.Validators;

public static class ImageValidator
{
    public static IRuleBuilderOptions<T, Stream> FileLength<T>(
        this IRuleBuilder<T, Stream> ruleBuilder)
    {
        return ruleBuilder
            .Must(c => c.Length <= 3145728)
            .WithMessage("File is too large.");
    }
    public static IRuleBuilderOptions<T, string> ImageFileExtension<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(c => 
                c == "image/jpeg"
                || c == "image/png")
            .WithMessage("Wrong file extension.");
    }
}