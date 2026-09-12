namespace NvdLesson06.Models;

public class StorefrontViewModel
{
    public IReadOnlyList<Category> Categories { get; init; } = [];
    public IReadOnlyList<Product> NewestProducts { get; init; } = [];
    public int TotalProducts { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public string StudentId { get; init; } = string.Empty;
}
