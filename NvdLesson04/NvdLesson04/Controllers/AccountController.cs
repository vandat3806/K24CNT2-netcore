using Microsoft.AspNetCore.Mvc;
using NvdLesson04.Models;

namespace NvdLesson04.Controllers;

public class AccountController : Controller
{
    private static readonly IReadOnlyList<Account> Accounts = new List<Account>
    {
        new()
        {
            Id = 1,
            Name = "Nguyễn Văn Đạt",
            Email = "2410900021@ntu.edu.vn",
            Phone = "0988 240 021",
            Address = "Thái Bình",
            Avatar = "/images/avatars/nguyen-van-dat.svg",
            Gender = 1,
            Bio = "Sinh viên lớp K24CNT2, Khoa Công nghệ Thông tin.",
            Birthday = new DateTime(2006, 8, 3)
        },
        new()
        {
            Id = 2,
            Name = "Hoàng Anh",
            Email = "anh@example.com",
            Phone = "0986 456 789",
            Address = "Hà Nội",
            Avatar = "/images/avatars/hoang-anh.svg",
            Gender = 1,
            Bio = "Yêu thích lập trình web và thiết kế giao diện.",
            Birthday = new DateTime(2003, 7, 15)
        },
        new()
        {
            Id = 3,
            Name = "Trường Giang",
            Email = "giang@example.com",
            Phone = "0977 321 456",
            Address = "Hải Phòng",
            Avatar = "/images/avatars/truong-giang.svg",
            Gender = 1,
            Bio = "Đang học ASP.NET Core MVC và cơ sở dữ liệu.",
            Birthday = new DateTime(2004, 10, 20)
        },
        new()
        {
            Id = 4,
            Name = "Hoàng Thúy",
            Email = "thuy@example.com",
            Phone = "0968 246 810",
            Address = "Nam Định",
            Avatar = "/images/avatars/hoang-thuy.svg",
            Gender = 0,
            Bio = "Quan tâm đến kiểm thử phần mềm và trải nghiệm người dùng.",
            Birthday = new DateTime(2004, 2, 12)
        }
    };

    public IActionResult Index()
    {
        ViewBag.Accounts = Accounts;
        ViewData["StudentName"] = "Nguyễn Văn Đạt";
        return View();
    }

    public IActionResult Profile(int id = 1)
    {
        Account? account = Accounts.FirstOrDefault(item => item.Id == id);
        if (account is null)
        {
            return NotFound($"Không tìm thấy tài khoản có id = {id}.");
        }

        ViewBag.Account = account;
        return View();
    }
}
