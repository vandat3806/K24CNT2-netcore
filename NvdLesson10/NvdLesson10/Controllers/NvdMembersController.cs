using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NvdLesson10.Models;
using NvdLesson10.Models.ViewModels;

namespace NvdLesson10.Controllers;

[Route("thanh-vien")]
public sealed class NvdMembersController : Controller
{
    private const int PageSize = 6;
    private readonly NvdLesson10EfDbContext _context;
    private readonly IPasswordHasher<NvdMember> _passwordHasher;

    public NvdMembersController(
        NvdLesson10EfDbContext context,
        IPasswordHasher<NvdMember> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(string? keyword, string? status, int page = 1)
    {
        var query = _context.NvdMembers.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim();
            query = query.Where(member =>
                member.NvdFullName.Contains(normalizedKeyword) ||
                member.NvdUserName.Contains(normalizedKeyword) ||
                member.NvdEmail.Contains(normalizedKeyword));
        }

        query = status?.ToLowerInvariant() switch
        {
            "active" => query.Where(member => member.NvdStatus),
            "inactive" => query.Where(member => !member.NvdStatus),
            _ => query
        };

        var totalFiltered = await query.CountAsync();
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalFiltered / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var model = new NvdMemberIndexViewModel
        {
            Members = await query
                .OrderByDescending(member => member.NvdCreatedAt)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(),
            Keyword = keyword?.Trim(),
            Status = status,
            CurrentPage = page,
            TotalPages = totalPages,
            TotalFiltered = totalFiltered,
            TotalMembers = await _context.NvdMembers.CountAsync(),
            ActiveMembers = await _context.NvdMembers.CountAsync(member => member.NvdStatus)
        };

        return View(model);
    }

    [HttpGet("chi-tiet/{id:long}")]
    public async Task<IActionResult> Details(long id)
    {
        var member = await _context.NvdMembers.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return member is null ? NotFound() : View(member);
    }

    [HttpGet("them-moi")]
    public IActionResult Create()
    {
        return View(new NvdMemberCreateViewModel());
    }

    [HttpPost("them-moi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NvdMemberCreateViewModel model)
    {
        await ValidateUniqueFieldsAsync(model.UserName, model.Email);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var member = new NvdMember
        {
            NvdUserName = model.UserName.Trim(),
            NvdFullName = model.FullName.Trim(),
            NvdEmail = model.Email.Trim(),
            NvdPhone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim(),
            NvdStatus = model.Status,
            NvdCreatedAt = DateTime.UtcNow
        };
        member.NvdPassword = _passwordHasher.HashPassword(member, model.Password);

        _context.NvdMembers.Add(member);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã thêm thành viên “{member.NvdFullName}” vào cơ sở dữ liệu.";
        return RedirectToAction(nameof(Details), new { id = member.Id });
    }

    [HttpGet("chinh-sua/{id:long}")]
    public async Task<IActionResult> Edit(long id)
    {
        var member = await _context.NvdMembers.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        if (member is null)
        {
            return NotFound();
        }

        return View(new NvdMemberEditViewModel
        {
            Id = member.Id,
            UserName = member.NvdUserName,
            FullName = member.NvdFullName,
            Email = member.NvdEmail,
            Phone = member.NvdPhone,
            Status = member.NvdStatus
        });
    }

    [HttpPost("chinh-sua/{id:long}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(long id, NvdMemberEditViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var member = await _context.NvdMembers.FirstOrDefaultAsync(item => item.Id == id);
        if (member is null)
        {
            return NotFound();
        }

        await ValidateUniqueFieldsAsync(model.UserName, model.Email, id);
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        member.NvdUserName = model.UserName.Trim();
        member.NvdFullName = model.FullName.Trim();
        member.NvdEmail = model.Email.Trim();
        member.NvdPhone = string.IsNullOrWhiteSpace(model.Phone) ? null : model.Phone.Trim();
        member.NvdStatus = model.Status;

        if (!string.IsNullOrWhiteSpace(model.NewPassword))
        {
            member.NvdPassword = _passwordHasher.HashPassword(member, model.NewPassword);
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.NvdMembers.AnyAsync(item => item.Id == id))
            {
                return NotFound();
            }

            throw;
        }

        TempData["SuccessMessage"] = $"Đã cập nhật thành viên “{member.NvdFullName}”.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost("doi-trang-thai/{id:long}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleStatus(long id)
    {
        var member = await _context.NvdMembers.FirstOrDefaultAsync(item => item.Id == id);
        if (member is null)
        {
            return NotFound();
        }

        member.NvdStatus = !member.NvdStatus;
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Đã đổi trạng thái của “{member.NvdFullName}”.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("xoa/{id:long}")]
    public async Task<IActionResult> Delete(long id)
    {
        var member = await _context.NvdMembers.AsNoTracking().FirstOrDefaultAsync(item => item.Id == id);
        return member is null ? NotFound() : View(member);
    }

    [HttpPost("xoa/{id:long}")]
    [ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(long id)
    {
        var member = await _context.NvdMembers.FindAsync(id);
        if (member is null)
        {
            return NotFound();
        }

        _context.NvdMembers.Remove(member);
        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Đã xóa thành viên “{member.NvdFullName}” khỏi cơ sở dữ liệu.";
        return RedirectToAction(nameof(Index));
    }

    private async Task ValidateUniqueFieldsAsync(string userName, string email, long? excludedId = null)
    {
        var normalizedUserName = userName.Trim();
        var normalizedEmail = email.Trim();

        if (await _context.NvdMembers.AnyAsync(member =>
                member.NvdUserName == normalizedUserName && member.Id != excludedId))
        {
            ModelState.AddModelError(nameof(NvdMemberFormViewModel.UserName), "Tên đăng nhập đã tồn tại trong cơ sở dữ liệu.");
        }

        if (await _context.NvdMembers.AnyAsync(member =>
                member.NvdEmail == normalizedEmail && member.Id != excludedId))
        {
            ModelState.AddModelError(nameof(NvdMemberFormViewModel.Email), "Email đã tồn tại trong cơ sở dữ liệu.");
        }
    }
}
