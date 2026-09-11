namespace NvdLesson04.Models;

public class Account
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Avatar { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Bio { get; set; } = string.Empty;
    public int Gender { get; set; }
    public DateTime Birthday { get; set; }

    public string GenderName => Gender == 1 ? "Nam" : "Nữ";
}
