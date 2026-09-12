using Microsoft.AspNetCore.Mvc;
using NvdLesson06.Models;

namespace NvdLesson06.Controllers;

public class ProductController : Controller
{
    public IActionResult Details(int id)
    {
        var product = CatalogData.Products.FirstOrDefault(item => item.Id == id);
        if (product is null)
        {
            return NotFound();
        }

        ViewBag.RelatedProducts = CatalogData.Products
            .Where(item => item.CategoryId == product.CategoryId && item.Id != id)
            .Take(3)
            .ToList();

        return View(product);
    }
}
