using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NvdLesson12.Migrations;

public partial class NvdInitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Banner",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Image = table.Column<string>(type: "varchar(260)", maxLength: 260, nullable: false),
                Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Banner", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Category",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Category", x => x.Id));

        migrationBuilder.CreateTable(
            name: "StdClass",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ClassName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_StdClass", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Subjects",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                SubjectName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table => table.PrimaryKey("PK_Subjects", x => x.Id));

        migrationBuilder.CreateTable(
            name: "Product",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                Image = table.Column<string>(type: "varchar(260)", maxLength: 260, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                SalePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false),
                Descriptions = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                CategoryId = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Product", x => x.Id);
                table.ForeignKey("FK_Product_Category_CategoryId", x => x.CategoryId, "Category", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Student",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StudentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                StudentEmail = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                StudentPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                StudentAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                StudentAvatar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                StudentBirthday = table.Column<DateTime>(type: "date", nullable: false),
                ClassId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Student", x => x.Id);
                table.ForeignKey("FK_Student_StdClass_ClassId", x => x.ClassId, "StdClass", "Id", onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Marks",
            columns: table => new
            {
                SubjectId = table.Column<int>(type: "int", nullable: false),
                StudentId = table.Column<int>(type: "int", nullable: false),
                Score = table.Column<double>(type: "float", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Marks", x => new { x.SubjectId, x.StudentId });
                table.ForeignKey("FK_Marks_Student_StudentId", x => x.StudentId, "Student", "Id", onDelete: ReferentialAction.Cascade);
                table.ForeignKey("FK_Marks_Subjects_SubjectId", x => x.SubjectId, "Subjects", "Id", onDelete: ReferentialAction.Cascade);
            });

        var created = new DateTime(2026, 9, 30, 0, 0, 0, DateTimeKind.Utc);
        migrationBuilder.InsertData("Banner", new[] { "Id", "CreatedDate", "Description", "Image", "Name", "Status" }, new object[,]
        {
            { 1, created, "Code First, quan hệ dữ liệu, CRUD và upload ảnh an toàn.", "/images/banner-ef.svg", "Lesson12 — Entity Framework Core", true },
            { 2, created, "Bài thực hành ASP.NET Core MVC — lớp K24CNT2.", "/images/banner-student.svg", "Nguyễn Văn Đạt — 2410900021", true }
        });
        migrationBuilder.InsertData("Category", new[] { "Id", "CreatedDate", "Name", "Status" }, new object[,]
        {
            { 1, created, "Máy tính xách tay", true }, { 2, created, "Điện thoại", true }, { 3, created, "Phụ kiện", true }
        });
        migrationBuilder.InsertData("StdClass", new[] { "Id", "ClassName" }, new object[,]
        {
            { 1, "K24CNT2" }, { 2, "K24CNT1" }
        });
        migrationBuilder.InsertData("Subjects", new[] { "Id", "SubjectName" }, new object[,]
        {
            { 1, "Lập trình ASP.NET Core MVC" }, { 2, "Cơ sở dữ liệu" }, { 3, "Lập trình hướng đối tượng" }
        });
        migrationBuilder.InsertData("Product", new[] { "Id", "CategoryId", "CreatedDate", "Descriptions", "Image", "Name", "Price", "SalePrice", "Status" }, new object[,]
        {
            { 1, 1, created, "Cấu hình cân bằng cho học tập và lập trình ASP.NET Core.", "/images/product-laptop.svg", "Laptop học tập NVD", 18990000m, 16990000m, true },
            { 2, 2, created, "Màn hình đẹp, pin lâu và hỗ trợ kết nối 5G.", "/images/product-phone.svg", "Điện thoại NVD 5G", 10990000m, 9990000m, true },
            { 3, 3, created, "Bàn phím gọn nhẹ dành cho góc học tập.", "/images/product-keyboard.svg", "Bàn phím cơ NVD", 1290000m, 990000m, true }
        });
        migrationBuilder.InsertData("Student", new[] { "Id", "ClassId", "StudentAddress", "StudentAvatar", "StudentBirthday", "StudentEmail", "StudentName", "StudentPhone" }, new object[,]
        {
            { 1, 1, "Hà Nội", "/images/avatar-student.svg", new DateTime(2006, 1, 1), "dat.2410900021@example.edu.vn", "Nguyễn Văn Đạt", "0900000021" },
            { 2, 2, "Hà Nội", "/images/avatar-student.svg", new DateTime(2005, 8, 20), "demo.student@example.edu.vn", "Sinh viên minh họa", "0900000099" }
        });
        migrationBuilder.InsertData("Marks", new[] { "SubjectId", "StudentId", "Score" }, new object[,]
        {
            { 1, 1, 9.2 }, { 2, 1, 8.8 }, { 1, 2, 8.0 }
        });

        migrationBuilder.CreateIndex("IX_Category_Name", "Category", "Name", unique: true);
        migrationBuilder.CreateIndex("IX_Product_CategoryId", "Product", "CategoryId");
        migrationBuilder.CreateIndex("IX_StdClass_ClassName", "StdClass", "ClassName", unique: true);
        migrationBuilder.CreateIndex("IX_Student_ClassId", "Student", "ClassId");
        migrationBuilder.CreateIndex("IX_Student_StudentEmail", "Student", "StudentEmail", unique: true);
        migrationBuilder.CreateIndex("IX_Student_StudentPhone", "Student", "StudentPhone", unique: true);
        migrationBuilder.CreateIndex("IX_Subjects_SubjectName", "Subjects", "SubjectName", unique: true);
        migrationBuilder.CreateIndex("IX_Marks_StudentId", "Marks", "StudentId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Banner");
        migrationBuilder.DropTable("Marks");
        migrationBuilder.DropTable("Product");
        migrationBuilder.DropTable("Student");
        migrationBuilder.DropTable("Subjects");
        migrationBuilder.DropTable("Category");
        migrationBuilder.DropTable("StdClass");
    }
}
