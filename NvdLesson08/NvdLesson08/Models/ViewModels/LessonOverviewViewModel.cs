using NvdLesson08.Models.DataModels;

namespace NvdLesson08.Models.ViewModels;

public sealed class LessonOverviewViewModel
{
    public int TotalMembers { get; init; }

    public int ActiveMembers { get; init; }

    public int Roles { get; init; }

    public NvdMember? LatestMember { get; init; }
}
