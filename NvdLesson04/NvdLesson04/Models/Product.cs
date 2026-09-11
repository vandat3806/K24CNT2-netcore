namespace NvdLesson04.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public decimal SalePrice { get; set; }
    public int CategoryId { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Status { get; set; }
    public DateTime CreatedAt { get; set; }
}
