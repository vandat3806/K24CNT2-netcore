using Microsoft.AspNetCore.Mvc;
using NvdLesson03.Models;

namespace NvdLesson03.Controllers;

/// <summary>
/// Controller minh họa Action, View và bốn cách truyền dữ liệu sang View.
/// Nvd là tiền tố theo tên sinh viên Nguyễn Văn Đạt.
/// </summary>
public class NvdProductController : Controller
{
    private static readonly IReadOnlyList<NvdProduct> NvdProducts = new List<NvdProduct>
    {
        new() { NvdProductId = "NVD-MB-001", NvdProductName = "Xiaomi Mi Mix 4", NvdYearRelease = 2021, NvdPrice = 7_800_000m },
        new() { NvdProductId = "NVD-MB-002", NvdProductName = "Redmi Note 10 Pro 5G", NvdYearRelease = 2021, NvdPrice = 4_200_000m },
        new() { NvdProductId = "NVD-LT-003", NvdProductName = "Laptop ASUS ROG G531", NvdYearRelease = 2019, NvdPrice = 15_900_000m },
        new() { NvdProductId = "NVD-KB-004", NvdProductName = "Bàn phím cơ AKKO", NvdYearRelease = 2024, NvdPrice = 1_450_000m },
        new() { NvdProductId = "NVD-MS-005", NvdProductName = "Chuột Logitech G304", NvdYearRelease = 2023, NvdPrice = 790_000m },
        new() { NvdProductId = "NVD-HP-006", NvdProductName = "Tai nghe HyperX Cloud", NvdYearRelease = 2024, NvdPrice = 1_690_000m },
        new() { NvdProductId = "NVD-MN-007", NvdProductName = "Màn hình LG UltraGear", NvdYearRelease = 2024, NvdPrice = 5_490_000m },
        new() { NvdProductId = "NVD-SS-008", NvdProductName = "SSD Samsung 1TB", NvdYearRelease = 2024, NvdPrice = 2_190_000m },
        new() { NvdProductId = "NVD-SP-009", NvdProductName = "Loa Bluetooth Xdobo X8 III", NvdYearRelease = 2023, NvdPrice = 1_250_000m },
        new() { NvdProductId = "NVD-GP-010", NvdProductName = "Tay cầm Xbox", NvdYearRelease = 2024, NvdPrice = 1_390_000m }
    };

    // Action trả về JSON, truy cập tại /danh-sach-san-pham.
    [HttpGet("danh-sach-san-pham")]
    public JsonResult Index()
    {
        return Json(NvdProducts);
    }

    // Action đưa collection sang View bằng ViewData, tương ứng ví dụ trong source bài học.
    [HttpGet("all")]
    public ViewResult NvdGetAllProduct()
    {
        ViewData["NvdProducts"] = NvdProducts;
        ViewBag.NvdStudentName = "Nguyễn Văn Đạt";
        return View();
    }

    // Truy cập bằng /NvdProduct/NvdDetails/NVD-MB-001 hoặc /san-pham/NVD-MB-001.
    public IActionResult NvdDetails(string id)
    {
        NvdProduct? product = NvdProducts.FirstOrDefault(item =>
            item.NvdProductId.Equals(id, StringComparison.OrdinalIgnoreCase));

        if (product is null)
        {
            return NotFound(new
            {
                status = 404,
                message = $"Không tìm thấy sản phẩm có mã {id}."
            });
        }

        return View(product);
    }

    /// <summary>
    /// Bốn cách đưa dữ liệu từ Controller ra View:
    /// ViewBag, ViewData, TempData và strongly typed Model.
    /// </summary>
    public ViewResult NvdDataTransfer()
    {
        NvdProduct product = NvdProducts[0];

        ViewBag.NvdStudentName = "Nguyễn Văn Đạt";
        ViewData["NvdStudentId"] = "2410900021";
        ViewData["NvdProduct"] = product;

        if (!TempData.ContainsKey("NvdMessage"))
        {
            TempData["NvdMessage"] = "TempData đã truyền thông báo từ Controller sang View.";
        }

        return View(product);
    }

    public RedirectToActionResult NvdSetTempData()
    {
        TempData["NvdMessage"] = "TempData vẫn tồn tại sau RedirectToAction sang request mới.";
        return RedirectToAction(nameof(NvdDataTransfer));
    }
}
