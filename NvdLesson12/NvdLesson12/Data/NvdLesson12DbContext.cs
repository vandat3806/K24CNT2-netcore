using Microsoft.EntityFrameworkCore;
using NvdLesson12.Models;

namespace NvdLesson12.Data;

public class NvdLesson12DbContext(DbContextOptions<NvdLesson12DbContext> options) : DbContext(options)
{
    public DbSet<NvdCategory> NvdCategories => Set<NvdCategory>();
    public DbSet<NvdProduct> NvdProducts => Set<NvdProduct>();
    public DbSet<NvdBanner> NvdBanners => Set<NvdBanner>();
    public DbSet<NvdStdClass> NvdStdClasses => Set<NvdStdClass>();
    public DbSet<NvdStudent> NvdStudents => Set<NvdStudent>();
    public DbSet<NvdSubject> NvdSubjects => Set<NvdSubject>();
    public DbSet<NvdMark> NvdMarks => Set<NvdMark>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<NvdCategory>()
            .HasIndex(x => x.NvdName)
            .IsUnique();

        modelBuilder.Entity<NvdProduct>()
            .HasOne(x => x.NvdCategory)
            .WithMany(x => x.NvdProducts)
            .HasForeignKey(x => x.NvdCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NvdStdClass>()
            .HasIndex(x => x.NvdClassName)
            .IsUnique();

        modelBuilder.Entity<NvdStudent>()
            .HasIndex(x => x.NvdStudentEmail)
            .IsUnique();
        modelBuilder.Entity<NvdStudent>()
            .HasIndex(x => x.NvdStudentPhone)
            .IsUnique();
        modelBuilder.Entity<NvdStudent>()
            .HasOne(x => x.NvdStdClass)
            .WithMany(x => x.NvdStudents)
            .HasForeignKey(x => x.NvdStdClassId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<NvdSubject>()
            .HasIndex(x => x.NvdSubjectName)
            .IsUnique();

        modelBuilder.Entity<NvdMark>()
            .HasKey(x => new { x.NvdSubjectId, x.NvdStudentId });
        modelBuilder.Entity<NvdMark>()
            .HasOne(x => x.NvdSubject)
            .WithMany(x => x.NvdMarks)
            .HasForeignKey(x => x.NvdSubjectId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<NvdMark>()
            .HasOne(x => x.NvdStudent)
            .WithMany(x => x.NvdMarks)
            .HasForeignKey(x => x.NvdStudentId)
            .OnDelete(DeleteBehavior.Cascade);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var created = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<NvdCategory>().HasData(
            new NvdCategory { NvdCategoryId = 1, NvdName = "Máy tính xách tay", NvdStatus = true, NvdCreatedDate = created },
            new NvdCategory { NvdCategoryId = 2, NvdName = "Điện thoại", NvdStatus = true, NvdCreatedDate = created },
            new NvdCategory { NvdCategoryId = 3, NvdName = "Phụ kiện", NvdStatus = true, NvdCreatedDate = created });

        modelBuilder.Entity<NvdProduct>().HasData(
            new NvdProduct { NvdProductId = 1, NvdName = "Laptop học tập NVD", NvdImage = "/images/product-laptop.svg", NvdPrice = 18990000, NvdSalePrice = 16990000, NvdStatus = true, NvdDescription = "Cấu hình cân bằng cho học tập và lập trình ASP.NET Core.", NvdCategoryId = 1, NvdCreatedDate = created },
            new NvdProduct { NvdProductId = 2, NvdName = "Điện thoại NVD 5G", NvdImage = "/images/product-phone.svg", NvdPrice = 10990000, NvdSalePrice = 9990000, NvdStatus = true, NvdDescription = "Màn hình đẹp, pin lâu và hỗ trợ kết nối 5G.", NvdCategoryId = 2, NvdCreatedDate = created },
            new NvdProduct { NvdProductId = 3, NvdName = "Bàn phím cơ NVD", NvdImage = "/images/product-keyboard.svg", NvdPrice = 1290000, NvdSalePrice = 990000, NvdStatus = true, NvdDescription = "Bàn phím gọn nhẹ dành cho góc học tập.", NvdCategoryId = 3, NvdCreatedDate = created });

        modelBuilder.Entity<NvdBanner>().HasData(
            new NvdBanner { NvdBannerId = 1, NvdName = "Lesson12 — Entity Framework Core", NvdImage = "/images/banner-ef.svg", NvdDescription = "Code First, quan hệ dữ liệu, CRUD và upload ảnh an toàn.", NvdCreatedDate = created, NvdStatus = true },
            new NvdBanner { NvdBannerId = 2, NvdName = "Nguyễn Văn Đạt — 2410900021", NvdImage = "/images/banner-student.svg", NvdDescription = "Bài thực hành ASP.NET Core MVC — lớp K24CNT2.", NvdCreatedDate = created, NvdStatus = true });

        modelBuilder.Entity<NvdStdClass>().HasData(
            new NvdStdClass { NvdStdClassId = 1, NvdClassName = "K24CNT2" },
            new NvdStdClass { NvdStdClassId = 2, NvdClassName = "K24CNT1" });

        modelBuilder.Entity<NvdStudent>().HasData(
            new NvdStudent { NvdStudentId = 1, NvdStudentName = "Nguyễn Văn Đạt", NvdStudentEmail = "dat.2410900021@example.edu.vn", NvdStudentPhone = "0900000021", NvdStudentAddress = "Hà Nội", NvdStudentAvatar = "/images/avatar-student.svg", NvdStudentBirthday = new DateTime(2006, 1, 1), NvdStdClassId = 1 },
            new NvdStudent { NvdStudentId = 2, NvdStudentName = "Sinh viên minh họa", NvdStudentEmail = "demo.student@example.edu.vn", NvdStudentPhone = "0900000099", NvdStudentAddress = "Hà Nội", NvdStudentAvatar = "/images/avatar-student.svg", NvdStudentBirthday = new DateTime(2005, 8, 20), NvdStdClassId = 2 });

        modelBuilder.Entity<NvdSubject>().HasData(
            new NvdSubject { NvdSubjectId = 1, NvdSubjectName = "Lập trình ASP.NET Core MVC" },
            new NvdSubject { NvdSubjectId = 2, NvdSubjectName = "Cơ sở dữ liệu" },
            new NvdSubject { NvdSubjectId = 3, NvdSubjectName = "Lập trình hướng đối tượng" });

        modelBuilder.Entity<NvdMark>().HasData(
            new NvdMark { NvdSubjectId = 1, NvdStudentId = 1, NvdScore = 9.2 },
            new NvdMark { NvdSubjectId = 2, NvdStudentId = 1, NvdScore = 8.8 },
            new NvdMark { NvdSubjectId = 1, NvdStudentId = 2, NvdScore = 8.0 });
    }
}
