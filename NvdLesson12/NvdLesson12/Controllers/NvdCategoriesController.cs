using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;

namespace NvdLesson12.Controllers;

public class NvdCategoriesController(NvdLesson12DbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, bool? status, CancellationToken cancellationToken)
    {
        var query = context.NvdCategories.AsNoTracking().Include(x => x.NvdProducts).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdName.Contains(keyword));
        }
        if (status.HasValue)
        {
            query = query.Where(x => x.NvdStatus == status.Value);
        }

        ViewBag.Search = search;
        ViewBag.Status = status;
        return View(await query.OrderBy(x => x.NvdName).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdCategories.AsNoTracking()
            .Include(x => x.NvdProducts)
            .FirstOrDefaultAsync(x => x.NvdCategoryId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public IActionResult Create() => View(new NvdCategory());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("NvdName,NvdStatus")] NvdCategory model, CancellationToken cancellationToken)
    {
        model.NvdName = model.NvdName.Trim();
        if (await context.NvdCategories.AnyAsync(x => x.NvdName == model.NvdName, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.NvdName), "Tên danh mục đã tồn tại.");
        }

        if (!ModelState.IsValid) return View(model);

        model.NvdCreatedDate = DateTime.UtcNow;
        context.Add(model);
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã thêm danh mục mới.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdCategories.FindAsync([id.Value], cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("NvdCategoryId,NvdName,NvdStatus")] NvdCategory model, CancellationToken cancellationToken)
    {
        if (id != model.NvdCategoryId) return NotFound();
        model.NvdName = model.NvdName.Trim();
        if (await context.NvdCategories.AnyAsync(x => x.NvdCategoryId != id && x.NvdName == model.NvdName, cancellationToken))
        {
            ModelState.AddModelError(nameof(model.NvdName), "Tên danh mục đã tồn tại.");
        }
        if (!ModelState.IsValid) return View(model);

        var item = await context.NvdCategories.FindAsync([id], cancellationToken);
        if (item is null) return NotFound();

        item.NvdName = model.NvdName;
        item.NvdStatus = model.NvdStatus;
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CategoryExistsAsync(id, cancellationToken)) return NotFound();
            throw;
        }

        TempData["Success"] = "Đã cập nhật danh mục.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdCategories.AsNoTracking()
            .Include(x => x.NvdProducts)
            .FirstOrDefaultAsync(x => x.NvdCategoryId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdCategories.Include(x => x.NvdProducts)
            .FirstOrDefaultAsync(x => x.NvdCategoryId == id, cancellationToken);
        if (item is null) return RedirectToAction(nameof(Index));
        if (item.NvdProducts.Count != 0)
        {
            TempData["Error"] = "Không thể xóa danh mục đang có sản phẩm.";
            return RedirectToAction(nameof(Delete), new { id });
        }

        context.Remove(item);
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã xóa danh mục.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdCategories.FindAsync([id], cancellationToken);
        if (item is null) return NotFound();
        item.NvdStatus = !item.NvdStatus;
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã đổi trạng thái danh mục.";
        return RedirectToAction(nameof(Index));
    }

    private Task<bool> CategoryExistsAsync(int id, CancellationToken cancellationToken) =>
        context.NvdCategories.AnyAsync(x => x.NvdCategoryId == id, cancellationToken);
}
