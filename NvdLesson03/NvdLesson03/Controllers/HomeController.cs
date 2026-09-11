using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson03.Models;

namespace NvdLesson03.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public ViewResult Index()
    {
        return View(CreateStudentInfo());
    }

    public ViewResult About()
    {
        return View(CreateStudentInfo());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static NvdStudentInfo CreateStudentInfo()
    {
        return new NvdStudentInfo
        {
            FullName = "Nguyễn Văn Đạt",
            StudentId = "2410900021",
            ClassName = "K24CNT2",
            Subject = "Phát triển ứng dụng web với công nghệ .NET"
        };
    }
}
