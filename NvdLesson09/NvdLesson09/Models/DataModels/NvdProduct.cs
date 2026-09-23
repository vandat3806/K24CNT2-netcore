using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NvdLesson09.Validation;

namespace NvdLesson09.Models.DataModels;

public sealed class NvdProduct
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm là bắt buộc.")]
    [StringLength(150, MinimumLength = 6, ErrorMessage = "Tên sản phẩm dài từ {2} đến {1} ký tự.")]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ảnh sản phẩm là bắt buộc.")]
    [Display(Name = "Ảnh sản phẩm")]
    public string Image { get; set; } = string.Empty;

    [Required(ErrorMessage = "Giá gốc là bắt buộc.")]
    [Range(typeof(decimal), "100000", "1000000000", ErrorMessage = "Giá gốc phải từ 100.000 đến 1.000.000.000 đồng.")]
    [DataType(DataType.Text)]
    [Display(Name = "Giá gốc")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Giá khuyến mãi là bắt buộc.")]
    [Range(typeof(decimal), "0", "1000000000", ErrorMessage = "Giá khuyến mãi không được âm.")]
    [DiscountPrice(nameof(Price), 10)]
    [DataType(DataType.Text)]
    [Display(Name = "Giá khuyến mãi")]
    public decimal SalePrice { get; set; }

    [Required(ErrorMessage = "Mô tả sản phẩm là bắt buộc.")]
    [StringLength(1500, ErrorMessage = "Mô tả không được vượt quá {1} ký tự.")]
    [NoForbiddenWords("admin", "spam", "fake")]
    [DataType(DataType.MultilineText)]
    [Display(Name = "Mô tả")]
    public string Description { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [ValidateNever]
    public NvdCategory Category { get; set; } = new();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
