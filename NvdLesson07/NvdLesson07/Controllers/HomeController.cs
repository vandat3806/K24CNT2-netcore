using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson07.Models;
using NvdLesson07.Models.DataModels;
using NvdLesson07.Models.ViewModels;
using NvdLesson07.Repositories;

namespace NvdLesson07.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INvdMemberRepository _repository;

    public HomeController(ILogger<HomeController> logger, INvdMemberRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Index()
    {
        var members = _repository.GetAll();
        var model = new LessonOverviewViewModel
        {
            TotalMembers = members.Count,
            ActiveMembers = members.Count(member => member.NvdIsActive),
            ModelPropertyCount = typeof(NvdMember).GetProperties().Length,
            StudentName = "Nguyễn Văn Đạt",
            StudentCode = "2410900021"
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
