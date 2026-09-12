using NvdLesson07.Models.DataModels;

namespace NvdLesson07.Models.ViewModels;

/// <summary>
/// ViewModel gom dữ liệu bảng, bộ lọc và thống kê dành riêng cho trang danh sách.
/// </summary>
public sealed class NvdMemberIndexViewModel
{
    public IReadOnlyList<NvdMember> Members { get; init; } = [];
    public string Keyword { get; init; } = string.Empty;
    public string Status { get; init; } = "all";
    public int TotalMembers { get; init; }
    public int ActiveMembers { get; init; }
    public int InactiveMembers => TotalMembers - ActiveMembers;
}
