# NvdLesson07 - Model trong ASP.NET Core MVC

**Sinh viên:** Nguyễn Văn Đạt

**Mã sinh viên:** 2410900021

**Lớp:** K24CNT2

## Nội dung đã thực hiện

- Data Model `NvdMember` gồm 8 thuộc tính, có giá trị mặc định và metadata hiển thị.
- Data Annotation: `Required`, `StringLength`, `RegularExpression`, `EmailAddress`, `Display`, `DataType` và `DisplayFormat`.
- Strongly typed View cho một Model, danh sách Model và ViewModel tổng hợp.
- Trang riêng minh họa `ViewBag` để so sánh với strongly typed Model.
- Model binding từ form vào action POST; giới hạn trường được bind để tránh over-posting.
- Kiểm tra `ModelState.IsValid`, thông báo lỗi tiếng Việt và kiểm tra trùng tên đăng nhập/email.
- Repository in-memory được cấp qua Dependency Injection, có đầy đủ Create, Read, Update, Delete.
- Áp dụng Post/Redirect/Get, Anti-forgery Token, `TempData` và xử lý HTTP 404.
- Tìm kiếm theo tên/tài khoản/email/lớp và lọc theo trạng thái.
- Giao diện ModelLab responsive, sử dụng Bootstrap và tài nguyên chạy hoàn toàn cục bộ.

## Đường dẫn kiểm tra

| Chức năng | URL |
|---|---|
| Tổng quan Lesson07 | `/` |
| Danh sách + ViewModel | `/thanh-vien` |
| Lọc thành viên hoạt động | `/thanh-vien?status=active` |
| Tìm kiếm lớp K24CNT2 | `/thanh-vien?keyword=K24CNT2` |
| Minh họa một Model | `/thanh-vien/model-don` |
| Minh họa danh sách qua ViewBag | `/thanh-vien/viewbag` |
| Thêm thành viên | `/thanh-vien/them-moi` |
| Chi tiết thành viên | `/thanh-vien/chi-tiet/1` |
| Cập nhật thành viên | `/thanh-vien/cap-nhat/1` |
| Xác nhận xóa | `/thanh-vien/xoa/1` |

## Chạy chương trình

```bash
cd NvdLesson07
dotnet restore --configfile NuGet.Config
dotnet run --project NvdLesson07/NvdLesson07.csproj
```

> Dữ liệu được lưu trong bộ nhớ để tập trung vào nội dung Model. Các thay đổi sẽ được đặt lại khi dừng chương trình.

## Cấu trúc chính

```text
NvdLesson07/
├── Controllers/
│   ├── HomeController.cs
│   └── NvdMemberController.cs
├── Models/
│   ├── DataModels/NvdMember.cs
│   └── ViewModels/
├── Repositories/
├── Views/
│   ├── Home/
│   ├── NvdMember/
│   └── Shared/
└── wwwroot/
```

## Tài liệu tham khảo

- Slide Lesson07: <https://docs.google.com/presentation/d/1IQxHu59vPSYhOmj81ahVXzk44oGQJGIS/edit?usp=sharing>
- Video: <https://youtube.com/playlist?list=PLNaPWM1HcIRI>
- Code demo: <https://github.com/tvchung/k24cnt2_netcore/tree/main/TvcLesson07Models>
