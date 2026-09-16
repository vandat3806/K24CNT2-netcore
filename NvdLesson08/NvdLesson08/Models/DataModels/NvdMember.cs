using System.ComponentModel.DataAnnotations;

namespace NvdLesson08.Models.DataModels;

public enum NvdMemberRole
{
    [Display(Name = "Quản trị viên")]
    Administrator,

    [Display(Name = "Biên tập viên")]
    Editor,

    [Display(Name = "Thành viên")]
    Member
}

public sealed class NvdMember
{
    public int Id { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public NvdMemberRole Role { get; set; }

    public bool IsActive { get; set; }

    public DateTime JoinedDate { get; set; }

    public DateTime CreatedAt { get; set; }
}
