using Microsoft.AspNetCore.Mvc;
using NvdLesson06.Models;

namespace NvdLesson06.ViewComponents;

public class HotProductViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int count = 4, double minRating = 4.7)
    {
        var products = CatalogData.Products
            .Where(product => product.IsHot && product.Rating >= minRating)
            .OrderByDescending(product => product.Sold)
            .Take(Math.Clamp(count, 1, 8))
            .ToList();

        ViewData["MinRating"] = minRating;
        return View(products);
    }
}
