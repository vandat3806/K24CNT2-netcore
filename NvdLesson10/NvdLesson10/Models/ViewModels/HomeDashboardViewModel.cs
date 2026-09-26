using NvdLesson10.Models;

namespace NvdLesson10.Models.ViewModels;

public sealed class HomeDashboardViewModel
{
    public int TotalMembers { get; init; }
    public int ActiveMembers { get; init; }
    public int InactiveMembers { get; init; }
    public string DatabaseProvider { get; init; } = string.Empty;
    public IReadOnlyList<NvdMember> LatestMembers { get; init; } = [];
}
