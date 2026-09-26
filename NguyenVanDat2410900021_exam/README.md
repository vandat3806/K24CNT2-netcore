# NguyenVanDat2410900021_exam

Bài thi ASP.NET Core MVC - Nguyễn Văn Đạt - MSV 2410900021 - K24CNT2.

## Chức năng đã làm

- ASP.NET Core MVC Template, target .NET 8.
- Entity Framework Core kết nối SQL Server.
- Menu Layout: Trang chủ / Thông tin sinh viên / Danh sách sinh viên.
- Controller `Home`, action `HvtAbout` hiển thị thông tin sinh viên.
- CRUD bảng `NvdStudent`: danh sách, thêm, chi tiết, sửa, xóa.
- Tìm kiếm sinh viên theo mã, họ tên hoặc lớp.
- Bootstrap + CSS tùy chỉnh, giao diện responsive.
- Kiểm tra dữ liệu bằng DataAnnotations và kiểm tra trùng mã sinh viên.

## Cơ sở dữ liệu

Database: `NvdEmployee_2410900021_Db`

Bảng CRUD: `dbo.NvdStudent`

Kết nối mặc định trong `appsettings.json`:
`Server=.\SQL2019;Database=NvdEmployee_2410900021_Db;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True`

Nếu SQL Server của máy có tên instance khác, chỉ sửa phần `Server=...`.

## Chạy bằng VS Code

```bash
dotnet restore
dotnet run
```

## Sinh viên

- Nguyễn Văn Đạt
- MSV: 2410900021
- Lớp: K24CNT2
