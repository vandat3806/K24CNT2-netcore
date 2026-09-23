using NvdLesson09.Models.DataModels;

namespace NvdLesson09.Repositories;

public sealed class InMemoryNvdProductRepository : INvdProductRepository
{
    private readonly object _syncRoot = new();
    private readonly List<NvdCategory> _categories =
    [
        new() { Id = 1, Name = "Thiết bị học tập" },
        new() { Id = 2, Name = "Phụ kiện máy tính" },
        new() { Id = 3, Name = "Sách công nghệ" },
        new() { Id = 4, Name = "Đồ dùng sáng tạo" }
    ];

    private readonly List<NvdProduct> _products;
    private int _nextId;

    public InMemoryNvdProductRepository()
    {
        _products =
        [
            CreateSeed(1, "Bàn phím cơ NVD Studio", "/products/keyboard.svg", 890000, 749000, "Bàn phím gọn nhẹ, phù hợp học lập trình và làm bài tập dài.", 2, 8),
            CreateSeed(2, "Giá đỡ laptop FlexDesk", "/products/laptop-stand.svg", 450000, 379000, "Giá đỡ nhôm chắc chắn giúp bố trí góc học tập gọn gàng.", 1, 7),
            CreateSeed(3, "Sổ tay thuật toán thực hành", "/products/notebook.svg", 180000, 149000, "Sổ ghi chú có bố cục dành cho bài toán, mã giả và kiểm thử.", 3, 6),
            CreateSeed(4, "Chuột không dây Focus M2", "/products/mouse.svg", 520000, 439000, "Chuột yên tĩnh, kết nối ổn định và thuận tiện khi học ở thư viện.", 2, 5),
            CreateSeed(5, "Đèn bàn học Aurora Mini", "/products/lamp.svg", 720000, 609000, "Đèn bàn điều chỉnh ba mức sáng cho không gian làm việc cá nhân.", 1, 4),
            CreateSeed(6, "Bộ thẻ ghi nhớ Clean Code", "/products/cards.svg", 240000, 199000, "Bộ thẻ tóm tắt các nguyên tắc viết mã dễ đọc và dễ bảo trì.", 3, 3),
            CreateSeed(7, "Bảng kế hoạch Weekly Flow", "/products/planner.svg", 320000, 269000, "Bảng lập kế hoạch tuần có thể lau xóa và tái sử dụng.", 4, 2),
            CreateSeed(8, "Túi phụ kiện Tech Pouch", "/products/pouch.svg", 390000, 329000, "Túi nhiều ngăn để sắp xếp cáp, bộ sạc và thiết bị nhỏ.", 4, 1)
        ];
        AttachCategories(_products);
        _nextId = _products.Max(product => product.Id) + 1;
    }

    public IReadOnlyList<NvdProduct> GetAll()
    {
        lock (_syncRoot)
        {
            return _products.Select(CloneProduct).ToList();
        }
    }

    public IReadOnlyList<NvdCategory> GetCategories()
    {
        lock (_syncRoot)
        {
            return _categories.Select(CloneCategory).ToList();
        }
    }

    public NvdProduct? GetById(int id)
    {
        lock (_syncRoot)
        {
            var product = _products.FirstOrDefault(item => item.Id == id);
            return product is null ? null : CloneProduct(product);
        }
    }

    public NvdProduct Add(NvdProduct product)
    {
        lock (_syncRoot)
        {
            var storedProduct = CloneProduct(product);
            storedProduct.Id = _nextId++;
            storedProduct.Category = CloneCategory(_categories.Single(category => category.Id == storedProduct.CategoryId));
            _products.Add(storedProduct);
            return CloneProduct(storedProduct);
        }
    }

    public bool Update(NvdProduct product)
    {
        lock (_syncRoot)
        {
            var index = _products.FindIndex(item => item.Id == product.Id);
            if (index < 0)
            {
                return false;
            }

            var storedProduct = CloneProduct(product);
            storedProduct.Category = CloneCategory(_categories.Single(category => category.Id == storedProduct.CategoryId));
            _products[index] = storedProduct;
            return true;
        }
    }

    public bool Delete(int id)
    {
        lock (_syncRoot)
        {
            return _products.RemoveAll(product => product.Id == id) > 0;
        }
    }

    public bool CategoryExists(int categoryId)
    {
        lock (_syncRoot)
        {
            return _categories.Any(category => category.Id == categoryId);
        }
    }

    private static NvdProduct CreateSeed(int id, string name, string image, decimal price, decimal salePrice, string description, int categoryId, int daysAgo)
    {
        return new NvdProduct
        {
            Id = id,
            Name = name,
            Image = image,
            Price = price,
            SalePrice = salePrice,
            Description = description,
            CategoryId = categoryId,
            CreatedAt = DateTime.UtcNow.AddDays(-daysAgo)
        };
    }

    private void AttachCategories(IEnumerable<NvdProduct> products)
    {
        foreach (var product in products)
        {
            product.Category = CloneCategory(_categories.Single(category => category.Id == product.CategoryId));
        }
    }

    private static NvdCategory CloneCategory(NvdCategory category)
    {
        return new NvdCategory { Id = category.Id, Name = category.Name };
    }

    private static NvdProduct CloneProduct(NvdProduct product)
    {
        return new NvdProduct
        {
            Id = product.Id,
            Name = product.Name,
            Image = product.Image,
            Price = product.Price,
            SalePrice = product.SalePrice,
            Description = product.Description,
            CategoryId = product.CategoryId,
            Category = CloneCategory(product.Category),
            CreatedAt = product.CreatedAt
        };
    }
}
