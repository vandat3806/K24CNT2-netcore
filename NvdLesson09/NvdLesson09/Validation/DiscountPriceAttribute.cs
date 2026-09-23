using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NvdLesson09.Validation;

[AttributeUsage(AttributeTargets.Property)]
public sealed class DiscountPriceAttribute : ValidationAttribute
{
    private readonly string _originalPriceProperty;
    private readonly decimal _minimumDiscountPercent;

    public DiscountPriceAttribute(string originalPriceProperty, double minimumDiscountPercent)
    {
        _originalPriceProperty = originalPriceProperty;
        _minimumDiscountPercent = (decimal)minimumDiscountPercent;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success;
        }

        var property = validationContext.ObjectType.GetProperty(_originalPriceProperty);
        if (property is null)
        {
            return new ValidationResult($"Không tìm thấy thuộc tính {_originalPriceProperty}.");
        }

        var originalValue = property.GetValue(validationContext.ObjectInstance);
        if (originalValue is null ||
            !decimal.TryParse(Convert.ToString(originalValue, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out var originalPrice) ||
            !decimal.TryParse(Convert.ToString(value, CultureInfo.InvariantCulture), NumberStyles.Number, CultureInfo.InvariantCulture, out var salePrice))
        {
            return ValidationResult.Success;
        }

        var maximumSalePrice = originalPrice * (1 - (_minimumDiscountPercent / 100));
        if (salePrice <= maximumSalePrice)
        {
            return ValidationResult.Success;
        }

        return new ValidationResult(
            ErrorMessage ?? $"Giá khuyến mãi phải thấp hơn giá gốc ít nhất {_minimumDiscountPercent:0}% (không vượt quá {maximumSalePrice:N0} đồng).",
            validationContext.MemberName is null ? null : [validationContext.MemberName]);
    }
}
