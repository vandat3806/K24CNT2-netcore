using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("Category")]
public class NvdCategory
{
    [Key]
    [Column("Id")]
    public int NvdCategoryId { get; set; }

    [Display(Name = "Tên danh mục")]
    [Required(ErrorMessage = "Tên danh mục không được để trống.")]
    [StringLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự.")]
    [Column("Name", TypeName = "nvarchar(100)")]
    public string NvdName { get; set; } = string.Empty;

    [Display(Name = "Đang hiển thị")]
    [Column("Status")]
    public bool NvdStatus { get; set; } = true;

    [Display(Name = "Ngày tạo")]
    [Column("CreatedDate")]
    public DateTime NvdCreatedDate { get; set; } = DateTime.UtcNow;

    public ICollection<NvdProduct> NvdProducts { get; set; } = new List<NvdProduct>();
}
