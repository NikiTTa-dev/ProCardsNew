using System.Text.RegularExpressions;
using FluentValidation;

namespace ProCardsNew.Application.Common.Validators;

public static class TextFieldsValidator
{
    public static IRuleBuilderOptions<T, string> ContainsNoMultipleSpaces<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"^(?!.*[\s][\s]+.*$).*").Success)
            .WithMessage("'{PropertyName}' can not contain multiple spaces.");
    }

    public static IRuleBuilderOptions<T, string> ContainsNoEdgeSpaces<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"^(?![\s].*|.*[\s]$).*").Success)
            .WithMessage("'{PropertyName}' can not contain edge spaces.");
    }

    public static IRuleBuilderOptions<T, string> ContainsNoSpaces<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"^(?!.*[\s].*$).*").Success)
            .WithMessage("'{PropertyName}' can not contain spaces.");
    }
    
    public static IRuleBuilderOptions<T, string> ContainsNumbersOnly<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"^(?!.*[^0-9].*$).*").Success)
            .WithMessage("'{PropertyName}' must be only numbers.");
    }
}