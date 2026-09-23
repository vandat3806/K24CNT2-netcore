using NvdLesson09.Models.DataModels;

namespace NvdLesson09.Models.ViewModels;

public sealed class LessonOverviewViewModel
{
    public int ProductCount { get; init; }
    public int CategoryCount { get; init; }
    public int SaleProductCount { get; init; }
    public IReadOnlyList<NvdProduct> LatestProducts { get; init; } = [];
}
