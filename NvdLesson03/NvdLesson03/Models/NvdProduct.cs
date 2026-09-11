namespace NvdLesson03.Models;

/// <summary>
/// Model sản phẩm dùng trong bài thực hành Controller và View.
/// </summary>
public class NvdProduct
{
    public string NvdProductId { get; set; } = string.Empty;
    public string NvdProductName { get; set; } = string.Empty;
    public int NvdYearRelease { get; set; }
    public decimal NvdPrice { get; set; }
}
