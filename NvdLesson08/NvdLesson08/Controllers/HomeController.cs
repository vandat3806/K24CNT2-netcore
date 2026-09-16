using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson08.Models;
using NvdLesson08.Models.ViewModels;
using NvdLesson08.Repositories;

namespace NvdLesson08.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly INvdMemberRepository _memberRepository;

    public HomeController(
        ILogger<HomeController> logger,
        INvdMemberRepository memberRepository)
    {
        _logger = logger;
        _memberRepository = memberRepository;
    }

    public IActionResult Index()
    {
        var members = _memberRepository.GetAll();
        var viewModel = new LessonOverviewViewModel
        {
            TotalMembers = members.Count,
            ActiveMembers = members.Count(member => member.IsActive),
            Roles = members.Select(member => member.Role).Distinct().Count(),
            LatestMember = members.OrderByDescending(member => member.CreatedAt).FirstOrDefault()
        };

        return View(viewModel);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
