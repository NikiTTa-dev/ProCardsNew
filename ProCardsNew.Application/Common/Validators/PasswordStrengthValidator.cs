using System.Text.RegularExpressions;
using FluentValidation;

namespace ProCardsNew.Application.Common.Validators;

public static class PasswordStrengthValidator
{
    public static IRuleBuilderOptions<T, string> ContainsLowerCaseCharacters<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"[a-z]").Success)
            .WithMessage("'{PropertyName}' must contain a-z characters.");
    }
    
    public static IRuleBuilderOptions<T, string> ContainsUpperCaseCharacters<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"[A-Z]").Success)
            .WithMessage("'{PropertyName}' must contain A-Z characters.");
    }
    
    public static IRuleBuilderOptions<T, string> ContainsNumbers<T>(
        this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .Must(p =>
                Regex.Match(p, @"[0-9]").Success)
            .WithMessage("'{PropertyName}' must contain numbers.");
    }
    
    public static IRuleBuilderOptions<T, string> Password<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int minLength,
        int maxLength)
    {
        return ruleBuilder
            .ContainsLowerCaseCharacters()
            .ContainsUpperCaseCharacters()
            .ContainsNumbers()
            .ContainsNoSpaces()
            .MinimumLength(minLength)
            .MaximumLength(maxLength);
    }
}