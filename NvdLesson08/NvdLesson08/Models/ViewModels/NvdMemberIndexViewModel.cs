using NvdLesson08.Models.DataModels;

namespace NvdLesson08.Models.ViewModels;

public sealed class NvdMemberIndexViewModel
{
    public IReadOnlyList<NvdMember> Members { get; init; } = [];

    public string Query { get; init; } = string.Empty;

    public NvdMemberRole? Role { get; init; }

    public string Status { get; init; } = "all";

    public int Page { get; init; } = 1;

    public int PageSize { get; init; } = 5;

    public int TotalItems { get; init; }

    public int TotalPages => Math.Max(1, (int)Math.Ceiling(TotalItems / (double)PageSize));

    public int ActiveCount { get; init; }

    public int InactiveCount { get; init; }
}
