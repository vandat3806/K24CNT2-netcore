# NvdLesson08 - Model trong ASP.NET Core MVC (Part 2)

## Thông tin sinh viên

- Họ và tên: **Nguyễn Văn Đạt**
- Mã sinh viên: **2410900021**
- Lớp: **K24CNT2**

## Nội dung bài thực hành

Dự án minh họa quy trình quản lý thành viên hoàn chỉnh bằng ASP.NET Core MVC (.NET 8):

- Xây dựng Data Model `NvdMember` và enum vai trò.
- Tách ViewModel cho thao tác thêm và cập nhật dữ liệu.
- Kiểm tra dữ liệu bằng Data Annotation và kiểm tra trùng tên đăng nhập/email.
- Thực hiện đầy đủ CRUD: danh sách, chi tiết, thêm, sửa và xóa.
- Tìm kiếm theo họ tên/tài khoản/email; lọc theo vai trò, trạng thái; phân trang.
- Đổi trạng thái thành viên trực tiếp tại danh sách.
- Áp dụng Repository, Dependency Injection, TempData và Post/Redirect/Get.
- Dùng Anti-forgery token cho các thao tác POST.
- Mật khẩu được băm bằng `PasswordHasher<TUser>`, không lưu hoặc hiển thị dạng rõ.
- Giao diện responsive, sử dụng tài nguyên Bootstrap cục bộ.

## Cấu trúc chính

```text
NvdLesson08/
├── Controllers/
│   ├── HomeController.cs
│   └── NvdMemberController.cs
├── Models/
│   ├── DataModels/NvdMember.cs
│   └── ViewModels/
├── Repositories/
│   ├── INvdMemberRepository.cs
│   └── InMemoryNvdMemberRepository.cs
├── Views/
│   ├── Home/
│   ├── NvdMember/
│   └── Shared/
└── wwwroot/
```

## Routes chính

| Chức năng | Method | URL |
|---|---|---|
| Trang tổng quan | GET | `/` |
| Danh sách thành viên | GET | `/thanh-vien` |
| Thêm thành viên | GET/POST | `/thanh-vien/them-moi` |
| Chi tiết thành viên | GET | `/thanh-vien/chi-tiet/{id}` |
| Cập nhật thành viên | GET/POST | `/thanh-vien/cap-nhat/{id}` |
| Xóa thành viên | GET/POST | `/thanh-vien/xoa/{id}` |
| Đổi trạng thái | POST | `/thanh-vien/doi-trang-thai/{id}` |

## Chạy dự án

Yêu cầu .NET SDK 8.0 trở lên.

```bash
cd NvdLesson08
dotnet restore --configfile NuGet.Config
dotnet run --project NvdLesson08/NvdLesson08.csproj
```

Sau đó mở địa chỉ được hiển thị trong terminal. Dữ liệu của bài được lưu trong bộ nhớ và trở về dữ liệu mẫu mỗi khi khởi động lại ứng dụng.
