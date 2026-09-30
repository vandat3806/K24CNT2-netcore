using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NvdLesson12.Data;
using NvdLesson12.Models;
using NvdLesson12.Models.ViewModels;

namespace NvdLesson12.Controllers;

public class NvdMarksController(NvdLesson12DbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, int? subjectId, CancellationToken cancellationToken)
    {
        var query = context.NvdMarks.AsNoTracking()
            .Include(x => x.NvdStudent).ThenInclude(x => x.NvdStdClass)
            .Include(x => x.NvdSubject)
            .AsQueryable();
        if (!string.IsNullOrWhiteSpace(search))
        {
            var keyword = search.Trim();
            query = query.Where(x => x.NvdStudent.NvdStudentName.Contains(keyword));
        }
        if (subjectId.HasValue) query = query.Where(x => x.NvdSubjectId == subjectId.Value);

        ViewBag.Search = search;
        ViewBag.SubjectId = subjectId;
        ViewBag.Subjects = new SelectList(
            await context.NvdSubjects.AsNoTracking().OrderBy(x => x.NvdSubjectName).ToListAsync(cancellationToken),
            nameof(NvdSubject.NvdSubjectId), nameof(NvdSubject.NvdSubjectName), subjectId);
        return View(await query.OrderBy(x => x.NvdStudent.NvdStudentName).ThenBy(x => x.NvdSubject.NvdSubjectName).ToListAsync(cancellationToken));
    }

    public async Task<IActionResult> Details(int studentId, int subjectId, CancellationToken cancellationToken)
    {
        var item = await FindAsync(studentId, subjectId, true, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        var model = new NvdMarkFormViewModel();
        await LoadSelectionsAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NvdMarkFormViewModel model, CancellationToken cancellationToken)
    {
        if (await context.NvdMarks.AnyAsync(x => x.NvdStudentId == model.NvdStudentId && x.NvdSubjectId == model.NvdSubjectId, cancellationToken))
            ModelState.AddModelError(string.Empty, "Sinh viên đã có điểm cho môn học này.");
        if (!ModelState.IsValid)
        {
            await LoadSelectionsAsync(model, cancellationToken);
            return View(model);
        }

        context.Add(new NvdMark
        {
            NvdStudentId = model.NvdStudentId,
            NvdSubjectId = model.NvdSubjectId,
            NvdScore = model.NvdScore
        });
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã thêm điểm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int studentId, int subjectId, CancellationToken cancellationToken)
    {
        var item = await FindAsync(studentId, subjectId, false, cancellationToken);
        if (item is null) return NotFound();
        var model = new NvdMarkFormViewModel
        {
            NvdStudentId = item.NvdStudentId,
            NvdSubjectId = item.NvdSubjectId,
            NvdScore = item.NvdScore
        };
        await LoadSelectionsAsync(model, cancellationToken);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int studentId, int subjectId, NvdMarkFormViewModel model, CancellationToken cancellationToken)
    {
        if (studentId != model.NvdStudentId || subjectId != model.NvdSubjectId) return NotFound();
        if (!ModelState.IsValid)
        {
            await LoadSelectionsAsync(model, cancellationToken);
            return View(model);
        }

        var item = await FindAsync(studentId, subjectId, false, cancellationToken);
        if (item is null) return NotFound();
        item.NvdScore = model.NvdScore;
        await context.SaveChangesAsync(cancellationToken);
        TempData["Success"] = "Đã cập nhật điểm.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int studentId, int subjectId, CancellationToken cancellationToken)
    {
        var item = await FindAsync(studentId, subjectId, true, cancellationToken);
        return item is null ? NotFound() : View(item);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int studentId, int subjectId, CancellationToken cancellationToken)
    {
        var item = await FindAsync(studentId, subjectId, false, cancellationToken);
        if (item is not null)
        {
            context.Remove(item);
            await context.SaveChangesAsync(cancellationToken);
            TempData["Success"] = "Đã xóa điểm.";
        }
        return RedirectToAction(nameof(Index));
    }

    private Task<NvdMark?> FindAsync(int studentId, int subjectId, bool includeNavigation, CancellationToken cancellationToken)
    {
        var query = context.NvdMarks.AsQueryable();
        if (includeNavigation)
            query = query.Include(x => x.NvdStudent).ThenInclude(x => x.NvdStdClass).Include(x => x.NvdSubject);
        return query.FirstOrDefaultAsync(x => x.NvdStudentId == studentId && x.NvdSubjectId == subjectId, cancellationToken);
    }

    private async Task LoadSelectionsAsync(NvdMarkFormViewModel model, CancellationToken cancellationToken)
    {
        model.NvdStudents = new SelectList(
            await context.NvdStudents.AsNoTracking().OrderBy(x => x.NvdStudentName).ToListAsync(cancellationToken),
            nameof(NvdStudent.NvdStudentId), nameof(NvdStudent.NvdStudentName), model.NvdStudentId);
        model.NvdSubjects = new SelectList(
            await context.NvdSubjects.AsNoTracking().OrderBy(x => x.NvdSubjectName).ToListAsync(cancellationToken),
            nameof(NvdSubject.NvdSubjectId), nameof(NvdSubject.NvdSubjectName), model.NvdSubjectId);
    }
}
