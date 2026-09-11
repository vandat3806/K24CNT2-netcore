namespace NvdLesson02.Models;

/// <summary>
/// Model sản phẩm dùng để minh họa việc truyền dữ liệu từ Controller sang View.
/// </summary>
public class NvdProduct
{
    public int Id { get; set; }
    public string ProductCode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public int YearRelease { get; set; }
    public decimal Price { get; set; }
}
