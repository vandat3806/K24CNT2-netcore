# NvdLesson06 - View trong ASP.NET Core MVC (Part 2)

**Sinh viên:** Nguyễn Văn Đạt

**Mã sinh viên:** 2410900021

**Lớp:** K24CNT2

## Nội dung đã thực hiện

- Layout riêng `_StoreLayout.cshtml` dùng `RenderBody` và ba vùng `RenderSection`: `Head`, `Announcement`, `Scripts`.
- `_ViewStart.cshtml` thiết lập layout mặc định; `_ViewImports.cshtml` khai báo namespace và Tag Helpers.
- Sáu Partial View: thông báo, hero banner, menu danh mục, product card, product grid và service strip.
- Strongly typed `StorefrontViewModel` truyền danh mục, sản phẩm mới và thông tin sinh viên sang View.
- `HomeController.ProductGrid` trả Partial View sau khi lọc theo danh mục/từ khóa.
- jQuery AJAX cập nhật danh sách và số kết quả mà không tải lại toàn trang; ô tìm kiếm có debounce 300 ms.
- `HotProductViewComponent` nhận tham số `count`, `minRating`, tự lọc và sắp xếp sản phẩm bán chạy.
- Trang chi tiết sản phẩm dùng named route và Partial View cho sản phẩm liên quan.
- Xử lý HTTP 404 khi sản phẩm không tồn tại.
- Giao diện NovaStore responsive, gồm 12 sản phẩm và tài nguyên SVG chạy cục bộ.

## Đường dẫn kiểm tra

| Chức năng | URL |
|---|---|
| Trang chủ | `/` |
| Partial sản phẩm AJAX | `/Home/ProductGrid` |
| Lọc danh mục Laptop | `/Home/ProductGrid?categoryId=1` |
| Tìm kiếm sản phẩm | `/Home/ProductGrid?keyword=Orbit` |
| Hướng dẫn cấu trúc View | `/huong-dan-view-part-2` |
| Chi tiết sản phẩm | `/san-pham/chi-tiet/1` |

## Chạy chương trình

```bash
cd NvdLesson06
dotnet restore --configfile NuGet.Config
dotnet run --project NvdLesson06/NvdLesson06.csproj
```

## Cấu trúc chính

```text
NvdLesson06/
├── Controllers/
├── Models/
├── ViewComponents/
├── Views/
│   ├── Home/
│   ├── Product/
│   └── Shared/
│       └── Components/HotProduct/Default.cshtml
└── wwwroot/
```

## Tài liệu tham khảo

- Slide: `Session 03 Tìm hiểu về Views`
- Lab: `Labguide03-AspNetCore-View`
- Code demo: <https://github.com/tvchung/k24cnt2_netcore/tree/main/TvcLesson05Views>
