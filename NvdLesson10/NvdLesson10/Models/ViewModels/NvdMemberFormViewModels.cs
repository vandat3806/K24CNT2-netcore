using System.ComponentModel.DataAnnotations;

namespace NvdLesson10.Models.ViewModels;

public abstract class NvdMemberFormViewModel
{
    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Tên đăng nhập dài từ {2} đến {1} ký tự.")]
    [RegularExpression("^[a-zA-Z0-9._]+$", ErrorMessage = "Tên đăng nhập chỉ gồm chữ, số, dấu chấm hoặc gạch dưới.")]
    [Display(Name = "Tên đăng nhập")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Họ và tên dài từ {2} đến {1} ký tự.")]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email là bắt buộc.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(100)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Số điện thoại không đúng định dạng.")]
    [RegularExpression("^[0-9]{9,12}$", ErrorMessage = "Số điện thoại gồm 9 đến 12 chữ số.")]
    [Display(Name = "Số điện thoại")]
    public string? Phone { get; set; }

    [Display(Name = "Đang hoạt động")]
    public bool Status { get; set; } = true;
}

public sealed class NvdMemberCreateViewModel : NvdMemberFormViewModel
{
    [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
    [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu dài từ {2} đến {1} ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu")]
    public string Password { get; set; } = string.Empty;
}

public sealed class NvdMemberEditViewModel : NvdMemberFormViewModel
{
    public long Id { get; set; }

    [StringLength(50, MinimumLength = 6, ErrorMessage = "Mật khẩu mới dài từ {2} đến {1} ký tự.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mật khẩu mới")]
    public string? NewPassword { get; set; }
}
