using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NvdLesson09.Models;
using NvdLesson09.Models.ViewModels;
using NvdLesson09.Repositories;

namespace NvdLesson09.Controllers;

public sealed class HomeController : Controller
{
    private readonly INvdProductRepository _repository;

    public HomeController(INvdProductRepository repository)
    {
        _repository = repository;
    }

    public IActionResult Index()
    {
        var products = _repository.GetAll();
        var model = new LessonOverviewViewModel
        {
            ProductCount = products.Count,
            CategoryCount = _repository.GetCategories().Count,
            SaleProductCount = products.Count(product => product.SalePrice < product.Price),
            LatestProducts = products.OrderByDescending(product => product.CreatedAt).Take(3).ToList()
        };

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
