using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("Marks")]
public class NvdMark
{
    [Display(Name = "Môn học")]
    [Column("SubjectId")]
    public int NvdSubjectId { get; set; }

    [Display(Name = "Sinh viên")]
    [Column("StudentId")]
    public int NvdStudentId { get; set; }

    [Display(Name = "Điểm")]
    [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
    [Column("Score", TypeName = "float")]
    public double NvdScore { get; set; }

    public NvdSubject NvdSubject { get; set; } = null!;
    public NvdStudent NvdStudent { get; set; } = null!;
}
