using System.ComponentModel.DataAnnotations;

namespace NvdLesson08.Models.ViewModels;

public sealed class NvdMemberCreateViewModel : NvdMemberFormViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
    [StringLength(64, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 đến 64 ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Mật khẩu xác nhận không khớp.")]
    [Display(Name = "Xác nhận mật khẩu")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
