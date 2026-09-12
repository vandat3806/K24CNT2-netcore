namespace NvdLesson05.Models;

public static class CatalogData
{
    public static IReadOnlyList<Category> Categories { get; } =
    [
        new() { Id = 1, Name = "Laptop", Icon = "▱", ProductCount = 3 },
        new() { Id = 2, Name = "Điện thoại", Icon = "▯", ProductCount = 3 },
        new() { Id = 3, Name = "Âm thanh", Icon = "♪", ProductCount = 2 },
        new() { Id = 4, Name = "Phụ kiện", Icon = "⌁", ProductCount = 2 }
    ];

    public static IReadOnlyList<Product> Products { get; } =
    [
        new() { Id = 1, Name = "Laptop NovaBook Air", Image = "/images/products/laptop.svg", Price = 21990000, OldPrice = 23990000, CategoryId = 1, CategoryName = "Laptop", Description = "Thiết kế mỏng nhẹ, màn hình sắc nét và pin dùng cả ngày.", IsHot = true, CreatedAt = new DateTime(2026, 9, 8) },
        new() { Id = 2, Name = "Điện thoại Orbit X", Image = "/images/products/phone.svg", Price = 16490000, OldPrice = 17990000, CategoryId = 2, CategoryName = "Điện thoại", Description = "Camera chống rung, hiệu năng mạnh và sạc nhanh tiện lợi.", IsHot = true, CreatedAt = new DateTime(2026, 9, 7) },
        new() { Id = 3, Name = "Tai nghe Pulse Pro", Image = "/images/products/audio.svg", Price = 2890000, OldPrice = 3290000, CategoryId = 3, CategoryName = "Âm thanh", Description = "Chống ồn chủ động và âm thanh cân bằng cho học tập.", IsHot = true, CreatedAt = new DateTime(2026, 9, 6) },
        new() { Id = 4, Name = "Hub kết nối Flex 8-in-1", Image = "/images/products/accessory.svg", Price = 1290000, CategoryId = 4, CategoryName = "Phụ kiện", Description = "Mở rộng kết nối HDMI, USB và thẻ nhớ trong một thiết bị.", IsHot = false, CreatedAt = new DateTime(2026, 9, 5) },
        new() { Id = 5, Name = "Laptop NovaBook Studio", Image = "/images/products/laptop.svg", Price = 31990000, CategoryId = 1, CategoryName = "Laptop", Description = "Hiệu năng đồ họa tốt, phù hợp lập trình và sáng tạo nội dung.", IsHot = true, CreatedAt = new DateTime(2026, 8, 30) },
        new() { Id = 6, Name = "Điện thoại Orbit Lite", Image = "/images/products/phone.svg", Price = 8490000, CategoryId = 2, CategoryName = "Điện thoại", Description = "Màn hình lớn, camera rõ và dung lượng pin bền bỉ.", IsHot = false, CreatedAt = new DateTime(2026, 8, 27) },
        new() { Id = 7, Name = "Loa không dây SoundPod", Image = "/images/products/audio.svg", Price = 1690000, OldPrice = 1990000, CategoryId = 3, CategoryName = "Âm thanh", Description = "Loa nhỏ gọn với chất âm rõ và khả năng chống nước.", IsHot = true, CreatedAt = new DateTime(2026, 8, 23) },
        new() { Id = 8, Name = "Bàn phím Nova Keys", Image = "/images/products/accessory.svg", Price = 1490000, CategoryId = 4, CategoryName = "Phụ kiện", Description = "Bàn phím gọn, kết nối đa thiết bị và gõ êm.", IsHot = false, CreatedAt = new DateTime(2026, 8, 18) },
        new() { Id = 9, Name = "Laptop NovaBook Go", Image = "/images/products/laptop.svg", Price = 17490000, CategoryId = 1, CategoryName = "Laptop", Description = "Cấu hình cân bằng cho học tập và công việc văn phòng.", IsHot = false, CreatedAt = new DateTime(2026, 8, 12) },
        new() { Id = 10, Name = "Điện thoại Orbit Mini", Image = "/images/products/phone.svg", Price = 6990000, CategoryId = 2, CategoryName = "Điện thoại", Description = "Thiết kế nhỏ gọn, thao tác thuận tiện bằng một tay.", IsHot = false, CreatedAt = new DateTime(2026, 8, 6) }
    ];
}
