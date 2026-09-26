using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenVanDat2410900021_exam.Models;

[Table("NvdStudent")]
public class NvdStudent
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mã sinh viên")]
    [StringLength(20)]
    [Display(Name = "Mã sinh viên")]
    public string NvdStudentCode { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng nhập họ tên")]
    [StringLength(100)]
    [Display(Name = "Họ và tên")]
    public string NvdName { get; set; } = string.Empty;

    [StringLength(10)]
    [Display(Name = "Giới tính")]
    public string? NvdGender { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Ngày sinh")]
    public DateTime? NvdBirthDay { get; set; }

    [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
    [StringLength(255)]
    [Display(Name = "Email")]
    public string? NvdEmail { get; set; }

    [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
    [StringLength(15)]
    [Display(Name = "Số điện thoại")]
    public string? NvdPhone { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập lớp")]
    [StringLength(30)]
    [Display(Name = "Lớp")]
    public string NvdClass { get; set; } = string.Empty;

    [Display(Name = "Đang hoạt động")]
    public bool NvdActive { get; set; } = true;
}
