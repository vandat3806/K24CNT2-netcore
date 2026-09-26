using NvdLesson10.Models;

namespace NvdLesson10.Models.ViewModels;

public sealed class NvdMemberIndexViewModel
{
    public IReadOnlyList<NvdMember> Members { get; init; } = [];
    public string? Keyword { get; init; }
    public string? Status { get; init; }
    public int CurrentPage { get; init; }
    public int TotalPages { get; init; }
    public int TotalFiltered { get; init; }
    public int TotalMembers { get; init; }
    public int ActiveMembers { get; init; }
}
