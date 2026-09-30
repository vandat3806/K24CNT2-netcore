using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NvdLesson12.Models;

[Table("Product")]
public class NvdProduct
{
    [Key]
    [Column("Id")]
    public int NvdProductId { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
    [StringLength(150, ErrorMessage = "Tên sản phẩm tối đa 150 ký tự.")]
    [Column("Name", TypeName = "nvarchar(150)")]
    public string NvdName { get; set; } = string.Empty;

    [Display(Name = "Ảnh sản phẩm")]
    [Required]
    [StringLength(260)]
    [Column("Image", TypeName = "varchar(260)")]
    public string NvdImage { get; set; } = string.Empty;

    [Display(Name = "Giá niêm yết")]
    [Range(0, 1_000_000_000, ErrorMessage = "Giá phải từ 0 đến 1 tỷ đồng.")]
    [Column("Price", TypeName = "decimal(18,2)")]
    public decimal NvdPrice { get; set; }

    [Display(Name = "Giá khuyến mại")]
    [Range(0, 1_000_000_000, ErrorMessage = "Giá khuyến mại phải từ 0 đến 1 tỷ đồng.")]
    [Column("SalePrice", TypeName = "decimal(18,2)")]
    public decimal NvdSalePrice { get; set; }

    [Display(Name = "Đang bán")]
    [Column("Status")]
    public bool NvdStatus { get; set; } = true;

    [Display(Name = "Mô tả")]
    [StringLength(1000, ErrorMessage = "Mô tả tối đa 1000 ký tự.")]
    [Column("Descriptions", TypeName = "nvarchar(1000)")]
    public string? NvdDescription { get; set; }

    [Display(Name = "Danh mục")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    [Column("CategoryId")]
    public int NvdCategoryId { get; set; }

    [Display(Name = "Ngày tạo")]
    [Column("CreatedDate")]
    public DateTime NvdCreatedDate { get; set; } = DateTime.UtcNow;

    public NvdCategory NvdCategory { get; set; } = null!;
}
