namespace NvdLesson10.Models;

// Lớp được mô phỏng theo kết quả sinh ra bởi Scaffold-DbContext (Database First).
public partial class NvdMember
{
    public long Id { get; set; }
    public string NvdUserName { get; set; } = string.Empty;
    public string NvdPassword { get; set; } = string.Empty;
    public string NvdFullName { get; set; } = string.Empty;
    public string NvdEmail { get; set; } = string.Empty;
    public string? NvdPhone { get; set; }
    public bool NvdStatus { get; set; }
    public DateTime NvdCreatedAt { get; set; }
}
