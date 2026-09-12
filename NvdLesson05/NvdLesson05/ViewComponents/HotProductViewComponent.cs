using Microsoft.AspNetCore.Mvc;
using NvdLesson05.Models;

namespace NvdLesson05.ViewComponents;

public class HotProductViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(int count = 4)
    {
        var products = CatalogData.Products
            .Where(product => product.IsHot)
            .OrderByDescending(product => product.CreatedAt)
            .Take(Math.Clamp(count, 1, 8))
            .ToList();

        return View(products);
    }
}
