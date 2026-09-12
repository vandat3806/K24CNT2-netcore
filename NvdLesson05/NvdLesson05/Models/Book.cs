using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace NvdLesson05.Models;

public class Book
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên sách.")]
    [StringLength(120, MinimumLength = 2, ErrorMessage = "Tên sách phải từ 2 đến 120 ký tự.")]
    [Display(Name = "Tên sách")]
    public string Title { get; set; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn tác giả.")]
    [Display(Name = "Tác giả")]
    public int AuthorId { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn thể loại.")]
    [Display(Name = "Thể loại")]
    public int GenreId { get; set; }

    [Range(typeof(decimal), "1000", "100000000", ErrorMessage = "Giá phải từ 1.000 đến 100.000.000 đồng.")]
    [Display(Name = "Giá bán")]
    public decimal Price { get; set; }

    [Range(1, 10000, ErrorMessage = "Số trang phải từ 1 đến 10.000.")]
    [Display(Name = "Số trang")]
    public int TotalPage { get; set; }

    [Display(Name = "Ảnh bìa")]
    public string Image { get; set; } = "/images/books/book-default.svg";

    [NotMapped]
    [Display(Name = "Tải ảnh bìa")]
    public IFormFile? ImageFile { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập phần giới thiệu.")]
    [StringLength(800, MinimumLength = 10, ErrorMessage = "Giới thiệu phải từ 10 đến 800 ký tự.")]
    [Display(Name = "Giới thiệu")]
    public string Summary { get; set; } = string.Empty;

    public string AuthorName { get; set; } = string.Empty;
    public string GenreName { get; set; } = string.Empty;
}
