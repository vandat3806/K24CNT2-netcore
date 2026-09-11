using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson04.Models;

namespace NvdLesson04.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.StudentName = "Nguyễn Văn Đạt";
        ViewData["StudentId"] = "2410900021";
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
