using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson10.Models;
using NvdLesson10.Models.ViewModels;

namespace NvdLesson10.Controllers;

public sealed class HomeController : Controller
{
    private readonly NvdLesson10EfDbContext _context;
    private readonly IConfiguration _configuration;

    public HomeController(NvdLesson10EfDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<IActionResult> Index()
    {
        var totalMembers = await _context.NvdMembers.CountAsync();
        var activeMembers = await _context.NvdMembers.CountAsync(member => member.NvdStatus);

        return View(new HomeDashboardViewModel
        {
            TotalMembers = totalMembers,
            ActiveMembers = activeMembers,
            InactiveMembers = totalMembers - activeMembers,
            DatabaseProvider = _configuration["DatabaseProvider"] ?? "SqlServer",
            LatestMembers = await _context.NvdMembers
                .AsNoTracking()
                .OrderByDescending(member => member.NvdCreatedAt)
                .Take(4)
                .ToListAsync()
        });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
