using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;
using NvdLesson12.Models.ViewModels;
using NvdLesson12.Services;

namespace NvdLesson12.Controllers;

public class NvdStudentsController(
    NvdLesson12DbContext context,
    IImageStorageService imageStorage) : Controller
{
    private const long MaxAvatarBytes = 2 * 1024 * 1024;

    public async Task<IActionResult> Index(string? search, int? classId, CancellationToken cancellationToken)
    {
        var query = context.NvdStudents.AsNoTracking().Include(x => x.NvdStdClass).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdStudentName.Contains(keyword) || x.NvdStudentEmail.Contains(keyword) || x.NvdStudentPhone.Contains(keyword));
        }
        if (classId.HasValue) query = query.Where(x => x.NvdStdClassId == classId.Value);

        ViewBag.Search = search;
        ViewBag.ClassId = classId;
        ViewBag.Classes = new SelectList(
            await context.NvdStdClasses.AsNoTracking().OrderBy(x => x.NvdClassName).ToListAsync(cancellationToken),
            nameof(NvdStdClass.NvdStdClassId), nameof(NvdStdClass.NvdClassName), classId);
        return View(await query.OrderBy(x => x.NvdStudentName).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdStudents.AsNoTracking()
            .Include(x => x.NvdStdClass)
            .Include(x => x.NvdMarks).ThenInclude(x => x.NvdSubject)
            .FirstOrDefaultAsync(x => x.NvdStudentId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new NvdStudentFormViewModel();
        await LoadClassesAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxAvatarBytes + 1024 * 100)]
    public async Task<IActionResult> Create(NvdStudentFormViewModel model, CancellationToken cancellationToken)
    {
        Normalize(model);
        await ValidateUniqueAsync(model, null, cancellationToken);
        if (model.NvdAvatarFile is null)
            ModelState.AddModelError(nameof(model.NvdAvatarFile), "Vui lòng chọn ảnh đại diện.");
        if (!ModelState.IsValid)
        {
            await LoadClassesAsync(model, cancellationToken);
            return View(model);
        }

        string avatarPath;
        try
        {
            avatarPath = await imageStorage.SaveImageAsync(model.NvdAvatarFile!, "students", MaxAvatarBytes, cancellationToken);
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(model.NvdAvatarFile), ex.Message);
            await LoadClassesAsync(model, cancellationToken);
            return View(model);
        }

        var item = new NvdStudent
        {
            NvdStudentName = model.NvdStudentName,
            NvdStudentEmail = model.NvdStudentEmail,
            NvdStudentPhone = model.NvdStudentPhone,
            NvdStudentAddress = model.NvdStudentAddress,
            NvdStudentAvatar = avatarPath,
            NvdStudentBirthday = model.NvdStudentBirthday.Date,
            NvdStdClassId = model.NvdStdClassId
        };
        try
        {
            context.Add(item);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await imageStorage.DeleteImageAsync(avatarPath, cancellationToken);
            throw;
        }

        TempData["Success"] = "Đã thêm sinh viên.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdStudents.AsNoTracking().FirstOrDefaultAsync(x => x.NvdStudentId == id, cancellationToken);
        if (item is null) return NotFound();
        var model = new NvdStudentFormViewModel
        {
            NvdStudentId = item.NvdStudentId,
            NvdStudentName = item.NvdStudentName,
            NvdStudentEmail = item.NvdStudentEmail,
            NvdStudentPhone = item.NvdStudentPhone,
            NvdStudentAddress = item.NvdStudentAddress,
            NvdStudentBirthday = item.NvdStudentBirthday,
            NvdStdClassId = item.NvdStdClassId,
            NvdCurrentAvatar = item.NvdStudentAvatar
        };
        await LoadClassesAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [RequestFormLimits(MultipartBodyLengthLimit = MaxAvatarBytes + 1024 * 100)]
    public async Task<IActionResult> Edit(int id, NvdStudentFormViewModel model, CancellationToken cancellationToken)
    {
        if (id != model.NvdStudentId) return NotFound();
        var item = await context.NvdStudents.FirstOrDefaultAsync(x => x.NvdStudentId == id, cancellationToken);
        if (item is null) return NotFound();

        Normalize(model);
        await ValidateUniqueAsync(model, id, cancellationToken);
        if (!ModelState.IsValid)
        {
            model.NvdCurrentAvatar = item.NvdStudentAvatar;
            await LoadClassesAsync(model, cancellationToken);
            return View(model);
        }

        string? newAvatarPath = null;
        if (model.NvdAvatarFile is not null)
        {
            try
            {
                newAvatarPath = await imageStorage.SaveImageAsync(model.NvdAvatarFile, "students", MaxAvatarBytes, cancellationToken);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(nameof(model.NvdAvatarFile), ex.Message);
                model.NvdCurrentAvatar = item.NvdStudentAvatar;
                await LoadClassesAsync(model, cancellationToken);
                return View(model);
            }
        }

        var oldAvatarPath = item.NvdStudentAvatar;
        item.NvdStudentName = model.NvdStudentName;
        item.NvdStudentEmail = model.NvdStudentEmail;
        item.NvdStudentPhone = model.NvdStudentPhone;
        item.NvdStudentAddress = model.NvdStudentAddress;
        item.NvdStudentBirthday = model.NvdStudentBirthday.Date;
        item.NvdStdClassId = model.NvdStdClassId;
        if (newAvatarPath is not null) item.NvdStudentAvatar = newAvatarPath;
        try
        {
            await context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            await imageStorage.DeleteImageAsync(newAvatarPath, cancellationToken);
            throw;
        }

        if (newAvatarPath is not null) await imageStorage.DeleteImageAsync(oldAvatarPath, cancellationToken);
        TempData["Success"] = "Đã cập nhật sinh viên.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id, CancellationToken cancellationToken)
    {
        if (id is null) return NotFound();
        var item = await context.NvdStudents.AsNoTracking().Include(x => x.NvdStdClass).Include(x => x.NvdMarks)
            .FirstOrDefaultAsync(x => x.NvdStudentId == id, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var item = await context.NvdStudents.FindAsync([id], cancellationToken);
        if (item is not null)
        {
            var avatarPath = item.NvdStudentAvatar;
            context.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
            await imageStorage.DeleteImageAsync(avatarPath, cancellationToken);
            TempData["Success"] = "Đã xóa sinh viên và các điểm liên quan.";
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task LoadClassesAsync(NvdStudentFormViewModel model, CancellationToken cancellationToken)
    {
        model.NvdClasses = new SelectList(
            await context.NvdStdClasses.AsNoTracking().OrderBy(x => x.NvdClassName).ToListAsync(cancellationToken),
            nameof(NvdStdClass.NvdStdClassId), nameof(NvdStdClass.NvdClassName), model.NvdStdClassId);
    }

    private async Task ValidateUniqueAsync(NvdStudentFormViewModel model, int? currentId, CancellationToken cancellationToken)
    {
        if (await context.NvdStudents.AnyAsync(x => x.NvdStudentId != currentId && x.NvdStudentEmail == model.NvdStudentEmail, cancellationToken))
            ModelState.AddModelError(nameof(model.NvdStudentEmail), "Email đã được sử dụng.");
        if (await context.NvdStudents.AnyAsync(x => x.NvdStudentId != currentId && x.NvdStudentPhone == model.NvdStudentPhone, cancellationToken))
            ModelState.AddModelError(nameof(model.NvdStudentPhone), "Số điện thoại đã được sử dụng.");
    }

    private static void Normalize(NvdStudentFormViewModel model)
    {
        model.NvdStudentName = model.NvdStudentName.Trim();
        model.NvdStudentEmail = model.NvdStudentEmail.Trim().ToLowerInvariant();
        model.NvdStudentPhone = model.NvdStudentPhone.Trim().Replace(" ", string.Empty, StringComparison.Ordinal);
        model.NvdStudentAddress = model.NvdStudentAddress.Trim();
    }
}
