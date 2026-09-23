using NvdLesson09.Models.DataModels;

namespace NvdLesson09.Models.ViewModels;

public sealed class NvdProductIndexViewModel
{
    public IReadOnlyList<NvdProduct> Products { get; init; } = [];
    public IReadOnlyList<NvdCategory> Categories { get; init; } = [];
    public string? Keyword { get; init; }
    public int? CategoryId { get; init; }
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalFiltered { get; init; }
    public int TotalProducts { get; init; }
    public int SaleProductCount { get; init; }
}
