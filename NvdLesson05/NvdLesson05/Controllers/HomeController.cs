using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson05.Models;

namespace NvdLesson05.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.StudentName = "Nguyễn Văn Đạt";
        ViewData["StudentId"] = "2410900021";
        ViewData["ClassName"] = "K24CNT2";

        var newestProducts = CatalogData.Products
            .OrderByDescending(product => product.CreatedAt)
            .Take(6)
            .ToList();

        return View(newestProducts);
    }

    public IActionResult RazorDemo()
    {
        ViewBag.Message = "Dữ liệu truyền bằng ViewBag";
        ViewData["Lesson"] = "Lesson05 - View trong ASP.NET Core MVC";
        TempData["Notice"] = "TempData tồn tại đến lần đọc tiếp theo.";

        return View(CatalogData.Categories);
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
