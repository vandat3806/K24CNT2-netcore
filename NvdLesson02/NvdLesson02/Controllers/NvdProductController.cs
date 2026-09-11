using Microsoft.AspNetCore.Mvc;
using NvdLesson02.Models;

namespace NvdLesson02.Controllers;

/// <summary>
/// Controller minh họa Actions, Views và 4 cách truyền dữ liệu sang View.
/// Tiền tố Nvd là viết tắt tên sinh viên Nguyen van dat.
/// </summary>
public class NvdProductController : Controller
{
    private static readonly IReadOnlyList<NvdProduct> Products = new List<NvdProduct>
    {
        new() { Id = 1, ProductCode = "NVD001", ProductName = "Laptop Dell Vostro", YearRelease = 2024, Price = 18_900_000 },
        new() { Id = 2, ProductCode = "NVD002", ProductName = "Bàn phím cơ", YearRelease = 2025, Price = 1_250_000 },
        new() { Id = 3, ProductCode = "NVD003", ProductName = "Chuột không dây", YearRelease = 2025, Price = 590_000 }
    };

    // Action trả về View với danh sách model strongly typed.
    public ViewResult NvdIndex()
    {
        return View(Products);
    }

    // Có thể truy cập bằng /NvdProduct/NvdDetails/2 hoặc route /san-pham/2.
    public IActionResult NvdDetails(int id)
    {
        NvdProduct? product = Products.FirstOrDefault(item => item.Id == id);

        if (product is null)
        {
            return NotFound($"Không tìm thấy sản phẩm có id = {id}.");
        }

        return View(product);
    }

    /// <summary>
    /// Minh họa đủ 4 cách đưa dữ liệu từ Controller ra View:
    /// ViewBag, ViewData, TempData và Model strongly typed.
    /// </summary>
    public ViewResult NvdDataTransfer()
    {
        NvdProduct product = Products[0];

        // Cách 1: ViewBag - thuộc tính động.
        ViewBag.NvdStudentName = "Nguyen van dat";

        // Cách 2: ViewData - từ điển key/value.
        ViewData["NvdStudentId"] = "2410900021";
        ViewData["NvdProduct"] = product;

        // Cách 3: TempData - dữ liệu sống qua request kế tiếp.
        if (!TempData.ContainsKey("NvdMessage"))
        {
            TempData["NvdMessage"] = "TempData đã truyền thông báo từ Controller sang View.";
        }

        // Cách 4: truyền model strongly typed trực tiếp vào View.
        return View(product);
    }

    // Đặt TempData rồi chuyển hướng sang một Action khác để chứng minh dữ liệu còn tồn tại.
    public RedirectToActionResult NvdSetTempData()
    {
        TempData["NvdMessage"] = "TempData vẫn còn sau khi RedirectToAction (request mới).";
        return RedirectToAction(nameof(NvdDataTransfer));
    }
}
