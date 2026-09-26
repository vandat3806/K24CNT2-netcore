using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NguyenVanDat2410900021_exam.Models;

namespace NguyenVanDat2410900021_exam.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult HvtAbout()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
