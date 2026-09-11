using Microsoft.AspNetCore.Mvc;
using NvdLesson04.Models;

namespace NvdLesson04.Controllers;

public class ProductController : Controller
{
    private static readonly IReadOnlyList<Category> Categories = new List<Category>
    {
        new() { Id = 1, Name = "Điện thoại" },
        new() { Id = 2, Name = "Laptop" },
        new() { Id = 3, Name = "Phụ kiện" },
        new() { Id = 4, Name = "Âm thanh" }
    };

    private static readonly IReadOnlyList<Product> Products = new List<Product>
    {
        new() { Id = 1, Name = "Xiaomi Mi Mix 4", Image = "/images/products/phone.svg", Price = 8_500_000m, SalePrice = 7_900_000m, CategoryId = 1, Description = "Điện thoại màn hình tràn viền, chip Snapdragon 888+ và camera ẩn dưới màn hình.", Status = true, CreatedAt = new DateTime(2026, 8, 20) },
        new() { Id = 2, Name = "Poco X3 GT", Image = "/images/products/phone.svg", Price = 5_200_000m, SalePrice = 4_600_000m, CategoryId = 1, Description = "Hiệu năng Dimensity 1100, màn hình 120 Hz và sạc nhanh 67 W.", Status = true, CreatedAt = new DateTime(2026, 8, 22) },
        new() { Id = 3, Name = "Redmi Note 10 Pro 5G", Image = "/images/products/phone.svg", Price = 4_800_000m, SalePrice = 4_200_000m, CategoryId = 1, Description = "Điện thoại 5G pin lớn, phù hợp học tập, giải trí và chơi game.", Status = true, CreatedAt = new DateTime(2026, 8, 25) },
        new() { Id = 4, Name = "ASUS ROG G531", Image = "/images/products/laptop.svg", Price = 17_900_000m, SalePrice = 15_900_000m, CategoryId = 2, Description = "Laptop gaming hiệu năng cao, bàn phím RGB và hệ thống tản nhiệt kép.", Status = true, CreatedAt = new DateTime(2026, 8, 28) },
        new() { Id = 5, Name = "ThinkPad X1 Carbon", Image = "/images/products/laptop.svg", Price = 28_900_000m, SalePrice = 26_500_000m, CategoryId = 2, Description = "Laptop mỏng nhẹ, bàn phím tốt và thời lượng pin phù hợp công việc.", Status = false, CreatedAt = new DateTime(2026, 9, 1) },
        new() { Id = 6, Name = "Chuột Logitech G304", Image = "/images/products/accessory.svg", Price = 890_000m, SalePrice = 790_000m, CategoryId = 3, Description = "Chuột không dây gọn nhẹ với cảm biến HERO và pin sử dụng lâu dài.", Status = true, CreatedAt = new DateTime(2026, 9, 2) },
        new() { Id = 7, Name = "Bàn phím AKKO 5075B", Image = "/images/products/accessory.svg", Price = 1_650_000m, SalePrice = 1_450_000m, CategoryId = 3, Description = "Bàn phím cơ layout 75%, hỗ trợ nhiều chế độ kết nối.", Status = true, CreatedAt = new DateTime(2026, 9, 3) },
        new() { Id = 8, Name = "SSD Samsung 980 1TB", Image = "/images/products/accessory.svg", Price = 2_390_000m, SalePrice = 2_190_000m, CategoryId = 3, Description = "Ổ cứng NVMe dung lượng 1 TB, tốc độ cao cho hệ điều hành và ứng dụng.", Status = true, CreatedAt = new DateTime(2026, 9, 4) },
        new() { Id = 9, Name = "Loa Xdobo X8 III", Image = "/images/products/audio.svg", Price = 1_450_000m, SalePrice = 1_250_000m, CategoryId = 4, Description = "Loa Bluetooth di động có âm thanh mạnh, pin lớn và thiết kế chắc chắn.", Status = true, CreatedAt = new DateTime(2026, 9, 5) },
        new() { Id = 10, Name = "Tai nghe HyperX Cloud III", Image = "/images/products/audio.svg", Price = 2_190_000m, SalePrice = 1_890_000m, CategoryId = 4, Description = "Tai nghe chụp tai êm, micro rõ và phù hợp chơi game trong thời gian dài.", Status = false, CreatedAt = new DateTime(2026, 9, 6) }
    };

    public IActionResult Index(int? categoryId)
    {
        if (categoryId.HasValue && Categories.All(item => item.Id != categoryId.Value))
        {
            return NotFound($"Không tìm thấy danh mục có id = {categoryId.Value}.");
        }

        IReadOnlyList<Product> result = categoryId.HasValue
            ? Products.Where(item => item.CategoryId == categoryId.Value).ToList()
            : Products;

        ViewBag.Categories = Categories;
        ViewBag.SelectedCategoryId = categoryId;
        ViewBag.SelectedCategoryName = Categories
            .FirstOrDefault(item => item.Id == categoryId)?.Name ?? "Tất cả sản phẩm";

        return View(result);
    }

    public IActionResult Details(int id)
    {
        Product? product = Products.FirstOrDefault(item => item.Id == id);
        if (product is null)
        {
            return NotFound($"Không tìm thấy sản phẩm có id = {id}.");
        }

        ViewBag.CategoryName = Categories
            .First(item => item.Id == product.CategoryId).Name;

        return View(product);
    }
}
