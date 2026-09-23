using System.ComponentModel.DataAnnotations;

namespace NvdLesson09.Validation;

[AttributeUsage(AttributeTargets.Property)]
public sealed class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly long _maximumBytes;

    public MaxFileSizeAttribute(long maximumBytes)
    {
        _maximumBytes = maximumBytes;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file || file.Length <= _maximumBytes)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            ErrorMessage ?? $"Dung lượng ảnh không được vượt quá {_maximumBytes / 1024 / 1024} MB.",
            validationContext.MemberName is null ? null : [validationContext.MemberName]);
    }
}
