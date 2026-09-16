using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NvdLesson08.Models.DataModels;
using NvdLesson08.Models.ViewModels;
using NvdLesson08.Repositories;

namespace NvdLesson08.Controllers;

[Route("thanh-vien")]
public sealed class NvdMemberController : Controller
{
    private const int PageSize = 5;
    private readonly INvdMemberRepository _memberRepository;
    private readonly IPasswordHasher<NvdMember> _passwordHasher;

    public NvdMemberController(
        INvdMemberRepository memberRepository,
        IPasswordHasher<NvdMember> passwordHasher)
    {
        _memberRepository = memberRepository;
        _passwordHasher = passwordHasher;
    }

    [HttpGet("")]
    public IActionResult Index(string? query, NvdMemberRole? role, string status = "all", int page = 1)
    {
        var allMembers = _memberRepository.GetAll();
        var filteredMembers = allMembers.AsEnumerable();
        var normalizedQuery = query?.Trim() ?? string.Empty;

        if (!string.IsNullOrWhiteSpace(normalizedQuery))
        {
            filteredMembers = filteredMembers.Where(member =>
                member.FullName.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ||
                member.UserName.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase) ||
                member.Email.Contains(normalizedQuery, StringComparison.OrdinalIgnoreCase));
        }

        if (role.HasValue)
        {
            filteredMembers = filteredMembers.Where(member => member.Role == role.Value);
        }

        status = status is "active" or "inactive" ? status : "all";
        filteredMembers = status switch
        {
            "active" => filteredMembers.Where(member => member.IsActive),
            "inactive" => filteredMembers.Where(member => !member.IsActive),
            _ => filteredMembers
        };

        var orderedMembers = filteredMembers
            .OrderBy(member => member.Id)
            .ToList();
        var totalItems = orderedMembers.Count;
        var totalPages = Math.Max(1, (int)Math.Ceiling(totalItems / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var viewModel = new NvdMemberIndexViewModel
        {
            Members = orderedMembers.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
            Query = normalizedQuery,
            Role = role,
            Status = status,
            Page = page,
            PageSize = PageSize,
            TotalItems = totalItems,
            ActiveCount = allMembers.Count(member => member.IsActive),
            InactiveCount = allMembers.Count(member => !member.IsActive)
        };

        return View(viewModel);
    }

    [HttpGet("them-moi")]
    public IActionResult Create()
    {
        return View(new NvdMemberCreateViewModel());
    }

    [HttpPost("them-moi")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(NvdMemberCreateViewModel viewModel)
    {
        ValidateUniqueFields(viewModel.UserName, viewModel.Email);
        ValidateJoinedDate(viewModel.JoinedDate);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var member = new NvdMember
        {
            UserName = viewModel.UserName.Trim(),
            FullName = viewModel.FullName.Trim(),
            Email = viewModel.Email.Trim(),
            PhoneNumber = viewModel.PhoneNumber.Trim(),
            Role = viewModel.Role,
            IsActive = viewModel.IsActive,
            JoinedDate = viewModel.JoinedDate.Date
        };
        member.PasswordHash = _passwordHasher.HashPassword(member, viewModel.Password);

        var id = _memberRepository.Add(member);
        TempData["SuccessMessage"] = $"Đã thêm thành viên {member.FullName}.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet("chi-tiet/{id:int}")]
    public IActionResult Details(int id)
    {
        var member = _memberRepository.GetById(id);
        return member is null ? NotFound() : View(member);
    }

    [HttpGet("cap-nhat/{id:int}")]
    public IActionResult Edit(int id)
    {
        var member = _memberRepository.GetById(id);
        if (member is null)
        {
            return NotFound();
        }

        return View(new NvdMemberEditViewModel
        {
            Id = member.Id,
            UserName = member.UserName,
            FullName = member.FullName,
            Email = member.Email,
            PhoneNumber = member.PhoneNumber,
            Role = member.Role,
            IsActive = member.IsActive,
            JoinedDate = member.JoinedDate
        });
    }

    [HttpPost("cap-nhat/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, NvdMemberEditViewModel viewModel)
    {
        if (id != viewModel.Id)
        {
            return BadRequest();
        }

        var member = _memberRepository.GetById(id);
        if (member is null)
        {
            return NotFound();
        }

        ValidateUniqueFields(viewModel.UserName, viewModel.Email, id);
        ValidateJoinedDate(viewModel.JoinedDate);
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        member.UserName = viewModel.UserName.Trim();
        member.FullName = viewModel.FullName.Trim();
        member.Email = viewModel.Email.Trim();
        member.PhoneNumber = viewModel.PhoneNumber.Trim();
        member.Role = viewModel.Role;
        member.IsActive = viewModel.IsActive;
        member.JoinedDate = viewModel.JoinedDate.Date;
        _memberRepository.Update(member);

        TempData["SuccessMessage"] = $"Đã cập nhật {member.FullName}.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet("xoa/{id:int}")]
    public IActionResult Delete(int id)
    {
        var member = _memberRepository.GetById(id);
        return member is null ? NotFound() : View(member);
    }

    [HttpPost("xoa/{id:int}")]
    [ValidateAntiForgeryToken]
    [ActionName(nameof(Delete))]
    public IActionResult DeleteConfirmed(int id)
    {
        var member = _memberRepository.GetById(id);
        if (member is null)
        {
            return NotFound();
        }

        _memberRepository.Delete(id);
        TempData["SuccessMessage"] = $"Đã xóa thành viên {member.FullName}.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("doi-trang-thai/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleStatus(int id, string? returnUrl)
    {
        if (!_memberRepository.ToggleStatus(id))
        {
            return NotFound();
        }

        TempData["SuccessMessage"] = "Đã thay đổi trạng thái thành viên.";
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return LocalRedirect(returnUrl);
        }

        return RedirectToAction(nameof(Index));
    }

    private void ValidateUniqueFields(string userName, string email, int? exceptId = null)
    {
        if (!string.IsNullOrWhiteSpace(userName) && _memberRepository.UserNameExists(userName, exceptId))
        {
            ModelState.AddModelError(nameof(NvdMemberFormViewModel.UserName), "Tên đăng nhập đã tồn tại.");
        }

        if (!string.IsNullOrWhiteSpace(email) && _memberRepository.EmailExists(email, exceptId))
        {
            ModelState.AddModelError(nameof(NvdMemberFormViewModel.Email), "Email đã được sử dụng.");
        }
    }

    private void ValidateJoinedDate(DateTime joinedDate)
    {
        if (joinedDate.Date > DateTime.Today)
        {
            ModelState.AddModelError(nameof(NvdMemberFormViewModel.JoinedDate), "Ngày tham gia không được ở tương lai.");
        }
    }
}
