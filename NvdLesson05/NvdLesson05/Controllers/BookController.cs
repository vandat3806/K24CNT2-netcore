using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NvdLesson05.Models;

namespace NvdLesson05.Controllers;

public class BookController : Controller
{
    private static readonly object SyncRoot = new();

    private static readonly List<(int Id, string Name)> Authors =
    [
        (1, "Nguyễn Nhật Ánh"),
        (2, "Tô Hoài"),
        (3, "Nam Cao"),
        (4, "Dale Carnegie")
    ];

    private static readonly List<(int Id, string Name)> Genres =
    [
        (1, "Văn học Việt Nam"),
        (2, "Thiếu nhi"),
        (3, "Kỹ năng sống"),
        (4, "Công nghệ")
    ];

    private static readonly List<Book> Books =
    [
        new() { Id = 1, Title = "Cho tôi xin một vé đi tuổi thơ", AuthorId = 1, GenreId = 1, Price = 96000, TotalPage = 208, Image = "/images/books/book-blue.svg", Summary = "Một câu chuyện trong trẻo về thế giới tuổi thơ, gia đình và tình bạn." },
        new() { Id = 2, Title = "Dế Mèn phiêu lưu ký", AuthorId = 2, GenreId = 2, Price = 78000, TotalPage = 192, Image = "/images/books/book-green.svg", Summary = "Hành trình trưởng thành giàu trải nghiệm của Dế Mèn qua nhiều miền đất." },
        new() { Id = 3, Title = "Đắc nhân tâm", AuthorId = 4, GenreId = 3, Price = 118000, TotalPage = 320, Image = "/images/books/book-orange.svg", Summary = "Những nguyên tắc giao tiếp tích cực và cách xây dựng mối quan hệ bền vững." },
        new() { Id = 4, Title = "Lão Hạc", AuthorId = 3, GenreId = 1, Price = 65000, TotalPage = 144, Image = "/images/books/book-purple.svg", Summary = "Tác phẩm văn học hiện thực giàu giá trị nhân văn về người nông dân Việt Nam." }
    ];

    public IActionResult Index(int? authorId, int? genreId)
    {
        var books = GetBookList().AsEnumerable();

        if (authorId.HasValue)
        {
            books = books.Where(book => book.AuthorId == authorId.Value);
        }

        if (genreId.HasValue)
        {
            books = books.Where(book => book.GenreId == genreId.Value);
        }

        LoadSelectLists(authorId, genreId);
        ViewBag.TotalBooks = Books.Count;
        return View(books.ToList());
    }

    public IActionResult Details(int id)
    {
        var book = GetBookById(id);
        return book is null ? NotFound() : View(book);
    }

    [HttpGet]
    public IActionResult Create()
    {
        LoadSelectLists();
        return View(new Book());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Book book)
    {
        await ValidateAndStoreImageAsync(book);

        if (!ModelState.IsValid)
        {
            LoadSelectLists(book.AuthorId, book.GenreId);
            return View(book);
        }

        lock (SyncRoot)
        {
            book.Id = Books.Count == 0 ? 1 : Books.Max(item => item.Id) + 1;
            Books.Add(ToStoredBook(book));
        }

        TempData["Success"] = $"Đã thêm sách “{book.Title}”.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var book = GetBookById(id);
        if (book is null)
        {
            return NotFound();
        }

        LoadSelectLists(book.AuthorId, book.GenreId);
        return View(book);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        var current = GetBookById(id);
        if (current is null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(book.Image))
        {
            book.Image = current.Image;
        }

        await ValidateAndStoreImageAsync(book);

        if (!ModelState.IsValid)
        {
            LoadSelectLists(book.AuthorId, book.GenreId);
            return View(book);
        }

        lock (SyncRoot)
        {
            var index = Books.FindIndex(item => item.Id == id);
            Books[index] = ToStoredBook(book);
        }

        TempData["Success"] = $"Đã cập nhật sách “{book.Title}”.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public IActionResult Latest()
    {
        var latestBooks = GetBookList()
            .OrderByDescending(book => book.Id)
            .Take(3)
            .ToList();

        return PartialView("_BookTablePartial", latestBooks);
    }

    public List<Book> GetBookList()
    {
        lock (SyncRoot)
        {
            return Books.Select(EnrichBook).ToList();
        }
    }

    public Book? GetBookById(int id)
    {
        lock (SyncRoot)
        {
            var book = Books.FirstOrDefault(item => item.Id == id);
            return book is null ? null : EnrichBook(book);
        }
    }

    private void LoadSelectLists(int? authorId = null, int? genreId = null)
    {
        ViewBag.Authors = new SelectList(Authors.Select(item => new { item.Id, item.Name }), "Id", "Name", authorId);
        ViewBag.Genres = new SelectList(Genres.Select(item => new { item.Id, item.Name }), "Id", "Name", genreId);
    }

    private async Task ValidateAndStoreImageAsync(Book book)
    {
        if (book.ImageFile is null || book.ImageFile.Length == 0)
        {
            return;
        }

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".svg" };
        var extension = Path.GetExtension(book.ImageFile.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
        {
            ModelState.AddModelError(nameof(Book.ImageFile), "Ảnh phải có định dạng JPG, PNG, WEBP hoặc SVG.");
            return;
        }

        if (book.ImageFile.Length > 2 * 1024 * 1024)
        {
            ModelState.AddModelError(nameof(Book.ImageFile), "Kích thước ảnh không được vượt quá 2 MB.");
            return;
        }

        var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "books", "uploads");
        Directory.CreateDirectory(uploadFolder);
        var fileName = $"{Guid.NewGuid():N}{extension}";
        var filePath = Path.Combine(uploadFolder, fileName);

        await using var stream = System.IO.File.Create(filePath);
        await book.ImageFile.CopyToAsync(stream);
        book.Image = $"/images/books/uploads/{fileName}";
    }

    private static Book EnrichBook(Book book)
    {
        var copy = ToStoredBook(book);
        copy.AuthorName = Authors.First(item => item.Id == book.AuthorId).Name;
        copy.GenreName = Genres.First(item => item.Id == book.GenreId).Name;
        return copy;
    }

    private static Book ToStoredBook(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title.Trim(),
        AuthorId = book.AuthorId,
        GenreId = book.GenreId,
        Price = book.Price,
        TotalPage = book.TotalPage,
        Image = string.IsNullOrWhiteSpace(book.Image) ? "/images/books/book-default.svg" : book.Image,
        Summary = book.Summary.Trim()
    };
}
