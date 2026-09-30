using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("Subjects")]
public class NvdSubject
{
    [Key]
    [Column("Id")]
    public int NvdSubjectId { get; set; }

    [Display(Name = "Tên môn học")]
    [Required(ErrorMessage = "Tên môn học không được để trống.")]
    [StringLength(100, ErrorMessage = "Tên môn học tối đa 100 ký tự.")]
    [Column("SubjectName", TypeName = "nvarchar(100)")]
    public string NvdSubjectName { get; set; } = string.Empty;

    public ICollection<NvdMark> NvdMarks { get; set; } = new List<NvdMark>();
}
