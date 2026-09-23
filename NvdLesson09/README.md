# NvdLesson09 - Data Annotation trong ASP.NET Core MVC

**Sinh viên:** Nguyễn Văn Đạt  
**Mã sinh viên:** 2410900021  
**Lớp:** K24CNT2

## Nội dung thực hiện

- Xây dựng model `NvdCategory` và `NvdProduct`.
- Dùng các Data Annotation có sẵn: `Required`, `StringLength`, `Range`, `DataType`, `Display`, `Key`.
- Tạo custom validation:
  - `DiscountPriceAttribute`: giá khuyến mãi không vượt quá 90% giá gốc.
  - `NoForbiddenWordsAttribute`: mô tả không chứa các từ bị cấm.
  - `AllowedExtensionsAttribute`: chỉ nhận ảnh JPG, JPEG, PNG hoặc WEBP.
  - `MaxFileSizeAttribute`: ảnh tối đa 2 MB.
- Kiểm tra danh mục được chọn có tồn tại.
- Upload ảnh vào `wwwroot/products` bằng tên tệp ngẫu nhiên.
- CRUD sản phẩm: danh sách, thêm, chi tiết, sửa và xóa.
- Tìm kiếm, lọc theo danh mục và phân trang.
- Dùng repository lưu dữ liệu trong bộ nhớ để tập trung vào nội dung Data Annotation.

## Chạy project

```bash
dotnet restore
dotnet run --project NvdLesson09/NvdLesson09.csproj
```

Sau đó mở địa chỉ được hiển thị trong terminal hoặc truy cập `/san-pham` để xem danh sách sản phẩm.

> Dữ liệu được lưu trong bộ nhớ nên sẽ trở về dữ liệu mẫu khi khởi động lại ứng dụng.

## Tài liệu tham khảo

- [Model validation trong ASP.NET Core MVC](https://learn.microsoft.com/aspnet/core/mvc/models/validation)
- [Upload tệp trong ASP.NET Core](https://learn.microsoft.com/aspnet/core/mvc/models/file-uploads)
