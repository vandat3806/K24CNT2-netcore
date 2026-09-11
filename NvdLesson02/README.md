# Lesson02 - Controller in ASP.NET Core MVC

## Thông tin sinh viên

- Họ và tên: **Nguyen van dat**
- Mã sinh viên: **2410900021**
- Lớp: **K24CNT2**
- Project: **NvdLesson02**
- Nền tảng: **ASP.NET Core MVC - .NET 8.0**

## Nội dung đã hoàn thành

### 1. Controller và Actions

- `HomeController`: trang chủ, thông tin sinh viên và xử lý lỗi.
- `NvdProductController`: danh sách sản phẩm, chi tiết sản phẩm và truyền dữ liệu sang View.
- `NvdActionResultController`: minh họa các loại kết quả trả về từ Action.
- `NvdRouteController`: minh họa các kiểu định tuyến.

Controller kế thừa lớp `Controller`. Mỗi public method trong Controller có thể là một Action, nhận request và trả về một kết quả thuộc hệ thống `ActionResult`.

### 2. Views cho Actions

Các Razor View được đặt theo đúng quy ước:

```text
Views/{Tên Controller}/{Tên Action}.cshtml
```

Ví dụ, Action `NvdProductController.NvdIndex()` dùng View `Views/NvdProduct/NvdIndex.cshtml`.

### 3. Bốn cách đưa dữ liệu từ Controller ra View

| Cách | Ví dụ trong bài | Công dụng |
|---|---|---|
| ViewBag | `ViewBag.NvdStudentName` | Truyền dữ liệu phụ bằng thuộc tính động |
| ViewData | `ViewData["NvdStudentId"]` | Truyền dữ liệu phụ theo key/value |
| TempData | `TempData["NvdMessage"]` | Giữ dữ liệu qua request kế tiếp, phù hợp sau Redirect |
| Strongly typed Model | `return View(product)` | Truyền model chính, an toàn kiểu và dễ bảo trì |

Trang chạy minh họa: `/NvdProduct/NvdDataTransfer`.

### 4. Các loại ActionResult

Project có Action chạy thật cho các loại:

- `ViewResult`
- `ContentResult`
- `JsonResult`
- `RedirectToActionResult`
- `RedirectResult`
- `FileContentResult`
- `StatusCodeResult` (HTTP 204)
- `NotFoundObjectResult` (HTTP 404)
- `PartialViewResult`
- `EmptyResult`

Trang tổng hợp: `/ket-qua`.

### 5. Routes

| Kiểu route | URL mẫu |
|---|---|
| Route mặc định | `/NvdProduct/NvdDetails/2` |
| Custom conventional route | `/san-pham/2` |
| Route riêng trang sinh viên | `/sinh-vien` |
| Attribute route | `/dinh-tuyen/xin-chao/Nguyen-van-dat` |
| Route constraint | `/dinh-tuyen/bai-hoc/2` |
| Named route | `/dinh-tuyen/ho-so/2410900021` |
| Query string | `/dinh-tuyen/tim-kiem?keyword=MVC&page=1` |

Trang tổng hợp: `/dinh-tuyen`.

## Cách chạy bài

### Visual Studio 2022

1. Mở file `NguyenVanDat_2410900021_Lesson02.sln`.
2. Chọn project `NvdLesson02` làm Startup Project nếu Visual Studio chưa tự chọn.
3. Nhấn `Ctrl + F5` hoặc nút Run.

### Dòng lệnh

```bash
dotnet restore --configfile NuGet.Config
dotnet run --project NvdLesson02/NvdLesson02.csproj
```

Sau đó mở URL được hiển thị trong Terminal.

## Đối chiếu kết quả kiểm tra

- Build Release: **thành công, 0 lỗi, 0 cảnh báo**.
- Các trang View chính: HTTP **200**.
- Action `StatusCodeDemo`: HTTP **204**.
- Action `NotFoundDemo` và route constraint sai: HTTP **404** đúng thiết kế.
- `TempData` được kiểm tra tồn tại sau `RedirectToAction`.
- Action tải tệp trả đúng tên `NguyenVanDat_2410900021.txt`.

Mã nguồn tham khảo của môn học: <https://github.com/tvchung/k24cnt2_netcore>
