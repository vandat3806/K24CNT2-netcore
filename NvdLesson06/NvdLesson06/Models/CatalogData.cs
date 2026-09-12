namespace NvdLesson06.Models;

public static class CatalogData
{
    public static IReadOnlyList<Category> Categories { get; } =
    [
        new() { Id = 1, Name = "Laptop", Icon = "▱", ProductCount = 3 },
        new() { Id = 2, Name = "Điện thoại", Icon = "▯", ProductCount = 3 },
        new() { Id = 3, Name = "Âm thanh", Icon = "♫", ProductCount = 3 },
        new() { Id = 4, Name = "Phụ kiện", Icon = "⌁", ProductCount = 3 }
    ];

    public static IReadOnlyList<Product> Products { get; } =
    [
        new() { Id = 1, Name = "NovaBook Air 14", Image = "/images/products/laptop.svg", Price = 21_990_000, OldPrice = 23_990_000, CategoryId = 1, CategoryName = "Laptop", Description = "Laptop mỏng nhẹ, màn hình sắc nét và pin dùng cả ngày.", Rating = 4.9, Sold = 124, IsHot = true, CreatedAt = new DateTime(2026, 9, 10) },
        new() { Id = 2, Name = "Orbit Phone X", Image = "/images/products/phone.svg", Price = 16_490_000, OldPrice = 17_990_000, CategoryId = 2, CategoryName = "Điện thoại", Description = "Camera chống rung, hiệu năng mạnh và sạc nhanh tiện lợi.", Rating = 4.8, Sold = 208, IsHot = true, CreatedAt = new DateTime(2026, 9, 9) },
        new() { Id = 3, Name = "Pulse Buds Pro", Image = "/images/products/audio.svg", Price = 2_890_000, OldPrice = 3_290_000, CategoryId = 3, CategoryName = "Âm thanh", Description = "Chống ồn chủ động và âm thanh cân bằng cho học tập.", Rating = 4.9, Sold = 315, IsHot = true, CreatedAt = new DateTime(2026, 9, 8) },
        new() { Id = 4, Name = "Flex Hub 8-in-1", Image = "/images/products/accessory.svg", Price = 1_290_000, OldPrice = 1_590_000, CategoryId = 4, CategoryName = "Phụ kiện", Description = "Mở rộng HDMI, USB-C, USB-A và khe đọc thẻ nhớ.", Rating = 4.7, Sold = 183, IsHot = false, CreatedAt = new DateTime(2026, 9, 7) },
        new() { Id = 5, Name = "NovaBook Studio 16", Image = "/images/products/laptop.svg", Price = 31_990_000, CategoryId = 1, CategoryName = "Laptop", Description = "Hiệu năng đồ họa tốt cho lập trình và sáng tạo nội dung.", Rating = 4.8, Sold = 76, IsHot = true, CreatedAt = new DateTime(2026, 9, 4) },
        new() { Id = 6, Name = "Orbit Phone Lite", Image = "/images/products/phone.svg", Price = 8_490_000, OldPrice = 9_190_000, CategoryId = 2, CategoryName = "Điện thoại", Description = "Màn hình lớn, camera rõ và dung lượng pin bền bỉ.", Rating = 4.6, Sold = 267, IsHot = false, CreatedAt = new DateTime(2026, 9, 2) },
        new() { Id = 7, Name = "SoundPod Mini", Image = "/images/products/audio.svg", Price = 1_690_000, OldPrice = 1_990_000, CategoryId = 3, CategoryName = "Âm thanh", Description = "Loa không dây nhỏ gọn, âm thanh rõ và kháng nước.", Rating = 4.8, Sold = 241, IsHot = true, CreatedAt = new DateTime(2026, 8, 29) },
        new() { Id = 8, Name = "Nova Keys 75", Image = "/images/products/accessory.svg", Price = 1_490_000, CategoryId = 4, CategoryName = "Phụ kiện", Description = "Bàn phím gọn, kết nối ba thiết bị và gõ êm.", Rating = 4.7, Sold = 162, IsHot = false, CreatedAt = new DateTime(2026, 8, 25) },
        new() { Id = 9, Name = "NovaBook Go", Image = "/images/products/laptop.svg", Price = 17_490_000, CategoryId = 1, CategoryName = "Laptop", Description = "Cấu hình cân bằng cho học tập và công việc văn phòng.", Rating = 4.6, Sold = 91, IsHot = false, CreatedAt = new DateTime(2026, 8, 20) },
        new() { Id = 10, Name = "Orbit Phone Mini", Image = "/images/products/phone.svg", Price = 6_990_000, CategoryId = 2, CategoryName = "Điện thoại", Description = "Thiết kế nhỏ gọn, thao tác thuận tiện bằng một tay.", Rating = 4.5, Sold = 119, IsHot = false, CreatedAt = new DateTime(2026, 8, 15) },
        new() { Id = 11, Name = "Pulse Headphone S", Image = "/images/products/audio.svg", Price = 2_190_000, CategoryId = 3, CategoryName = "Âm thanh", Description = "Tai nghe chụp tai êm, micro rõ cho học trực tuyến.", Rating = 4.7, Sold = 137, IsHot = false, CreatedAt = new DateTime(2026, 8, 10) },
        new() { Id = 12, Name = "Nova Mouse Air", Image = "/images/products/accessory.svg", Price = 790_000, OldPrice = 890_000, CategoryId = 4, CategoryName = "Phụ kiện", Description = "Chuột không dây nhẹ, cảm biến chính xác và pin lâu.", Rating = 4.8, Sold = 329, IsHot = true, CreatedAt = new DateTime(2026, 8, 5) }
    ];
}
