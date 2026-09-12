using NvdLesson07.Models.DataModels;

namespace NvdLesson07.Repositories;

/// <summary>
/// Kho dữ liệu in-memory phục vụ bài học Model, không sử dụng cơ sở dữ liệu.
/// </summary>
public sealed class InMemoryNvdMemberRepository : INvdMemberRepository
{
    private readonly object _syncRoot = new();
    private readonly List<NvdMember> _members =
    [
        new() { NvdMemberId = 1, NvdUserName = "nvd2410900021", NvdFullName = "Nguyễn Văn Đạt", NvdEmail = "dat.nguyen2410900021@example.com", NvdPhone = "0987654321", NvdClassName = "K24CNT2", NvdJoinedDate = new DateTime(2026, 9, 1), NvdIsActive = true },
        new() { NvdMemberId = 2, NvdUserName = "minhanh", NvdFullName = "Trần Minh Anh", NvdEmail = "minhanh@example.com", NvdPhone = "0912345678", NvdClassName = "K24CNT2", NvdJoinedDate = new DateTime(2026, 9, 2), NvdIsActive = true },
        new() { NvdMemberId = 3, NvdUserName = "hoangnam", NvdFullName = "Lê Hoàng Nam", NvdEmail = "hoangnam@example.com", NvdPhone = "0934567890", NvdClassName = "K24CNT1", NvdJoinedDate = new DateTime(2026, 9, 3), NvdIsActive = true },
        new() { NvdMemberId = 4, NvdUserName = "thuyduong", NvdFullName = "Phạm Thùy Dương", NvdEmail = "thuyduong@example.com", NvdPhone = "0965432109", NvdClassName = "K24CNT3", NvdJoinedDate = new DateTime(2026, 9, 4), NvdIsActive = false },
        new() { NvdMemberId = 5, NvdUserName = "quanghuy", NvdFullName = "Đỗ Quang Huy", NvdEmail = "quanghuy@example.com", NvdPhone = "0971122334", NvdClassName = "K24CNT2", NvdJoinedDate = new DateTime(2026, 9, 5), NvdIsActive = true },
        new() { NvdMemberId = 6, NvdUserName = "ngocmai", NvdFullName = "Vũ Ngọc Mai", NvdEmail = "ngocmai@example.com", NvdPhone = "0905566778", NvdClassName = "K24CNT1", NvdJoinedDate = new DateTime(2026, 9, 6), NvdIsActive = false }
    ];

    public IReadOnlyList<NvdMember> GetAll()
    {
        lock (_syncRoot)
        {
            return _members.Select(Clone).OrderBy(member => member.NvdMemberId).ToList();
        }
    }

    public NvdMember? GetById(int id)
    {
        lock (_syncRoot)
        {
            var member = _members.FirstOrDefault(item => item.NvdMemberId == id);
            return member is null ? null : Clone(member);
        }
    }

    public NvdMember Add(NvdMember member)
    {
        lock (_syncRoot)
        {
            var stored = Clone(member);
            stored.NvdMemberId = _members.Count == 0 ? 1 : _members.Max(item => item.NvdMemberId) + 1;
            stored.NvdJoinedDate = DateTime.Today;
            _members.Add(stored);
            return Clone(stored);
        }
    }

    public bool Update(NvdMember member)
    {
        lock (_syncRoot)
        {
            var index = _members.FindIndex(item => item.NvdMemberId == member.NvdMemberId);
            if (index < 0)
            {
                return false;
            }

            var joinedDate = _members[index].NvdJoinedDate;
            _members[index] = Clone(member);
            _members[index].NvdJoinedDate = joinedDate;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_syncRoot)
        {
            return _members.RemoveAll(item => item.NvdMemberId == id) > 0;
        }
    }

    public bool UserNameExists(string userName, int? exceptId = null)
    {
        lock (_syncRoot)
        {
            return _members.Any(item =>
                item.NvdMemberId != exceptId &&
                item.NvdUserName.Equals(userName.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }

    public bool EmailExists(string email, int? exceptId = null)
    {
        lock (_syncRoot)
        {
            return _members.Any(item =>
                item.NvdMemberId != exceptId &&
                item.NvdEmail.Equals(email.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }

    private static NvdMember Clone(NvdMember member) => new()
    {
        NvdMemberId = member.NvdMemberId,
        NvdUserName = member.NvdUserName,
        NvdFullName = member.NvdFullName,
        NvdEmail = member.NvdEmail,
        NvdPhone = member.NvdPhone,
        NvdClassName = member.NvdClassName,
        NvdJoinedDate = member.NvdJoinedDate,
        NvdIsActive = member.NvdIsActive
    };
}
