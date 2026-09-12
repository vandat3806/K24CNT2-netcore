using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson06.Models;

namespace NvdLesson06.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        var model = new StorefrontViewModel
        {
            Categories = CatalogData.Categories,
            NewestProducts = CatalogData.Products
                .OrderByDescending(product => product.CreatedAt)
                .Take(8)
                .ToList(),
            TotalProducts = CatalogData.Products.Count,
            StudentName = "Nguyễn Văn Đạt",
            StudentId = "2410900021"
        };

        ViewBag.Lesson = "Lesson06 - View trong ASP.NET Core MVC (Part 2)";
        return View(model);
    }

    [HttpGet]
    public IActionResult ProductGrid(int? categoryId, string? keyword)
    {
        var products = CatalogData.Products.AsEnumerable();

        if (categoryId.HasValue)
        {
            products = products.Where(product => product.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            products = products.Where(product =>
                product.Name.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase) ||
                product.CategoryName.Contains(keyword.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        var result = products
            .OrderByDescending(product => product.CreatedAt)
            .ToList();

        ViewData["FilterLabel"] = categoryId.HasValue
            ? CatalogData.Categories.FirstOrDefault(category => category.Id == categoryId)?.Name ?? "Không xác định"
            : "Tất cả sản phẩm";

        return PartialView("_ProductGrid", result);
    }

    public IActionResult ViewGuide()
    {
        return View(CatalogData.Categories);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
