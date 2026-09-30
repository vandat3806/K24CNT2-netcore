namespace NvdLesson12.Models.ViewModels;

public class NvdHomeViewModel
{
    public IReadOnlyList<NvdBanner> NvdBanners { get; init; } = [];
    public IReadOnlyList<NvdProduct> NvdProducts { get; init; } = [];
    public int NvdCategoryCount { get; init; }
    public int NvdStudentCount { get; init; }
    public int NvdSubjectCount { get; init; }
    public int NvdMarkCount { get; init; }
}

public class NvdProductCatalogViewModel
{
    public IReadOnlyList<NvdProduct> NvdProducts { get; init; } = [];
    public IReadOnlyList<NvdCategory> NvdCategories { get; init; } = [];
    public string? NvdSearch { get; init; }
    public int? NvdCategoryId { get; init; }
}
