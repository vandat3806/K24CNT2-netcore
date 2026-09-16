using System.ComponentModel.DataAnnotations;
using NvdLesson08.Models.DataModels;

namespace NvdLesson08.Models.ViewModels;

public abstract class NvdMemberFormViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
    [StringLength(30, MinimumLength = 4, ErrorMessage = "Tên đăng nhập phải từ 4 đến 30 ký tự.")]
    [RegularExpression("^[a-zA-Z0-9._]+$", ErrorMessage = "Tên đăng nhập chỉ gồm chữ, số, dấu chấm và gạch dưới.")]
    [Display(Name = "Tên đăng nhập")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(80, MinimumLength = 3, ErrorMessage = "Họ tên phải từ 3 đến 80 ký tự.")]
    [Display(Name = "Họ và tên")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
    [StringLength(120)]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [RegularExpression("^(0|\\+84)[0-9]{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0 (hoặc +84).")]
    [Display(Name = "Số điện thoại")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn vai trò.")]
    [Display(Name = "Vai trò")]
    public NvdMemberRole Role { get; set; } = NvdMemberRole.Member;

    [Display(Name = "Đang hoạt động")]
    public bool IsActive { get; set; } = true;

    [Required(ErrorMessage = "Vui lòng chọn ngày tham gia.")]
    [DataType(DataType.Date)]
    [Display(Name = "Ngày tham gia")]
    public DateTime JoinedDate { get; set; } = DateTime.Today;
}
