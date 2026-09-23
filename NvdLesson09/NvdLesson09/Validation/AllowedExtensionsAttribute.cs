using System.ComponentModel.DataAnnotations;

namespace NvdLesson09.Validation;

[AttributeUsage(AttributeTargets.Property)]
public sealed class AllowedExtensionsAttribute : ValidationAttribute
{
    private readonly HashSet<string> _extensions;

    public AllowedExtensionsAttribute(params string[] extensions)
    {
        _extensions = new HashSet<string>(extensions.Select(extension => extension.ToLowerInvariant()));
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is not IFormFile file)
        {
            return ValidationResult.Success;
        }

        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        return _extensions.Contains(extension)
            ? ValidationResult.Success
            : new ValidationResult(
                ErrorMessage ?? $"Chỉ chấp nhận ảnh có định dạng: {string.Join(", ", _extensions)}.",
                validationContext.MemberName is null ? null : [validationContext.MemberName]);
    }
}
