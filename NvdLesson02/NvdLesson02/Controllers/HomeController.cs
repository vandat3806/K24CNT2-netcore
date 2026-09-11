using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson02.Models;

namespace NvdLesson02.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(CreateStudentInfo());
    }

    public IActionResult About()
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
            FullName = "Nguyen van dat",
            StudentId = "2410900021",
            ClassName = "K24CNT2",
            Subject = "Phát triển ứng dụng web với công nghệ .NET"
        };
    }
}
