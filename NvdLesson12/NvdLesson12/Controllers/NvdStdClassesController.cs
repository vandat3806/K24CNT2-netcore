using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;

namespace NvdLesson12.Controllers;

public class NvdStdClassesController(NvdLesson12DbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, CancellationToken cancellationToken)
    {
        var query = context.NvdStdClasses.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdClassName.Contains(keyword));
        }
        ViewBag.Search = search;
        return View(await query.Include(x => x.NvdStudents).OrderBy(x => x.NvdClassName).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdStdClasses.AsNoTracking().Include(x => x.NvdStudents)
            .FirstOrDefaultAsync(x => x.NvdStdClassId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public IActionResult Create() => View(new NvdStdClass());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NvdClassName")] NvdStdClass model, CancellationToken cancellationToken)
    {
        model.NvdClassName = model.NvdClassName.Trim();
        if (await context.NvdStdClasses.AnyAsync(x => x.NvdClassName == model.NvdClassName, cancellationToken))
            ModelState.AddModelError(nameof(model.NvdClassName), "Tên lớp đã tồn tại.");
        if (!ModelState.IsValid) return View(model);

        context.Add(model);
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã thêm lớp học.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdStdClasses.FindAsync([id.Value], cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("NvdStdClassId,NvdClassName")] NvdStdClass model, CancellationToken cancellationToken)
    {
        if (id != model.NvdStdClassId) return NotFound();
        model.NvdClassName = model.NvdClassName.Trim();
        if (await context.NvdStdClasses.AnyAsync(x => x.NvdStdClassId != id && x.NvdClassName == model.NvdClassName, cancellationToken))
            ModelState.AddModelError(nameof(model.NvdClassName), "Tên lớp đã tồn tại.");
        if (!ModelState.IsValid) return View(model);

        var item = await context.NvdStdClasses.FindAsync([id], cancellationToken);
        if (item is null) return NotFound();
        item.NvdClassName = model.NvdClassName;
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã cập nhật lớp học.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdStdClasses.AsNoTracking().Include(x => x.NvdStudents)
            .FirstOrDefaultAsync(x => x.NvdStdClassId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdStdClasses.Include(x => x.NvdStudents)
            .FirstOrDefaultAsync(x => x.NvdStdClassId == id, cancellationToken);
        if (item is null) return RedirectToAction(nameof(Index));
        if (item.NvdStudents.Count != 0)
        {
            TempData["Error"] = "Không thể xóa lớp đang có sinh viên.";
            return RedirectToAction(nameof(Delete), new { id });
        }
        context.Remove(item);
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã xóa lớp học.";
        return RedirectToAction(nameof(Index));
    }
}
