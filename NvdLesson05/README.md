# NvdLesson05 - View trong ASP.NET Core MVC

**Sinh viên:** Nguyễn Văn Đạt

**Mã sinh viên:** 2410900021

**Lớp:** K24CNT2

## Nội dung đã thực hiện

- Razor Syntax: biến, biểu thức, `if/else`, `for`, `foreach`.
- Truyền dữ liệu từ Controller sang View bằng strongly typed Model, ViewBag, ViewData và TempData.
- HTML Helpers/Tag Helpers trong liên kết, bộ lọc và biểu mẫu.
- `BookController` với danh sách sách mẫu, xem chi tiết, thêm mới và chỉnh sửa.
- Data Annotation validation và tải ảnh bìa tối đa 2 MB.
- Partial View cho banner, danh mục, thẻ sản phẩm, bảng sách và form sách.
- Tải Partial View bằng jQuery AJAX tại trang quản lý sách.
- `HotProductViewComponent` nhận tham số `count` và hiển thị sản phẩm nổi bật.
- Layout riêng `_NvdLayout.cshtml`, cấu hình mặc định bằng `_ViewStart.cshtml`, sử dụng `RenderBody` và `RenderSection`.
- Trang sản phẩm theo yêu cầu bài tự luyện: danh mục bên trái, sản phẩm mới từ `HomeController.Index`, sản phẩm nổi bật từ View Component.
- Conventional Route và các route thân thiện trong `Program.cs`.
- Giao diện responsive, tài nguyên ảnh SVG chạy hoàn toàn cục bộ.

## Các đường dẫn chính

| Chức năng | URL |
|---|---|
| Trang chủ và sản phẩm | `/` |
| Quản lý sách | `/thu-vien-sach` hoặc `/Book` |
| Thêm sách | `/Book/Create` |
| Sửa sách | `/sach/chinh-sua/1` |
| Chi tiết sách | `/sach/chi-tiet/1` |
| Partial View AJAX | `/Book/Latest` |
| Razor Demo | `/Home/RazorDemo` |
| Chi tiết sản phẩm | `/san-pham/1` |

## Chạy chương trình

```bash
cd NvdLesson05
dotnet restore
dotnet run --project NvdLesson05/NvdLesson05.csproj
```

Sau đó mở địa chỉ HTTPS/HTTP được hiển thị trong terminal.

> Dữ liệu sách được lưu trong bộ nhớ để tập trung minh họa View và Controller. Khi dừng ứng dụng, các thay đổi sẽ trở về dữ liệu mẫu.

## Tài liệu tham khảo

- Slide: `Session 03 Tìm hiểu về Views`
- Lab: `Labguide03-AspNetCore-View`
- Code demo giảng viên: <https://github.com/tvchung/k24cnt2_netcore/tree/main/TvcLesson05Views>
