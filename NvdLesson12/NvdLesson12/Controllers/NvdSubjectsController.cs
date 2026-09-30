using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;

namespace NvdLesson12.Controllers;

public class NvdSubjectsController(NvdLesson12DbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
    {
        var query = context.NvdSubjects.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdSubjectName.Contains(keyword));
        }
        ViewBag.Search = search;
        return View(await query.Include(x => x.NvdMarks).OrderBy(x => x.NvdSubjectName).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdSubjects.AsNoTracking().Include(x => x.NvdMarks).ThenInclude(x => x.NvdStudent)
            .FirstOrDefaultAsync(x => x.NvdSubjectId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public IActionResult Create() => View(new NvdSubject());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NvdSubjectName")] NvdSubject model, CancellationToken cancellationToken)
    {
        model.NvdSubjectName = model.NvdSubjectName.Trim();
        if (await context.NvdSubjects.AnyAsync(x => x.NvdSubjectName == model.NvdSubjectName, cancellationToken))
            ModelState.AddModelError(nameof(model.NvdSubjectName), "Tên môn học đã tồn tại.");
        if (!ModelState.IsValid) return View(model);

        context.Add(model);
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã thêm môn học.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdSubjects.FindAsync([id.Value], cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("NvdSubjectId,NvdSubjectName")] NvdSubject model, CancellationToken cancellationToken)
    {
        if (id != model.NvdSubjectId) return NotFound();
        model.NvdSubjectName = model.NvdSubjectName.Trim();
        if (await context.NvdSubjects.AnyAsync(x => x.NvdSubjectId != id && x.NvdSubjectName == model.NvdSubjectName, cancellationToken))
            ModelState.AddModelError(nameof(model.NvdSubjectName), "Tên môn học đã tồn tại.");
        if (!ModelState.IsValid) return View(model);

        var item = await context.NvdSubjects.FindAsync([id], cancellationToken);
        if (item is null) return NotFound();
        item.NvdSubjectName = model.NvdSubjectName;
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã cập nhật môn học.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdSubjects.AsNoTracking().Include(x => x.NvdMarks)
            .FirstOrDefaultAsync(x => x.NvdSubjectId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdSubjects.FindAsync([id], cancellationToken);
        if (item is not null)
        {
            context.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
            TempData["Success"] = "Đã xóa môn học và các điểm liên quan.";
        }
        return RedirectToAction(nameof(Index));
    }
}
