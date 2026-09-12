using Microsoft.AspNetCore.Mvc;
using NvdLesson05.Models;

namespace NvdLesson05.Controllers;

public class ProductController : Controller
{
    public IActionResult Details(int id)
    {
        var product = CatalogData.Products.FirstOrDefault(item => item.Id == id);
        return product is null ? NotFound() : View(product);
    }
}
