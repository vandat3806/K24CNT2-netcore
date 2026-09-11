# Lesson04 - Controller Lab

## Thông tin sinh viên

- Họ và tên: **Nguyễn Văn Đạt**
- Mã sinh viên: **2410900021**
- Lớp: **K24CNT2**
- Project: **NvdLesson04**
- Nền tảng: **ASP.NET Core MVC - .NET 8.0**

## Nội dung bài làm

### Bài 1 - Account và Profile

- Tạo model `Account` gồm: `Id`, `Name`, `Email`, `Phone`, `Avatar`, `Address`, `Bio`, `Gender`, `Birthday`.
- Tạo `AccountController` và truyền `List<Account>` sang View bằng `ViewBag`.
- Hiển thị danh sách 4 tài khoản có ảnh đại diện.
- Xem hồ sơ tương ứng theo `id`.
- Dùng named route `account` và `profile` trên thẻ `a`.

### Bài tập tự làm - Product và Category

- Tạo model `Category` gồm `Id`, `Name`.
- Tạo model `Product` gồm `Id`, `Name`, `Image`, `Price`, `SalePrice`, `CategoryId`, `Description`, `Status`, `CreatedAt`.
- Hiển thị `List<Product>` và `List<Category>` với 10 sản phẩm mẫu.
- Đổi route từ `/Product` thành `/san-pham`.
- Bấm danh mục bên trái để lọc sản phẩm.
- Bấm nút **Xem chi tiết** để hiển thị đầy đủ thông tin sản phẩm theo `id` trên URL.
- Xử lý HTTP 404 cho danh mục hoặc sản phẩm không tồn tại.

## Các đường dẫn chính

| Nội dung | URL |
|---|---|
| Trang chủ | `/` |
| Danh sách tài khoản | `/tai-khoan` |
| Hồ sơ theo id | `/ho-so-cua-toi/1` |
| Tất cả sản phẩm | `/san-pham` |
| Sản phẩm theo danh mục | `/san-pham/danh-muc/1` |
| Chi tiết sản phẩm | `/san-pham/chi-tiet/1` |

## Cách chạy

### Visual Studio 2022

1. Mở `NvdLesson04.sln`.
2. Chọn project `NvdLesson04` làm Startup Project.
3. Nhấn `Ctrl + F5`.

### Dòng lệnh

```bash
dotnet restore --configfile NuGet.Config
dotnet run --project NvdLesson04/NvdLesson04.csproj
```

## Tài liệu bài lab

- Lab: `Labguide02-AspNetCore-Controller.pdf` - 19 trang.
- Nội dung: tạo controller, truyền object qua View, named route, tham số route và bài tự làm Product/Category.

## Kết quả kiểm tra

- Build Release: **thành công, 0 lỗi, 0 cảnh báo**.
- Trang chủ, tài khoản, hồ sơ, sản phẩm, lọc danh mục và chi tiết: HTTP **200**.
- Hồ sơ và sản phẩm không tồn tại: HTTP **404**.
- Danh sách đầy đủ có **10 sản phẩm**, lọc danh mục điện thoại còn **3 sản phẩm**.
- Danh sách tài khoản có **4 hồ sơ**; CSS và ảnh tĩnh được phục vụ thành công.
- Responsive: bố cục chuyển cột trên máy tính bảng và điện thoại.
