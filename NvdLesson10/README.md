# NvdLesson10 - Truy cập dữ liệu với Entity Framework Core

**Sinh viên:** Nguyễn Văn Đạt  
**Mã sinh viên:** 2410900021  
**Lớp:** K24CNT2

## Nội dung đã thực hiện

- Kết nối ASP.NET Core MVC với SQL Server bằng Entity Framework Core 8.
- Mô hình Database First gồm `NvdMember` và `NvdLesson10EfDbContext`.
- Đưa chuỗi kết nối vào `appsettings.json`, không ghi cứng trong `DbContext`.
- CRUD bất đồng bộ: danh sách, chi tiết, thêm, sửa, đổi trạng thái và xóa.
- LINQ: tìm kiếm, lọc trạng thái, sắp xếp và phân trang.
- `AsNoTracking()` cho các truy vấn chỉ đọc.
- Kiểm tra username/email duy nhất và băm mật khẩu trước khi lưu.
- Có script SQL tạo database, bảng và dữ liệu mẫu.
- Có chế độ SQLite riêng để chạy thử khi máy chưa cài SQL Server.

## 1. Chạy với SQL Server 2019

1. Mở SQL Server Management Studio và chạy file `Sql/NvdLesson10EFDb.sql`.
2. Mặc định project dùng Windows Authentication:

```text
Server=.\SQL2019;Database=TvcLesson10EFDb;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True
```

3. Nếu dùng SQL Server Authentication, lưu mật khẩu bằng User Secrets trong thư mục chứa file `.csproj`:

```powershell
dotnet user-secrets set "ConnectionStrings:NvdSqlServerConnection" "Server=.\SQL2019;Database=TvcLesson10EFDb;User Id=sa;Password=YOUR_PASSWORD;MultipleActiveResultSets=True;TrustServerCertificate=True"
```

4. Restore và chạy:

```powershell
dotnet restore --configfile ..\NuGet.Config
dotnet run
```

## 2. Lệnh Scaffold-DbContext

### Package Manager Console - Windows Authentication

```powershell
Scaffold-DbContext "Server=.\SQL2019;Database=TvcLesson10EFDb;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context NvdLesson10EfDbContext -Force
```

### Package Manager Console - SQL Server Authentication

```powershell
Scaffold-DbContext "Server=.\SQL2019;Database=TvcLesson10EFDb;User Id=sa;Password=YOUR_PASSWORD;MultipleActiveResultSets=True;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context NvdLesson10EfDbContext -Force
```

### Cách khuyến nghị, không ghi mật khẩu trong lệnh

```powershell
Scaffold-DbContext "Name=ConnectionStrings:NvdSqlServerConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context NvdLesson10EfDbContext -Force
```

> Nếu scaffold lại với `-Force`, nên sao lưu các thay đổi trong file model. Phần giao diện và validation của bài nằm trong ViewModel nên không bị ghi đè.

## 3. Chạy thử nhanh bằng SQLite

SQLite chỉ dùng để chạy thử trên máy không có SQL Server; cấu hình nộp bài vẫn là SQL Server.

```powershell
$env:DatabaseProvider="Sqlite"
dotnet run
```

Database SQLite và dữ liệu mẫu sẽ được tạo tự động khi ứng dụng khởi động.

## Cấu trúc chính

```text
NvdLesson10/
├── Controllers/
│   └── NvdMembersController.cs
├── Data/
│   └── NvdDbInitializer.cs
├── Models/
│   ├── NvdMember.cs
│   ├── NvdLesson10EfDbContext.cs
│   └── ViewModels/
├── Views/NvdMembers/
├── Program.cs
└── appsettings.json
```
