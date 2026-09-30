using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("StdClass")]
public class NvdStdClass
{
    [Key]
    [Column("Id")]
    public int NvdStdClassId { get; set; }

    [Display(Name = "Tên lớp")]
    [Required(ErrorMessage = "Tên lớp không được để trống.")]
    [StringLength(100, ErrorMessage = "Tên lớp tối đa 100 ký tự.")]
    [Column("ClassName", TypeName = "nvarchar(100)")]
    public string NvdClassName { get; set; } = string.Empty;

    public ICollection<NvdStudent> NvdStudents { get; set; } = new List<NvdStudent>();
}
