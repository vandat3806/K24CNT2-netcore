using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NvdLesson12.Data;

#nullable disable

namespace NvdLesson12.Migrations;

[DbContext(typeof(NvdLesson12DbContext))]
[Migration("20260930000000_NvdInitialCreate")]
partial class NvdInitialCreate
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder) =>
        NvdLesson12ModelSnapshotBuilder.Build(modelBuilder);
}
internal static class NvdLesson12ModelSnapshotBuilder
{
    internal static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.31")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("NvdLesson12.Models.NvdBanner", b =>
        {
            b.Property<int>("NvdBannerId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("Id");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NvdBannerId"));

            b.Property<DateTime>("NvdCreatedDate")
                .HasColumnType("datetime2")
                .HasColumnName("CreatedDate");
            b.Property<string>("NvdDescription")
                .HasMaxLength(500)
                .HasColumnType("nvarchar(500)")
                .HasColumnName("Description");
            b.Property<string>("NvdImage")
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnType("varchar(260)")
                .HasColumnName("Image");
            b.Property<string>("NvdName")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)")
                .HasColumnName("Name");
            b.Property<bool>("NvdStatus")
                .HasColumnType("bit")
                .HasColumnName("Status");

            b.HasKey("NvdBannerId");
            b.ToTable("Banner");

            b.HasData(
                new
                {
                    NvdBannerId = 1,
                    NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                    NvdDescription = "Code First, quan hệ dữ liệu, CRUD và upload ảnh an toàn.",
                    NvdImage = "/images/banner-ef.svg",
                    NvdName = "Lesson12 — Entity Framework Core",
                    NvdStatus = true
                },
                new
                {
                    NvdBannerId = 2,
                    NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc),
                    NvdDescription = "Bài thực hành ASP.NET Core MVC — lớp K24CNT2.",
                    NvdImage = "/images/banner-student.svg",
                    NvdName = "Nguyễn Văn Đạt — 2410900021",
                    NvdStatus = true
                });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdCategory", b =>
        {
            b.Property<int>("NvdCategoryId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("Id");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NvdCategoryId"));

            b.Property<DateTime>("NvdCreatedDate")
                .HasColumnType("datetime2")
                .HasColumnName("CreatedDate");
            b.Property<string>("NvdName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("Name");
            b.Property<bool>("NvdStatus")
                .HasColumnType("bit")
                .HasColumnName("Status");

            b.HasKey("NvdCategoryId");
            b.HasIndex("NvdName").IsUnique();
            b.ToTable("Category");

            b.HasData(
                new { NvdCategoryId = 1, NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc), NvdName = "Máy tính xách tay", NvdStatus = true },
                new { NvdCategoryId = 2, NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc), NvdName = "Điện thoại", NvdStatus = true },
                new { NvdCategoryId = 3, NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc), NvdName = "Phụ kiện", NvdStatus = true });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdMark", b =>
        {
            b.Property<int>("NvdSubjectId")
                .HasColumnType("int")
                .HasColumnName("SubjectId");
            b.Property<int>("NvdStudentId")
                .HasColumnType("int")
                .HasColumnName("StudentId");
            b.Property<double>("NvdScore")
                .HasColumnType("float")
                .HasColumnName("Score");

            b.HasKey("NvdSubjectId", "NvdStudentId");
            b.HasIndex("NvdStudentId");
            b.ToTable("Marks");

            b.HasData(
                new { NvdSubjectId = 1, NvdStudentId = 1, NvdScore = 9.2 },
                new { NvdSubjectId = 2, NvdStudentId = 1, NvdScore = 8.8 },
                new { NvdSubjectId = 1, NvdStudentId = 2, NvdScore = 8.0 });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdProduct", b =>
        {
            b.Property<int>("NvdProductId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("Id");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NvdProductId"));

            b.Property<int>("NvdCategoryId")
                .HasColumnType("int")
                .HasColumnName("CategoryId");
            b.Property<DateTime>("NvdCreatedDate")
                .HasColumnType("datetime2")
                .HasColumnName("CreatedDate");
            b.Property<string>("NvdDescription")
                .HasMaxLength(1000)
                .HasColumnType("nvarchar(1000)")
                .HasColumnName("Descriptions");
            b.Property<string>("NvdImage")
                .IsRequired()
                .HasMaxLength(260)
                .HasColumnType("varchar(260)")
                .HasColumnName("Image");
            b.Property<string>("NvdName")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)")
                .HasColumnName("Name");
            b.Property<decimal>("NvdPrice")
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Price");
            b.Property<decimal>("NvdSalePrice")
                .HasColumnType("decimal(18,2)")
                .HasColumnName("SalePrice");
            b.Property<bool>("NvdStatus")
                .HasColumnType("bit")
                .HasColumnName("Status");

            b.HasKey("NvdProductId");
            b.HasIndex("NvdCategoryId");
            b.ToTable("Product");

            b.HasData(
                new { NvdProductId = 1, NvdCategoryId = 1, NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc), NvdDescription = "Cấu hình cân bằng cho học tập và lập trình ASP.NET Core.", NvdImage = "/images/product-laptop.svg", NvdName = "Laptop học tập NVD", NvdPrice = 18990000m, NvdSalePrice = 16990000m, NvdStatus = true },
                new { NvdProductId = 2, NvdCategoryId = 2, NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc), NvdDescription = "Màn hình đẹp, pin lâu và hỗ trợ kết nối 5G.", NvdImage = "/images/product-phone.svg", NvdName = "Điện thoại NVD 5G", NvdPrice = 10990000m, NvdSalePrice = 9990000m, NvdStatus = true },
                new { NvdProductId = 3, NvdCategoryId = 3, NvdCreatedDate = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc), NvdDescription = "Bàn phím gọn nhẹ dành cho góc học tập.", NvdImage = "/images/product-keyboard.svg", NvdName = "Bàn phím cơ NVD", NvdPrice = 1290000m, NvdSalePrice = 990000m, NvdStatus = true });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdStdClass", b =>
        {
            b.Property<int>("NvdStdClassId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("Id");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NvdStdClassId"));

            b.Property<string>("NvdClassName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("ClassName");

            b.HasKey("NvdStdClassId");
            b.HasIndex("NvdClassName").IsUnique();
            b.ToTable("StdClass");

            b.HasData(
                new { NvdStdClassId = 1, NvdClassName = "K24CNT2" },
                new { NvdStdClassId = 2, NvdClassName = "K24CNT1" });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdStudent", b =>
        {
            b.Property<int>("NvdStudentId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("Id");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NvdStudentId"));

            b.Property<int>("NvdStdClassId")
                .HasColumnType("int")
                .HasColumnName("ClassId");
            b.Property<string>("NvdStudentAddress")
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("nvarchar(150)")
                .HasColumnName("StudentAddress");
            b.Property<string>("NvdStudentAvatar")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("StudentAvatar");
            b.Property<DateTime>("NvdStudentBirthday")
                .HasColumnType("date")
                .HasColumnName("StudentBirthday");
            b.Property<string>("NvdStudentEmail")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("StudentEmail");
            b.Property<string>("NvdStudentName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("StudentName");
            b.Property<string>("NvdStudentPhone")
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)")
                .HasColumnName("StudentPhone");

            b.HasKey("NvdStudentId");
            b.HasIndex("NvdStdClassId");
            b.HasIndex("NvdStudentEmail").IsUnique();
            b.HasIndex("NvdStudentPhone").IsUnique();
            b.ToTable("Student");

            b.HasData(
                new { NvdStudentId = 1, NvdStdClassId = 1, NvdStudentAddress = "Hà Nội", NvdStudentAvatar = "/images/avatar-student.svg", NvdStudentBirthday = new DateTime(2006, 1, 1), NvdStudentEmail = "dat.2410900021@example.edu.vn", NvdStudentName = "Nguyễn Văn Đạt", NvdStudentPhone = "0900000021" },
                new { NvdStudentId = 2, NvdStdClassId = 2, NvdStudentAddress = "Hà Nội", NvdStudentAvatar = "/images/avatar-student.svg", NvdStudentBirthday = new DateTime(2005, 8, 20), NvdStudentEmail = "demo.student@example.edu.vn", NvdStudentName = "Sinh viên minh họa", NvdStudentPhone = "0900000099" });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdSubject", b =>
        {
            b.Property<int>("NvdSubjectId")
                .ValueGeneratedOnAdd()
                .HasColumnType("int")
                .HasColumnName("Id");
            SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("NvdSubjectId"));

            b.Property<string>("NvdSubjectName")
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .HasColumnName("SubjectName");

            b.HasKey("NvdSubjectId");
            b.HasIndex("NvdSubjectName").IsUnique();
            b.ToTable("Subjects");

            b.HasData(
                new { NvdSubjectId = 1, NvdSubjectName = "Lập trình ASP.NET Core MVC" },
                new { NvdSubjectId = 2, NvdSubjectName = "Cơ sở dữ liệu" },
                new { NvdSubjectId = 3, NvdSubjectName = "Lập trình hướng đối tượng" });
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdMark", b =>
        {
            b.HasOne("NvdLesson12.Models.NvdStudent", "NvdStudent")
                .WithMany("NvdMarks")
                .HasForeignKey("NvdStudentId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.HasOne("NvdLesson12.Models.NvdSubject", "NvdSubject")
                .WithMany("NvdMarks")
                .HasForeignKey("NvdSubjectId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.Navigation("NvdStudent");
            b.Navigation("NvdSubject");
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdProduct", b =>
        {
            b.HasOne("NvdLesson12.Models.NvdCategory", "NvdCategory")
                .WithMany("NvdProducts")
                .HasForeignKey("NvdCategoryId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            b.Navigation("NvdCategory");
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdStudent", b =>
        {
            b.HasOne("NvdLesson12.Models.NvdStdClass", "NvdStdClass")
                .WithMany("NvdStudents")
                .HasForeignKey("NvdStdClassId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();
            b.Navigation("NvdStdClass");
        });

        modelBuilder.Entity("NvdLesson12.Models.NvdCategory", b => b.Navigation("NvdProducts"));
        modelBuilder.Entity("NvdLesson12.Models.NvdStdClass", b => b.Navigation("NvdStudents"));
        modelBuilder.Entity("NvdLesson12.Models.NvdStudent", b => b.Navigation("NvdMarks"));
        modelBuilder.Entity("NvdLesson12.Models.NvdSubject", b => b.Navigation("NvdMarks"));
    }
}
