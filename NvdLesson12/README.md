# NvdLesson12 — Entity Framework Core

- **Sinh viên:** Nguyễn Văn Đạt
- **Mã sinh viên:** 2410900021
- **Lớp:** K24CNT2

Bài thực hành Lesson12 được xây dựng bằng ASP.NET Core MVC, .NET 8 và Entity Framework Core theo hướng Code First.

## Nội dung đã thực hiện

- CRUD danh mục và sản phẩm; sản phẩm hỗ trợ upload ảnh.
- Action `Product` trong `HomeController`, hiển thị sản phẩm dạng thẻ/cột.
- CRUD banner có các trường `Id`, `Name`, `Image`, `Description`, `CreatedDate`, `Status`.
- Chỉ hiển thị banner đang hoạt động tại trang chủ.
- Mô hình quản lý sinh viên gồm `StdClass`, `Student`, `Subjects`, `Marks`.
- Khóa chính kép `(SubjectId, StudentId)` cho bảng `Marks`.
- Khóa ngoại, chỉ mục duy nhất cho email, số điện thoại, tên lớp, tên môn học và tên danh mục.
- CRUD đầy đủ và validation phía máy chủ cho tất cả các bảng.
- Tìm kiếm/lọc dữ liệu, dữ liệu mẫu mang thông tin sinh viên.
- Upload ảnh kiểm tra dung lượng, phần mở rộng, MIME type và chữ ký tệp; tên tệp được sinh ngẫu nhiên.
- Migration khởi tạo: `NvdInitialCreate`.

## Cấu trúc

```text
NvdLesson12/
├── NvdLesson12.sln
└── NvdLesson12/
    ├── Controllers/
    ├── Data/
    ├── Migrations/
    ├── Models/
    │   └── ViewModels/
    ├── Services/
    ├── Views/
    └── wwwroot/
```

## Chạy với SQL Server

Yêu cầu: .NET 8 SDK và SQL Server. Chuỗi kết nối Windows Authentication mặc định trỏ tới instance `.\\SQL2019`, database `NvdLesson12Db`.

```powershell
cd NvdLesson12/NvdLesson12
dotnet restore
dotnet ef database update
dotnet run
```

Nếu dùng Package Manager Console trong Visual Studio:

```powershell
Update-Database
```

Migration `NvdInitialCreate` đã được tạo sẵn bằng Code First. Khi thay đổi model, tạo migration kế tiếp bằng:

```powershell
Add-Migration NvdNextMigration
Update-Database
```

### SQL Server Authentication

Không lưu mật khẩu vào Git. Cấu hình bằng User Secrets:

```powershell
dotnet user-secrets set "ConnectionStrings:NvdSqlServerConnection" "Server=.\\SQL2019;Database=NvdLesson12Db;User Id=sa;Password=YOUR_PASSWORD;MultipleActiveResultSets=True;TrustServerCertificate=True"
dotnet ef database update
```

## Chạy nhanh với SQLite

SQLite phù hợp để xem và kiểm thử bài mà không cần cài SQL Server. Ứng dụng tự tạo database và dữ liệu mẫu khi chọn provider này.

PowerShell:

```powershell
$env:DatabaseProvider="Sqlite"
dotnet run
```

Bash:

```bash
DatabaseProvider=Sqlite dotnet run
```

Mở địa chỉ được in trên terminal, mặc định là `http://localhost:5212`.

## Kiểm thử đã thực hiện

- Build Release: không có warning và error.
- Kiểm tra 10 endpoint chính trả về HTTP 200.
- Kiểm tra CRUD, validation, ràng buộc duy nhất và khóa chính kép.
- Kiểm tra upload/xóa ảnh, HTTP 404 và chống giả mạo request (anti-forgery token).
