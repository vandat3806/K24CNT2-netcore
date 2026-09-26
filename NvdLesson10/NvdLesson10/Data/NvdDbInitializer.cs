using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NvdLesson10.Models;

namespace NvdLesson10.Data;

public static class NvdDbInitializer
{
    public static async Task InitializeAsync(IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<NvdLesson10EfDbContext>();
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher<NvdMember>>();

        await context.Database.EnsureCreatedAsync();
        if (await context.NvdMembers.AnyAsync())
        {
            return;
        }

        var members = new[]
        {
            CreateMember("nguyenvandat", "Nguyễn Văn Đạt", "dat.nguyen@ntu.edu.vn", "0988000021", true, 8),
            CreateMember("thanhdat", "Nguyễn Trần Thành Đạt", "thanhdat@ntu.edu.vn", "0988000020", true, 7),
            CreateMember("xuanbac", "Nguyễn Xuân Bắc", "xuanbac@ntu.edu.vn", "0988000031", true, 6),
            CreateMember("xuantruong", "Nguyễn Xuân Trường", "xuantruong@ntu.edu.vn", "0988000041", false, 5),
            CreateMember("manhtung", "Nguyễn Mạnh Tùng", "manhtung@ntu.edu.vn", "0988000051", true, 4),
            CreateMember("minhanh", "Trần Minh Anh", "minhanh@ntu.edu.vn", "0988000061", true, 3),
            CreateMember("thuha", "Lê Thu Hà", "thuha@ntu.edu.vn", "0988000071", false, 2),
            CreateMember("quanghuy", "Phạm Quang Huy", "quanghuy@ntu.edu.vn", "0988000081", true, 1)
        };

        foreach (var member in members)
        {
            member.NvdPassword = passwordHasher.HashPassword(member, "Nvd@123");
        }

        context.NvdMembers.AddRange(members);
        await context.SaveChangesAsync();
    }

    private static NvdMember CreateMember(string userName, string fullName, string email, string phone, bool status, int daysAgo)
    {
        return new NvdMember
        {
            NvdUserName = userName,
            NvdFullName = fullName,
            NvdEmail = email,
            NvdPhone = phone,
            NvdStatus = status,
            NvdCreatedAt = DateTime.UtcNow.AddDays(-daysAgo)
        };
    }
}
