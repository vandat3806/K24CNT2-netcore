using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("Student")]
public class NvdStudent
{
    [Key]
    [Column("Id")]
    public int NvdStudentId { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Họ tên không được để trống.")]
    [StringLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự.")]
    [Column("StudentName", TypeName = "nvarchar(100)")]
    public string NvdStudentName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email không được để trống.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(100)]
    [Column("StudentEmail", TypeName = "nvarchar(100)")]
    public string NvdStudentEmail { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Số điện thoại không được để trống.")]
    [RegularExpression(@"^(0|\+84)[0-9]{9,10}$", ErrorMessage = "Số điện thoại Việt Nam không hợp lệ.")]
    [StringLength(50)]
    [Column("StudentPhone", TypeName = "nvarchar(50)")]
    public string NvdStudentPhone { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Địa chỉ không được để trống.")]
    [StringLength(150, ErrorMessage = "Địa chỉ tối đa 150 ký tự.")]
    [Column("StudentAddress", TypeName = "nvarchar(150)")]
    public string NvdStudentAddress { get; set; } = string.Empty;

    [Display(Name = "Ảnh đại diện")]
    [Required]
    [StringLength(100)]
    [Column("StudentAvatar", TypeName = "nvarchar(100)")]
    public string NvdStudentAvatar { get; set; } = string.Empty;

    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    [Column("StudentBirthday", TypeName = "date")]
    public DateTime NvdStudentBirthday { get; set; }

    [Display(Name = "Lớp")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn lớp.")]
    [Column("ClassId")]
    public int NvdStdClassId { get; set; }

    public NvdStdClass NvdStdClass { get; set; } = null!;
    public ICollection<NvdMark> NvdMarks { get; set; } = new List<NvdMark>();
}
