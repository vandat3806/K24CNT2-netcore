using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;
using NvdLesson12.Models.ViewModels;

namespace NvdLesson12.Controllers;

public class HomeController(NvdLesson12DbContext context) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var model = new NvdHomeViewModel
        {
            NvdBanners = await context.NvdBanners
                .AsNoTracking()
                .Where(x => x.NvdStatus)
                .OrderByDescending(x => x.NvdCreatedDate)
                .ToListAsync(cancellationToken),
            NvdProducts = await context.NvdProducts
                .AsNoTracking()
                .Include(x => x.NvdCategory)
                .Where(x => x.NvdStatus && x.NvdCategory.NvdStatus)
                .OrderByDescending(x => x.NvdCreatedDate)
                .Take(6)
                .ToListAsync(cancellationToken),
            NvdCategoryCount = await context.NvdCategories.CountAsync(cancellationToken),
            NvdStudentCount = await context.NvdStudents.CountAsync(cancellationToken),
            NvdSubjectCount = await context.NvdSubjects.CountAsync(cancellationToken),
            NvdMarkCount = await context.NvdMarks.CountAsync(cancellationToken)
        };

        return View(model);
    }

    public async Task<IActionResult> Product(string? search, int? categoryId, CancellationToken cancellationToken)
    {
        var query = context.NvdProducts
            .AsNoTracking()
            .Include(x => x.NvdCategory)
            .Where(x => x.NvdStatus && x.NvdCategory.NvdStatus);

        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdName.Contains(keyword));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(x => x.NvdCategoryId == categoryId.Value);
        }

        var model = new NvdProductCatalogViewModel
        {
            NvdProducts = await query.OrderByDescending(x => x.NvdCreatedDate).ToListAsync(cancellationToken),
            NvdCategories = await context.NvdCategories.AsNoTracking().Where(x => x.NvdStatus).OrderBy(x => x.NvdName).ToListAsync(cancellationToken),
            NvdSearch = search,
            NvdCategoryId = categoryId
        };

        return View(model);
    }

    public IActionResult About() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
