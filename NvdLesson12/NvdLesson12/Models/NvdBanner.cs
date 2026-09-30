using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("Banner")]
public class NvdBanner
{
    [Key]
    [Column("Id")]
    public int NvdBannerId { get; set; }

    [Display(Name = "Tên banner")]
    [Required(ErrorMessage = "Tên banner không được để trống.")]
    [StringLength(150, ErrorMessage = "Tên banner tối đa 150 ký tự.")]
    [Column("Name", TypeName = "nvarchar(150)")]
    public string NvdName { get; set; } = string.Empty;

    [Display(Name = "Ảnh banner")]
    [Required]
    [StringLength(260)]
    [Column("Image", TypeName = "varchar(260)")]
    public string NvdImage { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự.")]
    [Column("Description", TypeName = "nvarchar(500)")]
    public string? NvdDescription { get; set; }

    [Display(Name = "Ngày tạo")]
    [Column("CreatedDate")]
    public DateTime NvdCreatedDate { get; set; } = DateTime.UtcNow;

    [Display(Name = "Đang hiển thị")]
    [Column("Status")]
    public bool NvdStatus { get; set; } = true;
}
