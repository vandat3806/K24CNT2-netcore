# Lesson03 - Controller in ASP.NET Core MVC

## Thông tin sinh viên

- Họ và tên: **Nguyễn Văn Đạt**
- Mã sinh viên: **2410900021**
- Lớp: **K24CNT2**
- Project: **NvdLesson03**
- Nền tảng: **ASP.NET Core MVC - .NET 8.0**

## Nội dung bài làm

- Tạo Controller và các Action.
- Tạo Razor View theo quy ước thư mục của MVC.
- Hiển thị danh sách 10 sản phẩm từ dữ liệu mẫu.
- Truyền dữ liệu từ Controller sang View bằng `ViewBag`, `ViewData`, `TempData` và strongly typed `Model`.
- Minh họa các loại `ActionResult`: View, Content, JSON, Redirect, File, StatusCode, NotFound, PartialView và Empty.
- Minh họa conventional route, custom route, attribute route, route parameter, constraint, named route và query string.

## Các đường dẫn chính

| Nội dung | URL |
|---|---|
| Trang chủ | `/` |
| JSON danh sách sản phẩm | `/danh-sach-san-pham` |
| View danh sách sản phẩm | `/all` |
| Bốn cách truyền dữ liệu | `/NvdProduct/NvdDataTransfer` |
| Các loại ActionResult | `/ket-qua` |
| Tìm hiểu Routes | `/dinh-tuyen` |
| Thông tin sinh viên | `/sinh-vien` |

## Cách chạy

### Visual Studio 2022

1. Mở `NvdLesson03.sln`.
2. Chọn `NvdLesson03` làm Startup Project.
3. Nhấn `Ctrl + F5`.

### Dòng lệnh

```bash
dotnet restore --configfile NuGet.Config
dotnet run --project NvdLesson03/NvdLesson03.csproj
```

Mã nguồn tham khảo của môn học: <https://github.com/tvchung/k24cnt2_netcore>

## Kết quả kiểm tra

- Build Release: **thành công, 0 lỗi, 0 cảnh báo**.
- Các trang View chính và route hợp lệ: HTTP **200**.
- JSON `/danh-sach-san-pham`: đúng **10 sản phẩm**.
- TempData: còn dữ liệu sau `RedirectToAction`.
- Action trạng thái: HTTP **204**; dữ liệu không tồn tại và route sai constraint: HTTP **404**.
- Action tải tệp trả đúng tên `NguyenVanDat_2410900021_Lesson03.txt`.
