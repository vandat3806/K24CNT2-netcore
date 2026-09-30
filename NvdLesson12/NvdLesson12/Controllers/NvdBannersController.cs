using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;
using NvdLesson12.Models.ViewModels;
using NvdLesson12.Services;

namespace NvdLesson12.Controllers;

public class NvdBannersController(
    NvdLesson12DbContext context,
    IImageStorageService imageStorage) : Controller
{
    private const long MaxImageBytes = 5 * 1024 * 1024;

    public async Task<IActionResult> Index(string? search, bool? status, CancellationToken cancellationToken)
    {
        var query = context.NvdBanners.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdName.Contains(keyword));
        }
        if (status.HasValue) query = query.Where(x => x.NvdStatus == status.Value);
        ViewBag.Search = search;
        ViewBag.Status = status;
        return View(await query.OrderByDescending(x => x.NvdCreatedDate).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdBanners.AsNoTracking().FirstOrDefaultAsync(x => x.NvdBannerId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public IActionResult Create() => View(new NvdBannerFormViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxImageBytes + 1024 * 100)]
    public async Task<IActionResult> Create(NvdBannerFormViewModel model, CancellationToken cancellationToken)
    {
        if (model.NvdImageFile is null)
            ModelState.AddModelError(nameof(model.NvdImageFile), "Vui lòng chọn ảnh banner.");
        if (!ModelState.IsValid) return View(model);

        string imagePath;
        try
        {
            imagePath = await imageStorage.SaveImageAsync(model.NvdImageFile!, "banners", MaxImageBytes, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(model.NvdImageFile), ex.Message);
            return View(model);
        }

        var item = new NvdBanner
        {
            NvdName = model.NvdName.Trim(),
            NvdDescription = model.NvdDescription?.Trim(),
            NvdImage = imagePath,
            NvdStatus = model.NvdStatus,
            NvdCreatedDate = DateTime.UtcNow
        };
        try
        {
            context.Add(item);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await imageStorage.DeleteImageAsync(imagePath, cancellationToken);
            throw;
        }

        TempData["Success"] = "Đã thêm banner mới.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdBanners.AsNoTracking().FirstOrDefaultAsync(x => x.NvdBannerId == id, cancellationToken);
        if (item is null) return NotFound();
        return View(new NvdBannerFormViewModel
        {
            NvdBannerId = item.NvdBannerId,
            NvdName = item.NvdName,
            NvdDescription = item.NvdDescription,
            NvdStatus = item.NvdStatus,
            NvdCurrentImage = item.NvdImage
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxImageBytes + 1024 * 100)]
    public async Task<IActionResult> Edit(int id, NvdBannerFormViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.NvdBannerId) return NotFound();
        var item = await context.NvdBanners.FirstOrDefaultAsync(x => x.NvdBannerId == id, cancellationToken);
        if (item is null) return NotFound();
        if (!ModelState.IsValid)
        {
            model.NvdCurrentImage = item.NvdImage;
            return View(model);
        }

        string? newImagePath = null;
        if (model.NvdImageFile is not null)
        {
            try
            {
                newImagePath = await imageStorage.SaveImageAsync(model.NvdImageFile, "banners", MaxImageBytes, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.NvdImageFile), ex.Message);
                model.NvdCurrentImage = item.NvdImage;
                return View(model);
            }
        }

        var oldImagePath = item.NvdImage;
        item.NvdName = model.NvdName.Trim();
        item.NvdDescription = model.NvdDescription?.Trim();
        item.NvdStatus = model.NvdStatus;
        if (newImagePath is not null) item.NvdImage = newImagePath;
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await imageStorage.DeleteImageAsync(newImagePath, cancellationToken);
            throw;
        }

        if (newImagePath is not null) await imageStorage.DeleteImageAsync(oldImagePath, cancellationToken);
        TempData["Success"] = "Đã cập nhật banner.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdBanners.AsNoTracking().FirstOrDefaultAsync(x => x.NvdBannerId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdBanners.FindAsync([id], cancellationToken);
        if (item is not null)
        {
            var imagePath = item.NvdImage;
            context.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
            await imageStorage.DeleteImageAsync(imagePath, cancellationToken);
            TempData["Success"] = "Đã xóa banner.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdBanners.FindAsync([id], cancellationToken);
        if (item is null) return NotFound();
        item.NvdStatus = !item.NvdStatus;
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã đổi trạng thái banner.";
        return RedirectToAction(nameof(Index));
    }
}
