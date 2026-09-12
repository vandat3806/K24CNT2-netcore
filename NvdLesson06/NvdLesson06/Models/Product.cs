namespace NvdLesson06.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal? OldPrice { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public double Rating { get; set; }
    public int Sold { get; set; }
    public bool IsHot { get; set; }
    public DateTime CreatedAt { get; set; }

    public int DiscountPercent => OldPrice.HasValue && OldPrice > Price
        ? (int)Math.Round((OldPrice.Value - Price) / OldPrice.Value * 100)
        : 0;
}
