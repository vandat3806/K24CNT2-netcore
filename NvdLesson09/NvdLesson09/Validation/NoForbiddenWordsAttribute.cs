using System.ComponentModel.DataAnnotations;

namespace NvdLesson09.Validation;

[AttributeUsage(AttributeTargets.Property)]
public sealed class NoForbiddenWordsAttribute : ValidationAttribute
{
    private readonly string[] _forbiddenWords;

    public NoForbiddenWordsAttribute(params string[] forbiddenWords)
    {
        _forbiddenWords = forbiddenWords;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not string text || string.IsNullOrWhiteSpace(text))
        {
            return ValidationResult.Success;
        }

        var matchedWord = _forbiddenWords.FirstOrDefault(word =>
            text.Contains(word, StringComparison.OrdinalIgnoreCase));

        return matchedWord is null
            ? ValidationResult.Success
            : new ValidationResult(
                ErrorMessage ?? $"Mô tả chứa từ không được phép: “{matchedWord}”.",
                validationContext.MemberName is null ? null : [validationContext.MemberName]);
    }
}
