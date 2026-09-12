using Microsoft.AspNetCore.Mvc;
using NvdLesson07.Models.DataModels;
using NvdLesson07.Models.ViewModels;
using NvdLesson07.Repositories;

namespace NvdLesson07.Controllers;

[Route("thanh-vien")]
public sealed class NvdMemberController : Controller
{
    private readonly INvdMemberRepository _repository;

    public NvdMemberController(INvdMemberRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("")]
    public IActionResult Index(string? keyword, string status = "all")
    {
        var allMembers = _repository.GetAll();
        IEnumerable<NvdMember> filteredMembers = allMembers;
        var normalizedKeyword = keyword?.Trim() ?? string.Empty;
        var normalizedStatus = status is "active" or "inactive" ? status : "all";

        if (!string.IsNullOrWhiteSpace(normalizedKeyword))
        {
            filteredMembers = filteredMembers.Where(member =>
                member.NvdFullName.Contains(normalizedKeyword, StringComparison.CurrentCultureIgnoreCase) ||
                member.NvdUserName.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                member.NvdEmail.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                member.NvdClassName.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase));
        }

        filteredMembers = normalizedStatus switch
        {
            "active" => filteredMembers.Where(member => member.NvdIsActive),
            "inactive" => filteredMembers.Where(member => !member.NvdIsActive),
            _ => filteredMembers
        };

        var model = new NvdMemberIndexViewModel
        {
            Members = filteredMembers.ToList(),
            Keyword = normalizedKeyword,
            Status = normalizedStatus,
            TotalMembers = allMembers.Count,
            ActiveMembers = allMembers.Count(member => member.NvdIsActive)
        };

        return View(model);
    }

    // Minh họa truyền một đối tượng Model trực tiếp sang View.
    [HttpGet("model-don")]
    public IActionResult GetMember()
    {
        var member = _repository.GetById(1);
        return member is null ? NotFound() : View(member);
    }

    // Minh họa truyền danh sách bằng ViewBag để so sánh với strongly typed Model.
    [HttpGet("viewbag")]
    public IActionResult GetMembers()
    {
        ViewBag.Members = _repository.GetAll();
        ViewBag.TransferMethod = "ViewBag (dynamic)";
        return View();
    }

    [HttpGet("chi-tiet/{id:int}")]
    public IActionResult Details(int id)
    {
        var member = _repository.GetById(id);
        return member is null ? NotFound() : View(member);
    }

    [HttpGet("them-moi")]
    public IActionResult Create()
    {
        return View(new NvdMember { NvdClassName = "K24CNT2", NvdIsActive = true });
    }

    [HttpPost("them-moi")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("NvdUserName,NvdFullName,NvdEmail,NvdPhone,NvdClassName,NvdIsActive")] NvdMember member)
    {
        Normalize(member);
        ValidateUniqueFields(member);

        if (!ModelState.IsValid)
        {
            return View(member);
        }

        var created = _repository.Add(member);
        TempData["SuccessMessage"] = $"Đã thêm thành viên {created.NvdFullName}.";
        return RedirectToAction(nameof(Details), new { id = created.NvdMemberId });
    }

    [HttpGet("cap-nhat/{id:int}")]
    public IActionResult Edit(int id)
    {
        var member = _repository.GetById(id);
        return member is null ? NotFound() : View(member);
    }

    [HttpPost("cap-nhat/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("NvdUserName,NvdFullName,NvdEmail,NvdPhone,NvdClassName,NvdIsActive")] NvdMember member)
    {
        if (_repository.GetById(id) is null)
        {
            return NotFound();
        }

        member.NvdMemberId = id;
        Normalize(member);
        ValidateUniqueFields(member, id);

        if (!ModelState.IsValid)
        {
            return View(member);
        }

        _repository.Update(member);
        TempData["SuccessMessage"] = $"Đã cập nhật thành viên {member.NvdFullName}.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet("xoa/{id:int}")]
    public IActionResult Delete(int id)
    {
        var member = _repository.GetById(id);
        return member is null ? NotFound() : View(member);
    }

    [HttpPost("xac-nhan-xoa/{id:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var member = _repository.GetById(id);
        if (member is null)
        {
            return NotFound();
        }

        _repository.Delete(id);
        TempData["SuccessMessage"] = $"Đã xóa thành viên {member.NvdFullName}.";
        return RedirectToAction(nameof(Index));
    }

    private void ValidateUniqueFields(NvdMember member, int? exceptId = null)
    {
        if (_repository.UserNameExists(member.NvdUserName, exceptId))
        {
            ModelState.AddModelError(nameof(member.NvdUserName), "Tên đăng nhập đã được sử dụng.");
        }

        if (_repository.EmailExists(member.NvdEmail, exceptId))
        {
            ModelState.AddModelError(nameof(member.NvdEmail), "Email đã được sử dụng.");
        }
    }

    private static void Normalize(NvdMember member)
    {
        member.NvdUserName = member.NvdUserName?.Trim() ?? string.Empty;
        member.NvdFullName = member.NvdFullName?.Trim() ?? string.Empty;
        member.NvdEmail = member.NvdEmail?.Trim() ?? string.Empty;
        member.NvdPhone = member.NvdPhone?.Trim() ?? string.Empty;
        member.NvdClassName = (member.NvdClassName?.Trim() ?? string.Empty).ToUpperInvariant();
    }
}
