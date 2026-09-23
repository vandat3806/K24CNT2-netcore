using Microsoft.AspNetCore.Mvc;
using NvdLesson09.Models.DataModels;
using NvdLesson09.Models.ViewModels;
using NvdLesson09.Repositories;

namespace NvdLesson09.Controllers;

[Route("san-pham")]
public sealed class NvdProductController : Controller
{
    private const int PageSize = 6;
    private readonly INvdProductRepository _repository;
    private readonly IWebHostEnvironment _environment;

    public NvdProductController(INvdProductRepository repository, IWebHostEnvironment environment)
    {
        _repository = repository;
        _environment = environment;
    }

    [HttpGet("")]
    public IActionResult Index(string? keyword, int? categoryId, int page = 1)
    {
        var products = _repository.GetAll().AsEnumerable();

        if (!string.IsNullOrWhiteSpace(keyword))
        {
            var normalizedKeyword = keyword.Trim();
            products = products.Where(product =>
                product.Name.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase) ||
                product.Description.Contains(normalizedKeyword, StringComparison.OrdinalIgnoreCase));
        }

        if (categoryId.HasValue)
        {
            products = products.Where(product => product.CategoryId == categoryId.Value);
        }

        var filteredProducts = products.OrderByDescending(product => product.CreatedAt).ToList();
        var totalPages = Math.Max(1, (int)Math.Ceiling(filteredProducts.Count / (double)PageSize));
        page = Math.Clamp(page, 1, totalPages);

        var allProducts = _repository.GetAll();
        var model = new NvdProductIndexViewModel
        {
            Products = filteredProducts.Skip((page - 1) * PageSize).Take(PageSize).ToList(),
            Categories = _repository.GetCategories(),
            Keyword = keyword?.Trim(),
            CategoryId = categoryId,
            CurrentPage = page,
            TotalPages = totalPages,
            TotalFiltered = filteredProducts.Count,
            TotalProducts = allProducts.Count,
            SaleProductCount = allProducts.Count(product => product.SalePrice < product.Price)
        };

        return View(model);
    }

    [HttpGet("them-moi")]
    public IActionResult Create()
    {
        return View(new NvdProductCreateViewModel
        {
            Categories = _repository.GetCategories()
        });
    }

    [HttpPost("them-moi")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(NvdProductCreateViewModel model)
    {
        ValidateCategory(model.CategoryId);
        ValidateImage(model.ImageFile, required: true);

        if (!ModelState.IsValid)
        {
            model.Categories = _repository.GetCategories();
            return View(model);
        }

        var imagePath = await SaveImageAsync(model.ImageFile!);
        var product = new NvdProduct
        {
            Name = model.Name.Trim(),
            Image = imagePath,
            Price = model.Price,
            SalePrice = model.SalePrice,
            Description = model.Description.Trim(),
            CategoryId = model.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        var createdProduct = _repository.Add(product);
        TempData["SuccessMessage"] = $"Đã thêm sản phẩm “{createdProduct.Name}”.";
        return RedirectToAction(nameof(Details), new { id = createdProduct.Id });
    }

    [HttpGet("chi-tiet/{id:int}")]
    public IActionResult Details(int id)
    {
        var product = _repository.GetById(id);
        return product is null ? NotFound() : View(product);
    }

    [HttpGet("chinh-sua/{id:int}")]
    public IActionResult Edit(int id)
    {
        var product = _repository.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        return View(new NvdProductEditViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            SalePrice = product.SalePrice,
            Description = product.Description,
            CategoryId = product.CategoryId,
            ExistingImage = product.Image,
            Categories = _repository.GetCategories()
        });
    }

    [HttpPost("chinh-sua/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, NvdProductEditViewModel model)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        var currentProduct = _repository.GetById(id);
        if (currentProduct is null)
        {
            return NotFound();
        }

        ValidateCategory(model.CategoryId);
        ValidateImage(model.ImageFile, required: false);

        if (!ModelState.IsValid)
        {
            model.ExistingImage = currentProduct.Image;
            model.Categories = _repository.GetCategories();
            return View(model);
        }

        var imagePath = model.ImageFile is null
            ? currentProduct.Image
            : await SaveImageAsync(model.ImageFile);

        currentProduct.Name = model.Name.Trim();
        currentProduct.Image = imagePath;
        currentProduct.Price = model.Price;
        currentProduct.SalePrice = model.SalePrice;
        currentProduct.Description = model.Description.Trim();
        currentProduct.CategoryId = model.CategoryId;

        _repository.Update(currentProduct);
        TempData["SuccessMessage"] = $"Đã cập nhật sản phẩm “{currentProduct.Name}”.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpGet("xoa/{id:int}")]
    public IActionResult Delete(int id)
    {
        var product = _repository.GetById(id);
        return product is null ? NotFound() : View(product);
    }

    [HttpPost("xoa/{id:int}")]
    [ActionName(nameof(Delete))]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var product = _repository.GetById(id);
        if (product is null)
        {
            return NotFound();
        }

        _repository.Delete(id);
        TempData["SuccessMessage"] = $"Đã xóa sản phẩm “{product.Name}”.";
        return RedirectToAction(nameof(Index));
    }

    private void ValidateCategory(int categoryId)
    {
        if (!_repository.CategoryExists(categoryId))
        {
            ModelState.AddModelError(nameof(NvdProductFormViewModel.CategoryId), "Danh mục đã chọn không tồn tại.");
        }
    }

    private void ValidateImage(IFormFile? imageFile, bool required)
    {
        if (required && imageFile is null)
        {
            return;
        }

        if (imageFile is not null && imageFile.Length == 0)
        {
            ModelState.AddModelError("ImageFile", "Tệp ảnh không được để trống.");
        }
    }

    private async Task<string> SaveImageAsync(IFormFile imageFile)
    {
        var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
        var fileName = $"nvd-{Guid.NewGuid():N}{extension}";
        var productDirectory = Path.Combine(_environment.WebRootPath, "products");
        Directory.CreateDirectory(productDirectory);

        var filePath = Path.Combine(productDirectory, fileName);
        await using var stream = new FileStream(filePath, FileMode.CreateNew);
        await imageFile.CopyToAsync(stream);

        return $"/products/{fileName}";
    }
}
