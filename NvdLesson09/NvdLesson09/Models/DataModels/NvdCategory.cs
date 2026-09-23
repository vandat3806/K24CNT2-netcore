using System.ComponentModel.DataAnnotations;

namespace NvdLesson09.Models.DataModels;

public sealed class NvdCategory
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên danh mục là bắt buộc.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên danh mục dài từ {2} đến {1} ký tự.")]
    [Display(Name = "Danh mục")]
    public string Name { get; set; } = string.Empty;
}
