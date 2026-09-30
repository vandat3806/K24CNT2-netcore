using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;
using NvdLesson12.Models.ViewModels;
using NvdLesson12.Services;

namespace NvdLesson12.Controllers;

public class NvdProductsController(
    NvdLesson12DbContext context,
    IImageStorageService imageStorage) : Controller
{
    private const long MaxImageBytes = 5 * 1024 * 1024;

    public async Task<IActionResult> Index(string? search, int? categoryId, bool? status, CancellationToken cancellationToken)
    {
        var query = context.NvdProducts.AsNoTracking().Include(x => x.NvdCategory).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdName.Contains(keyword));
        }
        if (categoryId.HasValue) query = query.Where(x => x.NvdCategoryId == categoryId.Value);
        if (status.HasValue) query = query.Where(x => x.NvdStatus == status.Value);

        ViewBag.Search = search;
        ViewBag.CategoryId = categoryId;
        ViewBag.Status = status;
        ViewBag.Categories = new SelectList(
            await context.NvdCategories.AsNoTracking().OrderBy(x => x.NvdName).ToListAsync(cancellationToken),
            nameof(NvdCategory.NvdCategoryId), nameof(NvdCategory.NvdName), categoryId);

        return View(await query.OrderByDescending(x => x.NvdCreatedDate).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdProducts.AsNoTracking().Include(x => x.NvdCategory)
            .FirstOrDefaultAsync(x => x.NvdProductId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new NvdProductFormViewModel();
        await LoadCategoriesAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxImageBytes + 1024 * 100)]
    public async Task<IActionResult> Create(NvdProductFormViewModel model, CancellationToken cancellationToken)
    {
        if (model.NvdImageFile is null)
            ModelState.AddModelError(nameof(model.NvdImageFile), "Vui lòng chọn ảnh sản phẩm.");

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync(model, cancellationToken);
            return View(model);
        }

        string? imagePath = null;
        try
        {
            imagePath = await imageStorage.SaveImageAsync(model.NvdImageFile!, "products", MaxImageBytes, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(model.NvdImageFile), ex.Message);
            await LoadCategoriesAsync(model, cancellationToken);
            return View(model);
        }

        var item = new NvdProduct
        {
            NvdName = model.NvdName.Trim(),
            NvdImage = imagePath,
            NvdPrice = model.NvdPrice,
            NvdSalePrice = model.NvdSalePrice,
            NvdStatus = model.NvdStatus,
            NvdDescription = model.NvdDescription?.Trim(),
            NvdCategoryId = model.NvdCategoryId,
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

        TempData["Success"] = "Đã thêm sản phẩm mới.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdProducts.AsNoTracking().FirstOrDefaultAsync(x => x.NvdProductId == id, cancellationToken);
        if (item is null) return NotFound();

        var model = new NvdProductFormViewModel
        {
            NvdProductId = item.NvdProductId,
            NvdName = item.NvdName,
            NvdPrice = item.NvdPrice,
            NvdSalePrice = item.NvdSalePrice,
            NvdStatus = item.NvdStatus,
            NvdDescription = item.NvdDescription,
            NvdCategoryId = item.NvdCategoryId,
            NvdCurrentImage = item.NvdImage
        };
        await LoadCategoriesAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxImageBytes + 1024 * 100)]
    public async Task<IActionResult> Edit(int id, NvdProductFormViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.NvdProductId) return NotFound();
        var item = await context.NvdProducts.FirstOrDefaultAsync(x => x.NvdProductId == id, cancellationToken);
        if (item is null) return NotFound();

        if (!ModelState.IsValid)
        {
            model.NvdCurrentImage = item.NvdImage;
            await LoadCategoriesAsync(model, cancellationToken);
            return View(model);
        }

        string? newImagePath = null;
        if (model.NvdImageFile is not null)
        {
            try
            {
                newImagePath = await imageStorage.SaveImageAsync(model.NvdImageFile, "products", MaxImageBytes, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.NvdImageFile), ex.Message);
                model.NvdCurrentImage = item.NvdImage;
                await LoadCategoriesAsync(model, cancellationToken);
                return View(model);
            }
        }

        var oldImagePath = item.NvdImage;
        item.NvdName = model.NvdName.Trim();
        item.NvdPrice = model.NvdPrice;
        item.NvdSalePrice = model.NvdSalePrice;
        item.NvdStatus = model.NvdStatus;
        item.NvdDescription = model.NvdDescription?.Trim();
        item.NvdCategoryId = model.NvdCategoryId;
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
        TempData["Success"] = "Đã cập nhật sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdProducts.AsNoTracking().Include(x => x.NvdCategory)
            .FirstOrDefaultAsync(x => x.NvdProductId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdProducts.FindAsync([id], cancellationToken);
        if (item is not null)
        {
            var imagePath = item.NvdImage;
            context.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
            await imageStorage.DeleteImageAsync(imagePath, cancellationToken);
            TempData["Success"] = "Đã xóa sản phẩm.";
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdProducts.FindAsync([id], cancellationToken);
        if (item is null) return NotFound();
        item.NvdStatus = !item.NvdStatus;
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã đổi trạng thái sản phẩm.";
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategoriesAsync(NvdProductFormViewModel model, CancellationToken cancellationToken)
    {
        model.NvdCategories = new SelectList(
            await context.NvdCategories.AsNoTracking().OrderBy(x => x.NvdName).ToListAsync(cancellationToken),
            nameof(NvdCategory.NvdCategoryId), nameof(NvdCategory.NvdName), model.NvdCategoryId);
    }
}
