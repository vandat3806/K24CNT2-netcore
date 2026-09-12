namespace NvdLesson07.Models.ViewModels;

public sealed class LessonOverviewViewModel
{
    public int TotalMembers { get; init; }
    public int ActiveMembers { get; init; }
    public int ModelPropertyCount { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public string StudentCode { get; init; } = string.Empty;
}
