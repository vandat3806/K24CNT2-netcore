using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenVanDat2410900021_exam.Data;
using NguyenVanDat2410900021_exam.Models;

namespace NguyenVanDat2410900021_exam.Controllers;

public class NvdStudentsController : Controller
{
    private readonly NvdDbContext _context;

    public NvdStudentsController(NvdDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchString)
    {
        var students = _context.NvdStudents.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchString))
        {
            searchString = searchString.Trim();
            students = students.Where(s =>
                s.NvdStudentCode.Contains(searchString) ||
                s.NvdName.Contains(searchString) ||
                s.NvdClass.Contains(searchString));
        }

        ViewData["CurrentFilter"] = searchString;
        return View(await students.OrderBy(s => s.Id).ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.NvdStudents
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (student == null) return NotFound();
        return View(student);
    }

    public IActionResult Create()
    {
        return View(new NvdStudent { NvdActive = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,NvdStudentCode,NvdName,NvdGender,NvdBirthDay,NvdEmail,NvdPhone,NvdClass,NvdActive")] NvdStudent student)
    {
        if (await _context.NvdStudents.AnyAsync(s => s.NvdStudentCode == student.NvdStudentCode))
        {
            ModelState.AddModelError(nameof(student.NvdStudentCode), "Mã sinh viên đã tồn tại");
        }

        if (!ModelState.IsValid) return View(student);

        _context.Add(student);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Thêm sinh viên thành công.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.NvdStudents.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,NvdStudentCode,NvdName,NvdGender,NvdBirthDay,NvdEmail,NvdPhone,NvdClass,NvdActive")] NvdStudent student)
    {
        if (id != student.Id) return NotFound();

        if (await _context.NvdStudents.AnyAsync(s => s.NvdStudentCode == student.NvdStudentCode && s.Id != student.Id))
        {
            ModelState.AddModelError(nameof(student.NvdStudentCode), "Mã sinh viên đã tồn tại");
        }

        if (!ModelState.IsValid) return View(student);

        try
        {
            _context.Update(student);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Cập nhật sinh viên thành công.";
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NvdStudentExists(student.Id)) return NotFound();
            throw;
        }

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var student = await _context.NvdStudents
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (student == null) return NotFound();
        return View(student);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.NvdStudents.FindAsync(id);
        if (student != null)
        {
            _context.NvdStudents.Remove(student);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Xóa sinh viên thành công.";
        }

        return RedirectToAction(nameof(Index));
    }

    private bool NvdStudentExists(int id)
    {
        return _context.NvdStudents.Any(e => e.Id == id);
    }
}
