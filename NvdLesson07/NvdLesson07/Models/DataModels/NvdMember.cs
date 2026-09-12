using System.ComponentModel.DataAnnotations;

namespace NvdLesson07.Models.DataModels;

/// <summary>
/// Data model mô tả một thành viên trong hệ thống.
/// Các Data Annotation vừa tạo metadata hiển thị, vừa khai báo quy tắc validation.
/// </summary>
public sealed class NvdMember
{
    [Display(Name = "Mã thành viên")]
    public int NvdMemberId { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập.")]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 30 ký tự.")]
    [RegularExpression("^[a-zA-Z0-9._]+$", ErrorMessage = "Tên đăng nhập chỉ gồm chữ không dấu, số, dấu chấm và gạch dưới.")]
    [Display(Name = "Tên đăng nhập")]
    public string NvdUserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập họ và tên.")]
    [StringLength(80, MinimumLength = 2, ErrorMessage = "Họ tên phải từ 2 đến 80 ký tự.")]
    [Display(Name = "Họ và tên")]
    public string NvdFullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập email.")]
    [EmailAddress(ErrorMessage = "Email chưa đúng định dạng.")]
    [StringLength(100, ErrorMessage = "Email tối đa 100 ký tự.")]
    [Display(Name = "Email")]
    public string NvdEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
    [RegularExpression("^(0[0-9]{9}|\\+84[0-9]{9})$", ErrorMessage = "Số điện thoại phải có dạng 0xxxxxxxxx hoặc +84xxxxxxxxx.")]
    [Display(Name = "Số điện thoại")]
    public string NvdPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập tên lớp.")]
    [StringLength(20, MinimumLength = 3, ErrorMessage = "Tên lớp phải từ 3 đến 20 ký tự.")]
    [RegularExpression("^[a-zA-Z0-9-]+$", ErrorMessage = "Tên lớp chỉ gồm chữ không dấu, số và dấu gạch ngang.")]
    [Display(Name = "Lớp")]
    public string NvdClassName { get; set; } = string.Empty;

    [Display(Name = "Ngày tham gia")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime NvdJoinedDate { get; set; } = DateTime.Today;

    [Display(Name = "Đang hoạt động")]
    public bool NvdIsActive { get; set; } = true;
}
