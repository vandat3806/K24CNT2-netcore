using Microsoft.AspNetCore.Identity;
using NvdLesson08.Models.DataModels;
using System.Security.Cryptography;

namespace NvdLesson08.Repositories;

public sealed class InMemoryNvdMemberRepository : INvdMemberRepository
{
    private readonly object _syncRoot = new();
    private readonly List<NvdMember> _members;
    private int _nextId;

    public InMemoryNvdMemberRepository(IPasswordHasher<NvdMember> passwordHasher)
    {
        _members = CreateSeedData(passwordHasher);
        _nextId = _members.Max(member => member.Id) + 1;
    }

    public IReadOnlyList<NvdMember> GetAll()
    {
        lock (_syncRoot)
        {
            return _members.Select(Clone).ToList();
        }
    }

    public NvdMember? GetById(int id)
    {
        lock (_syncRoot)
        {
            var member = _members.FirstOrDefault(item => item.Id == id);
            return member is null ? null : Clone(member);
        }
    }

    public bool UserNameExists(string userName, int? exceptId = null)
    {
        lock (_syncRoot)
        {
            return _members.Any(member =>
                member.Id != exceptId &&
                string.Equals(member.UserName, userName.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }

    public bool EmailExists(string email, int? exceptId = null)
    {
        lock (_syncRoot)
        {
            return _members.Any(member =>
                member.Id != exceptId &&
                string.Equals(member.Email, email.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }

    public int Add(NvdMember member)
    {
        lock (_syncRoot)
        {
            member.Id = _nextId++;
            member.CreatedAt = DateTime.Now;
            _members.Add(Clone(member));
            return member.Id;
        }
    }

    public bool Update(NvdMember member)
    {
        lock (_syncRoot)
        {
            var index = _members.FindIndex(item => item.Id == member.Id);
            if (index < 0)
            {
                return false;
            }

            _members[index] = Clone(member);
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_syncRoot)
        {
            var member = _members.FirstOrDefault(item => item.Id == id);
            return member is not null && _members.Remove(member);
        }
    }

    public bool ToggleStatus(int id)
    {
        lock (_syncRoot)
        {
            var member = _members.FirstOrDefault(item => item.Id == id);
            if (member is null)
            {
                return false;
            }

            member.IsActive = !member.IsActive;
            return true;
        }
    }

    private static NvdMember Clone(NvdMember member) => new()
    {
        Id = member.Id,
        UserName = member.UserName,
        PasswordHash = member.PasswordHash,
        FullName = member.FullName,
        Email = member.Email,
        PhoneNumber = member.PhoneNumber,
        Role = member.Role,
        IsActive = member.IsActive,
        JoinedDate = member.JoinedDate,
        CreatedAt = member.CreatedAt
    };

    private static List<NvdMember> CreateSeedData(IPasswordHasher<NvdMember> passwordHasher)
    {
        var seed = new[]
        {
            new NvdMember { Id = 1, UserName = "nguyenvandat", FullName = "Nguyễn Văn Đạt", Email = "dat.2410900021@example.com", PhoneNumber = "0987654321", Role = NvdMemberRole.Administrator, IsActive = true, JoinedDate = new DateTime(2024, 9, 5), CreatedAt = new DateTime(2024, 9, 5, 8, 0, 0) },
            new NvdMember { Id = 2, UserName = "minhanh", FullName = "Trần Minh Anh", Email = "minhanh@example.com", PhoneNumber = "0912345678", Role = NvdMemberRole.Editor, IsActive = true, JoinedDate = new DateTime(2025, 1, 12), CreatedAt = new DateTime(2025, 1, 12, 9, 15, 0) },
            new NvdMember { Id = 3, UserName = "hoangnam", FullName = "Lê Hoàng Nam", Email = "hoangnam@example.com", PhoneNumber = "0903456789", Role = NvdMemberRole.Member, IsActive = true, JoinedDate = new DateTime(2025, 2, 3), CreatedAt = new DateTime(2025, 2, 3, 10, 30, 0) },
            new NvdMember { Id = 4, UserName = "thuyduong", FullName = "Phạm Thùy Dương", Email = "thuyduong@example.com", PhoneNumber = "0934567890", Role = NvdMemberRole.Editor, IsActive = false, JoinedDate = new DateTime(2025, 3, 18), CreatedAt = new DateTime(2025, 3, 18, 14, 0, 0) },
            new NvdMember { Id = 5, UserName = "quanghuy", FullName = "Đỗ Quang Huy", Email = "quanghuy@example.com", PhoneNumber = "0945678901", Role = NvdMemberRole.Member, IsActive = true, JoinedDate = new DateTime(2025, 4, 9), CreatedAt = new DateTime(2025, 4, 9, 16, 20, 0) },
            new NvdMember { Id = 6, UserName = "baongoc", FullName = "Vũ Bảo Ngọc", Email = "baongoc@example.com", PhoneNumber = "0967890123", Role = NvdMemberRole.Member, IsActive = true, JoinedDate = new DateTime(2025, 5, 22), CreatedAt = new DateTime(2025, 5, 22, 11, 10, 0) },
            new NvdMember { Id = 7, UserName = "ducmanh", FullName = "Nguyễn Đức Mạnh", Email = "ducmanh@example.com", PhoneNumber = "0978901234", Role = NvdMemberRole.Member, IsActive = false, JoinedDate = new DateTime(2025, 6, 14), CreatedAt = new DateTime(2025, 6, 14, 13, 35, 0) },
            new NvdMember { Id = 8, UserName = "lanphuong", FullName = "Hoàng Lan Phương", Email = "lanphuong@example.com", PhoneNumber = "0889012345", Role = NvdMemberRole.Editor, IsActive = true, JoinedDate = new DateTime(2025, 7, 1), CreatedAt = new DateTime(2025, 7, 1, 15, 45, 0) },
            new NvdMember { Id = 9, UserName = "tuanviet", FullName = "Bùi Tuấn Việt", Email = "tuanviet@example.com", PhoneNumber = "0890123456", Role = NvdMemberRole.Member, IsActive = true, JoinedDate = new DateTime(2025, 8, 7), CreatedAt = new DateTime(2025, 8, 7, 8, 50, 0) }
        };

        foreach (var member in seed)
        {
            var temporarySecret = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
            member.PasswordHash = passwordHasher.HashPassword(member, temporarySecret);
        }

        return seed.ToList();
    }
}
