using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NvdLesson12.Models.ViewModels;

public class NvdProductFormViewModel : IValidatableObject
{
    public int NvdProductId { get; set; }

    [Display(Name = "Tên sản phẩm")]
    [Required(ErrorMessage = "Tên sản phẩm không được để trống.")]
    [StringLength(150, ErrorMessage = "Tên sản phẩm tối đa 150 ký tự.")]
    public string NvdName { get; set; } = string.Empty;

    [Display(Name = "Giá niêm yết")]
    [Range(0, 1_000_000_000, ErrorMessage = "Giá phải từ 0 đến 1 tỷ đồng.")]
    public decimal NvdPrice { get; set; }

    [Display(Name = "Giá khuyến mại")]
    [Range(0, 1_000_000_000, ErrorMessage = "Giá khuyến mại phải từ 0 đến 1 tỷ đồng.")]
    public decimal NvdSalePrice { get; set; }

    [Display(Name = "Đang bán")]
    public bool NvdStatus { get; set; } = true;

    [Display(Name = "Mô tả")]
    [StringLength(1000, ErrorMessage = "Mô tả tối đa 1000 ký tự.")]
    public string? NvdDescription { get; set; }

    [Display(Name = "Danh mục")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn danh mục.")]
    public int NvdCategoryId { get; set; }

    [Display(Name = "Tệp ảnh")]
    public IFormFile? NvdImageFile { get; set; }
    public string? NvdCurrentImage { get; set; }
    public IEnumerable<SelectListItem> NvdCategories { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (NvdSalePrice > NvdPrice)
        {
            yield return new ValidationResult(
                "Giá khuyến mại không được lớn hơn giá niêm yết.",
                [nameof(NvdSalePrice)]);
        }
    }
}

public class NvdBannerFormViewModel
{
    public int NvdBannerId { get; set; }

    [Display(Name = "Tên banner")]
    [Required(ErrorMessage = "Tên banner không được để trống.")]
    [StringLength(150)]
    public string NvdName { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    [StringLength(500)]
    public string? NvdDescription { get; set; }

    [Display(Name = "Đang hiển thị")]
    public bool NvdStatus { get; set; } = true;

    [Display(Name = "Tệp ảnh")]
    public IFormFile? NvdImageFile { get; set; }
    public string? NvdCurrentImage { get; set; }
}

public class NvdStudentFormViewModel : IValidatableObject
{
    public int NvdStudentId { get; set; }

    [Display(Name = "Họ và tên")]
    [Required(ErrorMessage = "Họ tên không được để trống.")]
    [StringLength(100)]
    public string NvdStudentName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email không được để trống.")]
    [EmailAddress(ErrorMessage = "Email không đúng định dạng.")]
    [StringLength(100)]
    public string NvdStudentEmail { get; set; } = string.Empty;

    [Display(Name = "Số điện thoại")]
    [Required(ErrorMessage = "Số điện thoại không được để trống.")]
    [RegularExpression(@"^(0|\+84)[0-9]{9,10}$", ErrorMessage = "Số điện thoại Việt Nam không hợp lệ.")]
    public string NvdStudentPhone { get; set; } = string.Empty;

    [Display(Name = "Địa chỉ")]
    [Required(ErrorMessage = "Địa chỉ không được để trống.")]
    [StringLength(150)]
    public string NvdStudentAddress { get; set; } = string.Empty;

    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    [Required(ErrorMessage = "Ngày sinh không được để trống.")]
    public DateTime NvdStudentBirthday { get; set; } = DateTime.Today.AddYears(-18);

    [Display(Name = "Lớp")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn lớp.")]
    public int NvdStdClassId { get; set; }

    [Display(Name = "Ảnh đại diện")]
    public IFormFile? NvdAvatarFile { get; set; }
    public string? NvdCurrentAvatar { get; set; }
    public IEnumerable<SelectListItem> NvdClasses { get; set; } = [];

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (NvdStudentBirthday.Date > DateTime.Today)
        {
            yield return new ValidationResult("Ngày sinh không được ở tương lai.", [nameof(NvdStudentBirthday)]);
        }
        else if (NvdStudentBirthday.Date < DateTime.Today.AddYears(-100))
        {
            yield return new ValidationResult("Ngày sinh không hợp lệ.", [nameof(NvdStudentBirthday)]);
        }
    }
}

public class NvdMarkFormViewModel
{
    [Display(Name = "Sinh viên")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn sinh viên.")]
    public int NvdStudentId { get; set; }

    [Display(Name = "Môn học")]
    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn môn học.")]
    public int NvdSubjectId { get; set; }

    [Display(Name = "Điểm")]
    [Range(0, 10, ErrorMessage = "Điểm phải nằm trong khoảng từ 0 đến 10.")]
    public double NvdScore { get; set; }

    public IEnumerable<SelectListItem> NvdStudents { get; set; } = [];
    public IEnumerable<SelectListItem> NvdSubjects { get; set; } = [];
}
